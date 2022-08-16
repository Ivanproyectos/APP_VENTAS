using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.Caja;
using Capa_Entidad.Administracion;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos.Caja
{
    public class Cls_Dat_Caja : Protected.DataBaseHelper
    {

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** listar venta uno  *************************************************/
        public Cls_Ent_Caja Caja_Listar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            Cls_Ent_Caja obj = new Cls_Ent_Caja();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_CAJA_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_FECHA_INICIO", SqlDbType.VarChar, 100)).Value = entidad.FEC_INICIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FECHA_FIN", SqlDbType.VarChar, 100)).Value = entidad.FEC_FIN;
                    //cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar, 100)).Value = entidad.COD_USUARIO;
                    if (entidad.COD_USUARIO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar, 100)).Value = entidad.COD_USUARIO; }
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad.ID_SUCURSAL;
                    dr = cmd.ExecuteReader();
                    int pos_COUNT_VENTA = dr.GetOrdinal("COUNT_VENTA");
                    int pos_TOTAL_VENTA = dr.GetOrdinal("TOTAL_VENTA");
                    int pos_COUNT_COBRAR = dr.GetOrdinal("COUNT_COBRAR");
                    int pos_TOTAL_COBRAR = dr.GetOrdinal("TOTAL_COBRAR");
                    int pos_COUNT_ADELANTO = dr.GetOrdinal("COUNT_ADELANTO");
                    int pos_TOTAL_ADELANTO = dr.GetOrdinal("TOTAL_ADELANTO");
                    int pos_COUNT_INGRESO = dr.GetOrdinal("COUNT_INGRESO");
                    int pos_TOTAL_INGRESO = dr.GetOrdinal("TOTAL_INGRESO");
                    int pos_COUNT_EGRESO = dr.GetOrdinal("COUNT_EGRESO");
                    int pos_TOTAL_EGRESO = dr.GetOrdinal("TOTAL_EGRESO");

                    int pos_COUNT_COMPRAS = dr.GetOrdinal("COUNT_COMPRAS");
                    int pos_TOTA_COMPRAS = dr.GetOrdinal("TOTAL_COMPRAS");
                    int pos_EGRESOS_NETO = dr.GetOrdinal("EGRESOS_NETO");
                    int pos_INGRESOS_NETO = dr.GetOrdinal("INGRESOS_NETO");
                    int pos_TOTAL_NETO = dr.GetOrdinal("TOTAL_NETO");

                    if (dr.HasRows)
                    {
                        //Cls_Ent_Cliente obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Caja();
                            if (dr.IsDBNull(pos_COUNT_VENTA)) obj.COUNT_VENTA = 0;
                            else obj.COUNT_VENTA = int.Parse(dr[pos_COUNT_VENTA].ToString());
                            if (dr.IsDBNull(pos_TOTAL_VENTA)) obj.TOTAL_VENTA = 0;
                            else obj.TOTAL_VENTA = decimal.Parse(dr[pos_TOTAL_VENTA].ToString());

                            if (dr.IsDBNull(pos_COUNT_COBRAR)) obj.COUNT_COBRAR = 0;
                            else obj.COUNT_COBRAR = int.Parse(dr[pos_COUNT_COBRAR].ToString());

                            if (dr.IsDBNull(pos_TOTAL_COBRAR)) obj.TOTAL_COBRAR = 0;
                            else obj.TOTAL_COBRAR = decimal.Parse(dr[pos_TOTAL_COBRAR].ToString());

                            if (dr.IsDBNull(pos_COUNT_ADELANTO)) obj.COUNT_ADELANTO = 0;
                            else obj.COUNT_ADELANTO = int.Parse(dr[pos_COUNT_ADELANTO].ToString());

                            if (dr.IsDBNull(pos_TOTAL_ADELANTO)) obj.TOTAL_ADELANTO = 0;
                            else obj.TOTAL_ADELANTO = decimal.Parse(dr[pos_TOTAL_ADELANTO].ToString());

                            if (dr.IsDBNull(pos_TOTAL_INGRESO)) obj.COUNT_INGRESO = 0;
                            else obj.COUNT_INGRESO = int.Parse(dr[pos_COUNT_INGRESO].ToString());

                            if (dr.IsDBNull(pos_TOTAL_INGRESO)) obj.TOTAL_INGRESO = 0;
                            else obj.TOTAL_INGRESO = decimal.Parse(dr[pos_TOTAL_INGRESO].ToString());

                            if (dr.IsDBNull(pos_COUNT_EGRESO)) obj.COUNT_EGRESO = 0;
                            else obj.COUNT_EGRESO = int.Parse(dr[pos_COUNT_EGRESO].ToString());

                            if (dr.IsDBNull(pos_TOTAL_EGRESO)) obj.TOTAL_EGRESO = 0;
                            else obj.TOTAL_EGRESO = decimal.Parse(dr[pos_TOTAL_EGRESO].ToString());

                            if (dr.IsDBNull(pos_COUNT_COMPRAS)) obj.COUNT_COMPRAS = 0;
                            else obj.COUNT_COMPRAS = int.Parse(dr[pos_COUNT_COMPRAS].ToString());

                            if (dr.IsDBNull(pos_TOTA_COMPRAS)) obj.TOTAL_COMPRAS = 0;
                            else obj.TOTAL_COMPRAS = decimal.Parse(dr[pos_TOTA_COMPRAS].ToString());

                            if (dr.IsDBNull(pos_EGRESOS_NETO)) obj.EGRESOS_NETO = 0;
                            else obj.EGRESOS_NETO = decimal.Parse(dr[pos_EGRESOS_NETO].ToString());

                            if (dr.IsDBNull(pos_INGRESOS_NETO)) obj.INGRESOS_NETO = 0;
                            else obj.INGRESOS_NETO = decimal.Parse(dr[pos_INGRESOS_NETO].ToString());

                            if (dr.IsDBNull(pos_TOTAL_NETO)) obj.TOTAL_NETO = 0;
                            else obj.TOTAL_NETO = decimal.Parse(dr[pos_TOTAL_NETO].ToString());


                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return obj;
        }

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** listar venta uno  *************************************************/
        public Cls_Ent_Caja Caja_Movimiento_Listar_Uno(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            Cls_Ent_Caja obj = new Cls_Ent_Caja();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_CAJA_MOVIMIENTO_LISTAR_UNO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_TIPO_MOVIMIENTO", SqlDbType.Int)).Value = entidad.ID_SUCURSAL;
                    dr = cmd.ExecuteReader();
                    int pos_ID_TIPO_MOVIMIENTO = dr.GetOrdinal("ID_TIPO_MOVIMIENTO");
                    int pos_ID_SUCURSAL = dr.GetOrdinal("ID_SUCURSAL");
                    int pos_DESC_MOVIMIENTO = dr.GetOrdinal("DESC_MOVIMIENTO");
                    int pos_MONTO = dr.GetOrdinal("MONTO");

                    if (dr.HasRows)
                    {
                        //Cls_Ent_Cliente obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Caja();
                            if (dr.IsDBNull(pos_ID_TIPO_MOVIMIENTO)) obj.ID_TIPO_MOVIMIENTO = 0;
                            else obj.ID_TIPO_MOVIMIENTO = int.Parse(dr[pos_ID_TIPO_MOVIMIENTO].ToString());

                            if (dr.IsDBNull(pos_ID_SUCURSAL)) obj.ID_SUCURSAL = 0;
                            else obj.ID_SUCURSAL = int.Parse(dr[pos_ID_SUCURSAL].ToString());

                            if (dr.IsDBNull(pos_DESC_MOVIMIENTO)) obj.DESC_MOVIMIENTO = "";
                            else obj.DESC_MOVIMIENTO = dr.GetString(pos_DESC_MOVIMIENTO);

                            if (dr.IsDBNull(pos_MONTO)) obj.MONTO = 0;
                            else obj.MONTO = decimal.Parse(dr[pos_MONTO].ToString());


                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return obj;
        }
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista los  movimientos *************************************************/

        public List<Cls_Ent_Caja> Caja_Movimiento_Listar(Cls_Ent_Caja entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Caja> lista = new List<Cls_Ent_Caja>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_CAJA_MOVIMIENTO_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (entidad_param.ID_SUCURSAL == 0)
                    { cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad_param.ID_SUCURSAL; }
                    if (entidad_param.USU_CREACION == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_USU_CREACION", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_USU_CREACION", SqlDbType.VarChar, 200)).Value = entidad_param.USU_CREACION; }
                    cmd.Parameters.Add(new SqlParameter("@PI_FEC_INICIO", SqlDbType.VarChar, 200)).Value = entidad_param.FEC_INICIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FEC_FIN", SqlDbType.VarChar, 200)).Value = entidad_param.FEC_FIN;
                    dr = cmd.ExecuteReader();
                    int pos_ID_TIPO_MOVIMIENTO = dr.GetOrdinal("ID_TIPO_MOVIMIENTO");
                    int pos_ID_SUCURSAL = dr.GetOrdinal("ID_SUCURSAL");
                    int pos_FLG_TIPO = dr.GetOrdinal("FLG_TIPO");
                    int pos_DESC_MOVIMIENTO = dr.GetOrdinal("DESC_MOVIMIENTO");
                    int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                    int pos_FECHA_CREACION = dr.GetOrdinal("FECHA_CREACION");
                    int pos_MONTO = dr.GetOrdinal("MONTO");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Caja obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Caja();
                            if (dr.IsDBNull(pos_ID_TIPO_MOVIMIENTO)) obj.ID_TIPO_MOVIMIENTO = 0;
                            else obj.ID_TIPO_MOVIMIENTO = int.Parse(dr[pos_ID_TIPO_MOVIMIENTO].ToString());
                            if (dr.IsDBNull(pos_FLG_TIPO)) obj.FLG_TIPO = 0;
                            else obj.FLG_TIPO = int.Parse(dr[pos_FLG_TIPO].ToString());
                            if (dr.IsDBNull(pos_DESC_MOVIMIENTO)) obj.DESC_MOVIMIENTO = "";
                            else obj.DESC_MOVIMIENTO = dr.GetString(pos_DESC_MOVIMIENTO);
                            if (dr.IsDBNull(pos_MONTO)) obj.MONTO = 0;
                            else obj.MONTO = decimal.Parse(dr[pos_MONTO].ToString());
                            if (dr.IsDBNull(pos_USU_CREACION)) obj.USU_CREACION = "";
                            else obj.USU_CREACION = dr.GetString(pos_USU_CREACION);
                            if (dr.IsDBNull(pos_FECHA_CREACION)) obj.FEC_CREACION = "";
                            else obj.FEC_CREACION = dr.GetString(pos_FECHA_CREACION);


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

        ///*********************************************** Inserta sucursal  *************************************************/

        public void Caja_Movimiento_Insertar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_CAJA_MOVIMIENTO_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad.ID_SUCURSAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_TIPO", SqlDbType.Int)).Value = entidad.FLG_TIPO;
                    //cmd.Parameters.Add(new SqlParameter("@PI_DESC_MOVIMIENTO", SqlDbType.VarChar, 1000)).Value = entidad.DESC_MOVIMIENTO;
                    if (entidad.DESC_MOVIMIENTO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_MOVIMIENTO", SqlDbType.VarChar,1000)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_MOVIMIENTO", SqlDbType.VarChar,1000)).Value = entidad.DESC_MOVIMIENTO; }
                    cmd.Parameters.Add(new SqlParameter("@PI_MONTO", SqlDbType.Decimal)).Value = entidad.MONTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_CREACION", SqlDbType.VarChar, 200)).Value = entidad.USU_CREACION;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                    if (PO_VALIDO == "0")
                    {
                        auditoria.Rechazar("No registrado");
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Actualiza  sucursal  *************************************************/

        public void Caja_Movimiento_Actualizar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_CAJA_MOVIMIENTO_ACTUALIZAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_TIPO_MOVIMIENTO", SqlDbType.Int)).Value = entidad.ID_TIPO_MOVIMIENTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad.ID_SUCURSAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_TIPO", SqlDbType.Int)).Value = entidad.FLG_TIPO;
                    //cmd.Parameters.Add(new SqlParameter("@PI_DESC_MOVIMIENTO", SqlDbType.VarChar, 1000)).Value = entidad.DESC_MOVIMIENTO;
                    if (entidad.DESC_MOVIMIENTO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_MOVIMIENTO", SqlDbType.VarChar,1000)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_MOVIMIENTO", SqlDbType.VarChar, 1000)).Value = entidad.DESC_MOVIMIENTO; }
                    cmd.Parameters.Add(new SqlParameter("@PI_MONTO", SqlDbType.Decimal)).Value = entidad.MONTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_MODIFICACION", SqlDbType.VarChar, 200)).Value = entidad.USU_MODIFICACION;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                    if (PO_VALIDO == "0")
                    {
                        auditoria.Rechazar("No Actualizado");
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Elimina sucursal  *************************************************/

        public void Caja_Movimiento_Eliminar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_CAJA_MOVIMIENTO_ELIMINAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_TIPO_MOVIMIENTO", SqlDbType.Int)).Value = entidad.ID_TIPO_MOVIMIENTO;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                    if (PO_VALIDO == "0")
                    {
                        auditoria.Rechazar("Registro no eliminado");
                    }
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
