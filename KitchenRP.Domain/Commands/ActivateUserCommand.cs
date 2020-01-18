namespace KitchenRP.Domain.Commands
{
    public class ActivateUserCommand
    {
        public string Uid { get; set; }
        public string Email { get; set; }
    }
}