using SistemaCadastro.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SistemaCadastro.Repos
{
    public class CorRepo
    {
        private string connectionString;

        public CorRepo()
        {
            connectionString = ConfigurationManager.ConnectionStrings["stringConexao"].ConnectionString;
        }

        public List<Cor> GetAll()
        {
            List<Cor> cores = new List<Cor>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT CorId, NomeCor FROM Cor", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cores.Add(new Cor
                    {
                        CorId = (int)reader["CorId"],
                        NomeCor = reader["NomeCor"].ToString()
                    });
                }
            }

            return cores;
        }

        public void Add(Cor cor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Cor (NomeCor) VALUES (@NomeCor)", connection);
                command.Parameters.AddWithValue("@NomeCor", cor.NomeCor);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Cor GetById(int id)
        {
            Cor cor = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT CorId, NomeCor FROM Cor WHERE CorId = @CorId", connection);
                command.Parameters.AddWithValue("@CorId", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    cor = new Cor
                    {
                        CorId = (int)reader["CorId"],
                        NomeCor = reader["NomeCor"].ToString()
                    };
                }
            }

            return cor;
        }

        public void Update(Cor cor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE Cor SET NomeCor = @NomeCor WHERE CorId = @CorId", connection);
                command.Parameters.AddWithValue("@NomeCor", cor.NomeCor);
                command.Parameters.AddWithValue("@CorId", cor.CorId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}