namespace iService_Admin.Models
{
    public partial class User
    {
        public string UserId { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Pass { get; set; }

        public string? Token { get; set; }

        public bool? NewsletterSub { get; set; } = false;

        public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();

        public virtual ICollection<Car> Cars { get; } = new List<Car>();
    }
}
