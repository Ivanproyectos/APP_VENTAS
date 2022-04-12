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
