namespace iService_Admin.Models
{
    public partial class Car
    {
        public int CarId { get; set; }

        public int UserId { get; set; }

        public string Manufacturer { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }
        public string ImageUrl { get; set; }

        //public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();

        //public virtual User User { get; set; } = null!;
    }
}
