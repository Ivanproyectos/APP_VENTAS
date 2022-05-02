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
       public int CANTIDAD { get; set; }
       public int ID_PRODUCTO { get; set; }

       public List<Cls_Ent_Translado_Producto> ListaDetalle { get; set; }
    }
}
