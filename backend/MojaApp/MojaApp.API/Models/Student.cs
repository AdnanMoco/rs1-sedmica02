namespace MojaApp.API.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int OpstinaRodjenjaId { get; set; }
        public Opstina OpstinaRodjenja { get; set; }
    }
}
