using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace ImprentaAPI.Data
{
    public class PostgreSQLConfiguration
    {
        public string ConnectionString { get; set; }
        public PostgreSQLConfiguration(string connectionString) => ConnectionString = connectionString;
        
        public NpgsqlConnection AbreConexion()
        {
            NpgsqlConnection conexion = new NpgsqlConnection();
            var cadena = ConnectionString;
            if (!string.IsNullOrWhiteSpace(cadena))
            {
                try
                {
                    conexion = new NpgsqlConnection(cadena);
                    conexion.Open();
                }
                catch(Exception)
                {
                    conexion.Close();
                }
            }
            return conexion;
        }
    }
}
