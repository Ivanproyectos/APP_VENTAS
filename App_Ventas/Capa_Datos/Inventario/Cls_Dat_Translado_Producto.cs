using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Inventario;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace Capa_Datos.Inventario
{
    public class Cls_Dat_Translado_Producto : Protected.DataBaseHelper
    {

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Inserta translado  *************************************************/

        public void Producto_Translado_Insertar(Cls_Ent_Translado_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_INVEN_TRANSLADO_PRODUCTO_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL_ORIGEN", SqlDbType.Int)).Value = entidad.ID_SUCURSAL_ORIGEN;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL_DESTINO", SqlDbType.Int)).Value = entidad.ID_SUCURSAL_DESTINO;
                    cmd.Parameters.Add(new SqlParameter("@PI_DETALLE", SqlDbType.VarChar, 1000)).Value = entidad.DETALLE;
                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_CREACION", SqlDbType.VarChar, 200)).Value = entidad.USU_CREACION;
                    cmd.Parameters.Add(new SqlParameter("PO_ID_TRANSLADO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_ID_TRANSLADO = cmd.Parameters["PO_ID_TRANSLADO"].Value.ToString();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                    auditoria.OBJETO = Convert.ToInt32(PO_ID_TRANSLADO);
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Inserta translado detalle  *************************************************/

        public void Producto_Translado_Detalle_Insertar(Cls_Ent_Translado_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_INVEN_TRANSLADO_DETALLE_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_TRANSLADO", SqlDbType.Int)).Value = entidad.ID_TRANSLADO;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PRODUCTO", SqlDbType.Int)).Value = entidad.ID_PRODUCTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_CANTIDAD", SqlDbType.Int)).Value = entidad.CANTIDAD;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL_DESTINO", SqlDbType.Int)).Value = entidad.ID_SUCURSAL_DESTINO;
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
