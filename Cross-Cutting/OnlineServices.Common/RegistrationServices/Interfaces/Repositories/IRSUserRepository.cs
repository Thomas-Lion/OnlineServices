using OnlineServices.Common.DataAccessHelpers;
using OnlineServices.Common.RegistrationServices.TransferObject;

using System.Collections.Generic;

namespace OnlineServices.Common.RegistrationServices.Interfaces
{
    public interface IRSUserRepository : IRepository<UserTO, int>
    {
        IEnumerable<UserTO> GetUserByRole(UserRole role);

        IEnumerable<UserTO> GetUsersBySession(SessionTO session);

        bool IsInSession(UserTO user, SessionTO session);
    }
}