//EXAMEN SEGUNDO SEGUIMIENTO
//Taller Avanzado de Desarrollo de Software
//Integrantes:
//Moedano Alcántara María Fernanda
//Ortiz Velazquez Gabriela Paola 

using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace apiRESTCine.Models
{
    public class clsSucursal
    {
        // Atributos (todos de tipo string)
        private string _clave;
        private string _nombre;
        private string _direccion;
        private string _url;
        private string _logo;

        // Métodos get y set
        public string clave { get { return _clave; } set { _clave = value; } }
        public string nombre { get { return _nombre; } set { _nombre = value; } }
        public string direccion { get { return _direccion; } set { _direccion = value; } }
        public string url { get { return _url; } set { _url = value; } }
        public string logo { get { return _logo; } set { _logo = value; } }

        // Cadena de conexión (definida en web.config)
        string cadConn = ConfigurationManager.ConnectionStrings["CineDb"].ConnectionString;

        // Constructor sin parámetros (vacío)
        public clsSucursal() { }

        // Constructor con parámetros (se asignan todos los atributos)
        public clsSucursal(string clave, string nombre, string direccion, string url, string logo)
        {
            this.clave = clave;
            this.nombre = nombre;
            this.direccion = direccion;
            this.url = url;
            this.logo = logo;
        }

        // Método para ejecutar la vista vwRptSucursales y retornar el DataSet con los datos
        public DataSet vwRptSucursales()
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(cadConn))
                {
                    string query = "SELECT * FROM vwRptSucursales";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, cnn);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        // Método para ejecutar el procedimiento almacenado spInsSucursales y retornar el DataSet con la bandera de estatus
        public DataSet spInsSucursales()
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(cadConn))
                {
                    cnn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("spInsSucursales", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre", this.nombre);
                        cmd.Parameters.AddWithValue("@p_direccion", this.direccion);
                        cmd.Parameters.AddWithValue("@p_homeweb", this.url);
                        cmd.Parameters.AddWithValue("@p_logo", this.logo);

                        // Parámetro de salida
                        MySqlParameter outParam = new MySqlParameter("@p_resultado", MySqlDbType.Int32)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outParam);

                        cmd.ExecuteNonQuery();

                        int resultado = Convert.ToInt32(outParam.Value);

                        // Crear un DataTable con la bandera de estatus
                        DataTable dt = new DataTable();
                        dt.Columns.Add("resultado", typeof(string));
                        dt.Rows.Add(resultado.ToString());
                        ds.Tables.Add(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        // Método para ejecutar el procedimiento almacenado spEliminarSucursal y retornar el DataSet con la bandera de estatus
        public DataSet spDelSucursal()
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(cadConn))
                {
                    cnn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("spEliminarSucursal", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Parámetro de entrada: convertir clave a entero
                        cmd.Parameters.AddWithValue("@p_cveSucursal", Convert.ToInt32(this.clave));

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        DataTable dt = new DataTable();
                        dt.Columns.Add("resultado", typeof(string));
                        // Si se afectó alguna fila, se considera éxito (0); de lo contrario, 1 (no se encontró)
                        dt.Rows.Add(filasAfectadas > 0 ? "0" : "1");
                        ds.Tables.Add(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("resultado", typeof(string));
                dt.Rows.Add("-1");
                ds.Tables.Add(dt);
                throw ex;
            }
            return ds;
        }
    }
}

