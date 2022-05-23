﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Negocio.Administracion;

namespace App_Ventas.Areas.Administracion.Repositorio
{
    public class PerfilRepositorio : IDisposable
    {

        private Cls_Rule_Perfil _rule = new Cls_Rule_Perfil();

        public List<Cls_Ent_Perfil> Perfil_Listar(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Perfil_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Perfil>();
            }
        }

        public Cls_Ent_Perfil Perfil_Listar_Uno(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Perfil_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Perfil();
            }
        }



        public void Perfil_Insertar(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Perfil_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Perfil_Actualizar(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Perfil_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Perfil_Estado(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Perfil_Estado(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Perfil_Eliminar(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Perfil_Eliminar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}