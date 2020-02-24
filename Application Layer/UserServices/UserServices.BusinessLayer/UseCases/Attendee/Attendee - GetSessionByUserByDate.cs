using OnlineServices.Common.Exceptions;
using OnlineServices.Common.RegistrationServices;
using OnlineServices.Common.RegistrationServices.TransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistrationServices.BusinessLayer.UseCase.Attendee
{
    public partial class RSAttendeeRole : IRSAttendeeRole
    {
        public SessionTO GetSessionByUserByDate(int userId, DateTime date)
        {
            try
            {
                var user = iRSUnitOfWork.UserRepository.GetById(userId);
                var session = iRSUnitOfWork.SessionRepository.GetByUser(user);
                var demandedSession = session.First(s => s.SessionDays.Any(x => x.Date == date));
                return demandedSession;
            }
            catch
            {
                throw new LoggedException($"Cannot found any session for the given date and user (Date={date}, User={userId})");
            }
        }
    }
}
