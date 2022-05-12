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

namespace Capa_Datos.Inventario
{
    public class Cls_Dat_Producto : Protected.DataBaseHelper
    {

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista productos paginado *************************************************/

        public List<Cls_Ent_Producto> Productos_Paginado(string ORDEN_COLUMNA, string ORDEN, int FILAS, int START, string @WHERE, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Producto> lista = new List<Cls_Ent_Producto>();
            using (SqlConnection cn = this.GetNewConnection())
            {
                string TABLA = "";
                SqlDataReader dr = null;
                SqlCommand cmd = new SqlCommand("USP_INVEN_PRODUCTO_PAGINACION", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PI_NROREGISTROS", SqlDbType.Int)).Value = FILAS;
                cmd.Parameters.Add(new SqlParameter("@PI_START", SqlDbType.Int)).Value = START;
                cmd.Parameters.Add(new SqlParameter("@PI_ORDEN_COLUMNA", SqlDbType.VarChar, 100)).Value = ORDEN_COLUMNA;
                cmd.Parameters.Add(new SqlParameter("@PI_ORDEN", SqlDbType.VarChar, 100)).Value = ORDEN;
                cmd.Parameters.Add(new SqlParameter("@PI_WHERE", SqlDbType.VarChar, 1000)).Value = @WHERE;
                cmd.Parameters.Add(new SqlParameter("PO_CUENTA", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                dr = cmd.ExecuteReader();
                int pos_ID_PRODUCTO = dr.GetOrdinal("ID_PRODUCTO");
                int pos_DESC_PRODUCTO = dr.GetOrdinal("DESC_PRODUCTO");
                int pos_COD_PRODUCTO = dr.GetOrdinal("COD_PRODUCTO");
                int pos_DESC_UNIDAD_MEDIDA = dr.GetOrdinal("DESC_UNIDAD_MEDIDA");
                int pos_ID_UNIDAD_MEDIDA = dr.GetOrdinal("ID_UNIDAD_MEDIDA");
                int pos_DESC_CATEGORIA = dr.GetOrdinal("DESC_CATEGORIA");
                int pos_PRECIO_COMPRA = dr.GetOrdinal("PRECIO_COMPRA");
                int pos_PRECIO_VENTA = dr.GetOrdinal("PRECIO_VENTA");
                int pos_STOCK = dr.GetOrdinal("STOCK");
                int pos_STOCK_MINIMO = dr.GetOrdinal("STOCK_MINIMO");
                int pos_FLG_SERIVICIO = dr.GetOrdinal("FLG_SERIVICIO");
                int pos_FLG_VENCE = dr.GetOrdinal("FLG_VENCE");
                int pos_FECHA_VENCIMIENTO = dr.GetOrdinal("FECHA_VENCIMIENTO");
                int pos_MARCA = dr.GetOrdinal("MARCA");
                int pos_MODELO = dr.GetOrdinal("MODELO");
                int pos_DETALLE = dr.GetOrdinal("DETALLE");
                int pos_COD_ARCHIVO = dr.GetOrdinal("COD_ARCHIVO");
                int pos_NOMBRE_ARCHIVO = dr.GetOrdinal("NOMBRE_ARCHIVO");
                int pos_EXTENSION = dr.GetOrdinal("EXTENSION");
                int pos_FLG_ESTADO = dr.GetOrdinal("FLG_ESTADO");
                int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                int pos_FECHA_CREACION = dr.GetOrdinal("FECHA_CREACION");
                int pos_USU_MODIFICACION = dr.GetOrdinal("USU_MODIFICACION");
                int pos_FEC_MODIFICACION = dr.GetOrdinal("FECHA_MODIFICACION");


                if (dr.HasRows)
                {
                    Cls_Ent_Producto obj = null;
                    int FILA = START + 1;
                    while (dr.Read())
                    {
                        obj = new Cls_Ent_Producto();
                        obj.FILA = FILA++; 

                        if (dr.IsDBNull(pos_ID_PRODUCTO)) obj.ID_PRODUCTO = 0;
                        else obj.ID_PRODUCTO = int.Parse(dr[pos_ID_PRODUCTO].ToString());
                        if (dr.IsDBNull(pos_DESC_PRODUCTO)) obj.DESC_PRODUCTO = "";
                        else obj.DESC_PRODUCTO = dr.GetString(pos_DESC_PRODUCTO);

                        if (dr.IsDBNull(pos_COD_PRODUCTO)) obj.COD_PRODUCTO = "";
                        else obj.COD_PRODUCTO = dr.GetString(pos_COD_PRODUCTO);

                        if (dr.IsDBNull(pos_DESC_UNIDAD_MEDIDA)) obj.DESC_UNIDAD_MEDIDA = "";
                        else obj.DESC_UNIDAD_MEDIDA = dr.GetString(pos_DESC_UNIDAD_MEDIDA);

                        if (dr.IsDBNull(pos_DESC_CATEGORIA)) obj.DESC_CATEGORIA = "";
                        else obj.DESC_CATEGORIA = dr.GetString(pos_DESC_CATEGORIA);

                        if (dr.IsDBNull(pos_PRECIO_COMPRA)) obj.PRECIO_COMPRA = 0;
                        else obj.PRECIO_COMPRA = decimal.Parse(dr[pos_PRECIO_COMPRA].ToString());

                        if (dr.IsDBNull(pos_ID_UNIDAD_MEDIDA)) obj.ID_UNIDAD_MEDIDA = 0;
                        else obj.ID_UNIDAD_MEDIDA = int.Parse(dr[pos_ID_UNIDAD_MEDIDA].ToString());


                        if (dr.IsDBNull(pos_PRECIO_VENTA)) obj.PRECIO_VENTA = 0;
                        else obj.PRECIO_VENTA = decimal.Parse(dr[pos_PRECIO_VENTA].ToString());

                        if (dr.IsDBNull(pos_STOCK)) obj.STOCK = 0;
                        else obj.STOCK = int.Parse(dr[pos_STOCK].ToString());

                        if (dr.IsDBNull(pos_STOCK_MINIMO)) obj.STOCK_MINIMO = 0;
                        else obj.STOCK_MINIMO = int.Parse(dr[pos_STOCK_MINIMO].ToString());

                        if (dr.IsDBNull(pos_FLG_SERIVICIO)) obj.FLG_SERVICIO = 0;
                        else obj.FLG_SERVICIO = int.Parse(dr[pos_FLG_SERIVICIO].ToString());


                        if (dr.IsDBNull(pos_FLG_VENCE)) obj.FLG_VENCE = 0;
                        else obj.FLG_VENCE = int.Parse(dr[pos_FLG_VENCE].ToString());

                        if (dr.IsDBNull(pos_FECHA_VENCIMIENTO)) obj.FECHA_VENCIMIENTO = "";
                        else obj.FECHA_VENCIMIENTO = dr.GetString(pos_FECHA_VENCIMIENTO);

                        if (dr.IsDBNull(pos_MARCA)) obj.MARCA = "";
                        else obj.MARCA = dr.GetString(pos_MARCA);

                        if (dr.IsDBNull(pos_MODELO)) obj.MODELO = "";
                        else obj.MODELO = dr.GetString(pos_MODELO);

                        if (dr.IsDBNull(pos_DETALLE)) obj.DETALLE = "";
                        else obj.DETALLE = dr.GetString(pos_DETALLE);



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

                        obj.MiArchivo = new Cls_Ent_Archivo();
                        {
                            if (dr.IsDBNull(pos_COD_ARCHIVO)) obj.MiArchivo.CODIGO_ARCHIVO  = "";
                            else obj.MiArchivo.CODIGO_ARCHIVO = dr.GetString(pos_COD_ARCHIVO);

                            if (dr.IsDBNull(pos_NOMBRE_ARCHIVO)) obj.MiArchivo.NOMBRE_ARCHIVO = "";
                            else obj.MiArchivo.NOMBRE_ARCHIVO = dr.GetString(pos_NOMBRE_ARCHIVO);

                            if (dr.IsDBNull(pos_EXTENSION)) obj.MiArchivo.EXTENSION = "";
                            else obj.MiArchivo.EXTENSION = dr.GetString(pos_EXTENSION);
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

        ///*********************************************** Lista los  cargo *************************************************/

        public List<Cls_Ent_Producto> Producto_Listar(Cls_Ent_Producto entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Producto> lista = new List<Cls_Ent_Producto>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_INVEN_PRODUCTO_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (entidad_param.DESC_PRODUCTO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_PRODUCTO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_PRODUCTO", SqlDbType.VarChar, 200)).Value = entidad_param.DESC_PRODUCTO; }

                    if (entidad_param.COD_PRODUCTO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_PRODUCTO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_PRODUCTO", SqlDbType.VarChar, 200)).Value = entidad_param.COD_PRODUCTO; }

                    if (entidad_param.ID_CATEGORIA == 0)
                    { cmd.Parameters.Add(new SqlParameter("@PI_ID_CATEGORIA", SqlDbType.Int)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_ID_CATEGORIA", SqlDbType.Int)).Value = entidad_param.ID_CATEGORIA; }

                    if (entidad_param.FLG_SERVICIO == 2)
                    { cmd.Parameters.Add(new SqlParameter("@PI_FLG_SERVICIO", SqlDbType.Int)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_FLG_SERVICIO", SqlDbType.Int)).Value = entidad_param.FLG_SERVICIO; } 

                    if (entidad_param.FLG_ESTADO == 2)
                    { cmd.Parameters.Add(new SqlParameter("@PI_FLG_ESTADO", SqlDbType.Int)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_FLG_ESTADO", SqlDbType.Int)).Value = entidad_param.FLG_ESTADO; }

                    if (entidad_param.ID_SUCURSAL == 0)
                    { cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad_param.ID_SUCURSAL; }

                    dr = cmd.ExecuteReader();
                    int pos_ID_PRODUCTO = dr.GetOrdinal("ID_PRODUCTO");
                    int pos_DESC_PRODUCTO = dr.GetOrdinal("DESC_PRODUCTO");
                    int pos_COD_PRODUCTO = dr.GetOrdinal("COD_PRODUCTO");
                    int pos_DESC_UNIDAD_MEDIDA = dr.GetOrdinal("DESC_UNIDAD_MEDIDA");
                    int pos_ID_UNIDAD_MEDIDA = dr.GetOrdinal("ID_UNIDAD_MEDIDA");                  
                    int pos_DESC_CATEGORIA = dr.GetOrdinal("DESC_CATEGORIA");
                    int pos_PRECIO_COMPRA = dr.GetOrdinal("PRECIO_COMPRA");
                    int pos_PRECIO_VENTA = dr.GetOrdinal("PRECIO_VENTA");
                    int pos_STOCK = dr.GetOrdinal("STOCK");
                    int pos_STOCK_MINIMO = dr.GetOrdinal("STOCK_MINIMO");
                    int pos_FLG_SERIVICIO = dr.GetOrdinal("FLG_SERIVICIO");
                    int pos_FLG_VENCE = dr.GetOrdinal("FLG_VENCE");
                    int pos_FECHA_VENCIMIENTO = dr.GetOrdinal("FECHA_VENCIMIENTO");
                    int pos_MARCA = dr.GetOrdinal("MARCA");
                    int pos_MODELO = dr.GetOrdinal("MODELO");
                    int pos_DETALLE = dr.GetOrdinal("DETALLE");
                    int pos_COD_ARCHIVO = dr.GetOrdinal("COD_ARCHIVO");
                    int pos_NOMBRE_ARCHIVO = dr.GetOrdinal("NOMBRE_ARCHIVO");
                    int pos_EXTENSION = dr.GetOrdinal("EXTENSION");
                    int pos_FLG_ESTADO = dr.GetOrdinal("FLG_ESTADO");
                    int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                    int pos_FECHA_CREACION = dr.GetOrdinal("FECHA_CREACION");
                    int pos_USU_MODIFICACION = dr.GetOrdinal("USU_MODIFICACION");
                    int pos_FEC_MODIFICACION = dr.GetOrdinal("FECHA_MODIFICACION");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Producto obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Producto();
                            if (dr.IsDBNull(pos_ID_PRODUCTO)) obj.ID_PRODUCTO = 0;
                            else obj.ID_PRODUCTO = int.Parse(dr[pos_ID_PRODUCTO].ToString());
                            if (dr.IsDBNull(pos_DESC_PRODUCTO)) obj.DESC_PRODUCTO = "";
                            else obj.DESC_PRODUCTO = dr.GetString(pos_DESC_PRODUCTO);

                            if (dr.IsDBNull(pos_COD_PRODUCTO)) obj.COD_PRODUCTO = "";
                            else obj.COD_PRODUCTO = dr.GetString(pos_COD_PRODUCTO);

                            if (dr.IsDBNull(pos_DESC_UNIDAD_MEDIDA)) obj.DESC_UNIDAD_MEDIDA = "";
                            else obj.DESC_UNIDAD_MEDIDA = dr.GetString(pos_DESC_UNIDAD_MEDIDA);

                            if (dr.IsDBNull(pos_DESC_CATEGORIA)) obj.DESC_CATEGORIA = "";
                            else obj.DESC_CATEGORIA = dr.GetString(pos_DESC_CATEGORIA);

                            if (dr.IsDBNull(pos_PRECIO_COMPRA)) obj.PRECIO_COMPRA = 0;
                            else obj.PRECIO_COMPRA = decimal.Parse(dr[pos_PRECIO_COMPRA].ToString());

                            if (dr.IsDBNull(pos_ID_UNIDAD_MEDIDA)) obj.ID_UNIDAD_MEDIDA = 0;
                            else obj.ID_UNIDAD_MEDIDA = int.Parse(dr[pos_ID_UNIDAD_MEDIDA].ToString());
                            

                            if (dr.IsDBNull(pos_PRECIO_VENTA)) obj.PRECIO_VENTA = 0;
                            else obj.PRECIO_VENTA = decimal.Parse(dr[pos_PRECIO_VENTA].ToString());

                            if (dr.IsDBNull(pos_STOCK)) obj.STOCK = 0;
                            else obj.STOCK = int.Parse(dr[pos_STOCK].ToString());

                            if (dr.IsDBNull(pos_STOCK_MINIMO)) obj.STOCK_MINIMO = 0;
                            else obj.STOCK_MINIMO = int.Parse(dr[pos_STOCK_MINIMO].ToString());

                            if (dr.IsDBNull(pos_FLG_SERIVICIO)) obj.FLG_SERVICIO = 0;
                            else obj.FLG_SERVICIO = int.Parse(dr[pos_FLG_SERIVICIO].ToString());

                            if (dr.IsDBNull(pos_FECHA_VENCIMIENTO)) obj.FECHA_VENCIMIENTO = "";
                            else obj.FECHA_VENCIMIENTO = dr.GetString(pos_FECHA_VENCIMIENTO);

                            if (dr.IsDBNull(pos_MARCA)) obj.MARCA = "";
                            else obj.MARCA = dr.GetString(pos_MARCA);

                            if (dr.IsDBNull(pos_MODELO)) obj.MODELO = "";
                            else obj.MODELO = dr.GetString(pos_MODELO);

                            if (dr.IsDBNull(pos_DETALLE)) obj.DETALLE = "";
                            else obj.DETALLE = dr.GetString(pos_DETALLE);

                           
                 
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


        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista los  cargo *************************************************/

        public List<Cls_Ent_Producto> Producto_Buscar_Listar(Cls_Ent_Producto entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Producto> lista = new List<Cls_Ent_Producto>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_CONS_BUSCAR_PRUDUCTO_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (entidad_param.DESC_PRODUCTO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_PRODUCTO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_PRODUCTO", SqlDbType.VarChar, 200)).Value = entidad_param.DESC_PRODUCTO; }

                    if (entidad_param.COD_PRODUCTO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_PRODUCTO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_PRODUCTO", SqlDbType.VarChar, 200)).Value = entidad_param.COD_PRODUCTO; }

                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad_param.ID_SUCURSAL;

                    dr = cmd.ExecuteReader();
                    int pos_ID_PRODUCTO = dr.GetOrdinal("ID_PRODUCTO");
                    int pos_DESC_PRODUCTO = dr.GetOrdinal("DESC_PRODUCTO");
                    int pos_COD_PRODUCTO = dr.GetOrdinal("COD_PRODUCTO");
                    int pos_DESC_UNIDAD_MEDIDA = dr.GetOrdinal("DESC_UNIDAD_MEDIDA");
                    int pos_COD_UNIDAD_MEDIDA = dr.GetOrdinal("COD_UNIDAD_MEDIDA");
                    int pos_ID_UNIDAD_MEDIDA = dr.GetOrdinal("ID_UNIDAD_MEDIDA");
                    int pos_DESC_CATEGORIA = dr.GetOrdinal("DESC_CATEGORIA");
                    int pos_PRECIO_COMPRA = dr.GetOrdinal("PRECIO_COMPRA");
                    int pos_PRECIO_VENTA = dr.GetOrdinal("PRECIO_VENTA");
                    int pos_STOCK = dr.GetOrdinal("STOCK");
                    int pos_STOCK_MINIMO = dr.GetOrdinal("STOCK_MINIMO");
                    int pos_FLG_SERIVICIO = dr.GetOrdinal("FLG_SERIVICIO");
                    int pos_FLG_VENCE = dr.GetOrdinal("FLG_VENCE");
                    int pos_FECHA_VENCIMIENTO = dr.GetOrdinal("FECHA_VENCIMIENTO");
                    int pos_MARCA = dr.GetOrdinal("MARCA");
                    int pos_MODELO = dr.GetOrdinal("MODELO");
                    int pos_DETALLE = dr.GetOrdinal("DETALLE");
                    int pos_COD_ARCHIVO = dr.GetOrdinal("COD_ARCHIVO");
                    int pos_NOMBRE_ARCHIVO = dr.GetOrdinal("NOMBRE_ARCHIVO");
                    int pos_EXTENSION = dr.GetOrdinal("EXTENSION");

                    if (dr.HasRows)
                    {
                        Cls_Ent_Producto obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Producto();
                            if (dr.IsDBNull(pos_ID_PRODUCTO)) obj.ID_PRODUCTO = 0;
                            else obj.ID_PRODUCTO = int.Parse(dr[pos_ID_PRODUCTO].ToString());
                            if (dr.IsDBNull(pos_DESC_PRODUCTO)) obj.DESC_PRODUCTO = "";
                            else obj.DESC_PRODUCTO = dr.GetString(pos_DESC_PRODUCTO);

                            if (dr.IsDBNull(pos_COD_PRODUCTO)) obj.COD_PRODUCTO = "";
                            else obj.COD_PRODUCTO = dr.GetString(pos_COD_PRODUCTO);

                            if (dr.IsDBNull(pos_DESC_UNIDAD_MEDIDA)) obj.DESC_UNIDAD_MEDIDA = "";
                            else obj.DESC_UNIDAD_MEDIDA = dr.GetString(pos_DESC_UNIDAD_MEDIDA);

                            if (dr.IsDBNull(pos_DESC_CATEGORIA)) obj.DESC_CATEGORIA = "";
                            else obj.DESC_CATEGORIA = dr.GetString(pos_DESC_CATEGORIA);

                            if (dr.IsDBNull(pos_PRECIO_COMPRA)) obj.PRECIO_COMPRA = 0;
                            else obj.PRECIO_COMPRA = decimal.Parse(dr[pos_PRECIO_COMPRA].ToString());

                            if (dr.IsDBNull(pos_PRECIO_VENTA)) obj.PRECIO_VENTA = 0;
                            else obj.PRECIO_VENTA = decimal.Parse(dr[pos_PRECIO_VENTA].ToString());

                            if (dr.IsDBNull(pos_STOCK)) obj.STOCK = 0;
                            else obj.STOCK = int.Parse(dr[pos_STOCK].ToString());

                            if (dr.IsDBNull(pos_STOCK_MINIMO)) obj.STOCK_MINIMO = 0;
                            else obj.STOCK_MINIMO = int.Parse(dr[pos_STOCK_MINIMO].ToString());

                            if (dr.IsDBNull(pos_FLG_SERIVICIO)) obj.FLG_SERVICIO = 0;
                            else obj.FLG_SERVICIO = int.Parse(dr[pos_FLG_SERIVICIO].ToString());

                            if (dr.IsDBNull(pos_ID_UNIDAD_MEDIDA)) obj.ID_UNIDAD_MEDIDA = 0;
                            else obj.ID_UNIDAD_MEDIDA = int.Parse(dr[pos_ID_UNIDAD_MEDIDA].ToString());

                            if (dr.IsDBNull(pos_FECHA_VENCIMIENTO)) obj.FECHA_VENCIMIENTO = "";
                            else obj.FECHA_VENCIMIENTO = dr.GetString(pos_FECHA_VENCIMIENTO);

                            if (dr.IsDBNull(pos_MARCA)) obj.MARCA = "";
                            else obj.MARCA = dr.GetString(pos_MARCA);

                            if (dr.IsDBNull(pos_MODELO)) obj.MODELO = "";
                            else obj.MODELO = dr.GetString(pos_MODELO);

                            if (dr.IsDBNull(pos_DETALLE)) obj.DETALLE = "";
                            else obj.DETALLE = dr.GetString(pos_DETALLE);

                            if (dr.IsDBNull(pos_COD_UNIDAD_MEDIDA)) obj.COD_UNIDAD_MEDIDA = "";
                            else obj.COD_UNIDAD_MEDIDA = dr.GetString(pos_COD_UNIDAD_MEDIDA);

                            obj.MiArchivo = new Cls_Ent_Archivo(); 
                           {

                               if (dr.IsDBNull(pos_EXTENSION)) obj.MiArchivo.CODIGO_ARCHIVO = "";
                               else obj.MiArchivo.CODIGO_ARCHIVO = dr.GetString(pos_COD_ARCHIVO);

                               if (dr.IsDBNull(pos_COD_ARCHIVO)) obj.MiArchivo.EXTENSION = "";
                               else obj.MiArchivo.EXTENSION = dr.GetString(pos_EXTENSION);

                           }

 
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

        ///*********************************************** Lista sucursal por id *************************************************/

        public Cls_Ent_Producto Producto_Listar_Uno(Cls_Ent_Producto entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            Cls_Ent_Producto obj = new Cls_Ent_Producto();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_INVEN_PRODUCTO_LISTAR_UNO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PRODUCTO", SqlDbType.BigInt)).Value = entidad_param.ID_PRODUCTO;
                    dr = cmd.ExecuteReader();
                    int pos_ID_PRODUCTO = dr.GetOrdinal("ID_PRODUCTO");
                    int pos_DESC_PRODUCTO = dr.GetOrdinal("DESC_PRODUCTO");
                    int pos_COD_PRODUCTO = dr.GetOrdinal("COD_PRODUCTO");
                    int pos_ID_SUCURSAL = dr.GetOrdinal("ID_SUCURSAL");
                    int pos_ID_UNIDAD_MEDIDA = dr.GetOrdinal("ID_UNIDAD_MEDIDA");
                    int pos_ID_CATEGORIA = dr.GetOrdinal("ID_CATEGORIA");
                    int pos_PRECIO_COMPRA = dr.GetOrdinal("PRECIO_COMPRA");
                    int pos_PRECIO_VENTA = dr.GetOrdinal("PRECIO_VENTA");
                    int pos_STOCK = dr.GetOrdinal("STOCK");
                    int pos_STOCK_MINIMO = dr.GetOrdinal("STOCK_MINIMO");
                    int pos_FLG_SERIVICIO = dr.GetOrdinal("FLG_SERIVICIO");
                    int pos_FLG_VENCE = dr.GetOrdinal("FLG_VENCE");
                    int pos_FECHA_VENCIMIENTO = dr.GetOrdinal("FECHA_VENCIMIENTO");
                    int pos_MARCA = dr.GetOrdinal("MARCA");
                    int pos_MODELO = dr.GetOrdinal("MODELO");
                    int pos_DETALLE = dr.GetOrdinal("DETALLE");
                    int pos_COD_ARCHIVO = dr.GetOrdinal("COD_ARCHIVO");
                    int pos_NOMBRE_ARCHIVO = dr.GetOrdinal("NOMBRE_ARCHIVO");
                    int pos_EXTENSION = dr.GetOrdinal("EXTENSION");
                    int pos_COD_UNIDAD_MEDIDA = dr.GetOrdinal("COD_UNIDAD_MEDIDA");
                    
                    if (dr.HasRows)
                    {
                        //Cls_Ent_Producto obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Producto();
                            if (dr.IsDBNull(pos_ID_PRODUCTO)) obj.ID_PRODUCTO = 0;
                            else obj.ID_PRODUCTO = int.Parse(dr[pos_ID_PRODUCTO].ToString());
                            if (dr.IsDBNull(pos_DESC_PRODUCTO)) obj.DESC_PRODUCTO = "";
                            else obj.DESC_PRODUCTO = dr.GetString(pos_DESC_PRODUCTO);

                            if (dr.IsDBNull(pos_COD_PRODUCTO)) obj.COD_PRODUCTO = "";
                            else obj.COD_PRODUCTO = dr.GetString(pos_COD_PRODUCTO);

                            if (dr.IsDBNull(pos_ID_SUCURSAL)) obj.ID_SUCURSAL = 0;
                            else obj.ID_SUCURSAL = int.Parse(dr[pos_ID_SUCURSAL].ToString());

                            if (dr.IsDBNull(pos_ID_UNIDAD_MEDIDA)) obj.ID_UNIDAD_MEDIDA = 0;
                            else obj.ID_UNIDAD_MEDIDA = int.Parse(dr[pos_ID_UNIDAD_MEDIDA].ToString());

                            if (dr.IsDBNull(pos_ID_CATEGORIA)) obj.ID_CATEGORIA = 0;
                            else obj.ID_CATEGORIA = int.Parse(dr[pos_ID_CATEGORIA].ToString());


                            if (dr.IsDBNull(pos_PRECIO_COMPRA)) obj.PRECIO_COMPRA = 0;
                            else obj.PRECIO_COMPRA = decimal.Parse(dr[pos_PRECIO_COMPRA].ToString());

                            if (dr.IsDBNull(pos_PRECIO_VENTA)) obj.PRECIO_VENTA = 0;
                            else obj.PRECIO_VENTA = decimal.Parse(dr[pos_PRECIO_VENTA].ToString());

                            if (dr.IsDBNull(pos_STOCK)) obj.STOCK = 0;
                            else obj.STOCK = int.Parse(dr[pos_STOCK].ToString());

                            if (dr.IsDBNull(pos_STOCK_MINIMO)) obj.STOCK_MINIMO = 0;
                            else obj.STOCK_MINIMO = int.Parse(dr[pos_STOCK_MINIMO].ToString());

                            if (dr.IsDBNull(pos_FLG_SERIVICIO)) obj.FLG_SERVICIO = 0;
                            else obj.FLG_SERVICIO = int.Parse(dr[pos_FLG_SERIVICIO].ToString());

                            if (dr.IsDBNull(pos_FLG_VENCE)) obj.FLG_VENCE = 0;
                            else obj.FLG_VENCE = int.Parse(dr[pos_FLG_VENCE].ToString());

                            if (dr.IsDBNull(pos_FECHA_VENCIMIENTO)) obj.FECHA_VENCIMIENTO = "";
                            else obj.FECHA_VENCIMIENTO = dr.GetString(pos_FECHA_VENCIMIENTO);

                            if (dr.IsDBNull(pos_MARCA)) obj.MARCA = "";
                            else obj.MARCA = dr.GetString(pos_MARCA);

                            if (dr.IsDBNull(pos_MODELO)) obj.MODELO = "";
                            else obj.MODELO = dr.GetString(pos_MODELO);

                            if (dr.IsDBNull(pos_DETALLE)) obj.DETALLE = "";
                            else obj.DETALLE = dr.GetString(pos_DETALLE);

                            if (dr.IsDBNull(pos_COD_UNIDAD_MEDIDA)) obj.COD_UNIDAD_MEDIDA = "";
                            else obj.COD_UNIDAD_MEDIDA = dr.GetString(pos_COD_UNIDAD_MEDIDA);

                            obj.MiArchivo = new Cls_Ent_Archivo(); 
                            {
                               if (dr.IsDBNull(pos_COD_ARCHIVO)) obj.MiArchivo.CODIGO_ARCHIVO = "";
                               else obj.MiArchivo.CODIGO_ARCHIVO = dr.GetString(pos_COD_ARCHIVO);

                               if (dr.IsDBNull(pos_NOMBRE_ARCHIVO)) obj.MiArchivo.NOMBRE_ARCHIVO = "";
                               else obj.MiArchivo.NOMBRE_ARCHIVO = dr.GetString(pos_NOMBRE_ARCHIVO);

                               if (dr.IsDBNull(pos_EXTENSION)) obj.MiArchivo.EXTENSION = "";
                               else obj.MiArchivo.EXTENSION = dr.GetString(pos_EXTENSION);
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

        ///*********************************************** Inserta sucursal  *************************************************/

        public void Producto_Insertar(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                if (entidad.MiArchivo == null)
                    entidad.MiArchivo = new Cls_Ent_Archivo(); 

                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_INVEN_PRODUCTO_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_COD_PRODUCTO", SqlDbType.VarChar, 200)).Value = entidad.COD_PRODUCTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_DESC_PRODUCTO", SqlDbType.VarChar, 200)).Value = entidad.DESC_PRODUCTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad.ID_SUCURSAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_UNIDAD_MEDIDA", SqlDbType.Int)).Value = entidad.ID_UNIDAD_MEDIDA;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_CATEGORIA", SqlDbType.Int)).Value = entidad.ID_CATEGORIA;
                    cmd.Parameters.Add(new SqlParameter("@PI_PRECIO_COMPRA", SqlDbType.Decimal)).Value = entidad.PRECIO_COMPRA;
                    cmd.Parameters.Add(new SqlParameter("@PI_PRECIO_VENTA ", SqlDbType.Decimal)).Value = entidad.PRECIO_VENTA;
                    cmd.Parameters.Add(new SqlParameter("@PI_STOCK", SqlDbType.Decimal)).Value = entidad.STOCK;
                    cmd.Parameters.Add(new SqlParameter("@PI_STOCK_MINIMO", SqlDbType.Decimal)).Value = entidad.STOCK_MINIMO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_SERVICIO", SqlDbType.Int)).Value = entidad.FLG_SERVICIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_VENCE", SqlDbType.Int)).Value = entidad.FLG_VENCE;

                    if (entidad.FECHA_VENCIMIENTO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_FECHA_VENCIMIENTO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_FECHA_VENCIMIENTO", SqlDbType.VarChar, 200)).Value = entidad.FECHA_VENCIMIENTO; }

                    if (entidad.MARCA == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_MARCA", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_MARCA", SqlDbType.VarChar, 200)).Value = entidad.MARCA; }
                    if (entidad.MODELO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_MODELO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_MODELO", SqlDbType.VarChar, 200)).Value = entidad.MODELO; }

                    if (entidad.DETALLE == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = entidad.DETALLE; }

                    if (entidad.MiArchivo.CODIGO_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_ARCHIVO", SqlDbType.VarChar, 100)).Value = "";  }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_ARCHIVO", SqlDbType.VarChar, 100)).Value = entidad.MiArchivo.CODIGO_ARCHIVO; }

                    if (entidad.MiArchivo.NOMBRE_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO", SqlDbType.VarChar, 100)).Value = entidad.MiArchivo.NOMBRE_ARCHIVO; }

                    if (entidad.MiArchivo.EXTENSION == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION", SqlDbType.VarChar, 100)).Value = entidad.MiArchivo.EXTENSION; }


                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_CREACION", SqlDbType.VarChar, 200)).Value = entidad.USU_CREACION;
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

        ///*********************************************** Actualiza  sucursal  *************************************************/

        public void Producto_Actualizar(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                if (entidad.MiArchivo == null)
                    entidad.MiArchivo = new Cls_Ent_Archivo(); 
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_INVEN_PRODUCTO_ACTUALIZAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PRODUCTO", SqlDbType.Int)).Value = entidad.ID_PRODUCTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_COD_PRODUCTO", SqlDbType.VarChar, 200)).Value = entidad.COD_PRODUCTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_DESC_PRODUCTO", SqlDbType.VarChar, 200)).Value = entidad.DESC_PRODUCTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad.ID_SUCURSAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_UNIDAD_MEDIDA", SqlDbType.Int)).Value = entidad.ID_UNIDAD_MEDIDA;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_CATEGORIA", SqlDbType.Int)).Value = entidad.ID_CATEGORIA;
                    cmd.Parameters.Add(new SqlParameter("@PI_PRECIO_COMPRA", SqlDbType.Decimal)).Value = entidad.PRECIO_COMPRA;
                    cmd.Parameters.Add(new SqlParameter("@PI_PRECIO_VENTA ", SqlDbType.Decimal)).Value = entidad.PRECIO_VENTA;
                    cmd.Parameters.Add(new SqlParameter("@PI_STOCK_MINIMO", SqlDbType.Decimal)).Value = entidad.STOCK_MINIMO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_SERVICIO", SqlDbType.Int)).Value = entidad.FLG_SERVICIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_VENCE", SqlDbType.Int)).Value = entidad.FLG_VENCE;

                    if (entidad.FECHA_VENCIMIENTO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_FECHA_VENCIMIENTO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_FECHA_VENCIMIENTO", SqlDbType.VarChar, 200)).Value = entidad.FECHA_VENCIMIENTO; }

                    if (entidad.MARCA == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_MARCA", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_MARCA", SqlDbType.VarChar, 200)).Value = entidad.MARCA; }
                    if (entidad.MODELO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_MODELO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_MODELO", SqlDbType.VarChar, 200)).Value = entidad.MODELO; }

                    if (entidad.DETALLE == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = entidad.DETALLE; }

                    if (entidad.MiArchivo.CODIGO_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_ARCHIVO", SqlDbType.VarChar, 100)).Value = "0";  }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_COD_ARCHIVO", SqlDbType.VarChar, 100)).Value = entidad.MiArchivo.CODIGO_ARCHIVO; }

                    if (entidad.MiArchivo.NOMBRE_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO", SqlDbType.VarChar, 100)).Value = entidad.MiArchivo.NOMBRE_ARCHIVO; }

                    if (entidad.MiArchivo.EXTENSION == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION", SqlDbType.VarChar, 100)).Value = entidad.MiArchivo.EXTENSION; }
                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_MODIFICACION", SqlDbType.VarChar, 200)).Value = entidad.USU_MODIFICACION;
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

        ///*********************************************** Elimina sucursal  *************************************************/

        public void Producto_Eliminar(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_INVEN_PRODUCTO_ELIMINAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PRODUCTO", SqlDbType.Int)).Value = entidad.ID_PRODUCTO;
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

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Cambia estado de sucursal  *************************************************/

        public void Producto_Estado(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_INVEN_PRODUCTO_ESTADO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PRODUCTO", SqlDbType.Int)).Value = entidad.ID_PRODUCTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_ESTADO", SqlDbType.VarChar, 200)).Value = entidad.FLG_ESTADO;
                    cmd.Parameters.Add(new SqlParameter("@PI_IP_MODIFICACION", SqlDbType.VarChar, 200)).Value = entidad.IP_MODIFICACION;
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
                        auditoria.Rechazar("Estado no cambiado ");
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

        ///*********************************************** Inserta moviento producto  *************************************************/

        public void Producto_Movimiento_Insertar(Cls_Ent_Movimiento_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_INVEN_MOV_PRODUCTO_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PRODUCTO", SqlDbType.BigInt)).Value = entidad.ID_PRODUCTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_CANTIDAD", SqlDbType.Int)).Value = entidad.CANTIDAD;
                    //cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = entidad.DETALLE;
                    if (entidad.DETALLE == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = entidad.DETALLE; }
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_MOVIMIENTO", SqlDbType.Int)).Value = entidad.FLG_MOVIMIENTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_CREACION", SqlDbType.VarChar, 200)).Value = entidad.USU_CREACION;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
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
