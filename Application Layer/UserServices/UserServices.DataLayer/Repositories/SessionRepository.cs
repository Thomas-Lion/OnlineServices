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

        public SessionTO Add(SessionTO Entity)
        {
            if (Entity is null)
                throw new ArgumentNullException(nameof(Entity));

            if (Entity.Id != 0)
                return Entity;

            if (Entity.Course.IsArchived)
                throw new ArgumentException("Course can not be archived");

            var sessionEF = Entity.ToEF();
            sessionEF.Course = registrationContext.Courses.FirstOrDefault(x => x.Id == Entity.Course.Id);

            sessionEF.UserSessions = new List<UserSessionEF>();
            var session = registrationContext.Sessions.Add(sessionEF).Entity;

            //TODO 1) userserssion.sessionid= nouvelle sessionid
            //TODO 2) registrationContext.UserSessions.Add

            UpdateUserSessions(Entity, session);

            return sessionEF.ToTransfertObject();
        }

        private void UpdateUserSessions(SessionTO session, SessionEF entity)
        {
            if ((session.Attendees != null))
            {
                foreach (var user in session.Attendees)
                {
                    //if (!registrationContext.Users.Any(x => x.Id == user.Id))
                    //{
                    //    var userSession = new UserSessionEF()
                    //    {
                    //        SessionId = session.Id,
                    //        Session = session,
                    //        UserId = user.Id,
                    //        User = registrationContext.Users.First(x => x.Id == user.Id)
                    //    };
                    //    registrationContext.UserSessions.Add(userSession);
                    //}

                    var userSession = new UserSessionEF()
                    {
                        SessionId = entity.Id,
                        Session = entity,
                        UserId = user.Id,
                        User = registrationContext.Users.First(x => x.Id == user.Id)
                    };
                    registrationContext.UserSessions.Add(userSession);
                }
            }
            if ((session.Teacher != null))
            {
                //if (registrationContext.Users.Any(x => x.Id == Entity.Teacher.Id))
                //{
                //    var teacherEF = new UserSessionEF()
                //    {
                //        SessionId = session.Id,
                //        Session = session,
                //        UserId = Entity.Teacher.Id,
                //        User = registrationContext.Users.First(x => x.Id == Entity.Teacher.Id)
                //    };

                //    registrationContext.UserSessions.Add(teacherEF);
                //}
                var teacherEF = new UserSessionEF()
                {
                    SessionId = entity.Id,
                    Session = entity,
                    UserId = session.Teacher.Id,
                    User = registrationContext.Users.First(x => x.Id == session.Teacher.Id)
                };

                registrationContext.UserSessions.Add(teacherEF);
            }
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
                .Where(x => x.User.Role == UserRole.Attendee)
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

        public SessionTO Update(SessionTO session)
        {
            if (session == null)
                throw new ArgumentNullException();

            if (!registrationContext.Sessions.Any(x => x.Id == session.Id))
                throw new ArgumentException("The session you are trying to update doens't exists");

            var entity = registrationContext.Sessions.FirstOrDefault(x => x.Id == session.Id);

            if (entity != default)
            {
                if (registrationContext.Courses.Any(x => x.Id == session.Course.Id))
                    entity.Course = registrationContext.Courses.FirstOrDefault(x => x.Id == session.Course.Id);

                foreach (var user in session.Attendees)
                {
                    if (!entity.UserSessions.Any(x => x.UserId == user.Id))
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
                }

                if (!entity.UserSessions.Any(x => x.User.Role == UserRole.Teacher))
                {
                    var userSession = new UserSessionEF()
                    {
                        Session = entity,
                        SessionId = entity.Id,
                        User = registrationContext.Users.FirstOrDefault(x => x.Id == session.Teacher.Id),
                        UserId = session.Teacher.Id
                    };
                    entity.UserSessions.Add(userSession);
                };
            }

            return registrationContext.Sessions.Update(entity).Entity.ToTransfertObject();
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