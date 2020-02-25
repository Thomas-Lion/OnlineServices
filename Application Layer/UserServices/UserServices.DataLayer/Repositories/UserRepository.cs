using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OnlineServices.Common.Exceptions;
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
            .Where(x => x.IsArchived != true)
            .Select(x => x.ToTransfertObject())
            .ToList();

        public UserTO GetById(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentException("Get User by Id Invalid Id");
            }
            if (!registrationContext.Users.Any(x => x.Id == Id))
            {
                throw new KeyNotFoundException($"UserRepository. GetById(int) No User with this Id: {Id}");
            }
            return registrationContext.Users
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == Id)
                    .ToTransfertObject();
        }

        public IEnumerable<UserTO> GetUserByRole(UserRole role)
        => registrationContext.Users
                .AsNoTracking()
                .Where(x => x.Role == role)
                .Select(x => x.ToTransfertObject())
                .ToList();

        public IEnumerable<UserTO> GetUsersBySession(SessionTO session)
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }
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

        public bool Remove(UserTO Entity)
        {
            if (Entity is null)
            {
                throw new ArgumentNullException(nameof(Entity));
            }
            if (Entity.Id <= 0)
            {
                throw new ArgumentException("User To Remove Invalid Id");
            }
            return Remove(Entity.Id);
            //var entityToDelete = registrationContext.Users.FirstOrDefault(x => x.Id == Entity.Id);
            //registrationContext.Users.Remove(entityToDelete);
            //return true;
        }

        public bool Remove(int Id)
        {
            var user = registrationContext.Users.FirstOrDefault(x => x.Id == Id);

            if (user is null)
            {
                throw new KeyNotFoundException($"UserRepository. Remove(Id) no user to delete.");
            }

            user.IsArchived = true;
            return registrationContext.Users.Update(user).Entity.IsArchived;
        }

        public UserTO Update(UserTO Entity)
        {
            if (Entity is null)
            {
                throw new ArgumentNullException(nameof(Entity));
            }
            if (Entity.Id <= 0)
            {
                throw new ArgumentException("User To Update Invalid Id");
            }
            if (!registrationContext.Users.Any(x => x.Id == Entity.Id))
            {
                throw new KeyNotFoundException($"UserRepository. Update(UserTO) Can't find user to update.");
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