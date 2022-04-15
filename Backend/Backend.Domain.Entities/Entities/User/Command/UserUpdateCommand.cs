using Backend.Domain.Entities.Util;

namespace Backend.Domain.Entities.Entities.User.Command
{
    public class UserUpdateCommand : Entity
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
