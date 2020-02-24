using System;
using System.Collections.Generic;
using System.Text;
using OnlineServices.Common.Extensions;
using RegistrationServices.BusinessLayer.Extensions;
using OnlineServices.Common.RegistrationServices.Interfaces;
using System.Linq;
using OnlineServices.Common.RegistrationServices;
using OnlineServices.Common.RegistrationServices.TransferObject;
using OnlineServices.Common.Exceptions;

namespace RegistrationServices.BusinessLayer.UseCase.Attendee
{
    public partial class RSAttendeeRole : IRSAttendeeRole
    {
        private readonly IRSUnitOfWork iRSUnitOfWork;

        public RSAttendeeRole(IRSUnitOfWork iRSUnitOfWork)
        {
            this.iRSUnitOfWork = iRSUnitOfWork ?? throw new System.ArgumentNullException(nameof(iRSUnitOfWork));
        }

        public RSAttendeeRole()
        {

        }
    }
}
