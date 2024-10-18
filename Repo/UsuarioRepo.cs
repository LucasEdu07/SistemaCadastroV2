using SistemaCadastro.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;

public class UsuarioRepo
{
    private string connectionString;

    public UsuarioRepo()
    {
        connectionString = ConfigurationManager.ConnectionStrings["stringConexao"].ConnectionString;
    }

    public Usuarios ObterUsuario(string nomeUsuario, string senha)
    {
        Usuarios usuario = null;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE NomeUsuario = @NomeUsuario AND Senha = @Senha";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NomeUsuario", nomeUsuario);
            cmd.Parameters.AddWithValue("@Senha", senha);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                usuario = new Usuarios
                {
                    UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                    NomeUsuario = reader["NomeUsuario"].ToString(),
                    TipoUsuario = reader["TipoUsuario"].ToString()
                };
            }

            reader.Close();
        }

        return usuario;
    }
}