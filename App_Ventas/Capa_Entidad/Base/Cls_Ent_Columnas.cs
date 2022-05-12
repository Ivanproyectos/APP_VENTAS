using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Base
{
    public class Cls_Ent_Columnas
    {
        public string ID_COLUMNA { get; set; }
        public string DESCRIPCION_COLUMNA { get; set; }
        public string CELDA_INICIO { get; set; }
        public int INT_CELDAS { get; set; }
        public float TAMANIO { get; set; }
        public bool AUTO_INCREMENTAR { get; set; }
        public string CELDA_FIN { get; set; }
        public List<Cls_Ent_Columnas_Condicion> CONDICIONES { get; set; }
    }

    public class Cls_Ent_Columnas_Condicion
    {
        public string ID_COLUMNA { get; set; }
        public string VALOR { get; set; }
        public UInt32 STYLE_INDEX { get; set; }
    }

    public class Cls_Ent_ErroresExcel
    {
        public int NRO_FILA { get; set; }
        public string DESCRIPCION { get; set; }
    }



}
