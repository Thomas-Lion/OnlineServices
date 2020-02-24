using OnlineServices.Common.RegistrationServices;
using OnlineServices.Common.RegistrationServices.TransferObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrationServices.BusinessLayer.UseCase.Attendee
{
    public partial class RSAttendeeRole : IRSAttendeeRole
    {
        public IEnumerable<SessionTO> GetSessionsByDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}