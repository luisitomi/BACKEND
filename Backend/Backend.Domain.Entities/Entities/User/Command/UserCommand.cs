namespace Backend.Domain.Entities.Entities.User.Command
{
    public class UserCommand : Login
    {
        public string Name { get; set; }
        public int Typeid { get; set; }
    }
}
