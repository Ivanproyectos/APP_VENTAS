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
    public class Cls_Dat_Devolucion : Protected.DataBaseHelper
    {

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista los  cargo *************************************************/

        public List<Cls_Ent_Devolucion> Devolucion_Listar(Cls_Ent_Devolucion entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Devolucion> lista = new List<Cls_Ent_Devolucion>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_DEVOLUCION_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (entidad_param.DESC_DEVOLUCION == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_DEVOLUCION", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_DEVOLUCION", SqlDbType.VarChar, 200)).Value = entidad_param.DESC_DEVOLUCION; }
                    if (entidad_param.FLG_ESTADO == 2)
                    { cmd.Parameters.Add(new SqlParameter("@PI_FLG_ESTADO", SqlDbType.Int)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_FLG_ESTADO", SqlDbType.Int)).Value = entidad_param.FLG_ESTADO; }
                    dr = cmd.ExecuteReader();
                    int pos_ID_DEVOLUCION = dr.GetOrdinal("ID_DEVOLUCION");
                    int pos_DESC_DEVOLUCION = dr.GetOrdinal("DESC_DEVOLUCION");
                    int pos_FLG_ESTADO = dr.GetOrdinal("FLG_ESTADO");
                    int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                    int pos_FECHA_CREACION = dr.GetOrdinal("FECHA_CREACION");
                    int pos_USU_MODIFICACION = dr.GetOrdinal("USU_MODIFICACION");
                    int pos_FEC_MODIFICACION = dr.GetOrdinal("FECHA_MODIFICACION");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Devolucion obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Devolucion();
                            if (dr.IsDBNull(pos_ID_DEVOLUCION)) obj.ID_DEVOLUCION = 0;
                            else obj.ID_DEVOLUCION = int.Parse(dr[pos_ID_DEVOLUCION].ToString());

                            if (dr.IsDBNull(pos_DESC_DEVOLUCION)) obj.DESC_DEVOLUCION = "";
                            else obj.DESC_DEVOLUCION = dr.GetString(pos_DESC_DEVOLUCION);

                            if (dr.IsDBNull(pos_FLG_ESTADO)) obj.FLG_ESTADO = 0;
                            else obj.FLG_ESTADO = int.Parse(dr[pos_FLG_ESTADO].ToString());
                            if (dr.IsDBNull(pos_USU_CREACION)) obj.USU_CREACION = "";
                            else obj.USU_CREACION = dr.GetString(pos_USU_CREACION);
                            if (dr.IsDBNull(pos_FECHA_CREACION)) obj.FEC_CREACION = "";
                            else obj.FEC_CREACION = dr.GetString(pos_FECHA_CREACION);
                            if (dr.IsDBNull(pos_USU_MODIFICACION)) obj.USU_MODIFICACION = "";
                            else obj.USU_MODIFICACION = dr.GetString(pos_USU_MODIFICACION);
                            if (dr.IsDBNull(pos_FEC_MODIFICACION)) obj.FEC_MODIFICACION = "";
                            else obj.FEC_MODIFICACION = dr.GetString(pos_FEC_MODIFICACION);

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

    }
}
