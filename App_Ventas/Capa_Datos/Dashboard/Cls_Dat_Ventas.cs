using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.Dashboard;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace Capa_Datos.Dashboard
{
    public class Cls_Dat_Ventas : Protected.DataBaseHelper
    {
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista VENTAS *************************************************/

        public List<Cls_Ent_Venta> Dashboard_Venta_Listar(Cls_Ent_Venta entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Venta> lista = new List<Cls_Ent_Venta>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_REPORTE_VENTAS", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure; 
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
                    int pos_STR_FEC_CREACION = dr.GetOrdinal("STR_FEC_CREACION");
                    int pos_TOTAL = dr.GetOrdinal("TOTAL");

                    if (dr.HasRows)
                    {
                        Cls_Ent_Venta obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Venta();
                            if (dr.IsDBNull(pos_TOTAL)) obj.TOTAL = 0;
                            else obj.TOTAL = decimal.Parse(dr[pos_TOTAL].ToString());
                            if (dr.IsDBNull(pos_STR_FEC_CREACION)) obj.STR_FEC_CREACION = "";
                            else obj.STR_FEC_CREACION = dr.GetString(pos_STR_FEC_CREACION);

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



        public List<Cls_Ent_Venta> Dashboard_Compras_Listar(Cls_Ent_Venta entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Venta> lista = new List<Cls_Ent_Venta>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_REPORTE_COMPRAS", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                    int pos_STR_FEC_CREACION = dr.GetOrdinal("STR_FEC_CREACION");
                    int pos_TOTAL = dr.GetOrdinal("TOTAL");

                    if (dr.HasRows)
                    {
                        Cls_Ent_Venta obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Venta();
                            if (dr.IsDBNull(pos_TOTAL)) obj.TOTAL = 0;
                            else obj.TOTAL = decimal.Parse(dr[pos_TOTAL].ToString());
                            if (dr.IsDBNull(pos_STR_FEC_CREACION)) obj.STR_FEC_CREACION = "";
                            else obj.STR_FEC_CREACION = dr.GetString(pos_STR_FEC_CREACION);

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
