using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Administracion;
namespace Capa_Entidad.CargaExcel
{
    public class Cls_Ent_Campo : Base.Cls_Ent_Base
    {
        public int ID_CAMPO { get; set; }
        public string COD_CAMPO { get; set; }
        public string DESCRIPCION_CAMPO { get; set; }
        public int NRO_CAMPO { get; set; }
        public string DATO_EJEMPLO { get; set; }
  
    }
}
