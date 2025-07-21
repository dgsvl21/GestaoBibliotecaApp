namespace GestaoBibliotecaApp.Models
{
    public class Leitor
    {
        //public int LeitorId { get; set; }
        //public string Nome { get; set; }
        //public string Email { get; set; }

        public int _leitorId;
        public string _nome;
        public string _email;

        public int LeitorId
        {
            get { return _leitorId; }
            set { _leitorId = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value.Trim(); }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value.Trim(); }
        }
    }
}
