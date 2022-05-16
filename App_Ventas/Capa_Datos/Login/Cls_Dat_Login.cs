using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Login;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

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

    }
}
