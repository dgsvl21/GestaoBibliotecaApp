using GestaoBibliotecaApp.Data;
using GestaoBibliotecaApp.Helpers;
using GestaoBibliotecaApp.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace GestaoBibliotecaApp.ViewModel
{
    public class LivroViewModel : INotifyPropertyChanged
    {
        private readonly DBHelper dbHelper = new DBHelper();

        public ObservableCollection<Livro> Livros { get; set; } = new ObservableCollection<Livro>();

        public ObservableCollection<Categoria> Categorias { get; set; } = new ObservableCollection<Categoria>();

        private Livro novoLivro = new Livro();
        public Livro NovoLivro
        {
            get => novoLivro;
            set { novoLivro = value; OnPropertyChanged(); }
        }

        public ICommand EliminarLivroCommand { get; }
        public ICommand AdicionarLivroCommand { get; }

        private Livro livroSelecionado;
        public Livro LivroSelecionado
        {
            get => livroSelecionado;
            set { livroSelecionado = value; OnPropertyChanged(); }
        }

        public LivroViewModel()
        {
            AdicionarLivroCommand = new RelayCommand(AdicionarLivro);
            EliminarLivroCommand = new RelayCommand(EliminarLivro, PodeEliminarLivro);
            CarregarLivros();
            CarregarCategorias();
        }

        private void CarregarCategorias()
        {
            Categorias.Clear();
            foreach (var categoria in dbHelper.ObterCategorias())
            {
                Categorias.Add(categoria);
            }
        }

        private bool PodeEliminarLivro()
        {
            return LivroSelecionado != null;
        }


        private void CarregarLivros()
        {
            Livros.Clear();
            foreach (var livro in dbHelper.ObterLivros())
            {
                Livros.Add(livro);
            } 
        }

        private void EliminarLivro() 
        {
            if (LivroSelecionado != null) 
            {
                try
                {
                    if (dbHelper.LivroTemEmprestimos(LivroSelecionado.LivroId))
                    {
                        MessageBox.Show("Este livro tem empréstimos associados.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    dbHelper.EliminarLivro(LivroSelecionado.LivroId);
                    MessageBox.Show("Livro eliminado com sucesso!");
                    CarregarLivros();
                    LivroSelecionado = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AdicionarLivro()
        {
            if (string.IsNullOrWhiteSpace(NovoLivro.Titulo) || 
                string.IsNullOrWhiteSpace(NovoLivro.Autor) || 
                NovoLivro.CategoriaId <= 0)
            {
                MessageBox.Show("Preencher todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                dbHelper.InserirLivro(NovoLivro);
                MessageBox.Show("Livro adicionado com sucesso!");
                NovoLivro = new Livro();
                CarregarLivros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
