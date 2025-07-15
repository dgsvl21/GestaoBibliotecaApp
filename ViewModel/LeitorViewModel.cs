using GestaoBibliotecaApp.Data;
using GestaoBibliotecaApp.Helpers;
using GestaoBibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace GestaoBibliotecaApp.ViewModel
{
    public class LeitorViewModel : INotifyPropertyChanged
    {
        private readonly DBHelper dbHelper = new DBHelper();

        public ObservableCollection<Leitor> Leitores { get; set; } = new ObservableCollection<Leitor>();

        public ICommand AdicionarLeitorCommand { get; }

        private Leitor novoLeitor = new Leitor();
        public Leitor NovoLeitor
        {
            get => novoLeitor;
            set { novoLeitor = value; OnPropertyChanged(); }
        }

        public ICommand EliminarLeitorCommand { get; }

        private Leitor leitorSelecionado;
        public Leitor LeitorSelecionado 
        {
            get => leitorSelecionado;
            set { leitorSelecionado = value; OnPropertyChanged(); }
        }

        public LeitorViewModel()
        {
            AdicionarLeitorCommand = new RelayCommand(AdicionarLeitor);
            EliminarLeitorCommand = new RelayCommand(EliminarLeitor, PodeEliminarLeitor);
            CarregarLeitores();
        }

        private bool PodeEliminarLeitor()
        {
            return LeitorSelecionado != null;
        }

        private void EliminarLeitor() 
        {
            if (leitorSelecionado != null) 
            {
                try
                {
                    if (dbHelper.LeitorTemEmprestimos(LeitorSelecionado.LeitorId))
                    {
                        MessageBox.Show("Este leitor tem empréstimos associados.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    dbHelper.EliminarLeitor(LeitorSelecionado.LeitorId);
                    MessageBox.Show("Leitor eliminado com sucesso!");
                    CarregarLeitores();
                    LeitorSelecionado = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CarregarLeitores()
        {
            Leitores.Clear();
            foreach (var leitor in dbHelper.ObterLeitores())
            {
                Leitores.Add(leitor);
            }
        }

        public void AdicionarLeitor()
        {
            if (string.IsNullOrWhiteSpace(NovoLeitor.Nome) || string.IsNullOrWhiteSpace(NovoLeitor.Email))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                dbHelper.InserirLeitor(NovoLeitor);
                MessageBox.Show("Leitor adicionado com sucesso!");
                NovoLeitor = new Leitor();
                CarregarLeitores();
                OnPropertyChanged(nameof(NovoLeitor));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
