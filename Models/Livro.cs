namespace GestaoBibliotecaApp.Models
{
    public class Livro
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int CategoriaId { get; set; }
        public string NomeCategoria { get; set; }
    }
}
