namespace KitchenRP.Domain.Commands
{
    public class AcceptReservationCommand
    {
        public long Id { get; set; }
        public long UserId { get; set; }
    }
}