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
    public class Cls_Dat_Dashboard : Protected.DataBaseHelper
    {
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** listar venta uno  *************************************************/
        public Cls_Ent_Dashboard Dashboard_Listar_Uno(Cls_Ent_Dashboard entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            Cls_Ent_Dashboard obj = new Cls_Ent_Dashboard();
            List<Cls_Ent_Dashboard> Lista_VentaMes = new List<Cls_Ent_Dashboard>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_DASH_COUNT_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.BigInt)).Value = entidad_param.ID_SUCURSAL;
                    dr = cmd.ExecuteReader();
                    int pos_TOTAL_VENTAS = dr.GetOrdinal("TOTAL_VENTAS");
                    int pos_MONTO_TOTAL_VENTAS = dr.GetOrdinal("MONTO_TOTAL_VENTAS");
                    int pos_TOTAL_COMPRAS = dr.GetOrdinal("TOTAL_COMPRAS");
                    int pos_TOTAL_DEVOLUCIONES = dr.GetOrdinal("TOTAL_DEVOLUCIONES");
                    if (dr.HasRows)
                    {
                        //Cls_Ent_Dashboard obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Dashboard();
                            if (dr.IsDBNull(pos_TOTAL_VENTAS)) obj.TOTAL_VENTAS = 0;
                            else obj.TOTAL_VENTAS = int.Parse(dr[pos_TOTAL_VENTAS].ToString());

                            if (dr.IsDBNull(pos_MONTO_TOTAL_VENTAS)) obj.MONTO_TOTAL_VENTAS =0;
                            else obj.MONTO_TOTAL_VENTAS = decimal.Parse(dr[pos_MONTO_TOTAL_VENTAS].ToString());

                            if (dr.IsDBNull(pos_TOTAL_COMPRAS)) obj.TOTAL_COMPRAS = 0;
                            else obj.TOTAL_COMPRAS = int.Parse(dr[pos_TOTAL_COMPRAS].ToString());

                            if (dr.IsDBNull(pos_TOTAL_DEVOLUCIONES)) obj.TOTAL_DEVOLUCIONES = 0;
                            else obj.TOTAL_DEVOLUCIONES = int.Parse(dr[pos_TOTAL_DEVOLUCIONES].ToString());
                        }
         
                    }

                    dr.Close();
                    obj.Lista_VentaMes = Dashboard_VentaMes_Listar(entidad_param, ref auditoria);
                    obj.Lista_Comparativa = Dashboard_Comparativa_Listar(entidad_param, ref auditoria);
                    obj.Lista_TipoPago = Dashboard_TipoPago_Listar(entidad_param, ref auditoria);
                    obj.Lista_ProductosMV = Dashboard_ProducosMasVendidos_Listar(entidad_param, ref auditoria);
                    
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return obj;
        }

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista grafico venta por mes *************************************************/

        public List<Cls_Ent_Dashboard> Dashboard_VentaMes_Listar(Cls_Ent_Dashboard entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Dashboard> lista = new List<Cls_Ent_Dashboard>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_DASH_VENTASMES_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad_param.ID_SUCURSAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_ANIO", SqlDbType.Int)).Value = entidad_param.ANIO; 
                    dr = cmd.ExecuteReader();
                    int pos_MES = dr.GetOrdinal("MES");
                    int pos_NUMERO_MES = dr.GetOrdinal("NUMERO_MES");
                    int pos_TOTAL = dr.GetOrdinal("TOTAL");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Dashboard obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Dashboard();
                            if (dr.IsDBNull(pos_NUMERO_MES)) obj.NUMERO_MES = 0;
                            else obj.NUMERO_MES = int.Parse(dr[pos_NUMERO_MES].ToString());

                            if (dr.IsDBNull(pos_MES)) obj.MES = "";
                            else obj.MES = dr.GetString(pos_MES);

                            if (dr.IsDBNull(pos_TOTAL)) obj.TOTAL = 0;
                            else obj.TOTAL = decimal.Parse(dr[pos_TOTAL].ToString());

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


        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista grafico comparativa*************************************************/

        public List<Cls_Ent_Dashboard> Dashboard_Comparativa_Listar(Cls_Ent_Dashboard entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Dashboard> lista = new List<Cls_Ent_Dashboard>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_DASH_COMPARATIVA_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad_param.ID_SUCURSAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_ANIO", SqlDbType.Int)).Value = entidad_param.ANIO;
                    dr = cmd.ExecuteReader();
                    int pos_MES = dr.GetOrdinal("MES");
                    int pos_NUMERO_MES = dr.GetOrdinal("NUMERO_MES");
                    int pos_TOTAL = dr.GetOrdinal("TOTAL");
                    int pos_TIPO = dr.GetOrdinal("TIPO");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Dashboard obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Dashboard();
                            if (dr.IsDBNull(pos_NUMERO_MES)) obj.NUMERO_MES = 0;
                            else obj.NUMERO_MES = int.Parse(dr[pos_NUMERO_MES].ToString());
                            if (dr.IsDBNull(pos_MES)) obj.MES = "";
                            else obj.MES = dr.GetString(pos_MES);
                            if (dr.IsDBNull(pos_TOTAL)) obj.TOTAL = 0;
                            else obj.TOTAL = decimal.Parse(dr[pos_TOTAL].ToString());
                            if (dr.IsDBNull(pos_TIPO)) obj.TIPO = "";
                            else obj.TIPO = dr.GetString(pos_TIPO);

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


        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista grafico comparativa*************************************************/

        public List<Cls_Ent_Dashboard> Dashboard_TipoPago_Listar(Cls_Ent_Dashboard entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Dashboard> lista = new List<Cls_Ent_Dashboard>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_DASH_TIPO_PAGO_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad_param.ID_SUCURSAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_ANIO", SqlDbType.Int)).Value = entidad_param.ANIO;
                    dr = cmd.ExecuteReader();
                    int pos_PORCENTAJE = dr.GetOrdinal("PORCENTAJE");
                    int pos_TIPO = dr.GetOrdinal("TIPO");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Dashboard obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Dashboard();
                            if (dr.IsDBNull(pos_PORCENTAJE)) obj.PORCENTAJE = 0;
                            else obj.PORCENTAJE = int.Parse(dr[pos_PORCENTAJE].ToString());
                            if (dr.IsDBNull(pos_TIPO)) obj.TIPO = "";
                            else obj.TIPO = dr.GetString(pos_TIPO);

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

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista productos mas vendidos*************************************************/

        public List<Cls_Ent_Dashboard> Dashboard_ProducosMasVendidos_Listar(Cls_Ent_Dashboard entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Dashboard> lista = new List<Cls_Ent_Dashboard>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_DASH_PRODUCTOSMASVENDIDOS_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad_param.ID_SUCURSAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_ANIO", SqlDbType.Int)).Value = entidad_param.ANIO;
                    dr = cmd.ExecuteReader();
                    int pos_CANTIDAD = dr.GetOrdinal("CANTIDAD");
                    int pos_PRODUCTO = dr.GetOrdinal("PRODUCTO");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Dashboard obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Dashboard();
                            if (dr.IsDBNull(pos_CANTIDAD)) obj.CANTIDAD = 0;
                            else obj.CANTIDAD = int.Parse(dr[pos_CANTIDAD].ToString());
                            if (dr.IsDBNull(pos_PRODUCTO)) obj.PRODUCTO = "";
                            else obj.PRODUCTO = dr.GetString(pos_PRODUCTO);

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
