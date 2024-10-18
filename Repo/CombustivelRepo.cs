using System;
using System.Collections.Generic;
using System.Configuration; // Adicionado para acessar o Web.config
using System.Data.SqlClient;
using SistemaCadastro.Models;

namespace SistemaCadastro.Repos
{
    public class CombustivelRepo
    {
        private string connectionString;

        // Constructor sem parâmetro que obtém a string de conexão do Web.config
        public CombustivelRepo()
        {
            connectionString = ConfigurationManager.ConnectionStrings["stringConexao"].ConnectionString;
        }

        // Método para obter todos os combustíveis
        public List<Combustivel> GetAll()
        {
            List<Combustivel> combustiveis = new List<Combustivel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT CombustivelId, Nome FROM Combustivel", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    combustiveis.Add(new Combustivel
                    {
                        CombustivelId = (int)reader["CombustivelId"],
                        Nome = reader["Nome"].ToString()
                    });
                }
            }

            return combustiveis;
        }

        // Método para adicionar um novo combustível
        public void Add(Combustivel combustivel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Combustivel (Nome) VALUES (@Nome)", connection);
                command.Parameters.AddWithValue("@Nome", combustivel.Nome);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Método para buscar combustível por ID
        public Combustivel GetById(int id)
        {
            Combustivel combustivel = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT CombustivelId, Nome FROM Combustivel WHERE CombustivelId = @CombustivelId", connection);
                command.Parameters.AddWithValue("@CombustivelId", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    combustivel = new Combustivel
                    {
                        CombustivelId = (int)reader["CombustivelId"],
                        Nome = reader["Nome"].ToString()
                    };
                }
            }

            return combustivel;
        }

        // Método para atualizar um combustível existente
        public void Update(Combustivel combustivel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE Combustivel SET Nome = @Nome WHERE CombustivelId = @CombustivelId", connection);
                command.Parameters.AddWithValue("@Nome", combustivel.Nome);
                command.Parameters.AddWithValue("@CombustivelId", combustivel.CombustivelId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Método para deletar um combustível
        public bool Delete(int id)
        {
            int rowsAffected;

            string query = "DELETE FROM Combustivel WHERE CombustivelId = @CombustivelId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CombustivelId", id);
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected >= 1;
        }
    }
}
