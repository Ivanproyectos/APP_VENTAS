using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Inventario;
using Capa_Negocio.Inventario;

namespace App_Ventas.Areas.Recursiva.Repositorio
{
    public class ProductosRepostiorio : IDisposable
    {
          private Cls_Rule_Producto _rule = new Cls_Rule_Producto();

     

             public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        
    }
}