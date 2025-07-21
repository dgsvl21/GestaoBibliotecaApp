namespace GestaoBibliotecaApp.Models
{
    public class Categoria
    {
        //public int Id { get; set; }
        //public string Nome { get; set; }
        private int _id;
        private string _nome;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value.Trim(); }
        }
    }
}
