using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.CargaExcel;
using Capa_Entidad.Administracion;
using Capa_Negocio.Inventario;
using Capa_Negocio.Listados_Combos;
using App_Ventas.Areas.Inventario.Models;
using App_Ventas.Areas.Inventario.Repositorio;
using App_Ventas.Areas.Recursiva.Repositorio;
using App_Ventas.Areas.Ventas.Models;
using Capa_Entidad.Inventario;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Data;

namespace App_Ventas.Areas.Inventario
{
    public class Css_Importar
    {
        public void Procesar_CargaMasiva(string RUTA_ARCHIVO_TEMPORAL, int ID_SUCURSAL, ref Cls_Ent_Auditoria auditoria)
        {
            List<Cls_Ent_Producto> items = new List<Cls_Ent_Producto>();
            DataTable midt = ProcesarHojaCalculo(RUTA_ARCHIVO_TEMPORAL, ref auditoria);
            if (auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (!auditoria.RECHAZAR)
                {
                    DataColumnCollection columns = midt.Columns;
                    if (columns.Count != 13) // numero de columnas requerido
                    {
                        auditoria.Rechazar("El archivo cargado no tiene el número de columnas requeridas.");
                    }
                    else
                    {
                        this.ValidarCarga(midt, ID_SUCURSAL, ref auditoria);
                        if (!auditoria.RECHAZAR)
                        {  //  validar carga correcto
                            items = ObtenerRegistros(midt, ID_SUCURSAL, ref auditoria);
                            if (items.Count > 0)
                            {
                                foreach (Cls_Ent_Producto entidad in items)
                                {
                                    using (ProductoRepositorio Productorepositorio = new ProductoRepositorio())
                                    {
                                        entidad.ID_SUCURSAL = ID_SUCURSAL;
                                        entidad.USU_CREACION = "iperez";
                                        Productorepositorio.Producto_Insertar(entidad, ref auditoria);
                                        if (!auditoria.EJECUCION_PROCEDIMIENTO)
                                        {
                                            string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                                            auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                                            break;
                                        }
                                    }
                                }
                                if (!auditoria.RECHAZAR)
                                    auditoria.OBJETO = items.Count();
                            }
                            else
                            {
                                auditoria.Rechazar("No se encontro registros para procesar.");
                            }

                        }
                    }
                }
            }
        }

        public List<Cls_Ent_Producto> ObtenerRegistros(DataTable Dt, int ID_SUCURSAL, ref Cls_Ent_Auditoria auditoria)
        {
            List<Cls_Ent_Producto> ListaProductos = new List<Cls_Ent_Producto>();
            Cls_Ent_Producto item = null;
            try
            {
                foreach (DataRow row in Dt.Rows)
                {
                    item = new Cls_Ent_Producto();
                    item.COD_PRODUCTO = Convert.ToString(row["COD_PRODUCTO"]).ToUpper();
                    item.DESC_PRODUCTO = Convert.ToString(row["DESC_PRODUCTO"]).ToUpper();
                    item.ID_CATEGORIA = Convert.ToInt32(row["ID_CATEGORIA"]);
                    item.ID_UNIDAD_MEDIDA = Convert.ToInt32(row["ID_UNIDAD_MEDIDA"]);
                    item.PRECIO_COMPRA = Convert.ToDecimal(row["PRECIO_COMPRA"]);
                    item.PRECIO_VENTA = Convert.ToDecimal(row["PRECIO_VENTA"]);
                    item.STOCK = Convert.ToInt32(row["STOCK"]);
                    item.STOCK_MINIMO = Convert.ToInt32(row["STOCK_MINIMO"]);
                    item.FLG_SERVICIO = Convert.ToInt32(row["FLG_SERVICIO"]);
                    item.FECHA_VENCIMIENTO = String.Format("{0:MM/dd/yyyy}", row["FECHA_VENCIMIENTO"]);
                    if (!string.IsNullOrEmpty(item.FECHA_VENCIMIENTO))
                        item.FLG_VENCE = 1;
                    item.MARCA = Convert.ToString(row["MARCA"]).ToUpper();
                    item.MODELO = Convert.ToString(row["MODELO"]).ToUpper();
                    item.DETALLE = Convert.ToString(row["DETALLE"]).ToUpper();
                    ListaProductos.Add(item);
                }

            }
            catch (Exception ex)
            {
                string mensaje = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                auditoria.Rechazar(mensaje);
            }

            return ListaProductos;
        }

        public void ValidarCarga(DataTable Dt, int ID_SUCURSAL, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                List<Cls_Ent_ErroresExcel> ListaErrores = new List<Cls_Ent_ErroresExcel>();
                List<Cls_Ent_Unidad_Medida> ListaUnidadMedida = new Cls_Rule_Listados().Unidad_Medida_Listar(ref auditoria);
                List<Cls_Ent_Categoria> ListaCategoria = new Cls_Rule_Categoria().Categoria_Listar(new Cls_Ent_Categoria { FLG_ESTADO = 1 }, ref auditoria);
                int filaExcel = 2; // primara fila de registros
                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow row in Dt.Rows)
                    {
                        if (!auditoria.RECHAZAR)
                        {
                            string COD_PRODUCTO = Convert.ToString(row["COD_PRODUCTO"].ToString());
                            if (!string.IsNullOrEmpty(COD_PRODUCTO))
                            {
                                COD_PRODUCTO = COD_PRODUCTO.Trim();
                                var Objeto_ExitCodigo = new Cls_Rule_Producto().Producto_Buscar_Listar(new Cls_Ent_Producto
                                {
                                    ID_SUCURSAL = ID_SUCURSAL,
                                    COD_PRODUCTO = COD_PRODUCTO
                                }, ref auditoria);
                                if (Objeto_ExitCodigo.Count > 0)
                                {
                                    ListaErrores.Add(this.ErrorEcxel(filaExcel, "[COD_PRODUCTO] " + COD_PRODUCTO + " ya se encuentra registrado."));
                                }
                            }
                            else
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[COD_PRODUCTO] es obligatorio"));
                            }
                            string DESC_PRODUCTO = Convert.ToString(row["DESC_PRODUCTO"].ToString());
                            if (!string.IsNullOrEmpty(DESC_PRODUCTO))
                            {
                                DESC_PRODUCTO = DESC_PRODUCTO.Trim();
                            }
                            else
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[DESC_PRODUCTO] es obligatorio"));
                            }
                            string ID_CATEROGIRA = Convert.ToString(row["ID_CATEGORIA"].ToString());
                            if (Recursos.Clases.Css_Validators.ValidarNumber(ID_CATEROGIRA))
                            {
                                var Objeto_Categoria = ListaCategoria.Where(p => p.ID_CATEGORIA == int.Parse(ID_CATEROGIRA)).FirstOrDefault();
                                if (Objeto_Categoria == null)
                                {
                                    ListaErrores.Add(this.ErrorEcxel(filaExcel, "[ID_CATEGORIA] " + ID_CATEROGIRA + " no se encuentra registrado en el Sistema."));
                                }
                            }
                            else
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[ID_CATEGORIA] debe ser de tipo numerico"));
                            }

                            string ID_UNIDAD_MEDIDA = Convert.ToString(row["ID_UNIDAD_MEDIDA"].ToString());
                            if (Recursos.Clases.Css_Validators.ValidarNumber(ID_UNIDAD_MEDIDA))
                            {
                                var Objeto_UnidadMedida = ListaUnidadMedida.Where(p => p.ID_UNIDAD_MEDIDA == int.Parse(ID_UNIDAD_MEDIDA)).FirstOrDefault();
                                if (Objeto_UnidadMedida == null)
                                {
                                    ListaErrores.Add(this.ErrorEcxel(filaExcel, "[ID_UNIDAD_MEDIDA]  " + ID_UNIDAD_MEDIDA + " no se encuentra registrado en el Sistema."));
                                }
                            }
                            else
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[ID_UNIDAD_MEDIDA] debe ser un número"));
                            }
                            string PRECIO_COMPRA = Convert.ToString(row["PRECIO_COMPRA"].ToString());
                            if (!Recursos.Clases.Css_Validators.ValidarDecimal(PRECIO_COMPRA))
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[PRECIO_COMPRA] debe ser de tipo numerico o decimal"));
                            }

                            string PRECIO_VENTA = Convert.ToString(row["PRECIO_VENTA"].ToString());
                            if (!Recursos.Clases.Css_Validators.ValidarDecimal(PRECIO_VENTA))
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[PRECIO_VENTA] debe ser de tipo numerico o decimal"));
                            }

                            string STOCK = Convert.ToString(row["STOCK"].ToString());
                            if (!Recursos.Clases.Css_Validators.ValidarNumber(STOCK))
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[STOCK] debe ser un número."));
                            }
                            string STOCK_MINIMO = Convert.ToString(row["STOCK_MINIMO"].ToString());
                            if (!Recursos.Clases.Css_Validators.ValidarNumber(STOCK_MINIMO))
                            {

                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[STOCK_MINIMO] debe ser un número."));
                            }
                            string FLG_SERVICIO = Convert.ToString(row["FLG_SERVICIO"].ToString());
                            if (!string.IsNullOrEmpty(FLG_SERVICIO))
                            {
                                if (!Recursos.Clases.Css_Validators.ValidarNumber(FLG_SERVICIO))
                                {
                                    ListaErrores.Add(this.ErrorEcxel(filaExcel, "[Es Servicio] debe ser un número, 0/1."));
                                }
                            }
                            else
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[Es Servicio] debe ser un número , 0/1."));
                            }

                            string FECHA_VENCIMIENTO = Convert.ToString(row["FECHA_VENCIMIENTO"].ToString());
                            if (!Recursos.Clases.Css_Validators.ValidarFecha(FECHA_VENCIMIENTO))
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[FECHA_VENCIMIENTO] no tiene el formato correto dd/mm/yyy."));
                            }

                            filaExcel++;
                        }
                    }
                }
                else
                {
                    auditoria.Rechazar("Archivo cargado no contiene registros para procesar.");
                }

                if (ListaErrores.Count > 0)
                {
                    auditoria.Rechazar("Se encontraron errores en el archivo cargado, por favor revisar los detalles y volver a subir el archivo.");
                    auditoria.OBJETO = ListaErrores;
                }
                else
                {
                    auditoria.Limpiar();
                }

            }
            catch (Exception ex)
            {
                string mensaje = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                auditoria.Rechazar(mensaje);
            }
        }

        private Cls_Ent_ErroresExcel ErrorEcxel(int NRO_FILA, string DESCRIPCION)
        {
            Cls_Ent_ErroresExcel errores = new Cls_Ent_ErroresExcel();
            errores.NRO_FILA = NRO_FILA;
            errores.DESCRIPCION = DESCRIPCION;
            return errores;
        }

        private DataTable ProcesarHojaCalculo(string RUTA_ARCHIVO_TEMPORAL, ref Cls_Ent_Auditoria auditoria)
        {
            List<Cls_Ent_Campo> ListaCampo = new List<Cls_Ent_Campo>();
            using (CargaExcelRepostiorio Repositorio = new CargaExcelRepostiorio())
            {
                ListaCampo = Repositorio.Campo_Listar(ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            DataTable midt = Recursos.Excel.Css_Excel.Excel_To_DataTable(RUTA_ARCHIVO_TEMPORAL, 0, ref auditoria);
            if (auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (!auditoria.RECHAZAR)
                {
                    string NombreHoja = "PlantillaProducto";
                    bool flgValidaNombre = false;
                    if (midt.TableName == NombreHoja)
                    {
                        flgValidaNombre = true;
                    }
                    if (flgValidaNombre == true)
                    {
                        foreach (Cls_Ent_Campo campo in ListaCampo)
                        {
                            midt.Columns[campo.DESCRIPCION_CAMPO.Trim()].ColumnName = campo.COD_CAMPO;
                        }
                    }
                    else
                    {
                        auditoria.Rechazar("El nombre de la hoja de excel tiene que ser 'PlantillaProducto'.");
                    }

                }
            }

            return midt;
        }
    }
}