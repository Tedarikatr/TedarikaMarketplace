using Entity.Auths;
using MediatR;

namespace Domain.Companies.Events
{
    public class CompanyCreatedEvent : INotification
    {
        public int CompanyId { get; }
        public int UserId { get; }
        public UserType UserType { get; }

        public CompanyCreatedEvent(int companyId, int userId, UserType userType)
        {
            CompanyId = companyId;
            UserId = userId;
            UserType = userType;
        }
    }
}
