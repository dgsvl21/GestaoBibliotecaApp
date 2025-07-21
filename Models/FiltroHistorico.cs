namespace GestaoBibliotecaApp.Models
{
    public class FiltroHistorico
    {
        //public string Tipo { get; set; }
        //public int Id { get; set; }
        //public string NomeOuTitulo { get; set; }

        public string _tipo;
        public int _id;
        public string _nomeOuTitulo;

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string NomeOuTitulo
        {
            get { return _nomeOuTitulo; }
            set { _nomeOuTitulo = value.Trim(); }
        }
    }
}
