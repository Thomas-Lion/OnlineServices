using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OnlineServices.Common.RegistrationServices.Interfaces;
using OnlineServices.Common.RegistrationServices.TransferObject;
using RegistrationServices.DataLayer.Extensions;

namespace RegistrationServices.DataLayer.Repositories
{
    public class UserRepository : IRSUserRepository
    {
        private readonly RegistrationContext registrationContext;

        public UserRepository(RegistrationContext userContext)
        {
            this.registrationContext = userContext;
            //userContext = Context ?? throw new ArgumentNullException($"{nameof(Context)} in UserRepository");
        }

        public UserTO Add(UserTO Entity)
        {
            if (Entity is null)
                throw new ArgumentNullException(nameof(Entity));
            if (Entity.Id != 0)
            {
                return Entity;
            }
            return registrationContext.Users.Add(Entity.ToEF()).Entity.ToTransfertObject();
        }

        public IEnumerable<UserTO> GetAll()
        => registrationContext.Users
            .AsNoTracking()
            .Include(x => x.UserSessions)
            .Select(x => x.ToTransfertObject())
            .ToList();

        public UserTO GetById(int Id)
        => registrationContext.Users
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == Id)
                .ToTransfertObject();

        public IEnumerable<UserTO> GetUserByRole(UserRole role)
        => registrationContext.Users
                .AsNoTracking()
                .Where(x => x.Role == role)
                .Select(x => x.ToTransfertObject())
                .ToList();

        public IEnumerable<UserTO> GetUsersBySession(SessionTO session)
        {
            if ((session.Attendees == null || session.Attendees.Count() == 0) && session.Teacher == null)
                throw new NullReferenceException();
            return registrationContext.UserSessions
                .AsNoTracking()
                .Where(x => x.SessionId == session.Id)
                .Select(x => x.User.ToTransfertObject())
                .ToList();
        }

        public bool IsInSession(UserTO user, SessionTO session)
        {
            var returnValue = false;
            var sessionList = GetUsersBySession(session);
            if (sessionList.Contains(user))
            {
                returnValue = true;
            }
            return returnValue;
        }

        public bool Remove(UserTO entity)
        {
            var entityToDelete = registrationContext.Users.FirstOrDefault(x => x.Id == entity.Id);
            registrationContext.Users.Remove(entityToDelete);
            return true;
        }

        public bool Remove(int Id)
        {
            //var returnValue = false;
            //var user = userContext.Users.FirstOrDefault(x => x.Id == Id);
            //if (user != default)
            //{
            //    try
            //    {
            //        userContext.Users.Remove(user);
            //        returnValue = true;
            //    }
            //    catch (Exception)
            //    {
            //        returnValue = false;
            //    }
            //}
            //return returnValue;
            try
            {
                var entityToDelete = registrationContext.Users.FirstOrDefault(x => x.Id == Id);
                registrationContext.Users.Remove(entityToDelete);
                return true;
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        public UserTO Update(UserTO Entity)
        {
            if (!registrationContext.Users.Any(x => x.Id == Entity.Id))
            {
                throw new Exception($"Can't find user to update. UserRepository");
            }
            var attachedUser = registrationContext.Users
                .FirstOrDefault(x => x.Id == Entity.Id);

            if (attachedUser != default)
            {
                attachedUser.UpdateFromDetached(Entity.ToEF());
            }

            return registrationContext.Users.Update(attachedUser).Entity.ToTransfertObject();
        }
    }
}