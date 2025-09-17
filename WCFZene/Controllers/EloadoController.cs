using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using WCFZene.Models;

namespace WCFZene.Controllers
{
    public class EloadoController
    {
        static MySqlConnection SqlConnection;
        private static void BuildConnection()
        {

            string connectionString = "SERVER = localhost;" +
                                       "DATABASE= zene;" +
                                       "UID = root;" +
                                       "PASSWORD =;" +
                                       "SSL MODE= none;";
            SqlConnection = new MySqlConnection();
            SqlConnection.ConnectionString = connectionString;
        }
        public string EloadoTorlese(int id)
        {
            BuildConnection();
            SqlConnection.Open();
            string sql = "DELETE FROM eloado WHERE Id = @id)";
            MySqlCommand cmd = new MySqlCommand(sql, SqlConnection);
            cmd.Parameters.AddWithValue("@id", id);
            int SorokSzama = cmd.ExecuteNonQuery();
            SqlConnection.Close();
            return SorokSzama > 0 ? "Sikeres törlés" : "Sikertelen törlés";
        }
        public string EloadoRogzitese(Eloado rogzitendo)
        {
            BuildConnection();
            SqlConnection.Open();
            string sql = "INSERT INTO `eloado`(`Id`, `Nev`, `Nemzetiseg`, `Szolo`) VALUES('[@nev,@nemzetiseg,@szolo]')";
            MySqlCommand cmd = new MySqlCommand(sql, SqlConnection);
            cmd.Parameters.AddWithValue("@id", rogzitendo.Id);
            cmd.Parameters.AddWithValue("@nev", rogzitendo.Nev);
            cmd.Parameters.AddWithValue("@nemzetiseg", rogzitendo.Nemzetiseg);
            cmd.Parameters.AddWithValue("@szolo", rogzitendo.Szolo);
            int SorokSzama = cmd.ExecuteNonQuery();
            SqlConnection.Close();
            return SorokSzama > 0 ? "Sikeres rögzítés" : "Sikertelen rögzítés";
        }

        public string EloadoModositasa(Eloado modositando)
        {
            BuildConnection();
            SqlConnection.Open();
            string sql = "UPDATE eloado SET Nev = @nev, Nemzetiseg = @nemzetiseg, Szolo = @szolo WHERE Id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, SqlConnection);
            cmd.Parameters.AddWithValue("@id", modositando.Id);
            cmd.Parameters.AddWithValue("@nev", modositando.Nev);
            cmd.Parameters.AddWithValue("@nemzetiseg", modositando.Nemzetiseg);
            cmd.Parameters.AddWithValue("@szolo", modositando.Szolo);
            int SorokSzama = cmd.ExecuteNonQuery();
            SqlConnection.Close();
            return SorokSzama > 0 ? "Sikeres módosítás" : "Sikertelen módosítás";
        }

        public List<Eloado> EloadokListaja()
        {
            List<Eloado> lista = new List<Eloado>();
            BuildConnection();
            SqlConnection.Open();
            string sql = "SELECT * FROM eloado";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = SqlConnection;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Eloado eloado = new Eloado();
                eloado.Id = reader.GetInt32("Id");
                eloado.Nev = reader.GetString("Nev");
                eloado.Nemzetiseg = reader.GetString("Nemzetiseg");
                eloado.Szolo = reader.GetBoolean("Szolo");
                lista.Add(eloado);
            }
            SqlConnection.Close();
            return lista;
        }
    }
}