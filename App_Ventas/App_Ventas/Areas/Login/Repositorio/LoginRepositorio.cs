using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Ventas.Areas.Login.Repositorio
{
    public class LoginRepositorio : IDisposable
    {



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}