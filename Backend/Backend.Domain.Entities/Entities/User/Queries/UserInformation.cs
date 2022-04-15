using Backend.Domain.Entities.Util;

namespace Backend.Domain.Entities.Entities.User.Queries
{
    public class UserInformation : Entity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}
