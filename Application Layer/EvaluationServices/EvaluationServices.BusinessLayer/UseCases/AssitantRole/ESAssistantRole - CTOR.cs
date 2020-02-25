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
        private readonly IRSAssistantRole iRSServiceRole;

        //Constructor
        public ESAssistantRole(IESUnitOfWork iESUnitOfWork, IRSAssistantRole iRSServiceRole)
        {
            this.iESUnitOfWork = iESUnitOfWork ?? throw new ArgumentNullException(nameof(iESUnitOfWork));
            this.iRSServiceRole = iRSServiceRole ?? throw new ArgumentNullException(nameof(iRSServiceRole));
        }
    }
}
