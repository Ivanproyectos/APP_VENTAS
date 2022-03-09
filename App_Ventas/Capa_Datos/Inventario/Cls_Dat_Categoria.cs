using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Inventario;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos.Inventario
{
    public class Cls_Dat_Categoria : Protected.DataBaseHelper
    {
        ///*********************************************** ----------------- **************************************************/

        ///*********************************************** Lista los  cargo *************************************************/

        public List<Cls_Ent_Categoria> Categoria_Listar(Cls_Ent_Categoria entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Categoria> lista = new List<Cls_Ent_Categoria>();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_INVEN_CATEGORIA_LISTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (entidad_param.DESC_CATEGORIA == null)
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_CATEGORIA", SqlDbType.VarChar, 200)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_DESC_CATEGORIA", SqlDbType.VarChar, 200)).Value = entidad_param.DESC_CATEGORIA; }
                    if (entidad_param.FLG_ESTADO == 2)
                    { cmd.Parameters.Add(new SqlParameter("@PI_FLG_ESTADO", SqlDbType.Int)).Value = DBNull.Value; }
                    else
                    { cmd.Parameters.Add(new SqlParameter("@PI_FLG_ESTADO", SqlDbType.Int)).Value = entidad_param.FLG_ESTADO; }
                    dr = cmd.ExecuteReader();
                    int pos_ID_CATEGORIA = dr.GetOrdinal("ID_CATEGORIA");
                    int pos_DESC_CATEGORIA = dr.GetOrdinal("DESC_CATEGORIA");
                    int pos_DESCRIPCION = dr.GetOrdinal("DESCRIPCION");
               
                    int pos_FLG_ESTADO = dr.GetOrdinal("FLG_ESTADO");
                    int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                    int pos_FECHA_CREACION = dr.GetOrdinal("FECHA_CREACION");
                    int pos_USU_MODIFICACION = dr.GetOrdinal("USU_MODIFICACION");
                    int pos_FEC_MODIFICACION = dr.GetOrdinal("FECHA_MODIFICACION");
                    if (dr.HasRows)
                    {
                        Cls_Ent_Categoria obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Categoria();
                            if (dr.IsDBNull(pos_ID_CATEGORIA)) obj.ID_CATEGORIA = 0;
                            else obj.ID_CATEGORIA = int.Parse(dr[pos_ID_CATEGORIA].ToString());
                            if (dr.IsDBNull(pos_DESC_CATEGORIA)) obj.DESC_CATEGORIA = "";
                            else obj.DESC_CATEGORIA = dr.GetString(pos_DESC_CATEGORIA);

                            if (dr.IsDBNull(pos_DESCRIPCION)) obj.DESCRIPCION = "";
                            else obj.DESCRIPCION = dr.GetString(pos_DESCRIPCION);

                

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

        public Cls_Ent_Categoria Categoria_Listar_Uno(Cls_Ent_Categoria entidad_param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            Cls_Ent_Categoria obj = new Cls_Ent_Categoria();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlDataReader dr = null;
                    SqlCommand cmd = new SqlCommand("USP_INVEN_CATEGORIA_LISTAR_UNO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_CATEGORIA", SqlDbType.BigInt)).Value = entidad_param.ID_CATEGORIA;
                    dr = cmd.ExecuteReader();
                    int pos_ID_CATEGORIA = dr.GetOrdinal("ID_CATEGORIA");
                    int pos_DESC_CATEGORIA = dr.GetOrdinal("DESC_CATEGORIA");
                    int pos_DESCRIPCION = dr.GetOrdinal("DESCRIPCION");

                    if (dr.HasRows)
                    {
                        //Cls_Ent_Categoria obj = null;
                        while (dr.Read())
                        {
                            obj = new Cls_Ent_Categoria();
                            if (dr.IsDBNull(pos_ID_CATEGORIA)) obj.ID_CATEGORIA = 0;
                            else obj.ID_CATEGORIA = int.Parse(dr[pos_ID_CATEGORIA].ToString());
                            if (dr.IsDBNull(pos_DESC_CATEGORIA)) obj.DESC_CATEGORIA = "";
                            else obj.DESC_CATEGORIA = dr.GetString(pos_DESC_CATEGORIA);

                            if (dr.IsDBNull(pos_DESCRIPCION)) obj.DESCRIPCION = "";
                            else obj.DESCRIPCION = dr.GetString(pos_DESCRIPCION);

                      
           
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

        public void Categoria_Insertar(Cls_Ent_Categoria entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_INVEN_CATEGORIA_INSERTAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_DESC_CATEGORIA", SqlDbType.VarChar, 200)).Value = entidad.DESC_CATEGORIA;
                    cmd.Parameters.Add(new SqlParameter("@PI_DESCRIPCION", SqlDbType.VarChar, 1000)).Value = entidad.DESCRIPCION;
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

        ///*********************************************** Actualiza  sucursal  *************************************************/

        public void Categoria_Actualizar(Cls_Ent_Categoria entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_INVEN_CATEGORIA_ACTUALIZAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_CATEGORIA", SqlDbType.Int)).Value = entidad.ID_CATEGORIA;
                    cmd.Parameters.Add(new SqlParameter("@PI_DESC_CATEGORIA", SqlDbType.VarChar, 200)).Value = entidad.DESC_CATEGORIA;
                    cmd.Parameters.Add(new SqlParameter("@PI_DESCRIPCION", SqlDbType.VarChar, 1000)).Value = entidad.DESCRIPCION;
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

        public void Categoria_Eliminar(Cls_Ent_Categoria entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_INVEN_CATEGORIA_ELIMINAR", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_CATEGORIA", SqlDbType.Int)).Value = entidad.ID_CATEGORIA;
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

        public void Categoria_Estado(Cls_Ent_Categoria entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (SqlConnection cn = this.GetNewConnection())
                {
                    SqlCommand cmd = new SqlCommand("USP_INVEN_CATEGORIA_ESTADO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PI_ID_CATEGORIA", SqlDbType.Int)).Value = entidad.ID_CATEGORIA;
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
