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
using System.Security.Cryptography;
using Capa_Token; 

namespace Capa_Datos.Login
{
    public class Cls_Dat_Login : Protected.DataBaseHelper
    {
       public List<Cls_Ent_Modulo> Usuario_Sistema_Modulo(long ID_PERFIL, ref Cls_Ent_Auditoria auditoria)
       {
           auditoria.Limpiar();
           List<Cls_Ent_Modulo> lista = new List<Cls_Ent_Modulo>();
           try
           {
               using (SqlConnection cn = this.GetNewConnection())
               {
                   SqlDataReader dr = null;
                   SqlCommand cmd = new SqlCommand("USP_LOGIN_MENU", cn);
                   cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   cmd.Parameters.Add(new SqlParameter("@PI_ID_PERFIL", SqlDbType.BigInt)).Value = ID_PERFIL;
                   cmd.Parameters.Add(new SqlParameter("@PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                   cmd.Parameters.Add(new SqlParameter("@PO_MENSAJE", SqlDbType.VarChar, 200)).Direction = System.Data.ParameterDirection.Output;

                   cmd.ExecuteNonQuery();
                   string PO_VALIDO = cmd.Parameters["@PO_VALIDO"].Value.ToString();
                   string PO_MENSAJE = cmd.Parameters["@PO_MENSAJE"].Value.ToString();
                   dr = cmd.ExecuteReader();

                   if (PO_VALIDO == "0")
                   {
                       auditoria.Rechazar(PO_MENSAJE);
                   }
                   else
                   {
                       int pos_ID_MODULO = dr.GetOrdinal("ID_MODULO");
                       int pos_ID_MODULO_PADRE = dr.GetOrdinal("ID_MODULO_PADRE");
                       int pos_ID_A = dr.GetOrdinal("ID_A");
                       int pos_ID_LI = dr.GetOrdinal("ID_LI");
                       int pos_IMAGEN = dr.GetOrdinal("IMAGEN");
                       int pos_URL_MODULO = dr.GetOrdinal("URL_MODULO");
                       int pos_DESC_MODULO = dr.GetOrdinal("DESC_MODULO");
                       int pos_NIVEL = dr.GetOrdinal("NIVEL");
                       int pos_FLG_LINK = dr.GetOrdinal("FLG_LINK");
                       int pos_FLG_SINGRUPO = dr.GetOrdinal("FLG_SINGRUPO");
                       if (dr.HasRows)
                       {
                           Cls_Ent_Modulo obj = null;
                           while (dr.Read())
                           {
                               obj = new Cls_Ent_Modulo();
                               if (dr.IsDBNull(pos_ID_MODULO)) obj.ID_MODULO = 0;
                               else obj.ID_MODULO = long.Parse(dr[pos_ID_MODULO].ToString());

                               if (dr.IsDBNull(pos_ID_MODULO_PADRE)) obj.ID_MODULO_PADRE = 0;
                               else obj.ID_MODULO_PADRE = long.Parse(dr[pos_ID_MODULO_PADRE].ToString());

                               if (dr.IsDBNull(pos_ID_A)) obj.ID_A = "";
                               else obj.ID_A = dr.GetString(pos_ID_A);

                               if (dr.IsDBNull(pos_ID_LI)) obj.ID_LI = "";
                               else obj.ID_LI = dr.GetString(pos_ID_LI);

                               if (dr.IsDBNull(pos_IMAGEN)) obj.IMAGEN = "";
                               else obj.IMAGEN = dr.GetString(pos_IMAGEN);

                               if (dr.IsDBNull(pos_URL_MODULO)) obj.URL_MODULO = "";
                               else obj.URL_MODULO = dr.GetString(pos_URL_MODULO);

                               if (dr.IsDBNull(pos_DESC_MODULO)) obj.DESC_MODULO = "";
                               else obj.DESC_MODULO = dr.GetString(pos_DESC_MODULO);

                               if (dr.IsDBNull(pos_URL_MODULO)) obj.URL_MODULO = "";
                               else obj.URL_MODULO = dr.GetString(pos_URL_MODULO);
                               if (dr.IsDBNull(pos_NIVEL)) obj.NIVEL = 0;
                               else obj.NIVEL = int.Parse(dr[pos_NIVEL].ToString());

                               if (dr.IsDBNull(pos_FLG_LINK)) obj.FLG_LINK = 0;
                               else obj.FLG_LINK = int.Parse(dr[pos_FLG_LINK].ToString());

                               if (dr.IsDBNull(pos_FLG_SINGRUPO)) obj.FLG_SINGRUPO = 0;
                               else obj.FLG_SINGRUPO = int.Parse(dr[pos_FLG_SINGRUPO].ToString());
                               

                               lista.Add(obj);
                           }
                       }
                       dr.Close();
                   }
               }
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
           }
           return lista;
       }

       public Cls_Ent_Usuario Login_Usuario(Cls_Ent_Usuario entidad_param, ref Cls_Ent_Auditoria auditoria)
       {
           Cls_Ent_Usuario entidad = new Cls_Ent_Usuario();
           auditoria.Limpiar();
           try
           {
               using (SqlConnection cn = this.GetNewConnection())
               {
                   SqlDataReader dr = null;
                   SqlCommand cmd = new SqlCommand("USP_SEG_USUARIO_VALIDAR", cn);
                   cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   cmd.Parameters.Add(new SqlParameter("@PI_COD_USUARIO", SqlDbType.VarChar, 200)).Value = entidad_param.COD_USUARIO;
                   cmd.Parameters.Add(new SqlParameter("@PI_CLAVE_USUARIO", SqlDbType.VarChar, 200)).Value = entidad_param.CLAVE_USUARIO;
                   cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                   cmd.Parameters.Add(new SqlParameter("PO_MENSAJE", SqlDbType.VarChar, 200)).Direction = System.Data.ParameterDirection.Output;
                   dr = cmd.ExecuteReader();
                   if (dr.HasRows)
                   {
                       int pos_ID_USUARIO = dr.GetOrdinal("ID_USUARIO");
                       int pos_COD_USUARIO = dr.GetOrdinal("COD_USUARIO");
                       //Cls_Ent_Usuario obj = null;
                       while (dr.Read())
                       {
                           if (dr.IsDBNull(pos_ID_USUARIO)) entidad.ID_USUARIO = 0;
                           else entidad.ID_USUARIO = int.Parse(dr[pos_ID_USUARIO].ToString());

                           if (dr.IsDBNull(pos_COD_USUARIO)) entidad.COD_USUARIO = "";
                           else entidad.COD_USUARIO = Convert.ToString(dr[pos_COD_USUARIO]);

                       }
                   }
                   dr.Close();
                   int PO_VALIDO = int.Parse(cmd.Parameters["PO_VALIDO"].Value.ToString());
                   string PO_MENSAJE = cmd.Parameters["PO_MENSAJE"].Value.ToString();
                   if (PO_VALIDO == 0)
                   {
                       auditoria.Rechazar(PO_MENSAJE);
                   }
               }
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
               entidad = new Cls_Ent_Usuario();
           }
           return entidad;
       }

       public Cls_Ent_Usuario Usuario(Cls_Ent_Usuario entidad_param, ref Cls_Ent_Auditoria auditoria)
       {
           Cls_Ent_Usuario obj = new Cls_Ent_Usuario();
           auditoria.Limpiar();
           try
           {
               using (SqlConnection cn = this.GetNewConnection())
               {
                   SqlDataReader dr = null;
                   SqlCommand cmd = new SqlCommand("USP_ADMIN_USUARIO_LISTAR_UNO", cn);
                   cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   cmd.Parameters.Add(new SqlParameter("@PI_ID_USUARIO", SqlDbType.Int)).Value = entidad_param.ID_USUARIO;
                   dr = cmd.ExecuteReader();
                   if (dr.HasRows)
                   {
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
                       int pos_NOMBRES_APE = dr.GetOrdinal("NOMBRES_APE");
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

                           if (dr.IsDBNull(pos_FLG_ADMIN)) obj.FLG_ADMIN = 0;
                           else obj.FLG_ADMIN = int.Parse(dr[pos_FLG_ADMIN].ToString());

                           obj.Lista_Sucursales = Usuario_Perfil_Listar(new Cls_Ent_Usuario_Perfil { ID_USUARIO = entidad_param.ID_USUARIO}, ref auditoria); 
                      

                       }
                   }
                   dr.Close();

               }
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
               obj = new Cls_Ent_Usuario();
           }
           return obj;
       }

       ///*********************************************** ----------------- **************************************************/

       ///*********************************************** Lista los sucursales *************************************************/

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

                           obj.ID_USUARIO_PERFIL_HASH =  Cls_Api_Encriptar.Encriptar(dr[pos_ID_USUARIO_PERFIL].ToString()); 

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

       public Cls_Ent_Usuario Usuario_Sistema(Cls_Ent_Usuario entidad_param, ref Cls_Ent_Auditoria auditoria)
       {
           Cls_Ent_Usuario obj = new Cls_Ent_Usuario();
           auditoria.Limpiar();
           try
           {
               using (SqlConnection cn = this.GetNewConnection())
               {
                   SqlDataReader dr = null;
                   SqlCommand cmd = new SqlCommand("USP_SEG_USUARIO_SISTEMA", cn);
                   cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   cmd.Parameters.Add(new SqlParameter("@PI_ID_USUARIO", SqlDbType.Int)).Value = entidad_param.ID_USUARIO;
                   cmd.Parameters.Add(new SqlParameter("@PI_ID_USUARIO_PEFIL", SqlDbType.Int)).Value = entidad_param.Perfil_Sucursal.ID_USUARIO_PERFIL;
                   dr = cmd.ExecuteReader();
                   if (dr.HasRows)
                   {
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
                       int pos_NOMBRES_APE = dr.GetOrdinal("NOMBRES_APE");
                       int pos_ID_PERFIL = dr.GetOrdinal("ID_PERFIL");
                       int pos_ID_SUCURSAL = dr.GetOrdinal("ID_SUCURSAL");
                       int pos_DESC_SUCURSAL = dr.GetOrdinal("DESC_SUCURSAL");
                       int pos_ABREV_USUARIO = dr.GetOrdinal("ABREV_USUARIO");
                       

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

                           if (dr.IsDBNull(pos_ABREV_USUARIO)) obj.ABREV_USUARIO = "";
                           else obj.ABREV_USUARIO = dr.GetString(pos_ABREV_USUARIO);


                           if (dr.IsDBNull(pos_CLAVE_USUARIO)) obj.CLAVE_USUARIO = "";
                           else obj.CLAVE_USUARIO = dr.GetString(pos_CLAVE_USUARIO);

                           if (dr.IsDBNull(pos_FLG_ADMIN)) obj.FLG_ADMIN = 0;
                           else obj.FLG_ADMIN = int.Parse(dr[pos_FLG_ADMIN].ToString());

                           if (dr.IsDBNull(pos_ID_PERFIL)) obj.Perfil_Sucursal.ID_PERFIL = 0;
                           else obj.Perfil_Sucursal.ID_PERFIL = int.Parse(dr[pos_ID_PERFIL].ToString());

                           if (dr.IsDBNull(pos_ID_SUCURSAL)) obj.Perfil_Sucursal.ID_SUCURSAL = 0;
                           else obj.Perfil_Sucursal.ID_SUCURSAL = int.Parse(dr[pos_ID_SUCURSAL].ToString());

                           if (dr.IsDBNull(pos_DESC_SUCURSAL)) obj.Perfil_Sucursal.DESC_SUCURSAL = "";
                           else obj.Perfil_Sucursal.DESC_SUCURSAL = dr.GetString(pos_DESC_SUCURSAL);

                       }
                   }
                   dr.Close();

               }
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
               obj = new Cls_Ent_Usuario();
           }
           return obj;
       }

    }
}
