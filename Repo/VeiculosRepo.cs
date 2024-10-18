using SistemaCadastro.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SistemaCadastro.Repo
{
    public class VeiculosRepo
    {
        private SqlConnection _con;
        private string connectionString;

        public VeiculosRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public VeiculosRepo() : this(ConfigurationManager.ConnectionStrings["stringConexao"].ToString())
        {
        }

        private void Connection()
        {
            _con = new SqlConnection(connectionString);
        }

        public bool IncluirVeiculo(Veiculos veiculoObj)
        {
            Connection();

            string query = @"INSERT INTO Veiculos
                     (Placa, Renavam, Chassi, Motor, Marca, Modelo, Combustivel, Cor, Ano, Situacao)
                     VALUES
                     (@Placa, @Renavam, @Chassi, @Motor, @Marca, @Modelo, @Combustivel, @Cor, @Ano, @Situacao)";

            using (SqlCommand command = new SqlCommand(query, _con))
            {
                command.Parameters.AddWithValue("@Placa", veiculoObj.Placa);
                command.Parameters.AddWithValue("@Renavam", veiculoObj.Renavam);
                command.Parameters.AddWithValue("@Chassi", veiculoObj.Chassi);
                command.Parameters.AddWithValue("@Motor", veiculoObj.Motor);
                command.Parameters.AddWithValue("@Marca", veiculoObj.Marca);
                command.Parameters.AddWithValue("@Modelo", veiculoObj.Modelo);
                command.Parameters.AddWithValue("@Combustivel", veiculoObj.Combustivel);
                command.Parameters.AddWithValue("@Cor", veiculoObj.Cor);
                command.Parameters.AddWithValue("@Ano", veiculoObj.Ano);
                command.Parameters.AddWithValue("@Situacao", veiculoObj.Status);

                _con.Open();
                command.ExecuteNonQuery();
            }

            _con.Close();
            return true;
        }

        public List<Veiculos> ObterVeiculos()
        {
            Connection();
            List<Veiculos> veiculosList = new List<Veiculos>();

            using (SqlCommand command = new SqlCommand("ObterVeiculos", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                _con.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Veiculos veiculo = new Veiculos()
                    {
                        VeiculosId = Convert.ToInt32(reader["VeiculosId"]),
                        Placa = Convert.ToString(reader["Placa"]),
                        Renavam = Convert.ToInt32(reader["Renavam"]),
                        Chassi = Convert.ToString(reader["Chassi"]),
                        Motor = Convert.ToString(reader["Motor"]),
                        Marca = Convert.ToString(reader["Marca"]),
                        Modelo = Convert.ToString(reader["Modelo"]),
                        Combustivel = Convert.ToString(reader["Combustivel"]),
                        Cor = Convert.ToString(reader["Cor"]),
                        Ano = Convert.ToInt32(reader["Ano"]),
                        Status = Convert.ToString(reader["Situacao"])
                    };

                    veiculosList.Add(veiculo);
                }

                _con.Close();

                return veiculosList;
            }
        }

        public bool AtualizarVeiculo(Veiculos veiculoObj)
        {
            Connection();

            int rowsAffected;

            string query = @"UPDATE Veiculos SET
                    Placa = @Placa,
                    Renavam = @Renavam,
                    Chassi = @Chassi,
                    Motor = @Motor,
                    Marca = @Marca,
                    Modelo = @Modelo,
                    Combustivel = @Combustivel,
                    Cor = @Cor,
                    Ano = @Ano,
                    Situacao = @Situacao
                    WHERE VeiculosId = @VeiculoId";

            using (SqlCommand command = new SqlCommand(query, _con))
            {
                command.Parameters.AddWithValue("@VeiculoId", veiculoObj.VeiculosId);
                command.Parameters.AddWithValue("@Placa", veiculoObj.Placa);
                command.Parameters.AddWithValue("@Renavam", veiculoObj.Renavam);
                command.Parameters.AddWithValue("@Chassi", veiculoObj.Chassi);
                command.Parameters.AddWithValue("@Motor", veiculoObj.Motor);
                command.Parameters.AddWithValue("@Marca", veiculoObj.Marca);
                command.Parameters.AddWithValue("@Modelo", veiculoObj.Modelo);
                command.Parameters.AddWithValue("@Combustivel", veiculoObj.Combustivel);
                command.Parameters.AddWithValue("@Cor", veiculoObj.Cor);
                command.Parameters.AddWithValue("@Ano", veiculoObj.Ano);
                command.Parameters.AddWithValue("@Situacao", veiculoObj.Status);

                _con.Open();

                rowsAffected = command.ExecuteNonQuery();
            }

            _con.Close();

            return rowsAffected >= 1;
        }

        public bool ExcluirVeiculo(int id)
        {
            Connection();

            int rowsAffected;

            string query = "DELETE FROM Veiculos WHERE VeiculosId = @VeiculoId";

            using (SqlCommand command = new SqlCommand(query, _con))
            {
                command.Parameters.AddWithValue("@VeiculoId", id);

                _con.Open();

                rowsAffected = command.ExecuteNonQuery();
            }

            _con.Close();

            return rowsAffected >= 1;
        }

        public List<string> ObterStatus()
        {
            Connection();
            List<string> statusList = new List<string>();

            string query = "SELECT DISTINCT Situacao FROM Veiculos";

            using (SqlCommand command = new SqlCommand(query, _con))
            {
                _con.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string status = Convert.ToString(reader["Situacao"]);
                    statusList.Add(status);
                }

                _con.Close();
            }

            return statusList;
        }
    }
}