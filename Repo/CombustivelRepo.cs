using SistemaCadastro.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SistemaCadastro.Repos
{
    public class CombustivelRepo
    {
        private string connectionString;

        public CombustivelRepo()
        {
            connectionString = ConfigurationManager.ConnectionStrings["stringConexao"].ConnectionString;
        }

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

        //public bool Delete(int id)
        //{
        //    int rowsAffected;

        //    string query = "DELETE FROM Combustivel WHERE CombustivelId = @CombustivelId";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@CombustivelId", id);
        //            connection.Open();
        //            rowsAffected = command.ExecuteNonQuery();
        //        }
        //    }

        //    return rowsAffected >= 1;
        //}  ==>>> Algo futuo pode vir disso ??? Vamos ver... É que na minha cabeça não faz sentido excluir Colunas, MAS sim Editar por outro Combustivel
    }
}