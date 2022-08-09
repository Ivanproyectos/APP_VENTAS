using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace Capa_Datos.Administracion
{
    public class Cls_Dat_Notificacion : Protected.DataBaseHelper
    {
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista los  Notificacion *************************************************/

        public List<Cls_Ent_Notificacion> Notificacion_Listar(Cls_Ent_Notificacion entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Notificacion> lista = new List<Cls_Ent_Notificacion>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_NOTI_NOTIFICACION_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ESTADO", SqlDbType.Int)).Value = entidad_param.ESTADO;
                    if (entidad_param.FECHA_INICIO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_FECHA_INICIO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_FECHA_INICIO", SqlDbType.VarChar, 200)).Value = entidad_param.FECHA_INICIO; }
                    //cmd.Parameters.Add(new SqlParameter("@PI_FECHA_FIN", SqlDbType.VarChar,200)).Value = entidad_param.FECHA_FIN;
                    if (entidad_param.FECHA_FIN == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_FECHA_FIN", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_FECHA_FIN", SqlDbType.VarChar, 200)).Value = entidad_param.FECHA_FIN; }

                    dr = cmd.ExecuteReader();
                    int pos_ID_NOTIFICACION = dr.GetOrdinal("ID_NOTIFICACION");
                    int pos_HORA = dr.GetOrdinal("HORA");
                    int pos_MENSAJE = dr.GetOrdinal("MENSAJE");
                    int pos_FECHA_REGISTRO = dr.GetOrdinal("FECHA_REGISTRO");
                    int pos_ESTADO = dr.GetOrdinal("ESTADO");
                    int pos_IMAGE = dr.GetOrdinal("IMAGE");
                    int pos_COLOR = dr.GetOrdinal("COLOR");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Notificacion obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Notificacion();
                            if (dr.IsDBNull(pos_ID_NOTIFICACION)) obj.ID_NOTIFICACION = 0;
                            else obj.ID_NOTIFICACION = long.Parse(dr[pos_ID_NOTIFICACION].ToString());
                            if (dr.IsDBNull(pos_HORA)) obj.HORA = "";
                            else obj.HORA = dr.GetString(pos_HORA);
                            if (dr.IsDBNull(pos_MENSAJE)) obj.MENSAJE = "";
                            else obj.MENSAJE = dr.GetString(pos_MENSAJE);
                            if (dr.IsDBNull(pos_FECHA_REGISTRO)) obj.FECHA_REGISTRO = "";
                            else obj.FECHA_REGISTRO = dr.GetString(pos_FECHA_REGISTRO);
                            if (dr.IsDBNull(pos_ESTADO)) obj.ESTADO = 0;
                            else obj.ESTADO = int.Parse(dr[pos_ESTADO].ToString());
                            if (dr.IsDBNull(pos_IMAGE)) obj.IMAGE = "";
                            else obj.IMAGE = dr.GetString(pos_IMAGE);
                            if (dr.IsDBNull(pos_COLOR)) obj.COLOR = "";
                            else obj.COLOR = dr.GetString(pos_COLOR);
                            lista.Add(obj);
                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return lista;
        }

        public void Notificacion_Estado( ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_NOTI_NOTIFICACION_ESTADO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }
    }
}
