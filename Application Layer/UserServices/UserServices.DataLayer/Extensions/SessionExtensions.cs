using OnlineServices.Common.RegistrationServices.TransferObject;
using RegistrationServices.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistrationServices.DataLayer.Extensions
{
    public static class SessionExtensions
    {
        public static SessionTO ToTransfertObject(this SessionEF session)
        {
            var sessionTO = new SessionTO()
            {
                Id = session.Id,
                Course = session.Course?.ToTransfertObject(),
                //SessionDays = session.Dates.Select(x => x.ToTransfertObject()).ToList(),
            };

            if (session.UserSessions.Any(x => x.User.Role == UserRole.Teacher))
            {
                sessionTO.Teacher = session.UserSessions.FirstOrDefault(x => x.User.Role == UserRole.Teacher).User.ToTransfertObject();
            }

            if (session.UserSessions.Any(x => x.User.Role == UserRole.Attendee))
            {
                sessionTO.Attendees = session.UserSessions.Where(x => x.User.Role == UserRole.Attendee).Select(x => x.User.ToTransfertObject()).ToList();
            }
            return sessionTO;
        }

        public static SessionEF ToEF(this SessionTO session)
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            var result = new SessionEF()
            {
                Id = session.Id,
                Course = session.Course.ToEF(),
                Dates = session.SessionDays?.Select(x => x.ToEF()).ToList()
            };

            if (session.Attendees == null)
            {
                return result;
            }

            result.UserSessions = new List<UserSessionEF>();

            return result;
        }

        public static SessionEF UpdateFromDetached(this SessionEF attachedEF, SessionEF detachedEF)
        {
            if (attachedEF is null)
                throw new ArgumentNullException();

            if (detachedEF is null)
                throw new NullReferenceException();

            if (attachedEF.Id != detachedEF.Id)
                throw new Exception("Cannot update userEF entity because it' not the same.");

            if ((attachedEF != default) && (detachedEF != default))
            {
                attachedEF.Course = detachedEF.Course;
                attachedEF.Dates = detachedEF.Dates;
                attachedEF.UserSessions = detachedEF.UserSessions;
            }

            return attachedEF;
        }
    }
}