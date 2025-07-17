using GestaoBibliotecaApp.Data;
using GestaoBibliotecaApp.Helpers;
using GestaoBibliotecaApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace GestaoBibliotecaApp.ViewModels
{
    public class HistoricoViewModel : INotifyPropertyChanged
    {
        private readonly DBHelper dbHelper = new DBHelper();

        public ObservableCollection<string> TiposHistorico { get; set; } = new ObservableCollection<string> { "Leitor", "Livro" };
        public ObservableCollection<FiltroHistorico> OpcoesFiltro { get; set; } = new ObservableCollection<FiltroHistorico>();
        public ObservableCollection<Emprestimo> HistoricoFiltrado { get; set; } = new ObservableCollection<Emprestimo>();

        private string tipoSelecionado;
        public string TipoSelecionado
        {
            get => tipoSelecionado;
            set
            {
                tipoSelecionado = value;
                OnPropertyChanged();
                AtualizarOpcoesFiltro();
            }
        }

        private FiltroHistorico entidadeSelecionada;
        public FiltroHistorico EntidadeSelecionada
        {
            get => entidadeSelecionada;
            set
            {
                entidadeSelecionada = value;
                OnPropertyChanged();
            }
        }

        public ICommand AtualizarDadosCommand { get; }

        public ICommand CarregarHistoricoCommand { get; }

        public HistoricoViewModel()
        {
            CarregarHistoricoCommand = new RelayCommand(CarregarHistorico);
            AtualizarDadosCommand = new RelayCommand(AtualizarOpcoesFiltro);
        }

        private void AtualizarOpcoesFiltro()
        {
            OpcoesFiltro.Clear();

            if (TipoSelecionado == "Leitor")
            {
                foreach (var leitor in dbHelper.ObterLeitores())
                {
                    OpcoesFiltro.Add(new FiltroHistorico
                    {
                        Tipo = "Leitor",
                        Id = leitor.LeitorId,
                        NomeOuTitulo = leitor.Nome
                    });
                }
            }
            else if (TipoSelecionado == "Livro")
            {
                foreach (var livro in dbHelper.ObterLivros())
                {
                    OpcoesFiltro.Add(new FiltroHistorico
                    {
                        Tipo = "Livro",
                        Id = livro.LivroId,
                        NomeOuTitulo = livro.Titulo
                    });
                }
            }
        }

        private void CarregarHistorico()
        {
            HistoricoFiltrado.Clear();

            if (EntidadeSelecionada == null || string.IsNullOrEmpty(TipoSelecionado))
            {
                System.Windows.MessageBox.Show("Selecione todos os campos.", "Aviso", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            List<Emprestimo> emprestimos = new List<Emprestimo>();

            if (TipoSelecionado == "Leitor")
            {
                emprestimos = dbHelper.ObterEmprestimosPorLeitor(EntidadeSelecionada.Id);
            }
            else if (TipoSelecionado == "Livro")
            {
                emprestimos = dbHelper.ObterEmprestimosPorLivro(EntidadeSelecionada.Id);
            }

            if (emprestimos.Count == 0) 
            { 
                System.Windows.MessageBox.Show("Nenhum histórico encontrado.", "Aviso", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            }
            else 
            {
                foreach (var emprestimo in emprestimos)
                {
                    HistoricoFiltrado.Add(emprestimo);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string nome = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
