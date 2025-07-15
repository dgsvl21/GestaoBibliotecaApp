using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBibliotecaApp.Models
{
    public class Emprestimo
    {
        public int EmprestimoId { get; set; }
        public int LivroId { get; set; }
        public int LeitorId { get; set; }
        public Livro Livro { get; set; }
        public Leitor Leitor { get; set; }
        public String NomeLeitor { get; set; }
        public String TituloLivro { get; set; }
        public DateTime? DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public bool Devolvido => DataDevolucao != null;
    }
}
