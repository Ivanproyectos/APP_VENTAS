using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Cls_Ent_Auditoria
    {
        public object OBJETO { get; set; }
        public object OBJETO2 { get; set; }
        public object OBJETO3 { get; set; }
        public string MENSAJE_SALIDA { get; set; }
        public string ERROR_LOG { get; set; }
        public bool EJECUCION_PROCEDIMIENTO { get; set; }
        public bool RECHAZAR { get; set; }

        public void Limpiar()
        {
            OBJETO = null;
            RECHAZAR = false;
            MENSAJE_SALIDA = "";
            ERROR_LOG = "";
            EJECUCION_PROCEDIMIENTO = true;
        }

        public void Rechazar(string mensaje)
        {
            OBJETO = null;
            RECHAZAR = true;
            MENSAJE_SALIDA = mensaje;
            ERROR_LOG = mensaje;
            EJECUCION_PROCEDIMIENTO = true;
        }

        public void Error(Exception ex)
        {
            OBJETO = null;
            RECHAZAR = true;
            MENSAJE_SALIDA = ex.Message;
            ERROR_LOG = ex.ToString();
            EJECUCION_PROCEDIMIENTO = false;
        }
    }
}
