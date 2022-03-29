﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Datos.Listados_Combos;

namespace Capa_Negocio.Listados_Combos
{
  public  class Cls_Rule_Listados
    {
      private Cls_Dat_Listados OData = new Cls_Dat_Listados();

      public List<Cls_Ent_Ubigeo> Ubigeo_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Ubigeo_Listar( ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Ubigeo>();
            }
        }

      public List<Cls_Ent_Tipo_Documento> Tipo_Documento_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Tipo_Documento_Listar(ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Tipo_Documento>();
            }
        }

      public List<Cls_Ent_Unidad_Medida> Unidad_Medida_Listar(ref Cls_Ent_Auditoria auditoria)
      {
          try
          {
              return OData.Unidad_Medida_Listar(ref auditoria);
          }
          catch (Exception ex)
          {
              auditoria.Error(ex);
              return new List<Cls_Ent_Unidad_Medida>();
          }
      }


      public List<Cls_Ent_Tipo_Comprobante> Tipo_Comprobante_Listar(ref Cls_Ent_Auditoria auditoria)
      {
          try
          {
              return OData.Tipo_Comprobante_Listar(ref auditoria);
          }
          catch (Exception ex)
          {
              auditoria.Error(ex);
              return new List<Cls_Ent_Tipo_Comprobante>();
          }
      }

      public List<Cls_Ent_Cliente> Clientes_ListarXComprobante(string ID_TIPO_COMPROBANTE ,ref Cls_Ent_Auditoria auditoria)
      {
          try
          {
              return OData.Clientes_ListarXComprobante( ID_TIPO_COMPROBANTE,ref auditoria);
          }
          catch (Exception ex)
          {
              auditoria.Error(ex);
              return new List<Cls_Ent_Cliente>();
          }
      }
      

      

    }
}
