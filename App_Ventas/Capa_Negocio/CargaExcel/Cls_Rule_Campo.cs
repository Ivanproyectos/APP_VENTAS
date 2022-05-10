using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.CargaExcel;
using Capa_Datos.CargaExcel;

namespace Capa_Negocio.CargaExcel
{
    public class Cls_Rule_Campo
    {
        private Cls_Dat_Campo OData = new Cls_Dat_Campo();
        public List<Cls_Ent_Campo> Campo_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Campo_Listar(ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Campo>();
            }
        }

     

    }
}
