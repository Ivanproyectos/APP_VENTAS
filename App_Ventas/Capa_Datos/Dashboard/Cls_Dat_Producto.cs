using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.Inventario;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos.Dashboard
{
    public class Cls_Dat_Producto : Protected.DataBaseHelper
    {

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista producto movimiento*************************************************/

        public List<Cls_Ent_Movimiento_Producto> Dashboard_ProductoMovimiento_Listar(Cls_Ent_Movimiento_Producto entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Movimiento_Producto> lista = new List<Cls_Ent_Movimiento_Producto>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_REPORTE_MOV_PRODUCTO_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar,200)).Value = entidad_param.COD_USUARIO;
                    if (entidad_param.COD_USUARIO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar, 200)).Value = entidad_param.COD_USUARIO; }

                    //cmd.Parameters.Add(new SqlParameter("@PI_FECHA_INICIO", SqlDbType.VarChar,200)).Value = entidad_param.FECHA_INICIO;
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
                    int pos_ID_MOVIMIENTO = dr.GetOrdinal("ID_MOVIMIENTO");
                    int pos_MOVIMIENTO = dr.GetOrdinal("MOVIMIENTO");
                    int pos_CANTIDAD = dr.GetOrdinal("CANTIDAD");
                    int pos_DESC_PRODUCTO = dr.GetOrdinal("DESC_PRODUCTO");
                    int pos_FEC_CREACION = dr.GetOrdinal("FEC_CREACION");
                    int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                    int pos_DETALLE = dr.GetOrdinal("DETALLE");
                    int pos_ID_UNIDAD_MEDIDA = dr.GetOrdinal("ID_UNIDAD_MEDIDA");
                    int pos_COD_UNIDAD_MEDIDA = dr.GetOrdinal("COD_UNIDAD_MEDIDA");

                    if (dr.HasRows)
                    {
                        Cls_Ent_Movimiento_Producto obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Movimiento_Producto();
                            if (dr.IsDBNull(pos_ID_MOVIMIENTO)) obj.ID_MOVIMIENTO = 0;
                            else obj.ID_MOVIMIENTO = int.Parse(dr[pos_ID_MOVIMIENTO].ToString());
                            if (dr.IsDBNull(pos_MOVIMIENTO)) obj.MOVIMIENTO = "";
                            else obj.MOVIMIENTO = dr.GetString(pos_MOVIMIENTO);
                            if (dr.IsDBNull(pos_CANTIDAD)) obj.CANTIDAD = 0;
                            else obj.CANTIDAD = int.Parse(dr[pos_CANTIDAD].ToString());
                            if (dr.IsDBNull(pos_DESC_PRODUCTO)) obj.DESC_PRODUCTO = "";
                            else obj.DESC_PRODUCTO = dr.GetString(pos_DESC_PRODUCTO);
                            if (dr.IsDBNull(pos_FEC_CREACION)) obj.FEC_CREACION = "";
                            else obj.FEC_CREACION = dr.GetString(pos_FEC_CREACION);

                            if (dr.IsDBNull(pos_USU_CREACION)) obj.USU_CREACION = "";
                            else obj.USU_CREACION = dr.GetString(pos_USU_CREACION);

                            if (dr.IsDBNull(pos_DETALLE)) obj.DETALLE = "";
                            else obj.DETALLE = dr.GetString(pos_DETALLE);

                            if (dr.IsDBNull(pos_ID_UNIDAD_MEDIDA)) obj.ID_UNIDAD_MEDIDA = 0;
                            else obj.ID_UNIDAD_MEDIDA = int.Parse(dr[pos_ID_UNIDAD_MEDIDA].ToString());

                            if (dr.IsDBNull(pos_COD_UNIDAD_MEDIDA)) obj.COD_UNIDAD_MEDIDA = "";
                            else obj.COD_UNIDAD_MEDIDA = dr.GetString(pos_COD_UNIDAD_MEDIDA);


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

        ///*********************************************** Lista producto movimiento*************************************************/

        public List<Cls_Ent_Translado_Producto> Dashboard_ProductoTranslados_Listar(Cls_Ent_Translado_Producto entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Translado_Producto> lista = new List<Cls_Ent_Translado_Producto>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_REPORTE_TRANSL_PRODUCTO_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar,200)).Value = entidad_param.COD_USUARIO;
                    if (entidad_param.COD_USUARIO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar, 200)).Value = entidad_param.COD_USUARIO; }

                    //cmd.Parameters.Add(new SqlParameter("@PI_FECHA_INICIO", SqlDbType.VarChar,200)).Value = entidad_param.FECHA_INICIO;
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
                    int pos_ID_TRANSLADO = dr.GetOrdinal("ID_TRANSLADO");
                    int pos_DESC_SUCURSAL_ORIGEN = dr.GetOrdinal("DESC_SUCURSAL_ORIGEN");
                    int pos_DESC_SUCURSAL_DESTINO = dr.GetOrdinal("DESC_SUCURSAL_DESTINO");
                    int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                    int pos_DETALLE = dr.GetOrdinal("DETALLE");
                    int pos_FEC_CREACION = dr.GetOrdinal("FEC_CREACION");

                    if (dr.HasRows)
                    {
                        Cls_Ent_Translado_Producto obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Translado_Producto();
                            if (dr.IsDBNull(pos_ID_TRANSLADO)) obj.ID_TRANSLADO = 0;
                            else obj.ID_TRANSLADO = int.Parse(dr[pos_ID_TRANSLADO].ToString());
                            if (dr.IsDBNull(pos_DESC_SUCURSAL_ORIGEN)) obj.DESC_SUCURSAL_ORIGEN = "";
                            else obj.DESC_SUCURSAL_ORIGEN = dr.GetString(pos_DESC_SUCURSAL_ORIGEN);
                            if (dr.IsDBNull(pos_DESC_SUCURSAL_DESTINO)) obj.DESC_SUCURSAL_DESTINO = "";
                            else obj.DESC_SUCURSAL_DESTINO = dr.GetString(pos_DESC_SUCURSAL_DESTINO);
                            if (dr.IsDBNull(pos_FEC_CREACION)) obj.FEC_CREACION = "";
                            else obj.FEC_CREACION = dr.GetString(pos_FEC_CREACION);

                            if (dr.IsDBNull(pos_USU_CREACION)) obj.USU_CREACION = "";
                            else obj.USU_CREACION = dr.GetString(pos_USU_CREACION);

                            if (dr.IsDBNull(pos_DETALLE)) obj.DETALLE = "";
                            else obj.DETALLE = dr.GetString(pos_DETALLE);



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
