namespace CinemaReservationSystem.MVC.Areas.TicketReservation.ViewModels
{
    public record AuditoriumMemberSeatsGetVM(int Id, int TotalSeats, string Name, List<string> Seats);
}
