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
        public IEnumerable<SessionTO> GetSessionsByDate(DateTime date)
        {

            try
            {
                return iRSUnitOfWork.SessionRepository.GetSessionsByDate(date);
            }
            catch
            {
                throw new LoggedException($"Cannot found any session for the given date (Date={date})");
            }
        }
    }
}