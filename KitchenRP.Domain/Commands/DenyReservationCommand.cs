namespace KitchenRP.Domain.Commands
{
    public class DenyReservationCommand
    {
        public long Id { get; set; }
        public long UserId { get; set; }
    }
}