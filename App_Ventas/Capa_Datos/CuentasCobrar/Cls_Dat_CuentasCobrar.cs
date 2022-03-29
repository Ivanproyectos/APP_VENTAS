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
namespace Capa_Datos.CuentasCobrar
{
    public class Cls_Dat_CuentasCobrar : Protected.DataBaseHelper
    {
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Inserta cobrar cuenta  *************************************************/

        public void CuentasCobrar_Insertar(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {

                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_CUENTASCOBRAR_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_CLIENTE", SqlDbType.Int)).Value = entidad.ID_CLIENTE;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_VENTA", SqlDbType.Int)).Value = entidad.ID_VENTA;
                    cmd.Parameters.Add(new SqlParameter("@PI_MONTO", SqlDbType.Decimal)).Value = entidad.TOTAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_TIPO_PAGO", SqlDbType.Int)).Value = entidad.FLG_TIPO_PAGO;
                    if (entidad.NRO_OPERACION == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_NRO_OPERACION", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_NRO_OPERACION", SqlDbType.VarChar, 100)).Value = entidad.NRO_OPERACION; }

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

    }
}
