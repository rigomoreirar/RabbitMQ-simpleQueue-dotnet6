namespace FormulaAirline.API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string PassangerName { get; set; } = "";
        public string PassportNumber { get; set; } = "";
        public string From { get; set; } = "";
        public string To { get; set; } = "";
        public int Status { get; set; }
    }
}