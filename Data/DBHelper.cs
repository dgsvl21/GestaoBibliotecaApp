using GestaoBibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GestaoBibliotecaApp.Data
{
    public class DBHelper
    {
        private readonly string connectionString = "Server=localhost\\SQLEXPRESS;Database=BibliotecaBD;Trusted_Connection=True;";

        public List<Livro> ObterLivros()
        {
            List<Livro> livros = new List<Livro>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ListarLivros", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        livros.Add(new Livro
                        {
                            LivroId = (int)reader["LivroId"],
                            Titulo = reader["Titulo"].ToString(),
                            Autor = reader["Autor"].ToString(),
                            CategoriaId = (int)reader["CategoriaId"],
                            NomeCategoria = reader["NomeCategoria"].ToString()
                        });
                    }
                }
            }
            return livros;
        }

        public void InserirLivro(Livro livro)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("InserirLivro", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Titulo", livro.Titulo);
                cmd.Parameters.AddWithValue("@Autor", livro.Autor);
                cmd.Parameters.AddWithValue("@CategoriaId", livro.CategoriaId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Categoria> ObterCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ListarCategorias", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(new Categoria
                        {
                            Id = (int)reader["CategoriaId"],
                            Nome = reader["Nome"].ToString()
                        });
                    }
                }
            }
            return categorias;
        }

        public void InserirCategoria(Categoria categoria)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("InserirCategoria", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nome", categoria.Nome);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InserirLeitor(Leitor Leitor)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("InserirLeitor", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nome", Leitor.Nome);
                cmd.Parameters.AddWithValue("@Email", Leitor.Email);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Leitor> ObterLeitores()
        {
            List<Leitor> leitores = new List<Leitor>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ListarLeitores", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        leitores.Add(new Leitor
                        {
                            LeitorId = (int)reader["LeitorId"],
                            Nome = reader["Nome"].ToString(),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
            }
            return leitores;
        }

        public bool LeitorTemEmprestimos(int leitorId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Emprestimos WHERE LeitorId = @LeitorId", conn))
            {
                cmd.Parameters.AddWithValue("@LeitorId", leitorId);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public void InserirEmprestimo(Emprestimo e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("InserirEmprestimo", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LeitorId", e.LeitorId);
                cmd.Parameters.AddWithValue("@LivroId", e.LivroId);
                cmd.Parameters.AddWithValue("@DataEmprestimo", e.DataEmprestimo);
                cmd.Parameters.AddWithValue("@DataDevolucao", (object)e.DataDevolucao ?? DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        //Read
        public List<Emprestimo> ObterEmprestimos()
        {
            List<Emprestimo> emprestimos = new List<Emprestimo>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ListarEmprestimosComDetalhes", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        emprestimos.Add(new Emprestimo
                        {
                            EmprestimoId = (int)reader["EmprestimoId"],
                            LeitorId = (int)reader["LeitorId"],
                            LivroId = (int)reader["LivroId"],
                            NomeLeitor = reader["NomeLeitor"].ToString(),
                            TituloLivro = reader["TituloLivro"].ToString(),
                            DataEmprestimo = (DateTime)reader["DataEmprestimo"],
                            DataDevolucao = reader["DataDevolucao"] as DateTime?
                        });
                    }
                }
            }
            return emprestimos;
        }

        public void MarcarDevolucao(int emprestimoId, DateTime dataDevolucao)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("MarcarDevolucao", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmprestimoId", emprestimoId);
                cmd.Parameters.AddWithValue("@DataDevolucao", dataDevolucao);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarLeitor(int leitorId) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("EliminarLeitor", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LeitorId", leitorId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
        public void EliminarLivro(int livroId) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("EliminarLivro", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LivroId", livroId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarEmprestimo(int emprestimoId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("EliminarEmprestimo", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmprestimoId", emprestimoId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool LivroTemEmprestimos(int livroId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Emprestimos WHERE LivroId = @LivroId", conn))
            {
                cmd.Parameters.AddWithValue("@LivroId", livroId);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public void AtualizarDataDevolucao (int emprestimoId, DateTime novaData)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("AtualizarDataDevolucao", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmprestimoId", emprestimoId);
                cmd.Parameters.AddWithValue("@DataDevolucao", novaData);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Emprestimo> ObterEmprestimosPorLeitor(int leitorId)
        {
            List<Emprestimo> lista = new List<Emprestimo>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ListarEmprestimosPorLeitorComDetalhes", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LeitorId", leitorId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Emprestimo
                        {
                            EmprestimoId = (int)reader["EmprestimoId"],
                            LeitorId = (int)reader["LeitorId"],
                            LivroId = (int)reader["LivroId"],
                            NomeLeitor = reader["NomeLeitor"].ToString(),
                            TituloLivro = reader["TituloLivro"].ToString(),
                            DataEmprestimo = (DateTime)reader["DataEmprestimo"],
                            DataDevolucao = reader["DataDevolucao"] as DateTime?
                        });
                    }
                }
            }
            return lista;
        }

        public List<Emprestimo> ObterEmprestimosPorLivro(int livroId)
        {
            List<Emprestimo> lista = new List<Emprestimo>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ListarEmprestimosPorLivro", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LivroId", livroId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Emprestimo
                        {
                            EmprestimoId = (int)reader["EmprestimoId"],
                            LeitorId = (int)reader["LeitorId"],
                            LivroId = (int)reader["LivroId"],
                            NomeLeitor = reader["NomeLeitor"].ToString(),
                            TituloLivro = reader["TituloLivro"].ToString(),
                            DataEmprestimo = (DateTime)reader["DataEmprestimo"],
                            DataDevolucao = reader["DataDevolucao"] as DateTime?
                        });
                    }
                }
            }
            return lista;
        }



    }
}
