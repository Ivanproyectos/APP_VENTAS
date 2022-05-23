using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Login;
using Capa_Entidad.Administracion;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos.Login
{
    public class Cls_Dat_ModulosPerfil : Protected.DataBaseHelper
    {
        public List<Cls_Ent_Modulo> Modulos_Listar( ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Modulo> lista = new List<Cls_Ent_Modulo>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_SEG_MODULOS_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    int pos_ID_SISTEMA_MODULO = dr.GetOrdinal("ID_MODULO");
                    int pos_ID_SISTEMA_MODULO_PADRE = dr.GetOrdinal("ID_MODULO_PADRE");
                    int pos_ID_A = dr.GetOrdinal("ID_A");
                    int pos_ID_LI = dr.GetOrdinal("ID_LI");
                    int pos_IMAGEN = dr.GetOrdinal("IMAGEN");
                    int pos_DESC_MODULO = dr.GetOrdinal("DESC_MODULO");
                    int pos_ORDEN = dr.GetOrdinal("ORDEN");
                    int pos_NIVEL = dr.GetOrdinal("NIVEL");
                    int pos_FLG_ESTADO = dr.GetOrdinal("FLG_ESTADO");

                    if (dr != null)
                    {
                        if (dr.HasRows)
                        {
                            Cls_Ent_Modulo entidad_2 = null;
                            while (dr.Read())
                            {
                                entidad_2 = new Cls_Ent_Modulo();

                                if (dr.IsDBNull(pos_ID_SISTEMA_MODULO)) entidad_2.ID_MODULO = 0;
                                else entidad_2.ID_MODULO = Convert.ToInt32(dr[pos_ID_SISTEMA_MODULO]);
                                if (dr.IsDBNull(pos_ID_SISTEMA_MODULO_PADRE)) entidad_2.ID_MODULO_PADRE = 0;
                                else entidad_2.ID_MODULO_PADRE = Convert.ToInt32(dr[pos_ID_SISTEMA_MODULO_PADRE]);

                                if (dr.IsDBNull(pos_ID_A)) entidad_2.ID_A = "";
                                else entidad_2.ID_A = Convert.ToString(dr[pos_ID_A]);
                                if (dr.IsDBNull(pos_ID_LI)) entidad_2.ID_LI = "";
                                else entidad_2.ID_LI = Convert.ToString(dr[pos_ID_LI]);
                                if (dr.IsDBNull(pos_IMAGEN)) entidad_2.IMAGEN = "";
                                else entidad_2.IMAGEN = Convert.ToString(dr[pos_IMAGEN]);
                                if (dr.IsDBNull(pos_DESC_MODULO)) entidad_2.DESC_MODULO = "";
                                else entidad_2.DESC_MODULO = Convert.ToString(dr[pos_DESC_MODULO]);
                                if (dr.IsDBNull(pos_ORDEN)) entidad_2.ORDEN = 0;
                                else entidad_2.ORDEN = Convert.ToInt32(dr[pos_ORDEN]);

                                if (dr.IsDBNull(pos_NIVEL)) entidad_2.NIVEL = 0;
                                else entidad_2.NIVEL = Convert.ToInt32(dr[pos_NIVEL]);

                                if (dr.IsDBNull(pos_FLG_ESTADO)) entidad_2.FLG_ESTADO = 0;
                                else entidad_2.FLG_ESTADO = Convert.ToInt32(dr[pos_FLG_ESTADO]);

                                lista.Add(entidad_2);
                            }
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Modulo>();
            }
            return lista;
        }

        public List<Cls_Ent_Modulo> Perfiles_Modulos_Listar(Cls_Ent_Sistemas_Perfiles entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Modulo> lista = new List<Cls_Ent_Modulo>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_SEG_PERF_MODU_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PERFIL", SqlDbType.Int)).Value = entidad.ID_PERFIL;
                    dr = cmd.ExecuteReader();
                    int pos_ID_SISTEMA_MODULO = dr.GetOrdinal("ID_MODULO");
                    int pos_ID_SISTEMA_MODULO_PADRE = dr.GetOrdinal("ID_MODULO_PADRE");
                    int pos_ID_A = dr.GetOrdinal("ID_A");
                    int pos_ID_LI = dr.GetOrdinal("ID_LI");
                    int pos_IMAGEN = dr.GetOrdinal("IMAGEN");
                    int pos_DESC_MODULO = dr.GetOrdinal("DESC_MODULO");
                    int pos_ORDEN = dr.GetOrdinal("ORDEN");
                    int pos_NIVEL = dr.GetOrdinal("NIVEL");
                    int pos_FLG_ESTADO = dr.GetOrdinal("FLG_ESTADO");

                    if (dr != null)
                    {
                        if (dr.HasRows)
                        {
                            Cls_Ent_Modulo entidad_2 = null;
                            while (dr.Read())
                            {
                                entidad_2 = new Cls_Ent_Modulo();

                                if (dr.IsDBNull(pos_ID_SISTEMA_MODULO)) entidad_2.ID_MODULO = 0;
                                else entidad_2.ID_MODULO = Convert.ToInt32(dr[pos_ID_SISTEMA_MODULO]);
                                if (dr.IsDBNull(pos_ID_SISTEMA_MODULO_PADRE)) entidad_2.ID_MODULO_PADRE = 0;
                                else entidad_2.ID_MODULO_PADRE = Convert.ToInt32(dr[pos_ID_SISTEMA_MODULO_PADRE]);

                                if (dr.IsDBNull(pos_ID_A)) entidad_2.ID_A = "";
                                else entidad_2.ID_A = Convert.ToString(dr[pos_ID_A]);
                                if (dr.IsDBNull(pos_ID_LI)) entidad_2.ID_LI = "";
                                else entidad_2.ID_LI = Convert.ToString(dr[pos_ID_LI]);
                                if (dr.IsDBNull(pos_IMAGEN)) entidad_2.IMAGEN = "";
                                else entidad_2.IMAGEN = Convert.ToString(dr[pos_IMAGEN]);
                                if (dr.IsDBNull(pos_DESC_MODULO)) entidad_2.DESC_MODULO = "";
                                else entidad_2.DESC_MODULO = Convert.ToString(dr[pos_DESC_MODULO]);
                                if (dr.IsDBNull(pos_ORDEN)) entidad_2.ORDEN = 0;
                                else entidad_2.ORDEN = Convert.ToInt32(dr[pos_ORDEN]);

                                if (dr.IsDBNull(pos_NIVEL)) entidad_2.NIVEL = 0;
                                else entidad_2.NIVEL = Convert.ToInt32(dr[pos_NIVEL]);

                                if (dr.IsDBNull(pos_FLG_ESTADO)) entidad_2.FLG_ESTADO = 0;
                                else entidad_2.FLG_ESTADO = Convert.ToInt32(dr[pos_FLG_ESTADO]);

                                lista.Add(entidad_2);
                            }
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Modulo>();
            }
            return lista;
        }

        public void Perfiles_Modulos_Registrar(Cls_Ent_Sistemas_Perfiles entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_SEG_PERF_MODU_REGISTRAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PERFIL", SqlDbType.Int)).Value = entidad.ID_PERFIL;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_MODULO", SqlDbType.Int)).Value = entidad.ID_MODULO;
                    cmd.Parameters.Add(new SqlParameter("@PI_USU_CREACION", SqlDbType.VarChar, 200)).Value = entidad.USU_CREACION;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_MENSAJE", SqlDbType.VarChar, 200)).Direction = System.Data.ParameterDirection.Output;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                        string PO_MENSAJE = cmd.Parameters["PO_MENSAJE"].Value.ToString();
                        if (PO_VALIDO == "0")
                        {
                            auditoria.Rechazar(PO_MENSAJE);
                        }
                    }
                    catch (Exception ex)
                    {
                        auditoria.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Perfiles_Modulos_Eliminar(Cls_Ent_Sistemas_Perfiles entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_SEG_PERF_MODU_ELIMINAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PERFIL", SqlDbType.Int)).Value = entidad.ID_PERFIL;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_MODULO", SqlDbType.Int)).Value = entidad.ID_PERFIL;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_MENSAJE", SqlDbType.VarChar, 200)).Direction = System.Data.ParameterDirection.Output;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                        string PO_MENSAJE = cmd.Parameters["PO_MENSAJE"].Value.ToString();
                        if (PO_VALIDO == "0")
                        {
                            auditoria.Rechazar(PO_MENSAJE);
                        }
                    }
                    catch (Exception ex)
                    {
                        auditoria.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        } 

    }
}
