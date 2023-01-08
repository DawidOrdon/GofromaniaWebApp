namespace GofromaniaWebApp.Models
{
    public class Opinie
    {

        public int Id { get; set; }
        public int Ocena { get; set; }
        public string Opinia { get; set; }
        public string Autor { get; set; }
        public string Ip { get; set; }
        public bool Aktwyne { get; set; }
        public string Admin { get; set; }
        public Opinie()
        {

        }
    }
}
