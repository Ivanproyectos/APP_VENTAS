using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.Compras;
using Capa_Entidad.Administracion;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos.Compras
{
    public class Cls_Dat_Compras : Protected.DataBaseHelper
    {

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista usuarios paginado *************************************************/

        public List<Cls_Ent_Compras> Compras_Paginado(string ORDEN_COLUMNA, string ORDEN, int FILAS, int PAGINA, string @WHERE, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Compras> lista = new List<Cls_Ent_Compras>();
            using (SqlConnection cn = this.GetNewConnection())
            {
                SqlDataReader dr = null;
                SqlCommand cmd = new SqlCommand("USP_COMPRA_COMPRAS_PAGINACION", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("PO_RESULTADO", SqlDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@PI_PAGINA", SqlDbType.Int)).Value = PAGINA;
                cmd.Parameters.Add(new SqlParameter("@PI_NROREGISTROS", SqlDbType.Int)).Value = FILAS;
                cmd.Parameters.Add(new SqlParameter("@PI_ORDEN_COLUMNA", SqlDbType.VarChar, 100)).Value = ORDEN_COLUMNA;
                cmd.Parameters.Add(new SqlParameter("@PI_ORDEN", SqlDbType.VarChar, 100)).Value = ORDEN;
                cmd.Parameters.Add(new SqlParameter("@PI_WHERE", SqlDbType.VarChar, 1000)).Value = @WHERE;
                cmd.Parameters.Add(new SqlParameter("PO_CUENTA", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                dr = cmd.ExecuteReader();
                int pos_ID_COMPRA = dr.GetOrdinal("ID_COMPRA");
                int pos_COD_COMPROBANTE = dr.GetOrdinal("COD_COMPROBANTE");
                int pos_FEC_COMPROBANTE = dr.GetOrdinal("STR_FECHA_COMPROBANTE");
                int pos_FLG_ESTADO_CREDITO = dr.GetOrdinal("DESC_ESTADO_COMPRA");
                int pos_ID_TIPO_PAGO = dr.GetOrdinal("ID_TIPO_PAGO");
                int pos_FLG_ANULADO = dr.GetOrdinal("FLG_ANULADO");
                int pos_ID_SUCURSAL = dr.GetOrdinal("ID_SUCURSAL");
                int pos_ID_TIPO_COMPROBANTE = dr.GetOrdinal("ID_TIPO_COMPROBANTE");
                int pos_ID_PROVEEDOR = dr.GetOrdinal("ID_PROVEEDOR");
                int pos_PROVEEDOR = dr.GetOrdinal("PROVEEDOR");
                int pos_DESC_TIPO_COMPROBANTE = dr.GetOrdinal("DESC_TIPO_COMPROBANTE");
                int pos_SUB_TOTAL = dr.GetOrdinal("SUB_TOTAL");
                int pos_IGV = dr.GetOrdinal("IGV");
                int pos_DESCUENTO = dr.GetOrdinal("DESCUENTO");
                int pos_TOTAL = dr.GetOrdinal("TOTAL");
                int pos_DETALLE = dr.GetOrdinal("DETALLE");
                int pos_DESC_TIPO_PAGO = dr.GetOrdinal("DESC_TIPO_PAGO");
                int pos_DESC_ESTADO_COMPRA = dr.GetOrdinal("DESC_ESTADO_COMPRA");
                int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                int pos_FEC_CREACION = dr.GetOrdinal("STR_FECHA_COMPRA");
                int pos_USU_MODIFICACION = dr.GetOrdinal("USU_ANULACION");
                int pos_FEC_MODIFICACION = dr.GetOrdinal("STR_FECHA_ANULACION");
                int pos_NRO_OPERACION = dr.GetOrdinal("NRO_OPERACION");


                if (dr.HasRows)
                {
                    Cls_Ent_Compras obj = null;
                    int FILA = 0;
                    while (dr.Read())
                    {
                        obj = new Cls_Ent_Compras();
                        obj.FILA = FILA++;

                        if (dr.IsDBNull(pos_ID_COMPRA)) obj.ID_COMPRA = 0;
                        else obj.ID_COMPRA = int.Parse(dr[pos_ID_COMPRA].ToString());

                        if (dr.IsDBNull(pos_COD_COMPROBANTE)) obj.COD_COMPROBANTE = "";
                        else obj.COD_COMPROBANTE = dr.GetString(pos_COD_COMPROBANTE);

                        if (dr.IsDBNull(pos_FLG_ANULADO)) obj.FLG_ANULADO = 0;
                        else obj.FLG_ANULADO = int.Parse(dr[pos_FLG_ANULADO].ToString());

                        if (dr.IsDBNull(pos_ID_TIPO_PAGO)) obj.ID_TIPO_PAGO = 0;
                        else obj.ID_TIPO_PAGO = int.Parse(dr[pos_ID_TIPO_PAGO].ToString());


                        if (dr.IsDBNull(pos_DESC_TIPO_COMPROBANTE)) obj.DESC_TIPO_COMPROBANTE = "";
                        else obj.DESC_TIPO_COMPROBANTE = dr.GetString(pos_DESC_TIPO_COMPROBANTE);

                        if (dr.IsDBNull(pos_SUB_TOTAL)) obj.SUB_TOTAL = 0;
                        else obj.SUB_TOTAL = decimal.Parse(dr[pos_SUB_TOTAL].ToString());

                        if (dr.IsDBNull(pos_IGV)) obj.IGV = 0;
                        else obj.IGV = decimal.Parse(dr[pos_IGV].ToString());

                        if (dr.IsDBNull(pos_DESCUENTO)) obj.DESCUENTO = 0;
                        else obj.DESCUENTO = decimal.Parse(dr[pos_DESCUENTO].ToString());

                        if (dr.IsDBNull(pos_TOTAL)) obj.TOTAL = 0;
                        else obj.TOTAL = decimal.Parse(dr[pos_TOTAL].ToString());

 
                        if (dr.IsDBNull(pos_DETALLE)) obj.DETALLE = "";
                        else obj.DETALLE = dr.GetString(pos_DETALLE);

                        if (dr.IsDBNull(pos_NRO_OPERACION)) obj.NRO_OPERACION = "";
                        else obj.NRO_OPERACION = dr.GetString(pos_NRO_OPERACION);

                        if (dr.IsDBNull(pos_FEC_COMPROBANTE)) obj.FECHA_COMPROBANTE = "";
                        else obj.FECHA_COMPROBANTE = dr.GetString(pos_FEC_COMPROBANTE);


                        if (dr.IsDBNull(pos_DESC_TIPO_PAGO)) obj.DESC_TIPO_PAGO = "";
                        else obj.DESC_TIPO_PAGO = dr.GetString(pos_DESC_TIPO_PAGO);


                        if (dr.IsDBNull(pos_DESC_ESTADO_COMPRA)) obj.DESC_ESTADO_COMPRA = "";
                        else obj.DESC_ESTADO_COMPRA = dr.GetString(pos_DESC_ESTADO_COMPRA);

                        if (dr.IsDBNull(pos_USU_CREACION)) obj.USU_CREACION = "";
                        else obj.USU_CREACION = dr.GetString(pos_USU_CREACION);
                        if (dr.IsDBNull(pos_FEC_CREACION)) obj.FEC_CREACION = "";
                        else obj.FEC_CREACION = dr.GetString(pos_FEC_CREACION);
                        if (dr.IsDBNull(pos_USU_MODIFICACION)) obj.USU_MODIFICACION = "";
                        else obj.USU_MODIFICACION = dr.GetString(pos_USU_MODIFICACION);
                        if (dr.IsDBNull(pos_FEC_MODIFICACION)) obj.FEC_MODIFICACION = "";
                        else obj.FEC_MODIFICACION = dr.GetString(pos_FEC_MODIFICACION);

                 

                        obj.Proveedor = new Cls_Ent_Proveedor();
                        {
                            if (dr.IsDBNull(pos_PROVEEDOR)) obj.Proveedor.NOMBRES_APE = "";
                            else obj.Proveedor.NOMBRES_APE = dr.GetString(pos_PROVEEDOR);
                        }


                        lista.Add(obj);
                    }
                }
                dr.Close();
                int CUENTA = int.Parse(cmd.Parameters["PO_CUENTA"].Value.ToString());
                auditoria.OBJETO = CUENTA;
            }


            return lista;
        }



        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Inserta COMPRAS  *************************************************/

        public void Compras_Insertar(Cls_Ent_Compras entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {

                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_COMPRA_COMPRAS_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_TIPO_PAGO", SqlDbType.Int)).Value = entidad.ID_TIPO_PAGO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FECHA_COMPROBANTE", SqlDbType.VarChar, 200)).Value = entidad.FECHA_COMPROBANTE;
                    cmd.Parameters.Add(new SqlParameter("@PI_COD_COMPROBANTE", SqlDbType.VarChar, 200)).Value = entidad.COD_COMPROBANTE;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PROVEEDOR", SqlDbType.Int)).Value = entidad.ID_PROVEEDOR;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad.ID_SUCURSAL;
                    //cmd.Parameters.Add(new SqlParameter("@PI_NRO_OPERACION", SqlDbType.Int)).Value = entidad.NRO_OPERACION;
                    if (entidad.NRO_OPERACION == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_NRO_OPERACION", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_NRO_OPERACION", SqlDbType.VarChar, 100)).Value = entidad.NRO_OPERACION; }
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_TIPO_COMPROBANTE", SqlDbType.VarChar, 100)).Value = entidad.ID_TIPO_COMPROBANTE;
                    cmd.Parameters.Add(new SqlParameter("@PI_SUB_TOTAL", SqlDbType.Decimal)).Value = entidad.SUB_TOTAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_IGV ", SqlDbType.Decimal)).Value = entidad.IGV;
                    cmd.Parameters.Add(new SqlParameter("@PI_DESCUENTO", SqlDbType.Decimal)).Value = entidad.DESCUENTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_TOTAL", SqlDbType.Decimal)).Value = entidad.TOTAL;
                    if (entidad.DETALLE == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = entidad.DETALLE; }

                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_CREACION", SqlDbType.VarChar, 200)).Value = entidad.USU_CREACION;
                    cmd.Parameters.Add(new SqlParameter("PO_ID_COMPRA", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_MENSAJE", SqlDbType.VarChar, 200)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_ID_COMPRA = cmd.Parameters["PO_ID_COMPRA"].Value.ToString();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                    string PO_MENSAJE = cmd.Parameters["PO_MENSAJE"].Value.ToString();
                    if (PO_VALIDO == "0")
                    {
                        auditoria.Rechazar(PO_MENSAJE);
                    }
                    else
                    {
                        auditoria.OBJETO = PO_ID_COMPRA;
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

        ///*********************************************** Inserta COMPRAS  *************************************************/

        public void Compras_Detalle_Insertar(Cls_Ent_Compras_Detalle entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_COMPRA_COMPRAS_DETALLE_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PRODUCTO", SqlDbType.Int)).Value = entidad.ID_PRODUCTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_COMPRA", SqlDbType.Int)).Value = entidad.ID_COMPRA;
                    cmd.Parameters.Add(new SqlParameter("@PI_PRECIO", SqlDbType.Decimal)).Value = entidad.PRECIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_CANTIDAD", SqlDbType.Int)).Value = entidad.CANTIDAD;
                    cmd.Parameters.Add(new SqlParameter("@PI_IMPORTE", SqlDbType.Decimal)).Value = entidad.IMPORTE;
                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_CREACION", SqlDbType.VarChar, 200)).Value = entidad.USU_CREACION;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    //if (PO_VALIDO == "0")
                    //{
                    //    auditoria.Rechazar(PO_MENSAJE);
                    //}

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Inserta COMPRAS  *************************************************/

        public void Compras_AnularVenta(Cls_Ent_Compras entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_COMPRA_COMPRAS_ANULAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_COMPRA", SqlDbType.Int)).Value = entidad.ID_COMPRA;
                    cmd.Parameters.Add(new SqlParameter("@PI_USU_MODIFICACION", SqlDbType.VarChar, 200)).Value = entidad.USU_MODIFICACION;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_MENSAJE", SqlDbType.VarChar, 200)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                    string PO_MENSAJE = cmd.Parameters["PO_MENSAJE"].Value.ToString();
                    if (PO_VALIDO == "0")
                    {
                        auditoria.Rechazar(PO_MENSAJE);
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

        ///*********************************************** Lista ventas detalle *************************************************/

        public List<Cls_Ent_Compras_Detalle> Compras_Detallecompras_Listar(Cls_Ent_Compras_Detalle entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Compras_Detalle> Lista = new List<Cls_Ent_Compras_Detalle>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_COMPRA_COMPRAS_DETALLE_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_COMPRA", SqlDbType.BigInt)).Value = entidad_param.ID_COMPRA;
                    dr = cmd.ExecuteReader();
                    int pos_ID_COMPRA_DETALLE = dr.GetOrdinal("ID_COMPRA_DETALLE");
                    int pos_ID_PRODUCTO = dr.GetOrdinal("ID_PRODUCTO");
                    int pos_DESC_PRODUCTO = dr.GetOrdinal("DESC_PRODUCTO");
                    int pos_PRECIO = dr.GetOrdinal("PRECIO");
                    int pos_CANTIDAD = dr.GetOrdinal("CANTIDAD");
                    int pos_IMPORTE = dr.GetOrdinal("IMPORTE");

                    int pos_COD_UNIDAD_MEDIDA = dr.GetOrdinal("COD_UNIDAD_MEDIDA");
                    int pos_ID_UNIDAD_MEDIDA = dr.GetOrdinal("ID_UNIDAD_MEDIDA");

                    if (dr.HasRows)
                    {
                        Cls_Ent_Compras_Detalle obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Compras_Detalle();
                            if (dr.IsDBNull(pos_ID_COMPRA_DETALLE)) obj.ID_COMPRA_DETALLE = 0;
                            else obj.ID_COMPRA_DETALLE = int.Parse(dr[pos_ID_COMPRA_DETALLE].ToString());
                            if (dr.IsDBNull(pos_ID_PRODUCTO)) obj.ID_PRODUCTO = 0;
                            else obj.ID_PRODUCTO = int.Parse(dr[pos_ID_PRODUCTO].ToString());
                            if (dr.IsDBNull(pos_DESC_PRODUCTO)) obj.DESC_PRODUCTO = "";
                            else obj.DESC_PRODUCTO = dr.GetString(pos_DESC_PRODUCTO);
                            if (dr.IsDBNull(pos_PRECIO)) obj.PRECIO = 0;
                            else obj.PRECIO = decimal.Parse(dr[pos_PRECIO].ToString());
                            if (dr.IsDBNull(pos_CANTIDAD)) obj.CANTIDAD = 0;
                            else obj.CANTIDAD = int.Parse(dr[pos_CANTIDAD].ToString());
                            if (dr.IsDBNull(pos_IMPORTE)) obj.IMPORTE = 0;
                            else obj.IMPORTE = decimal.Parse(dr[pos_IMPORTE].ToString());
                            if (dr.IsDBNull(pos_COD_UNIDAD_MEDIDA)) obj.COD_UNIDAD_MEDIDA = "";
                            else obj.COD_UNIDAD_MEDIDA = dr.GetString(pos_COD_UNIDAD_MEDIDA);
                            if (dr.IsDBNull(pos_ID_UNIDAD_MEDIDA)) obj.ID_UNIDAD_MEDIDA = 0;
                            else obj.ID_UNIDAD_MEDIDA = int.Parse(dr[pos_ID_UNIDAD_MEDIDA].ToString());

                            Lista.Add(obj);

                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return Lista;
        }




        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** listar venta uno  *************************************************/
        public Cls_Ent_Compras Compras_Listar_Uno(Cls_Ent_Compras entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            Cls_Ent_Compras obj = new Cls_Ent_Compras();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_COMPRA_COMPRAS_LISTAR_UNO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_COMPRA", SqlDbType.BigInt)).Value = entidad_param.ID_COMPRA;
                    dr = cmd.ExecuteReader();
                    int pos_ID_COMPRA = dr.GetOrdinal("ID_COMPRA");
                    int pos_COD_COMPROBANTE = dr.GetOrdinal("COD_COMPROBANTE");
                    int pos_FECHA_COMPROBANTE = dr.GetOrdinal("FECHA_COMPROBANTE");
                    int pos_SUB_TOTAL = dr.GetOrdinal("SUB_TOTAL");
                    int pos_IGV = dr.GetOrdinal("IGV");
                    int pos_DESCUENTO = dr.GetOrdinal("DESCUENTO");
                    int pos_TOTAL = dr.GetOrdinal("TOTAL");

                    if (dr.HasRows)
                    {
                        //Cls_Ent_Cliente obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Compras();
                            if (dr.IsDBNull(pos_ID_COMPRA)) obj.ID_COMPRA = 0;
                            else obj.ID_COMPRA = int.Parse(dr[pos_ID_COMPRA].ToString());

                            if (dr.IsDBNull(pos_COD_COMPROBANTE)) obj.COD_COMPROBANTE = "";
                            else obj.COD_COMPROBANTE = dr.GetString(pos_COD_COMPROBANTE);

                            if (dr.IsDBNull(pos_SUB_TOTAL)) obj.SUB_TOTAL = 0;
                            else obj.SUB_TOTAL = decimal.Parse(dr[pos_SUB_TOTAL].ToString());

                            if (dr.IsDBNull(pos_IGV)) obj.IGV = 0;
                            else obj.IGV = decimal.Parse(dr[pos_IGV].ToString());

                            if (dr.IsDBNull(pos_DESCUENTO)) obj.DESCUENTO = 0;
                            else obj.DESCUENTO = decimal.Parse(dr[pos_DESCUENTO].ToString());

                            if (dr.IsDBNull(pos_TOTAL)) obj.TOTAL = 0;
                            else obj.TOTAL = decimal.Parse(dr[pos_TOTAL].ToString());




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






    }
}
