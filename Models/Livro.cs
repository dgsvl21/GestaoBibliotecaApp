namespace GestaoBibliotecaApp.Models
{
    public class Livro
    {
        //public int LivroId { get; set; }
        //public string Titulo { get; set; }
        //public string Autor { get; set; }
        //public int CategoriaId { get; set; }
        //public string NomeCategoria { get; set; }

        private int _livroId;
        private string _titulo;
        private string _autor;
        private int _categoriaId;
        private string _nomeCategoria;

        public int LivroId
        {
            get { return _livroId; }
            set { _livroId = value; }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value.Trim(); }
        }

        public string Autor
        {
            get { return _autor; }
            set { _autor = value.Trim(); }
        }

        public int CategoriaId
        {
            get { return _categoriaId; }
            set { _categoriaId = value; }
        }

        public string NomeCategoria
        {
            get { return _nomeCategoria; }
            set { _nomeCategoria = value.Trim(); }

        }
    }
}
