using System;

namespace GestaoBibliotecaApp.Models
{
    public class Emprestimo
    {
        //public int EmprestimoId { get; set; }
        //public int LivroId { get; set; }
        //public int LeitorId { get; set; }
        //public Livro Livro { get; set; }
        //public Leitor Leitor { get; set; }
        //public String NomeLeitor { get; set; }
        //public String TituloLivro { get; set; }
        //public DateTime? DataEmprestimo { get; set; }
        //public DateTime? DataDevolucao { get; set; }
        //public bool Devolvido => DataDevolucao != null;

        private int _emprestimoId;
        private int _livroId;
        private int _leitorId;
        private Livro _livro;
        private Leitor _leitor;
        private string _nomeLeitor;
        private string _tituloLivro;
        private DateTime? _dataEmprestimo;
        private DateTime? _dataDevolucao;

        public int EmprestimoId
        {
            get { return _emprestimoId; }
            set { _emprestimoId = value; }
        }

        public int LivroId
        {
            get { return _livroId; }
            set { _livroId = value; }
        }

        public int LeitorId
        {
            get { return _leitorId; }
            set { _leitorId = value; }
        }

        public Livro Livro
        {
            get { return _livro; }
            set { _livro = value; }
        }

        public Leitor Leitor
        {
            get { return _leitor; }
            set { _leitor = value; }
        }

        public string NomeLeitor
        {
            get { return _nomeLeitor; }
            set { _nomeLeitor = value.Trim(); }
        }

        public string TituloLivro
        {
            get { return _tituloLivro; }
            set { _tituloLivro = value.Trim(); }
        }

        public DateTime? DataEmprestimo
        {
            get { return _dataEmprestimo; }
            set { _dataEmprestimo = value; }
        }

        public DateTime? DataDevolucao
        {
            get { return _dataDevolucao; }
            set { _dataDevolucao = value; }
        }

        public bool Devolvido
        {
            get { return DataDevolucao != null; }
        }


    }
}
