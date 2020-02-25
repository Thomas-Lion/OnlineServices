using OnlineServices.Common.EvaluationServices;
using OnlineServices.Common.EvaluationServices.Interfaces;
using OnlineServices.Common.EvaluationServices.TransfertObjects;
using OnlineServices.Common.RegistrationServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationServices.BusinessLayer.UseCases.AssitantRole
{
    public partial class ESAssistantRole : IESAssistantRole

    {
        private readonly IESUnitOfWork iESUnitOfWork;
        private readonly IRSAssistantRole iRSAssistantRole;

        //Constructor
        public ESAssistantRole(IESUnitOfWork iESUnitOfWork, IRSAssistantRole iRSAssistantRole)
        {
            this.iESUnitOfWork = iESUnitOfWork ?? throw new ArgumentNullException(nameof(iESUnitOfWork));
            this.iRSAssistantRole = iRSAssistantRole ?? throw new ArgumentNullException(nameof(iRSAssistantRole));
        }
    }
}