﻿using System;
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
        private readonly RegistrationContext userContext;

        public UserRepository(RegistrationContext userContext)
        {
            this.userContext = userContext;
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
            return userContext.Users.Add(Entity.ToEF()).Entity.ToTransfertObject();
        }

        public IEnumerable<UserTO> GetAll()
        => userContext.Users
            .AsNoTracking()
            .Include(x => x.UserSessions)
            .Select(x => x.ToTransfertObject())
            .ToList();

        public UserTO GetById(int Id)
        => userContext.Users
                .AsNoTracking()
                .Include(x => x.UserSessions)
                .FirstOrDefault(x => x.Id == Id).ToTransfertObject();

        public IEnumerable<UserTO> GetByRole(UserRole role)
        => userContext.Users
                .AsNoTracking()
                .Include(x => x.Role)
                .Include(x => x.Name)
                .Include(x => x.Email)
                .Include(x => x.Company)
                .Where(x => x.Role == role)
                .Select(x => x.ToTransfertObject())
                .ToList();

        public IEnumerable<UserTO> GetBySession(SessionTO session)
        {
            return userContext.UserSessions
                .Where(x => x.SessionId == session.Id)
                .Select(x => x.User.ToTransfertObject())
                .ToList();
        }

        public IEnumerable<SessionTO> GetSessions(UserTO user)
        {
            throw new NotImplementedException();
        }

        public bool IsInSession(UserTO user, SessionTO session)
        {
            var returnValue = false;
            var sessionList = GetBySession(session);
            if (sessionList.Contains(user))
            {
                returnValue = true;
            }
            return returnValue;
        }

        public bool Remove(UserTO entity)
        => Remove(entity.Id);

        public bool Remove(int Id)
        {
            var returnValue = false;
            var user = userContext.Users.FirstOrDefault(x => x.Id == Id);
            if (user != default)
            {
                try
                {
                    userContext.Users.Remove(user);
                    returnValue = true;
                }
                catch (Exception)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

        public UserTO Update(UserTO Entity)
        {
            if (!userContext.Users.Any(x => x.Id == Entity.Id))
            {
                throw new Exception($"Can't find user to update. UserRepository");
            }
            var attachedUser = userContext.Users
                .Include(x => x.Role)
                .Include(x => x.Name)
                .Include(x => x.Email)
                .Include(x => x.Company)
                .FirstOrDefault(x => x.Id == Entity.Id);

            if (attachedUser != default)
            {
                attachedUser.UpdateFromDetached(Entity.ToEF());
            }

            return userContext.Users.Update(attachedUser).Entity.ToTransfertObject();
        }
    }
}