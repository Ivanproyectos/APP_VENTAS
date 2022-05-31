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

namespace Capa_Datos.Administracion
{
    public class Cls_Dat_Usuario_Perfil : Protected.DataBaseHelper
    {
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista los  cargo *************************************************/

        public List<Cls_Ent_Usuario_Perfil> Usuario_Perfil_Listar(Cls_Ent_Usuario_Perfil entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Usuario_Perfil> lista = new List<Cls_Ent_Usuario_Perfil>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_USUARIO_PERFIL_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_USUARIO", SqlDbType.Int)).Value = entidad_param.ID_USUARIO; 
                    dr = cmd.ExecuteReader();
                    int pos_ID_USUARIO_PERFIL = dr.GetOrdinal("ID_USUARIO_PERFIL");
                    int pos_DESC_SUCURSAL = dr.GetOrdinal("DESC_SUCURSAL");
                    int pos_DESC_PERFIL = dr.GetOrdinal("DESC_PERFIL");
                    int pos_FEC_ACTIVACION = dr.GetOrdinal("FEC_ACTIVACION");
                    int pos_FEC_DESACTIVACION = dr.GetOrdinal("FEC_DESACTIVACION");
           
                    int pos_FLG_ESTADO = dr.GetOrdinal("FLG_ESTADO");
                    int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                    int pos_FECHA_CREACION = dr.GetOrdinal("FECHA_CREACION");
                    int pos_USU_MODIFICACION = dr.GetOrdinal("USU_MODIFICACION");
                    int pos_FEC_MODIFICACION = dr.GetOrdinal("FECHA_MODIFICACION");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Usuario_Perfil obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Usuario_Perfil();
                            if (dr.IsDBNull(pos_ID_USUARIO_PERFIL)) obj.ID_USUARIO_PERFIL = 0;
                            else obj.ID_USUARIO_PERFIL = int.Parse(dr[pos_ID_USUARIO_PERFIL].ToString());

                            if (dr.IsDBNull(pos_DESC_SUCURSAL)) obj.DESC_SUCURSAL = "";
                            else obj.DESC_SUCURSAL = dr.GetString(pos_DESC_SUCURSAL);

                            if (dr.IsDBNull(pos_DESC_PERFIL)) obj.DESC_PERFIL = "";
                            else obj.DESC_PERFIL = dr.GetString(pos_DESC_PERFIL);

                            if (dr.IsDBNull(pos_FEC_ACTIVACION)) obj.FEC_ACTIVACION = "";
                            else obj.FEC_ACTIVACION = dr.GetString(pos_FEC_ACTIVACION);

                            if (dr.IsDBNull(pos_FEC_DESACTIVACION)) obj.FEC_DESACTIVACION = "";
                            else obj.FEC_DESACTIVACION = dr.GetString(pos_FEC_DESACTIVACION);


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

        ///*********************************************** Inserta sucursal  *************************************************/

        public void Usuario_Perfil_Insertar(Cls_Ent_Usuario_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_USUARIO_PERFIL_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_SUCURSAL", SqlDbType.Int)).Value = entidad.ID_SUCURSAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_USUARIO", SqlDbType.Int)).Value = entidad.ID_USUARIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_PERFIL", SqlDbType.Int)).Value = entidad.ID_PERFIL;
                    cmd.Parameters.Add(new SqlParameter("@PI_FEC_ACTIVACION", SqlDbType.VarChar, 200)).Value = entidad.FEC_ACTIVACION;
                    cmd.Parameters.Add(new SqlParameter("@PI_FEC_DESACTIVACION", SqlDbType.VarChar,200)).Value = entidad.FEC_DESACTIVACION;     
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

        ///*********************************************** Elimina sucursal  *************************************************/

        public void Usuario_Perfil_Eliminar(Cls_Ent_Usuario_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_USUARIO_PERFIL_ELIMINAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_USUARIO_PERFIL", SqlDbType.Int)).Value = entidad.ID_USUARIO_PERFIL;
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

        public void Usuario_Perfil_Estado(Cls_Ent_Usuario_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_USUARIO_PERFIL_ESTADO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_USUARIO_PERFIL", SqlDbType.Int)).Value = entidad.ID_USUARIO_PERFIL;
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

    }
}
