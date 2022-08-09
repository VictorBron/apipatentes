using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace APIACCESOREST.Models
{
    public class CONEXIONSP
    {
        public static string server = WebConfigurationManager.AppSettings["serverbd"].ToString();
        public static string initialcatalog = WebConfigurationManager.AppSettings["initialcatolog"].ToString();
        public static string userbd = WebConfigurationManager.AppSettings[nameof(userbd)].ToString();
        public static string passwordbd = WebConfigurationManager.AppSettings[nameof(passwordbd)].ToString();
        public static string ConString = "Data source=" + CONEXIONSP.server + "; Initial Catalog=" + CONEXIONSP.initialcatalog + "; User ID=" + CONEXIONSP.userbd + "; Password=" + CONEXIONSP.passwordbd;



    public static void RegistraAcceso(registroingreso registroingreso)
    {
        try
        {
            SqlCommand selectCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Connection = new SqlConnection(CONEXIONSP.ConString);
            selectCommand.Connection.Open();
            selectCommand.Parameters.Clear();
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.CommandText = "REGISTRA_ACCESO_WEB";
            selectCommand.Parameters.AddWithValue("@MOV_ID_MOVIMIENTO",1);
            selectCommand.Parameters.AddWithValue("@IP",registroingreso.ip);
            selectCommand.Parameters.AddWithValue("@ID_VEHICULO", DBNull.Value);
            selectCommand.Parameters.AddWithValue("@ID_PERSONA_APROBADA",registroingreso.codigoAutorizacion);
            selectCommand.Parameters.AddWithValue("@TEMPERATURA",registroingreso.temperatura);
            selectCommand.ExecuteNonQuery();
            selectCommand.Connection.Close();
            selectCommand.Dispose();
        }
        catch (Exception ex)
        {
            throw;
        }
        }

    public static DATOS_MOVIMIENTO_MOVIL_Result ValidaAcceso(int rut,string ip)
    {
        try
        {
            DATOS_MOVIMIENTO_MOVIL_Result Datos = new DATOS_MOVIMIENTO_MOVIL_Result();
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection = new SqlConnection(CONEXIONSP.ConString);
            cmd.Connection.Open();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DATOS_MOVIMIENTO_MOVIL";
            cmd.Parameters.AddWithValue("@RUT", rut);
            cmd.Parameters.AddWithValue("@IP", ip);
            da.Fill(dt);
            cmd.Connection.Close();
            cmd.Dispose();

            if (dt.Rows.Count > 0)
            {
                Datos.EMPRESA = dt.Rows[0]["EMPRESA"].ToString();
                Datos.AUTORIZACION = dt.Rows[0]["AUTORIZACION"].ToString();
                Datos.ESTADO = dt.Rows[0]["ESTADO"].ToString();
                Datos.ID_PERSONA_APROBADA = int.Parse(dt.Rows[0]["ID_PERSONA_APROBADA"].ToString());




            }
            return Datos;
        }
        catch(Exception ex)
        {
            throw new Exception("Error al validar rut:" + ex.Message.ToString());
        }
      



    }
        public static List<Personas_Imagenes> PersonasImagenes()
        {
            List<Personas_Imagenes> LP = new List<Personas_Imagenes>();

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection = new SqlConnection(CONEXIONSP.ConString);
            cmd.Connection.Open();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LISTA_PERSONAS";
            da.Fill(dt);
            cmd.Connection.Close();
            cmd.Dispose();

            if (dt.Rows.Count > 0)
            {

                foreach (DataRow item in dt.Rows)
                {
                    Personas_Imagenes pi = new Personas_Imagenes();
                    pi.Rut = int.Parse(item["RUT"].ToString());
                    pi.fotos = fotos(int.Parse(item["ID_PERSONA"].ToString()));

                    //menu.Append(item["MENU"].ToString() + "/");
                    LP.Add(pi);
                }

             

            }

            return LP;
        }
        public static List<int> fotos(int ID_PERSONA)
        {
            List<int> LF = new List<int>();
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection = new SqlConnection(CONEXIONSP.ConString);
            cmd.Connection.Open();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "FOTOS_PERSONA";
            cmd.Parameters.AddWithValue("@ID_PERSONA", ID_PERSONA);
            da.Fill(dt);
            cmd.Connection.Close();
            cmd.Dispose();

            if (dt.Rows.Count > 0)
            {

                foreach (DataRow item in dt.Rows)
                {

                    LF.Add(int.Parse(item["IMPE_CORRELATIVO"].ToString()));
                   
                }

            }
            
            return LF;
        }


        public static Personas_Imagenes PersonaImagen(int rut,int idFoto)
        {
            Personas_Imagenes pi = new Personas_Imagenes();

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection = new SqlConnection(CONEXIONSP.ConString);
            cmd.Connection.Open();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RECUPERA_FOTO";
            cmd.Parameters.AddWithValue("@RUT", rut);
            cmd.Parameters.AddWithValue("@ID_FOTO", idFoto);
            da.Fill(dt);
            cmd.Connection.Close();
            cmd.Dispose();

            if (dt.Rows.Count > 0)
            {

                pi.Rut = int.Parse(dt.Rows[0]["RUT"].ToString());
         
              


            }

            return pi;
        }
        public static Imagen ImagenPersona(int rut, int idFoto)
        {
            Imagen pi = new Imagen();

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection = new SqlConnection(CONEXIONSP.ConString);
            cmd.Connection.Open();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RECUPERA_FOTO";
            cmd.Parameters.AddWithValue("@RUT", rut);
            cmd.Parameters.AddWithValue("@ID_FOTO", idFoto);
            da.Fill(dt);
            cmd.Connection.Close();
            cmd.Dispose();

            if (dt.Rows.Count > 0)
            {

                pi.Rut = int.Parse(dt.Rows[0]["RUT"].ToString());
                pi.idFoto = int.Parse(dt.Rows[0]["IMPE_CORRELATIVO"].ToString());
                pi.fotoB64 = dt.Rows[0]["IMPE_IMAGEN"].ToString();



            }

            return pi;
        }

        public static DATOS_MOVIMIENTO_MOVIL_Result ValidaAccesoVehicular(Vehiculo VE )
        {
            try
            {
                DATOS_MOVIMIENTO_MOVIL_Result Datos = new DATOS_MOVIMIENTO_MOVIL_Result();
                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection = new SqlConnection(CONEXIONSP.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DATOS_MOVIMIENTO_PATENTE";
                cmd.Parameters.AddWithValue("@PATENTE", VE.placa);
                cmd.Parameters.AddWithValue("@IP", VE.ip);
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    Datos.ID_VEHICULO = int.Parse(dt.Rows[0]["ID_VEHICULO"].ToString());
                    Datos.AUTORIZACION = dt.Rows[0]["AUTORIZACION"].ToString();
                    Datos.ESTADO = dt.Rows[0]["ESTADO"].ToString();
                    Datos.ID_PERSONA_APROBADA = int.Parse(dt.Rows[0]["ID_PERSONA_APROBADA"].ToString());
                    Datos.ESTADO = dt.Rows[0]["ESTADO"].ToString();
                    Datos.TIPO_MOVIMIENTO = dt.Rows[0]["TIPOMOVIMIENTO"].ToString();
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar patente:" + ex.Message.ToString());
            }

        }

        public static void RegistraAccesoVehiculo(registroingreso registroingreso)
        {
            try
            {
                SqlCommand selectCommand = new SqlCommand();
                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                selectCommand.Connection = new SqlConnection(CONEXIONSP.ConString);
                selectCommand.Connection.Open();
                selectCommand.Parameters.Clear();
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.CommandText = "REGISTRA_ACCESO_WEB";
                selectCommand.Parameters.AddWithValue("@MOV_ID_MOVIMIENTO", registroingreso.tipomovimiento);
                selectCommand.Parameters.AddWithValue("@IP", registroingreso.ip);
                selectCommand.Parameters.AddWithValue("@ID_VEHICULO", registroingreso.idVehichulo);
                selectCommand.Parameters.AddWithValue("@ID_PERSONA_APROBADA", registroingreso.codigoAutorizacion);
                selectCommand.Parameters.AddWithValue("@TEMPERATURA", 33);
                selectCommand.ExecuteNonQuery();
                selectCommand.Connection.Close();
                selectCommand.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}