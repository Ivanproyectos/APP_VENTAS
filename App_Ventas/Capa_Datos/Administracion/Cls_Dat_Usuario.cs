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
    public class Cls_Dat_Usuario : Protected.DataBaseHelper
    {
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista los  cargo *************************************************/

        public List<Cls_Ent_Usuario> Usuario_Listar(Cls_Ent_Usuario entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Usuario> lista = new List<Cls_Ent_Usuario>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_USUARIO_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (entidad_param.NOMBRES_APE == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRES_APE", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRES_APE", SqlDbType.VarChar, 200)).Value = entidad_param.NOMBRES_APE; }

                    if (entidad_param.DNI == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_NUMERO_DOCUMENTO", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_NUMERO_DOCUMENTO", SqlDbType.VarChar, 200)).Value = entidad_param.DNI; }

                    if (entidad_param.FLG_ESTADO == 2)
                    { cmd.Parameters.Add(new SqlParameter("@PI_FLG_ESTADO", SqlDbType.Int)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_FLG_ESTADO", SqlDbType.Int)).Value = entidad_param.FLG_ESTADO; }
                    dr = cmd.ExecuteReader();
                    int pos_ID_USUARIO = dr.GetOrdinal("ID_USUARIO");
                    int pos_NOMBRES_APE = dr.GetOrdinal("NOMBRES_APE");
                    int pos_DNI = dr.GetOrdinal("DNI");
                    int pos_ID_TIPO_DOCUMENTO = dr.GetOrdinal("ID_TIPO_DOCUMENTO");
                    int pos_CELULAR = dr.GetOrdinal("CELULAR");
                    int pos_TELEFONO = dr.GetOrdinal("TELEFONO");
                    int pos_CORREO = dr.GetOrdinal("CORREO");
                    int pos_COD_USUARIO = dr.GetOrdinal("COD_USUARIO");
                    int pos_CLAVE_USUARIO = dr.GetOrdinal("CLAVE_USUARIO");
                    int pos_FLG_ADMIN = dr.GetOrdinal("FLG_ADMIN");
                    int pos_DESC_TIPO_DOCUMENTO = dr.GetOrdinal("DESC_TIPO_DOCUMENTO");

                    int pos_FLG_ESTADO = dr.GetOrdinal("FLG_ESTADO");
                    int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                    int pos_FECHA_CREACION = dr.GetOrdinal("FECHA_CREACION");
                    int pos_USU_MODIFICACION = dr.GetOrdinal("USU_MODIFICACION");
                    int pos_FEC_MODIFICACION = dr.GetOrdinal("FECHA_MODIFICACION");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Usuario obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Usuario();
                            if (dr.IsDBNull(pos_ID_USUARIO)) obj.ID_USUARIO = 0;
                            else obj.ID_USUARIO = int.Parse(dr[pos_ID_USUARIO].ToString());

                            if (dr.IsDBNull(pos_NOMBRES_APE)) obj.NOMBRES_APE = "";
                            else obj.NOMBRES_APE = dr.GetString(pos_NOMBRES_APE);

                            if (dr.IsDBNull(pos_DNI)) obj.DNI = "";
                            else obj.DNI = dr.GetString(pos_DNI);

                            if (dr.IsDBNull(pos_ID_TIPO_DOCUMENTO)) obj.ID_TIPO_DOCUMENTO = 0;
                            else obj.ID_TIPO_DOCUMENTO = int.Parse(dr[pos_ID_TIPO_DOCUMENTO].ToString());

                            if (dr.IsDBNull(pos_CORREO)) obj.CORREO = "";
                            else obj.CORREO = dr.GetString(pos_CORREO);

                            if (dr.IsDBNull(pos_CELULAR)) obj.CELULAR = "";
                            else obj.CELULAR = dr.GetString(pos_CELULAR);

                            if (dr.IsDBNull(pos_TELEFONO)) obj.TELEFONO = "";
                            else obj.TELEFONO = dr.GetString(pos_TELEFONO);

                            if (dr.IsDBNull(pos_COD_USUARIO)) obj.COD_USUARIO = "";
                            else obj.COD_USUARIO = dr.GetString(pos_COD_USUARIO);

                            if (dr.IsDBNull(pos_CLAVE_USUARIO)) obj.CLAVE_USUARIO = "";
                            else obj.CLAVE_USUARIO = dr.GetString(pos_CLAVE_USUARIO);


                            if (dr.IsDBNull(pos_DESC_TIPO_DOCUMENTO)) obj.DESC_TIPO_DOCUMENTO = "";
                            else obj.DESC_TIPO_DOCUMENTO = dr.GetString(pos_DESC_TIPO_DOCUMENTO);

                            if (dr.IsDBNull(pos_FLG_ADMIN)) obj.FLG_ADMIN = 0;
                            else obj.FLG_ADMIN = int.Parse(dr[pos_FLG_ADMIN].ToString());


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

        ///*********************************************** Lista sucursal por id *************************************************/

        public Cls_Ent_Usuario Usuario_Listar_Uno(Cls_Ent_Usuario entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            Cls_Ent_Usuario obj = new Cls_Ent_Usuario();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_USUARIO_LISTAR_UNO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_USUARIO", SqlDbType.BigInt)).Value = entidad_param.ID_USUARIO;
                    dr = cmd.ExecuteReader();
                    int pos_ID_USUARIO = dr.GetOrdinal("ID_USUARIO");
                    int pos_NOMBRE = dr.GetOrdinal("NOMBRE");
                    int pos_APE_PATERNO = dr.GetOrdinal("APE_PATERNO");
                    int pos_APE_MATERNO = dr.GetOrdinal("APE_MATERNO");
                    int pos_DNI = dr.GetOrdinal("DNI");
                    int pos_ID_TIPO_DOCUMENTO = dr.GetOrdinal("ID_TIPO_DOCUMENTO");
                    int pos_CELULAR = dr.GetOrdinal("CELULAR");
                    int pos_TELEFONO = dr.GetOrdinal("TELEFONO");
                    int pos_CORREO = dr.GetOrdinal("CORREO");
                    int pos_COD_USUARIO = dr.GetOrdinal("COD_USUARIO");
                    int pos_CLAVE_USUARIO = dr.GetOrdinal("CLAVE_USUARIO");
                    int pos_FLG_ADMIN = dr.GetOrdinal("FLG_ADMIN");
                    if (dr.HasRows)
                    {
                        //Cls_Ent_Usuario obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Usuario();
                            if (dr.IsDBNull(pos_ID_USUARIO)) obj.ID_USUARIO = 0;
                            else obj.ID_USUARIO = int.Parse(dr[pos_ID_USUARIO].ToString());

                            if (dr.IsDBNull(pos_NOMBRE)) obj.NOMBRE = "";
                            else obj.NOMBRE = dr.GetString(pos_NOMBRE);

                            if (dr.IsDBNull(pos_APE_PATERNO)) obj.APE_PATERNO = "";
                            else obj.APE_PATERNO = dr.GetString(pos_APE_PATERNO);

                            if (dr.IsDBNull(pos_APE_MATERNO)) obj.APE_MATERNO = "";
                            else obj.APE_MATERNO = dr.GetString(pos_APE_MATERNO);

                            if (dr.IsDBNull(pos_DNI)) obj.DNI = "";
                            else obj.DNI = dr.GetString(pos_DNI);

                            if (dr.IsDBNull(pos_ID_TIPO_DOCUMENTO)) obj.ID_TIPO_DOCUMENTO = 0;
                            else obj.ID_TIPO_DOCUMENTO = int.Parse(dr[pos_ID_TIPO_DOCUMENTO].ToString());

                            if (dr.IsDBNull(pos_CORREO)) obj.CORREO = "";
                            else obj.CORREO = dr.GetString(pos_CORREO);

                            if (dr.IsDBNull(pos_CELULAR)) obj.CELULAR = "";
                            else obj.CELULAR = dr.GetString(pos_CELULAR);

                            if (dr.IsDBNull(pos_TELEFONO)) obj.TELEFONO = "";
                            else obj.TELEFONO = dr.GetString(pos_TELEFONO);

                            if (dr.IsDBNull(pos_COD_USUARIO)) obj.COD_USUARIO = "";
                            else obj.COD_USUARIO = dr.GetString(pos_COD_USUARIO);

                            if (dr.IsDBNull(pos_CLAVE_USUARIO)) obj.CLAVE_USUARIO = "";
                            else obj.CLAVE_USUARIO = dr.GetString(pos_CLAVE_USUARIO);



                            if (dr.IsDBNull(pos_FLG_ADMIN)) obj.FLG_ADMIN = 0;
                            else obj.FLG_ADMIN = int.Parse(dr[pos_FLG_ADMIN].ToString());
                      
           
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

        public void Usuario_Insertar(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_USUARIO_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE", SqlDbType.VarChar, 200)).Value = entidad.NOMBRE;
                    cmd.Parameters.Add(new SqlParameter("@PI_APE_PATERNO", SqlDbType.VarChar, 200)).Value = entidad.APE_PATERNO;
                    cmd.Parameters.Add(new SqlParameter("@PI_APE_MATERNO", SqlDbType.VarChar, 200)).Value = entidad.APE_MATERNO;
                    cmd.Parameters.Add(new SqlParameter("@PI_DNI", SqlDbType.VarChar, 200)).Value = entidad.DNI;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_TIPO_DOCUMENTO", SqlDbType.Int)).Value = entidad.ID_TIPO_DOCUMENTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_CELULAR", SqlDbType.VarChar, 9)).Value = entidad.CELULAR;
                    cmd.Parameters.Add(new SqlParameter("@PI_TELEFONO", SqlDbType.VarChar, 7)).Value = entidad.TELEFONO;
                    cmd.Parameters.Add(new SqlParameter("@PI_CORREO", SqlDbType.VarChar, 200)).Value = entidad.CORREO;
                    cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar, 200)).Value = entidad.COD_USUARIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_CLAVE_USUARIO", SqlDbType.VarChar, 200)).Value = entidad.CLAVE_USUARIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_ADMIN", SqlDbType.Int)).Value = entidad.FLG_ADMIN;
                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_CREACION", SqlDbType.VarChar, 200)).Value = entidad.USU_CREACION;
                    cmd.Parameters.Add(new SqlParameter("@PI_COLOR_BADGE", SqlDbType.VarChar, 200)).Value = entidad.COLOR_BADGE;
                    cmd.Parameters.Add(new SqlParameter("PO_ID_USUARIO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("PO_MENSAJE", SqlDbType.VarChar, 200)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_ID_USUARIO = cmd.Parameters["PO_ID_USUARIO"].Value.ToString();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                    string PO_MENSAJE = cmd.Parameters["PO_MENSAJE"].Value.ToString();
                    if (PO_VALIDO == "0")
                    {
                        auditoria.Rechazar(PO_MENSAJE);
                    }
                    else {
                        auditoria.OBJETO = Convert.ToInt32(PO_ID_USUARIO); 
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

        public void Usuario_Actualizar(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_USUARIO_ACTUALIZAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_USUARIO", SqlDbType.Int)).Value = entidad.ID_USUARIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE", SqlDbType.VarChar, 200)).Value = entidad.NOMBRE;
                    cmd.Parameters.Add(new SqlParameter("@PI_APE_PATERNO", SqlDbType.VarChar, 200)).Value = entidad.APE_PATERNO;
                    cmd.Parameters.Add(new SqlParameter("@PI_APE_MATERNO", SqlDbType.VarChar, 200)).Value = entidad.APE_MATERNO;
                    cmd.Parameters.Add(new SqlParameter("@PI_DNI", SqlDbType.VarChar, 200)).Value = entidad.DNI;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_TIPO_DOCUMENTO", SqlDbType.Int)).Value = entidad.ID_TIPO_DOCUMENTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_CELULAR", SqlDbType.VarChar, 9)).Value = entidad.CELULAR;
                    cmd.Parameters.Add(new SqlParameter("@PI_TELEFONO", SqlDbType.VarChar, 7)).Value = entidad.TELEFONO;
                    cmd.Parameters.Add(new SqlParameter("@PI_CORREO", SqlDbType.VarChar, 200)).Value = entidad.CORREO;
                    cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar, 200)).Value = entidad.COD_USUARIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_CLAVE_USUARIO", SqlDbType.VarChar, 200)).Value = entidad.CLAVE_USUARIO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_ADMIN", SqlDbType.Int)).Value = entidad.FLG_ADMIN;
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

        public void Usuario_Eliminar(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_USUARIO_ELIMINAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_USUARIO", SqlDbType.Int)).Value = entidad.ID_USUARIO;
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

        public void Usuario_Estado(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_USUARIO_ESTADO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_USUARIO", SqlDbType.Int)).Value = entidad.ID_USUARIO;
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
