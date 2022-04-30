using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Inventario
{
   public class Cls_Ent_Translado_Producto : Base.Cls_Ent_Base
    {
       public int ID_TRANSLADO { get; set; }
       public int ID_SUCURSAL_ORIGEN { get; set; }
       public int ID_SUCURSAL_DESTINO { get; set; }
       public string DETALLE { get; set; }
       public List<Cls_Ent_Movimiento_Producto> Lista_Detalle { get; set; }
    }
}
