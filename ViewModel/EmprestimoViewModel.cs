using GestaoBibliotecaApp.Data;
using GestaoBibliotecaApp.Helpers;
using GestaoBibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace GestaoBibliotecaApp.ViewModel
{
    public class EmprestimoViewModel : INotifyPropertyChanged
    {
        private readonly DBHelper dbHelper = new DBHelper();

        public ObservableCollection<Emprestimo> Emprestimos { get; set; } = new ObservableCollection<Emprestimo>();
        public ObservableCollection<Livro> Livros { get; set; } = new ObservableCollection<Livro>();
        public ObservableCollection<Leitor> Leitores { get; set; } = new ObservableCollection<Leitor>();

        private Emprestimo novoEmprestimo { get; set; } = new Emprestimo 
        {
            DataEmprestimo = DateTime.Today,
            DataDevolucao = DateTime.Today.AddDays(7)
        };

        public Emprestimo NovoEmprestimo 
        {
            get => novoEmprestimo;
            set { novoEmprestimo = value; OnPropertyChanged(); }
        }

        private Emprestimo emprestimoSelecionado;
        public Emprestimo EmprestimoSelecionado
        {
            get => emprestimoSelecionado;
            set { emprestimoSelecionado = value; OnPropertyChanged(); }
        }

        private DateTime? dataDevolucaoAtualizar;
        public DateTime? DataDevolucaoAtualizar 
        { 
            get => dataDevolucaoAtualizar;
            set { dataDevolucaoAtualizar = value; OnPropertyChanged(); }
        }

        public ICommand EliminarEmprestimoCommand { get; }

        public ICommand AdicionarEmprestimoCommand { get; }

        public ICommand AtualizarLeitoresCommand { get; }

        public ICommand AtualizarDevolucaoCommand { get; }


        public EmprestimoViewModel()
        {
            AdicionarEmprestimoCommand = new RelayCommand(AdicionarEmprestimo);
            AtualizarLeitoresCommand = new RelayCommand(CarregarLeitoresELivros);
            EliminarEmprestimoCommand = new RelayCommand(EliminarEmprestimo, PodeEliminarEmprestimo);
            AtualizarDevolucaoCommand = new RelayCommand(AtualizarDevolucao);
            CarregarEmprestimos();
        }

        //Update
        private void AtualizarDevolucao() 
        {
            if (EmprestimoSelecionado == null || DataDevolucaoAtualizar == null)
            {
                MessageBox.Show("Selecione um empréstimo e uma nova data de devolução", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else 
            {
                EmprestimoSelecionado.DataDevolucao = DataDevolucaoAtualizar.Value;
                dbHelper.AtualizarDataDevolucao(EmprestimoSelecionado.EmprestimoId, DataDevolucaoAtualizar.Value);
                MessageBox.Show("Data de devolução atualizada");
                CarregarEmprestimos();
            }
        }

        //Delete
        private void EliminarEmprestimo() 
        { 
            if (EmprestimoSelecionado != null) 
            {
                dbHelper.EliminarEmprestimo(EmprestimoSelecionado.EmprestimoId);
                CarregarEmprestimos();
                EmprestimoSelecionado = null;
            }
        }

        private bool PodeEliminarEmprestimo()
        {
            return EmprestimoSelecionado != null;
        }

        private void CarregarEmprestimos()
        {
            Emprestimos.Clear();
            foreach (var emprestimo in dbHelper.ObterEmprestimos())
            {
                Emprestimos.Add(emprestimo);
            }

            Livros.Clear();
            foreach (var livro in dbHelper.ObterLivros())
            {
                Livros.Add(livro);
            }

            Leitores.Clear();
            foreach (var leitor in dbHelper.ObterLeitores())
            {
                Leitores.Add(leitor);
            }

            CarregarLeitoresELivros();
        }

        //Create
        public void AdicionarEmprestimo()
        {
            try 
            { 
                if (NovoEmprestimo.LivroId == 0 || NovoEmprestimo.LeitorId == 0)
                {
                    MessageBox.Show("Leitor e Livro devem ser selecionados.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (NovoEmprestimo.DataEmprestimo == null)
                {
                    NovoEmprestimo.DataEmprestimo = DateTime.Now;
                }

                dbHelper.InserirEmprestimo(NovoEmprestimo);

                MessageBox.Show("Empréstimo adicionado com sucesso!");
                NovoEmprestimo = new Emprestimo();
                OnPropertyChanged(nameof(NovoEmprestimo));
                CarregarEmprestimos();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void CarregarLeitoresELivros()
        {
            Leitores.Clear();
            foreach (var leitor in dbHelper.ObterLeitores())
            {
                Leitores.Add(leitor);
            }
            Livros.Clear();
            foreach (var livro in dbHelper.ObterLivros())
            {
                Livros.Add(livro);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
