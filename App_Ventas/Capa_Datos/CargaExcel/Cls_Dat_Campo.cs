using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.CargaExcel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos.CargaExcel
{
    public class Cls_Dat_Campo : Protected.DataBaseHelper
    {
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista los  cargo *************************************************/
        ///
        public List<Cls_Ent_Campo> Campo_Listar( ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Campo> lista = new List<Cls_Ent_Campo>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_CARGA_CAMPO_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    int pos_ID_CAMPO = dr.GetOrdinal("ID_CAMPO");
                    int pos_COD_CAMPO = dr.GetOrdinal("COD_CAMPO");
                    int pos_DESCRIPCION_CAMPO = dr.GetOrdinal("DESCRIPCION_CAMPO");
                    int pos_NRO_CAMPO = dr.GetOrdinal("NRO_CAMPO");
                    int pos_DATO_EJEMPLO = dr.GetOrdinal("DATO_EJEMPLO");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Campo obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Campo();
                            if (dr.IsDBNull(pos_ID_CAMPO)) obj.ID_CAMPO = 0;
                            else obj.ID_CAMPO = int.Parse(dr[pos_ID_CAMPO].ToString());

                            if (dr.IsDBNull(pos_COD_CAMPO)) obj.COD_CAMPO = "";
                            else obj.COD_CAMPO = dr.GetString(pos_COD_CAMPO);

                            if (dr.IsDBNull(pos_DESCRIPCION_CAMPO)) obj.DESCRIPCION_CAMPO = "";
                            else obj.DESCRIPCION_CAMPO = dr.GetString(pos_DESCRIPCION_CAMPO);

                            if (dr.IsDBNull(pos_NRO_CAMPO)) obj.NRO_CAMPO = 0;
                            else obj.NRO_CAMPO = int.Parse(dr[pos_ID_CAMPO].ToString());

                            if (dr.IsDBNull(pos_DATO_EJEMPLO)) obj.DATO_EJEMPLO = "";
                            else obj.DATO_EJEMPLO = dr.GetString(pos_DATO_EJEMPLO);

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
