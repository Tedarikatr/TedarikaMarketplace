using Entity.Auths;
using MediatR;

namespace Domain.Companies.Events
{
    public class CompanyCreatedEvent : INotification
    {
        public int CompanyId { get; }
        public int UserId { get; }
        public UserType UserType { get; }
        public string AwsIamUserArn { get; }

        public CompanyCreatedEvent(int companyId, int userId, UserType userType, string awsIamUserArn)
        {
            CompanyId = companyId;
            UserId = userId;
            UserType = userType;
            AwsIamUserArn = awsIamUserArn;
        }
    }
}
