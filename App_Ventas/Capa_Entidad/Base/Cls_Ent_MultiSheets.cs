using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Base
{
    public class Cls_Ent_MultiSheets
    {
        public Cls_Ent_MultiSheets() {
            COLUMNS = new List<Cls_Ent_Columnas>();
        }
        public List<Cls_Ent_Columnas> COLUMNS { get; set; }
        public DataTable dt { get; set; }
        public bool ONLYCOLUMN { get; set; }
        public Cls_Ent_Titulo TITLE { get; set; }
        public string NAME_SHEET { get; set; }
        public UInt32 ORDEN_INDEX { get; set; }

    }

    [Serializable]
    public class Cls_Ent_MultiSheets<T> : Cls_Ent_MultiSheets
    {
        private List<T> _LIST;
        public List<T> LIST
        {
            get
            {
                return _LIST;
            }
            set
            {
                _LIST = value;
            }
        }
        public Type Type
        {
            get
            {
                return typeof(T);
            }
        }

 
    }
}
