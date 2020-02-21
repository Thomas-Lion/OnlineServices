using OnlineServices.Common.AttendanceServices.Interfaces;
using OnlineServices.Common.RegistrationServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace OS.AttendanceServices.BusinessLayer.UseCases
{
    public partial class ASAttendeeRole
    {
        private readonly ICheckInRepository checkInRepository;
        private readonly IRSAssistantRole userServices;

        public ASAttendeeRole(ICheckInRepository checkInRepository, IRSAssistantRole userServices)
        {
            this.checkInRepository = checkInRepository ?? throw new ArgumentNullException(nameof(checkInRepository));
            this.userServices = userServices;
        }
    }
}
