namespace BiblioTrack.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public bool IsDisponibility { get; set; }

        public void Disponibility() {
            IsDisponibility = true;
        }
        public void NoDisponibility() {
            this.IsDisponibility = false;
        }

        public string GetStatusDísponibility()
        {
            return IsDisponibility ? "Disponível" : "Não disponível";
        }
    }
}
