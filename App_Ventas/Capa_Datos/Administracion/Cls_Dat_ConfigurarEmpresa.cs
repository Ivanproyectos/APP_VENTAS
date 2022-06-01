using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Base; 
using Capa_Entidad.Administracion;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos.Administracion
{
    public class Cls_Dat_ConfigurarEmpresa : Protected.DataBaseHelper
    {
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista los  cargo *************************************************/
        ///
        public Cls_Ent_configurarEmpresa configurarEmpresa_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            Cls_Ent_configurarEmpresa obj = new Cls_Ent_configurarEmpresa();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_CONFIGURAREMPRESA_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    int pos_ID_CONFIGURACION = dr.GetOrdinal("ID_CONFIGURACION");
                    int pos_RUC = dr.GetOrdinal("RUC");
                    int pos_RAZON_SOCIAL = dr.GetOrdinal("RAZON_SOCIAL");
                    int pos_NOMBRE_COMERCIAL = dr.GetOrdinal("NOMBRE_COMERCIAL");
                    int pos_URBANIZACION = dr.GetOrdinal("URBANIZACION");
                    int pos_DIRECCION_FISCAL = dr.GetOrdinal("DIRECCION_FISCAL");             
                    int pos_COD_UBIGEO = dr.GetOrdinal("COD_UBIGEO");
                    int pos_TELEFONO = dr.GetOrdinal("TELEFONO");
                    int pos_NOMBRE_IMPUESTO = dr.GetOrdinal("NOMBRE_IMPUESTO");
                    int pos_IMPUESTO = dr.GetOrdinal("IMPUESTO");
                    int pos_FLG_IMPRIMIR = dr.GetOrdinal("FLG_IMPRIMIR");
                    int pos_FLG_IMPUESTO = dr.GetOrdinal("FLG_IMPUESTO");
                    int pos_SIMBOLO_MONEDA = dr.GetOrdinal("SIMBOLO_MONEDA");
                    int pos_CORREO = dr.GetOrdinal("CORREO");
                    int pos_CODIGO_ARCHIVO_ISOTIPO = dr.GetOrdinal("CODIGO_ARCHIVO_ISOTIPO");
                    int pos_DNOMBRE_ARCHIVO_ISOTIPO = dr.GetOrdinal("NOMBRE_ARCHIVO_ISOTIPO");
                    int pos_EXTENSION_ARCHIVO_ISOTIPO = dr.GetOrdinal("EXTENSION_ARCHIVO_ISOTIPO");

                    int pos_CODIGO_ARCHIVO_LOGO = dr.GetOrdinal("CODIGO_ARCHIVO_LOGO");
                    int pos_NOMBRE_ARCHIVO_LOGO = dr.GetOrdinal("NOMBRE_ARCHIVO_LOGO");
                    int pos_EXTENSION_ARCHIVO_LOGO = dr.GetOrdinal("EXTENSION_ARCHIVO_LOGO");
                    int pos_DESC_UBIGEO = dr.GetOrdinal("DESC_UBIGEO");
                    
                    if (dr.HasRows)
                    {
                        //Cls_Ent_configurarEmpresa obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_configurarEmpresa();
                            if (dr.IsDBNull(pos_ID_CONFIGURACION)) obj.ID_CONFIGURACION = 0;
                            else obj.ID_CONFIGURACION = int.Parse(dr[pos_ID_CONFIGURACION].ToString());

                            if (dr.IsDBNull(pos_RUC)) obj.RUC = "";
                            else obj.RUC = dr.GetString(pos_RUC);
                            if (dr.IsDBNull(pos_RAZON_SOCIAL)) obj.RAZON_SOCIAL = "";
                            else obj.RAZON_SOCIAL = dr.GetString(pos_RAZON_SOCIAL);

                            if (dr.IsDBNull(pos_URBANIZACION)) obj.URBANIZACION = "";
                            else obj.URBANIZACION = dr.GetString(pos_URBANIZACION);

                            if (dr.IsDBNull(pos_NOMBRE_COMERCIAL)) obj.NOMBRE_COMERCIAL = "";
                            else obj.NOMBRE_COMERCIAL = dr.GetString(pos_NOMBRE_COMERCIAL);

                            if (dr.IsDBNull(pos_DIRECCION_FISCAL)) obj.DIRECCION_FISCAL = "";
                            else obj.DIRECCION_FISCAL = dr.GetString(pos_DIRECCION_FISCAL);

                            if (dr.IsDBNull(pos_COD_UBIGEO)) obj.COD_UBIGEO = "";
                            else obj.COD_UBIGEO = dr.GetString(pos_COD_UBIGEO);

                            if (dr.IsDBNull(pos_TELEFONO)) obj.TELEFONO = "";
                            else obj.TELEFONO = dr.GetString(pos_TELEFONO);

                            if (dr.IsDBNull(pos_NOMBRE_IMPUESTO)) obj.NOMBRE_IMPUESTO = "";
                            else obj.NOMBRE_IMPUESTO = dr.GetString(pos_NOMBRE_IMPUESTO);

                            if (dr.IsDBNull(pos_IMPUESTO)) obj.IMPUESTO = 0;
                            else obj.IMPUESTO = decimal.Parse(dr[pos_IMPUESTO].ToString());

                            if (dr.IsDBNull(pos_FLG_IMPRIMIR)) obj.FLG_IMPRIMIR = 0;
                            else obj.FLG_IMPRIMIR = int.Parse(dr[pos_FLG_IMPRIMIR].ToString());

                            if (dr.IsDBNull(pos_FLG_IMPUESTO)) obj.FLG_IMPUESTO = 0;
                            else obj.FLG_IMPUESTO = int.Parse(dr[pos_FLG_IMPUESTO].ToString());

                            if (dr.IsDBNull(pos_SIMBOLO_MONEDA)) obj.SIMBOLO_MONEDA = "";
                            else obj.SIMBOLO_MONEDA = dr.GetString(pos_SIMBOLO_MONEDA);

                            if (dr.IsDBNull(pos_CORREO)) obj.CORREO = "";
                            else obj.CORREO = dr.GetString(pos_CORREO);

                            if (dr.IsDBNull(pos_CODIGO_ARCHIVO_ISOTIPO)) obj.CODIGO_ARCHIVO_ISOTIPO = "";
                            else obj.CODIGO_ARCHIVO_ISOTIPO = dr.GetString(pos_CODIGO_ARCHIVO_ISOTIPO);

                            if (dr.IsDBNull(pos_DNOMBRE_ARCHIVO_ISOTIPO)) obj.NOMBRE_ARCHIVO_ISOTIPO = "";
                            else obj.NOMBRE_ARCHIVO_ISOTIPO = dr.GetString(pos_DNOMBRE_ARCHIVO_ISOTIPO);

                            if (dr.IsDBNull(pos_EXTENSION_ARCHIVO_ISOTIPO)) obj.EXTENSION_ARCHIVO_ISOTIPO = "";
                            else obj.EXTENSION_ARCHIVO_ISOTIPO = dr.GetString(pos_EXTENSION_ARCHIVO_ISOTIPO);

                            if (dr.IsDBNull(pos_CODIGO_ARCHIVO_LOGO)) obj.CODIGO_ARCHIVO_LOGO = "";
                            else obj.CODIGO_ARCHIVO_LOGO = dr.GetString(pos_CODIGO_ARCHIVO_LOGO);

                            if (dr.IsDBNull(pos_NOMBRE_ARCHIVO_LOGO)) obj.NOMBRE_ARCHIVO_LOGO = "";
                            else obj.NOMBRE_ARCHIVO_LOGO = dr.GetString(pos_NOMBRE_ARCHIVO_LOGO);

                            if (dr.IsDBNull(pos_EXTENSION_ARCHIVO_LOGO)) obj.EXTENSION_ARCHIVO_LOGO = "";
                            else obj.EXTENSION_ARCHIVO_LOGO = dr.GetString(pos_EXTENSION_ARCHIVO_LOGO);

                            if (dr.IsDBNull(pos_DESC_UBIGEO)) obj.DESC_UBIGEO = "";
                            else obj.DESC_UBIGEO = dr.GetString(pos_DESC_UBIGEO);

                            //lista.Add(obj);
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

        ///*********************************************** Inserta   *************************************************/
        ///
        public void ConfigurarEmpresa_Insertar(Cls_Ent_configurarEmpresa entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                if (entidad.Archivo_Logo == null)
                entidad.Archivo_Logo = new Cls_Ent_Archivo();
                if (entidad.Archivo_Isotipo == null)
                entidad.Archivo_Isotipo = new Cls_Ent_Archivo(); 

                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_CONFIGURAREMPRESA_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_RUC", SqlDbType.VarChar, 200)).Value = entidad.RUC;
                    cmd.Parameters.Add(new SqlParameter("@PI_RAZON_SOCIAL", SqlDbType.VarChar, 200)).Value = entidad.RAZON_SOCIAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_COMERCIAL", SqlDbType.VarChar, 200)).Value = entidad.NOMBRE_COMERCIAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_URBANIZACION", SqlDbType.VarChar, 200)).Value = entidad.URBANIZACION;
                    cmd.Parameters.Add(new SqlParameter("@PI_DIRECCION_FISCAL", SqlDbType.VarChar, 200)).Value = entidad.DIRECCION_FISCAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_COD_UBIGEO", SqlDbType.VarChar, 200)).Value = entidad.COD_UBIGEO;
                    cmd.Parameters.Add(new SqlParameter("@PI_TELEFONO", SqlDbType.VarChar, 200)).Value = entidad.TELEFONO;
                    cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_IMPUESTO", SqlDbType.VarChar, 200)).Value = entidad.NOMBRE_IMPUESTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_IMPUESTO", SqlDbType.Decimal)).Value = entidad.IMPUESTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_IMPRIMIR", SqlDbType.Int)).Value = entidad.FLG_IMPRIMIR;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_IMPUESTO", SqlDbType.Int)).Value = entidad.FLG_IMPUESTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_SIMBOLO_MONEDA", SqlDbType.VarChar, 200)).Value = entidad.SIMBOLO_MONEDA;
                    cmd.Parameters.Add(new SqlParameter("@PI_CORREO", SqlDbType.VarChar, 200)).Value = entidad.CORREO;

                    if (entidad.Archivo_Isotipo.CODIGO_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_CODIGO_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value =  DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_CODIGO_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Isotipo.CODIGO_ARCHIVO ; }

                     if (entidad.Archivo_Isotipo.NOMBRE_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value =  DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Isotipo.NOMBRE_ARCHIVO; }

                    if (entidad.Archivo_Isotipo.EXTENSION == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value =  DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Isotipo.EXTENSION; }

                    if (entidad.Archivo_Logo.CODIGO_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_CODIGO_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value =  DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_CODIGO_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Logo.CODIGO_ARCHIVO; }

                     if (entidad.Archivo_Logo.NOMBRE_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value =  DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Logo.NOMBRE_ARCHIVO ; }

                    if (entidad.Archivo_Logo.EXTENSION == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value =  DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Logo.EXTENSION; }
                    cmd.Parameters.Add(new SqlParameter("@PI_USUARIO_CREACION", SqlDbType.VarChar, 200)).Value = entidad.USU_CREACION;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                    if (PO_VALIDO == "0")
                    {
                        auditoria.Rechazar("Error al insertar ConfigEmpresa");
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

        ///*********************************************** ACTAULIZAR   *************************************************/
        ///
        public void ConfigurarEmpresa_Actualizar(Cls_Ent_configurarEmpresa entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                if (entidad.Archivo_Logo == null)
                    entidad.Archivo_Logo = new Cls_Ent_Archivo();
                if (entidad.Archivo_Isotipo == null)
                    entidad.Archivo_Isotipo = new Cls_Ent_Archivo();

                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_ADMIN_CONFIGURAREMPRESA_ACTUALIZAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_CONFIGURACION", SqlDbType.Int)).Value = entidad.ID_CONFIGURACION;
                    cmd.Parameters.Add(new SqlParameter("@PI_RUC", SqlDbType.VarChar, 200)).Value = entidad.RUC;
                    cmd.Parameters.Add(new SqlParameter("@PI_RAZON_SOCIAL", SqlDbType.VarChar, 200)).Value = entidad.RAZON_SOCIAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_COMERCIAL", SqlDbType.VarChar, 200)).Value = entidad.NOMBRE_COMERCIAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_URBANIZACION", SqlDbType.VarChar, 200)).Value = entidad.URBANIZACION;
                    cmd.Parameters.Add(new SqlParameter("@PI_DIRECCION_FISCAL", SqlDbType.VarChar, 200)).Value = entidad.DIRECCION_FISCAL;
                    cmd.Parameters.Add(new SqlParameter("@PI_COD_UBIGEO", SqlDbType.VarChar, 200)).Value = entidad.COD_UBIGEO;
                    cmd.Parameters.Add(new SqlParameter("@PI_TELEFONO", SqlDbType.VarChar, 200)).Value = entidad.TELEFONO;
                    cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_IMPUESTO", SqlDbType.VarChar, 200)).Value = entidad.NOMBRE_IMPUESTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_IMPUESTO", SqlDbType.Decimal)).Value = entidad.IMPUESTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_IMPRIMIR", SqlDbType.Int)).Value = entidad.FLG_IMPRIMIR;
                    cmd.Parameters.Add(new SqlParameter("@PI_FLG_IMPUESTO", SqlDbType.Int)).Value = entidad.FLG_IMPUESTO;
                    cmd.Parameters.Add(new SqlParameter("@PI_SIMBOLO_MONEDA", SqlDbType.VarChar, 200)).Value = entidad.SIMBOLO_MONEDA;
                    cmd.Parameters.Add(new SqlParameter("@PI_CORREO", SqlDbType.VarChar, 200)).Value = entidad.CORREO;

                    if (entidad.Archivo_Isotipo.CODIGO_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_CODIGO_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value = "0";  }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_CODIGO_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Isotipo.CODIGO_ARCHIVO; }

                    if (entidad.Archivo_Isotipo.NOMBRE_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Isotipo.NOMBRE_ARCHIVO; }

                    if (entidad.Archivo_Isotipo.EXTENSION == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION_ARCHIVO_ISOTIPO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Isotipo.EXTENSION; }

                    if (entidad.Archivo_Logo.CODIGO_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_CODIGO_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value = "0";  }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_CODIGO_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Logo.CODIGO_ARCHIVO; }

                    if (entidad.Archivo_Logo.NOMBRE_ARCHIVO == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_NOMBRE_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Logo.NOMBRE_ARCHIVO; }

                    if (entidad.Archivo_Logo.EXTENSION == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_EXTENSION_ARCHIVO_LOGO", SqlDbType.VarChar, 100)).Value = entidad.Archivo_Logo.EXTENSION; }
                    cmd.Parameters.Add(new SqlParameter("@PI_USU_MODIFICACION", SqlDbType.VarChar, 200)).Value = entidad.USU_MODIFICACION;
                    cmd.Parameters.Add(new SqlParameter("PO_VALIDO", SqlDbType.Int)).Direction = System.Data.ParameterDirection.Output;
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    cmd.ExecuteReader();
                    string PO_VALIDO = cmd.Parameters["PO_VALIDO"].Value.ToString();
                    if (PO_VALIDO == "0")
                    {
                        auditoria.Rechazar("Error al actualizar ConfigEmpresa");
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
