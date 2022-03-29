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


        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista tipo documento *************************************************/
        ///
        public List<Cls_Ent_Unidad_Medida> Unidad_Medida_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Unidad_Medida> lista = new List<Cls_Ent_Unidad_Medida>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_CONS_UNIDAD_MEDIDA_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    int pos_ID_UNIDAD_MEDIDA = dr.GetOrdinal("ID_UNIDAD_MEDIDA");
                    int pos_DESC_UNIDAD_MEDIDA = dr.GetOrdinal("DESC_UNIDAD_MEDIDA");

                    if (dr.HasRows)
                    {
                        Cls_Ent_Unidad_Medida obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Unidad_Medida();
                            if (dr.IsDBNull(pos_ID_UNIDAD_MEDIDA)) obj.ID_UNIDAD_MEDIDA = 0;
                            else obj.ID_UNIDAD_MEDIDA = int.Parse(dr[pos_ID_UNIDAD_MEDIDA].ToString());
                            if (dr.IsDBNull(pos_DESC_UNIDAD_MEDIDA)) obj.DESC_UNIDAD_MEDIDA = "";
                            else obj.DESC_UNIDAD_MEDIDA = dr.GetString(pos_DESC_UNIDAD_MEDIDA);
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

        ///*********************************************** Lista tipo comprobante *************************************************/
        ///
        public List<Cls_Ent_Tipo_Comprobante> Tipo_Comprobante_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Tipo_Comprobante> lista = new List<Cls_Ent_Tipo_Comprobante>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_CONS_TIPO_COMPROBANTE_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    int pos_ID_TIPO_COMPROBANTE = dr.GetOrdinal("ID_TIPO_COMPROBANTE");
                    int pos_DESC_TIPO_COMPROBANTE = dr.GetOrdinal("DESC_TIPO_COMPROBANTE");

                    if (dr.HasRows)
                    {
                        Cls_Ent_Tipo_Comprobante obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Tipo_Comprobante();
                      
                            if (dr.IsDBNull(pos_ID_TIPO_COMPROBANTE)) obj.ID_TIPO_COMPROBANTE = "";
                            else obj.ID_TIPO_COMPROBANTE = dr.GetString(pos_ID_TIPO_COMPROBANTE);

                            if (dr.IsDBNull(pos_DESC_TIPO_COMPROBANTE)) obj.DESC_TIPO_COMPROBANTE = "";
                            else obj.DESC_TIPO_COMPROBANTE = dr.GetString(pos_DESC_TIPO_COMPROBANTE);
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

        ///*********************************************** Lista CLIENTES POR ID COMPROBATE *************************************************/
        ///
        public List<Cls_Ent_Cliente> Clientes_ListarXComprobante(string ID_TIPO_COMPROBANTE, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Cliente> lista = new List<Cls_Ent_Cliente>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_CONS_CLIENTE_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_TIPO_COMPROBANTE", SqlDbType.VarChar, 200)).Value = ID_TIPO_COMPROBANTE; 
                    dr = cmd.ExecuteReader();
                    int pos_ID_CLIENTE = dr.GetOrdinal("ID_CLIENTE");
                    int pos_NOMBRES_APE = dr.GetOrdinal("NOMBRES_APE");
                    int pos_NUMERO_DOCUMENTO = dr.GetOrdinal("NUMERO_DOCUMENTO");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Cliente obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Cliente();
                            if (dr.IsDBNull(pos_ID_CLIENTE)) obj.ID_CLIENTE = 0;
                            else obj.ID_CLIENTE = int.Parse(dr[pos_ID_CLIENTE].ToString());
                            if (dr.IsDBNull(pos_NOMBRES_APE)) obj.NOMBRES_APE = "";
                            else obj.NOMBRES_APE = dr.GetString(pos_NOMBRES_APE);

                            if (dr.IsDBNull(pos_NUMERO_DOCUMENTO)) obj.NUMERO_DOCUMENTO = "";
                            else obj.NUMERO_DOCUMENTO = dr.GetString(pos_NUMERO_DOCUMENTO);
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
