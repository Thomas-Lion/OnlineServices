using Microsoft.EntityFrameworkCore;
using OnlineServices.Common.RegistrationServices.Interfaces;
using OnlineServices.Common.RegistrationServices.TransferObject;
using RegistrationServices.DataLayer.Entities;
using RegistrationServices.DataLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistrationServices.DataLayer.Repositories
{
    public class SessionRepository : IRSSessionRepository
    {
        private RegistrationContext registrationContext;

        public SessionRepository(RegistrationContext registrationContext)
        {
            this.registrationContext = registrationContext;
        }

        public SessionTO Add(SessionTO session)
        {
            if (session is null)
                throw new ArgumentNullException(nameof(session));

            if (session.Id != 0)
                return session;

            if (session.Course.IsArchived)
                throw new ArgumentException("Course can not be archived");

            var entity = session.ToEF();
            entity.Course = registrationContext.Courses.FirstOrDefault(x => x.Id == session.Course.Id);

            entity.UserSessions = new List<UserSessionEF>();
            entity = registrationContext.Sessions.Add(entity).Entity;

            UpdateUserSessions(session, entity);

            return entity.ToTransfertObject();
        }

        public SessionTO Update(SessionTO session)
        {
            if (session == null)
                throw new ArgumentNullException();

            if (!registrationContext.Sessions.Any(x => x.Id == session.Id))
                throw new ArgumentException("The session you are trying to update doesn't exists");

            var entity = registrationContext.Sessions.FirstOrDefault(x => x.Id == session.Id);

            if (entity != default)
            {
                if (registrationContext.Courses.Any(x => x.Id == session.Course.Id))
                    entity.Course = registrationContext.Courses.FirstOrDefault(x => x.Id == session.Course.Id);

                UpdateUserSessions(session, entity);
            }

            return registrationContext.Sessions.Update(entity).Entity.ToTransfertObject();
        }

        private void UpdateUserSessions(SessionTO session, SessionEF entity)
        {
            entity.UserSessions.Clear();
            registrationContext.RemoveRange(registrationContext.UserSessions.Where(x => x.SessionId == session.Id));

            foreach (var user in session.Attendees)
            {
                var userSession = new UserSessionEF()
                {
                    Session = entity,
                    SessionId = entity.Id,
                    User = registrationContext.Users.FirstOrDefault(x => x.Id == user.Id),
                    UserId = user.Id
                };
                registrationContext.UserSessions.Add(userSession);
            }

            var teacherSession = new UserSessionEF()
            {
                Session = entity,
                SessionId = entity.Id,
                User = registrationContext.Users.FirstOrDefault(x => x.Id == session.Teacher.Id),
                UserId = session.Teacher.Id
            };
            entity.UserSessions.Add(teacherSession);
        }

        public IEnumerable<SessionTO> GetAll()
            => registrationContext.Sessions
                .AsNoTracking()
                .Include(x => x.UserSessions).ThenInclude(x => x.User)
                .Include(x => x.Dates)
                .Select(x => x.ToTransfertObject())
                .ToList();

        public SessionTO GetById(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException();

            if (!registrationContext.Sessions.Any(x => x.Id == Id))
                throw new ArgumentException($"There is no  session at Id{Id}");

            return registrationContext.Sessions
            .AsNoTracking()
            .Include(x => x.UserSessions).ThenInclude(x => x.User)
            .Include(x => x.Dates)
            .FirstOrDefault(x => x.Id == Id).ToTransfertObject();
        }

        public IEnumerable<DateTime> GetDates(SessionTO session)
            => registrationContext.Sessions
            .AsNoTracking()
            .SelectMany(x => x.Dates.Select(x => x.Date));

        public IEnumerable<UserTO> GetStudents(SessionTO session)
            => registrationContext.UserSessions
                .AsNoTracking()
                .Where(x => (x.User.Role == UserRole.Attendee)&&(x.User.IsActivated))
                .Select(x => x.User.ToTransfertObject()).ToList();

        public bool Remove(SessionTO entity)
            => Remove(entity.Id)
;

        public bool Remove(int Id)
        {
            if (!registrationContext.Sessions.Any(x => x.Id == Id))
                throw new ArgumentException($"There is no session at Id {Id}");

            var sessionToDelete = registrationContext.Sessions.FirstOrDefault(x => x.Id == Id);

            try
            {
                registrationContext.Sessions.Remove(sessionToDelete);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public IEnumerable<SessionTO> GetByUser(UserTO user)
        {
            if (user.Role == UserRole.Assistant)
                throw new ArgumentException("Assistant can not subscribe to sessions");

            return GetAll().Where(x => (x.Attendees.Any(y => y.Id == user.Id))
            || (x.Teacher.Id == user.Id));
        }

        public IEnumerable<SessionTO> GetSessionsByDate(DateTime date)
            => GetAll().Where(x => x.SessionDays.Any(y => y.Date == date));
    }
}