using EvaluationServices.DataLayer;
using OnlineServices.Common.DataAccessHelpers;
using OnlineServices.Common.EvaluationServices;
using OnlineServices.Common.EvaluationServices.Interfaces;
using OnlineServices.Common.EvaluationServices.TransfertObjects;
using OnlineServices.Common.RegistrationServices;
using OnlineServices.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationServices.BusinessLayer.UseCases
{
    public partial class ESAttendeeRole : IESAttendeeRole
    {
        private readonly IESUnitOfWork iESUnitOfWork;
        private readonly IRSAssistantRole iRSAssistantRole;

        // Constructor
        public ESAttendeeRole(IESUnitOfWork iESUnitOfWork, IRSAssistantRole iRSAssistantRole)
        {
            this.iESUnitOfWork = iESUnitOfWork ?? throw new ArgumentNullException(nameof(iESUnitOfWork));
            this.iRSAssistantRole = iRSAssistantRole ?? throw new ArgumentNullException(nameof(iRSAssistantRole));
        }
    }
}
