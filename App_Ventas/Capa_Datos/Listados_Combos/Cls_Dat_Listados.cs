using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos.Listados_Combos
{
    public class Cls_Dat_Listados : Protected.DataBaseHelper
    {
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista los  cargo *************************************************/
        ///
        public List<Cls_Ent_Ubigeo> Ubigeo_Listar( ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Ubigeo> lista = new List<Cls_Ent_Ubigeo>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_CONS_UBIGEO_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    int pos_ID_UBIGEO = dr.GetOrdinal("ID_UBIGEO");
                    int pos_DESC_UBIGEO = dr.GetOrdinal("DESC_UBIGEO");
         
                    if (dr.HasRows)
                    {
                        Cls_Ent_Ubigeo obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Ubigeo();
                            if (dr.IsDBNull(pos_ID_UBIGEO)) obj.ID_UBIGEO = "";
                            else obj.ID_UBIGEO = dr.GetString(pos_ID_UBIGEO);
                            if (dr.IsDBNull(pos_DESC_UBIGEO)) obj.DESC_UBIGEO = "";
                            else obj.DESC_UBIGEO = dr.GetString(pos_DESC_UBIGEO);
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

        ///*********************************************** Lista tipo documento *************************************************/
        ///
        public List<Cls_Ent_Tipo_Documento> Tipo_Documento_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Tipo_Documento> lista = new List<Cls_Ent_Tipo_Documento>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_CONS_TIPO_DOCUMENTO_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    int pos_ID_TIPO_DOCUMENTO = dr.GetOrdinal("ID_TIPO_DOCUMENTO");
                    int pos_DESC_TIPO_DOCUMENTO = dr.GetOrdinal("DESC_TIPO_DOCUMENTO");

                    if (dr.HasRows)
                    {
                        Cls_Ent_Tipo_Documento obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Tipo_Documento();
                            if (dr.IsDBNull(pos_ID_TIPO_DOCUMENTO)) obj.ID_TIPO_DOCUMENTO = 0;
                            else obj.ID_TIPO_DOCUMENTO = int.Parse(dr[pos_ID_TIPO_DOCUMENTO].ToString());
                            if (dr.IsDBNull(pos_DESC_TIPO_DOCUMENTO)) obj.DESC_TIPO_DOCUMENTO = "";
                            else obj.DESC_TIPO_DOCUMENTO = dr.GetString(pos_DESC_TIPO_DOCUMENTO);
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
