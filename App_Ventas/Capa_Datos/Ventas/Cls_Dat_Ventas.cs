using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.Ventas;
using Capa_Entidad.Administracion;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos.Ventas
{
    public class Cls_Dat_Ventas : Protected.DataBaseHelper
    {
       
        

            
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista usuarios paginado *************************************************/

        public List<Cls_Ent_Ventas> Ventas_Paginado(string ORDEN_COLUMNA, string ORDEN, int FILAS, int PAGINA, string @WHERE, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Ventas> lista = new List<Cls_Ent_Ventas>();
            using (SqlConnection cn = this.GetNewConnection())
            {
                string TABLA = "";
                SqlDataReader dr = null;
                SqlCommand cmd = new SqlCommand("USP_VENTA_VENTAS_PAGINACION", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("PO_RESULTADO", SqlDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@PI_PAGINA", SqlDbType.Int)).Value = PAGINA;
                cmd.Parameters.Add(new SqlParameter("@PI_NROREGISTROS", SqlDbType.Int)).Value = FILAS;
                cmd.Parameters.Add(new SqlParameter("@PI_ORDEN_COLUMNA", SqlDbType.VarChar, 100)).Value = ORDEN_COLUMNA;
                cmd.Parameters.Add(new SqlParameter("@PI_ORDEN", SqlDbType.VarChar, 100)).Value = ORDEN;
                cmd.Parameters.Add(new SqlParameter("@PI_WHERE", SqlDbType.VarChar, 1000)).Value = @WHERE;
                cmd.Parameters.Add(new SqlParameter("@PI_TABLA", SqlDbType.VarChar, 100)).Value = TABLA;
                cmd.Parameters.Add(new SqlParameter("PO_CUENTA", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                dr = cmd.ExecuteReader();
                int pos_ID_VENTA = dr.GetOrdinal("ID_VENTA");
                int pos_COD_COMPROBANTE = dr.GetOrdinal("COD_COMPROBANTE");
                int pos_FLG_ANULADO = dr.GetOrdinal("FLG_ANULADO");
                int pos_FLG_ESTADO_CREDITO = dr.GetOrdinal("FLG_ESTADO_CREDITO");
                int pos_FLG_TIPO_PAGO = dr.GetOrdinal("FLG_TIPO_PAGO");
                //int pos_FECHA_VENTA = dr.GetOrdinal("STR_FECHA_VENTA");
                int pos_ID_CLIENTE = dr.GetOrdinal("ID_CLIENTE");
                int pos_ID_SUCURSAL = dr.GetOrdinal("ID_SUCURSAL");
                int pos_ID_TIPO_COMPROBANTE = dr.GetOrdinal("ID_TIPO_COMPROBANTE");
                int pos_CLIENTE = dr.GetOrdinal("CLIENTE");
                int pos_DESC_TIPO_COMPROBANTE = dr.GetOrdinal("DESC_TIPO_COMPROBANTE");
                int pos_SUB_TOTAL = dr.GetOrdinal("SUB_TOTAL");
                int pos_IGV = dr.GetOrdinal("IGV");
                int pos_DESCUENTO = dr.GetOrdinal("DESCUENTO");
                int pos_TOTAL = dr.GetOrdinal("TOTAL");
                int pos_DETALLE = dr.GetOrdinal("DETALLE");
                int pos_DEBE = dr.GetOrdinal("DEBE");
                int pos_ADELANTO = dr.GetOrdinal("ADELANTO");               
                int pos_DESC_TIPO_VENTA = dr.GetOrdinal("DESC_TIPO_VENTA");
                int pos_DESC_ESTADO_CREDITO = dr.GetOrdinal("DESC_ESTADO_CREDITO");
                int pos_DESC_ESTADO_VENTA = dr.GetOrdinal("DESC_ESTADO_VENTA");
                int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                int pos_FEC_CREACION = dr.GetOrdinal("STR_FECHA_VENTA");
                int pos_USU_MODIFICACION = dr.GetOrdinal("USU_MODIFICACION");
                int pos_FEC_MODIFICACION = dr.GetOrdinal("STR_FECHA_MODIFICACION");
                int pos_NRO_OPERACION = dr.GetOrdinal("NRO_OPERACION");
                int pos_STR_FECHA_CREDITO_CANCELADO = dr.GetOrdinal("STR_FECHA_CREDITO_CANCELADO");
                
     
                if (dr.HasRows)
                {
                    Cls_Ent_Ventas obj = null;
                    int FILA = 0;
                    while (dr.Read())
                    {
                        obj = new Cls_Ent_Ventas();
                        obj.FILA = FILA++;

                        if (dr.IsDBNull(pos_ID_VENTA)) obj.ID_VENTA = 0;
                        else obj.ID_VENTA = int.Parse(dr[pos_ID_VENTA].ToString());

                        if (dr.IsDBNull(pos_COD_COMPROBANTE)) obj.COD_COMPROBANTE = "";
                        else obj.COD_COMPROBANTE = dr.GetString(pos_COD_COMPROBANTE);

                        if (dr.IsDBNull(pos_FLG_ANULADO)) obj.FLG_ANULADO = 0;
                        else obj.FLG_ANULADO = int.Parse(dr[pos_FLG_ANULADO].ToString());

                        if (dr.IsDBNull(pos_FLG_TIPO_PAGO)) obj.FLG_TIPO_PAGO = 0;
                        else obj.FLG_TIPO_PAGO = int.Parse(dr[pos_FLG_TIPO_PAGO].ToString());


                        //if (dr.IsDBNull(pos_FECHA_VENTA)) obj.FECHA_VENTA = "";
                        //else obj.FECHA_VENTA = dr.GetString(pos_FECHA_VENTA);

                        if (dr.IsDBNull(pos_FLG_ESTADO_CREDITO)) obj.FLG_ESTADO_CREDITO = 0;
                        else obj.FLG_ESTADO_CREDITO = int.Parse(dr[pos_FLG_ESTADO_CREDITO].ToString());

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

                        if (dr.IsDBNull(pos_DEBE)) obj.DEBE = 0;
                        else obj.DEBE = decimal.Parse(dr[pos_DEBE].ToString());

                        if (dr.IsDBNull(pos_ADELANTO)) obj.ADELANTO = 0;
                        else obj.ADELANTO = decimal.Parse(dr[pos_ADELANTO].ToString());


                        if (dr.IsDBNull(pos_DETALLE)) obj.DETALLE = "";
                        else obj.DETALLE = dr.GetString(pos_DETALLE);

                        if (dr.IsDBNull(pos_NRO_OPERACION)) obj.NRO_OPERACION = "";
                        else obj.NRO_OPERACION = dr.GetString(pos_NRO_OPERACION);


                        if (dr.IsDBNull(pos_DESC_TIPO_VENTA)) obj.DESC_TIPO_VENTA = "";
                        else obj.DESC_TIPO_VENTA = dr.GetString(pos_DESC_TIPO_VENTA);

                        if (dr.IsDBNull(pos_DESC_ESTADO_CREDITO)) obj.DESC_ESTADO_CREDITO = "";
                        else obj.DESC_ESTADO_CREDITO = dr.GetString(pos_DESC_ESTADO_CREDITO);

                        if (dr.IsDBNull(pos_DESC_ESTADO_VENTA)) obj.DESC_ESTADO_VENTA = "";
                        else obj.DESC_ESTADO_VENTA = dr.GetString(pos_DESC_ESTADO_VENTA);

                        if (dr.IsDBNull(pos_USU_CREACION)) obj.USU_CREACION = "";
                        else obj.USU_CREACION = dr.GetString(pos_USU_CREACION);
                        if (dr.IsDBNull(pos_FEC_CREACION)) obj.FEC_CREACION = "";
                        else obj.FEC_CREACION = dr.GetString(pos_FEC_CREACION);
                        if (dr.IsDBNull(pos_USU_MODIFICACION)) obj.USU_MODIFICACION = "";
                        else obj.USU_MODIFICACION = dr.GetString(pos_USU_MODIFICACION);
                        if (dr.IsDBNull(pos_FEC_MODIFICACION)) obj.FEC_MODIFICACION = "";
                        else obj.FEC_MODIFICACION = dr.GetString(pos_FEC_MODIFICACION);

                        if (dr.IsDBNull(pos_STR_FECHA_CREDITO_CANCELADO)) obj.STR_FECHA_CREDITO_CANCELADO = "";
                        else obj.STR_FECHA_CREDITO_CANCELADO = dr.GetString(pos_STR_FECHA_CREDITO_CANCELADO);

                        obj.Cliente = new Cls_Ent_Cliente();
                        {
                            if (dr.IsDBNull(pos_CLIENTE)) obj.Cliente.NOMBRES_APE = "";
                            else obj.Cliente.NOMBRES_APE = dr.GetString(pos_CLIENTE);
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

        ///*********************************************** Inserta VENTAS  *************************************************/

        public void Ventas_Insertar(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
  
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_VENTA_VENTAS_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_TIPO_PAGO", SqlDbType.Int)).Value = entidad.FLG_TIPO_PAGO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FECHA_VENTA", SqlDbType.VarChar, 200)).Value = entidad.FECHA_VENTA;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_CLIENTE", SqlDbType.Int)).Value = entidad.ID_CLIENTE;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad.ID_SUCURSAL;
                    //cmd.Parameters.Add(new SqlParameter("@PI_NRO_OPERACION", SqlDbType.Int)).Value = entidad.NRO_OPERACION;
                    if (entidad.NRO_OPERACION == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_NRO_OPERACION", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_NRO_OPERACION", SqlDbType.VarChar, 100)).Value = entidad.NRO_OPERACION; }
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_TIPO_COMPROBANTE", SqlDbType.VarChar,100)).Value = entidad.ID_TIPO_COMPROBANTE;
                    cmd.Parameters.Add(new SqlParameter("@PI_SUB_TOTAL", SqlDbType.Decimal)).Value = entidad.SUB_TOTAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_IGV ", SqlDbType.Decimal)).Value = entidad.IGV;
                    cmd.Parameters.Add(new SqlParameter("@PI_DESCUENTO", SqlDbType.Decimal)).Value = entidad.DESCUENTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_TOTAL", SqlDbType.Decimal)).Value = entidad.TOTAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_ADELANTO", SqlDbType.Decimal)).Value = entidad.ADELANTO;
                    //cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = entidad.DETALLE;
                    if (entidad.DETALLE == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = entidad.DETALLE; }

                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_CREACION", SqlDbType.VarChar, 200)).Value = entidad.USU_CREACION;
                    cmd.Parameters.Add(new SqlParameter("PO_CODIGO_COMPROBANTE", SqlDbType.VarChar, 200)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_ID_VENTA", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_MENSAJE", SqlDbType.VarChar, 200)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_CODIGO_COMPROBANTE = cmd.Parameters["PO_CODIGO_COMPROBANTE"].Value.ToString();
                    string PO_ID_VENTA = cmd.Parameters["PO_ID_VENTA"].Value.ToString();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                    string PO_MENSAJE = cmd.Parameters["PO_MENSAJE"].Value.ToString();
                    if (PO_VALIDO == "0")
                    {
                        auditoria.Rechazar(PO_MENSAJE);
                    }
                    else {
                        auditoria.OBJETO = PO_ID_VENTA;
                        auditoria.OBJETO2 = PO_CODIGO_COMPROBANTE; 
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

        ///*********************************************** Inserta VENTAS  *************************************************/

        public void Ventas_ActualizarVenta_Credito(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
  
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_VENTA_VENTAS_CREDITO_ACTUALIZAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_VENTA", SqlDbType.Int)).Value = entidad.ID_VENTA_CREDITO;
                    cmd.Parameters.Add(new SqlParameter("@PI_IGV", SqlDbType.Int)).Value = entidad.IGV;
                    cmd.Parameters.Add(new SqlParameter("@PI_DESCUENTO", SqlDbType.Int)).Value = entidad.DESCUENTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_TOTAL", SqlDbType.Decimal)).Value = entidad.TOTAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_ADELANTO", SqlDbType.Decimal)).Value = entidad.ADELANTO;
                    //cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar,1000)).Value = entidad.DETALLE;
                    if (entidad.DETALLE == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = entidad.DETALLE; }

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

        ///*********************************************** Inserta VENTAS  *************************************************/

        public void Ventas_Detalle_Insertar(Cls_Ent_Ventas_Detalle entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
               using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_VENTA_VENTAS_DETALLE_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PRODUCTO", SqlDbType.Int)).Value = entidad.ID_PRODUCTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_VENTA", SqlDbType.Int)).Value = entidad.ID_VENTA;
                    cmd.Parameters.Add(new SqlParameter("@PI_PRECIO", SqlDbType.Decimal)).Value = entidad.PRECIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_CANTIDAD", SqlDbType.Int)).Value = entidad.CANTIDAD;
                    cmd.Parameters.Add(new SqlParameter("@PI_IMPORTE", SqlDbType.Decimal)).Value = entidad.IMPORTE;
                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_CREACION", SqlDbType.VarChar, 200)).Value = entidad.USU_CREACION;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_TIPO_COMPROBANTE", SqlDbType.VarChar, 200)).Value = entidad.ID_TIPO_COMPROBANTE;
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

        ///*********************************************** Inserta VENTAS  *************************************************/

        public void Ventas_AnularVenta(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
               using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_VENTA_VENTAS_ANULAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_VENTA", SqlDbType.Int)).Value = entidad.ID_VENTA;
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

        public List<Cls_Ent_Ventas_Detalle> Ventas_Detalleventas_Listar(Cls_Ent_Ventas_Detalle entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
           List<Cls_Ent_Ventas_Detalle> Lista = new  List<Cls_Ent_Ventas_Detalle>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_VENTA_VENTAS_DETALLE_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_VENTA", SqlDbType.BigInt)).Value = entidad_param.ID_VENTA;
                    dr = cmd.ExecuteReader();
                    int pos_ID_VENTA_DETALLE = dr.GetOrdinal("ID_VENTA_DETALLE");
                    int pos_ID_PRODUCTO = dr.GetOrdinal("ID_PRODUCTO");
                    int pos_DESC_PRODUCTO = dr.GetOrdinal("DESC_PRODUCTO");
                    int pos_PRECIO = dr.GetOrdinal("PRECIO");
                    int pos_CANTIDAD = dr.GetOrdinal("CANTIDAD");
                    int pos_IMPORTE = dr.GetOrdinal("IMPORTE");
                    int pos_FLG_DEVUELTO = dr.GetOrdinal("FLG_DEVUELTO");
                    int pos_COD_UNIDAD_MEDIDA = dr.GetOrdinal("COD_UNIDAD_MEDIDA");
                    int pos_ID_UNIDAD_MEDIDA = dr.GetOrdinal("ID_UNIDAD_MEDIDA");
                    
                    if (dr.HasRows)
                    {
                        Cls_Ent_Ventas_Detalle obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Ventas_Detalle();
                            if (dr.IsDBNull(pos_ID_VENTA_DETALLE)) obj.ID_VENTA_DETALLE = 0;
                            else obj.ID_VENTA_DETALLE = int.Parse(dr[pos_ID_VENTA_DETALLE].ToString());

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

                            if (dr.IsDBNull(pos_FLG_DEVUELTO)) obj.FLG_DEVUELTO = 0;
                            else obj.FLG_DEVUELTO = int.Parse(dr[pos_FLG_DEVUELTO].ToString());

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

        ///*********************************************** DEVOLER PRODUCTO  *************************************************/

        public void Ventas_Detalle_DevolverProducto(Cls_Ent_Ventas_Detalle entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
               using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_VENTA_VENTAS_DEVOLVER_PRODUCTO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_VENTA_DETALLE", SqlDbType.Int)).Value = entidad.ID_VENTA_DETALLE;
                    cmd.Parameters.Add(new SqlParameter("@PI_CANTIDAD", SqlDbType.Int)).Value = entidad.CANTIDAD;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_DEVOLUCION", SqlDbType.VarChar, 1000)).Value = entidad.ID_DEVOLUCION;
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

        ///*********************************************** listar venta uno  *************************************************/
        public Cls_Ent_Ventas Ventas_Listar_Uno(Cls_Ent_Ventas entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            Cls_Ent_Ventas obj = new Cls_Ent_Ventas();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_VENTA_VENTA_LISTAR_UNO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_VENTA", SqlDbType.BigInt)).Value = entidad_param.ID_VENTA;
                    dr = cmd.ExecuteReader();
                    int pos_ID_VENTA = dr.GetOrdinal("ID_VENTA");
                    int pos_COD_COMPROBANTE = dr.GetOrdinal("COD_COMPROBANTE");
                    int pos_FLG_ANULADO = dr.GetOrdinal("FLG_ANULADO");
                    int pos_FLG_TIPO_PAGO = dr.GetOrdinal("FLG_TIPO_PAGO");
                    int pos_ID_CLIENTE = dr.GetOrdinal("ID_CLIENTE");
                    int pos_ID_TIPO_COMPROBANTE = dr.GetOrdinal("ID_TIPO_COMPROBANTE");
                    int pos_SUB_TOTAL = dr.GetOrdinal("SUB_TOTAL");
                    int pos_IGV = dr.GetOrdinal("IGV");
                    int pos_DESCUENTO = dr.GetOrdinal("DESCUENTO");
                    int pos_TOTAL = dr.GetOrdinal("TOTAL");
                    int pos_DEBE = dr.GetOrdinal("DEBE");
                    int pos_DETALLE = dr.GetOrdinal("DETALLE");
                    int pos_ADELANTO = dr.GetOrdinal("ADELANTO");
                    int pos_FLG_ESTADO_CREDITO = dr.GetOrdinal("FLG_ESTADO_CREDITO");
                    int pos_FECHA_CREACION = dr.GetOrdinal("FECHA_CREACION");
                    int pos_CLIENTE = dr.GetOrdinal("CLIENTE");
                    int pos_DOCUMENTO_CLIENTE = dr.GetOrdinal("DOCUMENTO_CLIENTE");
                    int pos_ID_SUCURSAL = dr.GetOrdinal("ID_SUCURSAL");
                    int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                    int pos_ID_TIPO_DOCUMENTO_CLIENTE = dr.GetOrdinal("ID_TIPO_DOCUMENTO_CLIENTE");
                    int pos_DIRECCION_CLIENTE = dr.GetOrdinal("DIRECCION_CLIENTE");

                    if (dr.HasRows)
                    {
                        //Cls_Ent_Cliente obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Ventas();
                            if (dr.IsDBNull(pos_ID_VENTA)) obj.ID_VENTA = 0;
                            else obj.ID_VENTA = int.Parse(dr[pos_ID_VENTA].ToString());


                            if (dr.IsDBNull(pos_COD_COMPROBANTE)) obj.COD_COMPROBANTE = "";
                            else obj.COD_COMPROBANTE = dr.GetString(pos_COD_COMPROBANTE);

                            if (dr.IsDBNull(pos_FLG_ANULADO)) obj.FLG_ANULADO = 0;
                            else obj.FLG_ANULADO = int.Parse(dr[pos_FLG_ANULADO].ToString());

                            if (dr.IsDBNull(pos_FLG_TIPO_PAGO)) obj.FLG_TIPO_PAGO = 0;
                            else obj.FLG_TIPO_PAGO = int.Parse(dr[pos_FLG_TIPO_PAGO].ToString());

                            if (dr.IsDBNull(pos_ID_CLIENTE)) obj.ID_CLIENTE = 0;
                            else obj.ID_CLIENTE = int.Parse(dr[pos_ID_CLIENTE].ToString());


                            if (dr.IsDBNull(pos_ID_TIPO_COMPROBANTE)) obj.ID_TIPO_COMPROBANTE = "";
                            else obj.ID_TIPO_COMPROBANTE = dr.GetString(pos_ID_TIPO_COMPROBANTE);

                            if (dr.IsDBNull(pos_SUB_TOTAL)) obj.SUB_TOTAL = 0;
                            else obj.SUB_TOTAL = decimal.Parse(dr[pos_SUB_TOTAL].ToString());

                            if (dr.IsDBNull(pos_IGV)) obj.IGV = 0;
                            else obj.IGV = decimal.Parse(dr[pos_IGV].ToString());

                            if (dr.IsDBNull(pos_DESCUENTO)) obj.DESCUENTO = 0;
                            else obj.DESCUENTO = decimal.Parse(dr[pos_DESCUENTO].ToString());

                            if (dr.IsDBNull(pos_TOTAL)) obj.TOTAL = 0;
                            else obj.TOTAL = decimal.Parse(dr[pos_TOTAL].ToString());

                            if (dr.IsDBNull(pos_ADELANTO)) obj.ADELANTO = 0;
                            else obj.ADELANTO = decimal.Parse(dr[pos_ADELANTO].ToString());

                            if (dr.IsDBNull(pos_DEBE)) obj.DEBE = 0;
                            else obj.DEBE = decimal.Parse(dr[pos_DEBE].ToString());

                            if (dr.IsDBNull(pos_DETALLE)) obj.DETALLE = "";
                            else obj.DETALLE = dr.GetString(pos_DETALLE);

                            if (dr.IsDBNull(pos_FLG_ESTADO_CREDITO)) obj.FLG_ESTADO_CREDITO = 0;
                            else obj.FLG_ESTADO_CREDITO = int.Parse(dr[pos_FLG_ESTADO_CREDITO].ToString());

                            if (dr.IsDBNull(pos_FECHA_CREACION)) obj.FEC_CREACION = "";
                            else obj.FEC_CREACION = dr.GetString(pos_FECHA_CREACION);


                            if (dr.IsDBNull(pos_ID_SUCURSAL)) obj.ID_SUCURSAL = 0;
                            else obj.ID_SUCURSAL = int.Parse(dr[pos_ID_SUCURSAL].ToString());

                            if (dr.IsDBNull(pos_USU_CREACION)) obj.USU_CREACION = "";
                            else obj.USU_CREACION = dr.GetString(pos_USU_CREACION);

                 
                            obj.Cliente = new Cls_Ent_Cliente();
                            {
                                if (dr.IsDBNull(pos_DIRECCION_CLIENTE)) obj.Cliente.DIRECCION = "";
                                else obj.Cliente.DIRECCION = dr.GetString(pos_DIRECCION_CLIENTE);

                                if (dr.IsDBNull(pos_CLIENTE)) obj.Cliente.NOMBRES_APE = "";
                                else obj.Cliente.NOMBRES_APE = dr.GetString(pos_CLIENTE);

                                if (dr.IsDBNull(pos_DOCUMENTO_CLIENTE)) obj.Cliente.NUMERO_DOCUMENTO = "";
                                else obj.Cliente.NUMERO_DOCUMENTO = dr.GetString(pos_DOCUMENTO_CLIENTE);

                                if (dr.IsDBNull(pos_ID_TIPO_DOCUMENTO_CLIENTE)) obj.Cliente.ID_TIPO_DOCUMENTO = 0;
                                else obj.Cliente.ID_TIPO_DOCUMENTO = int.Parse(dr[pos_ID_TIPO_DOCUMENTO_CLIENTE].ToString());


                            }

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

        ///*********************************************** validar  *************************************************/

        public void Ventas_ValidarCliente_Credito(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
               using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_VENTA_VENTAS_VALIDARCLIENTE_CREDITO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_CLIENTE", SqlDbType.Int)).Value = entidad.ID_CLIENTE;
                    cmd.Parameters.Add(new SqlParameter("@PI_SUCURSAL", SqlDbType.Int)).Value = entidad.ID_SUCURSAL;
                    cmd.Parameters.Add(new SqlParameter("PO_ID_VENTA", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                    string PO_ID_VENTA = cmd.Parameters["PO_ID_VENTA"].Value.ToString();

                    if (PO_VALIDO == "1")
                    {
                        auditoria.OBJETO = PO_ID_VENTA;
                    }
                    else {
                        auditoria.Rechazar("Cliente sin credito pendiente"); 
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
