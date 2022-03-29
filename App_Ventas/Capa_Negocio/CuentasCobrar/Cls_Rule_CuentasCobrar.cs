using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Ventas;
using Capa_Datos.CuentasCobrar;

namespace Capa_Negocio.CuentasCobrar
{
    public class Cls_Rule_CuentasCobrar
    {
        private Cls_Dat_CuentasCobrar OData = new Cls_Dat_CuentasCobrar();


        public void CuentasCobrar_Insertar(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.CuentasCobrar_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }
    }
}
