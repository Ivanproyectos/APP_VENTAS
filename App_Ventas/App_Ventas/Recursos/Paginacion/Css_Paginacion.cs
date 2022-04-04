using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Collections;

namespace App_Ventas.Recursos.Paginacion
{
    public class Css_Paginacion
    {
        public static string formatearFecha(string fecha)
        {
            var date = fecha.Split('/');
            return string.Format("{0}-{1}-{2}", date[2].ToString(), date[1].ToString(), date[0].ToString());

        }

        public static string ConvertToSql(Css_Filter filtros)
        {
            string whereFiltro = string.Empty;
            string valor = string.Empty;
            if (filtros.rules == null)
            {
                //   filtros.rules = New List(Of Rule)
            }
            foreach (var filtro in filtros.rules)
            {
                valor = string.Empty;
                string filterFormatString = string.Empty;
                string[] data = filtro.data.Split('+');

                switch (filtro.op)
                {
                    case "bw":
                        filterFormatString = " {0} {1} like '{2}%'";
                        valor = (filtro.data.Trim().ToUpper());
                        break;
                    // equal ==
                    case "eq":
                        filterFormatString = " {0} {1} = {2}";
                        valor = (filtro.data.Trim().ToUpper());
                        break;
                    // not equal !=
                    case "ne":
                        filterFormatString = " {0} {1} <> {2}";
                        valor = (filtro.data.Trim().ToUpper());
                        break;
                    // string.Contains()
                    case "cn":
                        filterFormatString = " {0} {1} like '%{2}%'";
                        valor = (filtro.data.Trim().ToUpper());
                        break;
                    case "fe":
                        filterFormatString = " {0} Cast({1} as date) = '{2}'";
                        valor = formatearFecha(filtro.data.Trim().ToUpper());
                        break;
                }
                if ((data.Length > 1))
                {
                    ArrayList totalParametro = new ArrayList();
                    foreach (string parametro in data)
                    {
                        if (parametro != "")
                        {
                            totalParametro.Add(parametro);
                        }
                    }
                    int contador = 1;
                    for (int index = 0; index <= totalParametro.Count; index++)
                    {
                        if (index == 0)
                        {
                            if (contador == totalParametro.Count)
                            {
                                whereFiltro += string.Format(filterFormatString, filtros.groupOp, filtro.field, (totalParametro[0].ToString().Trim().ToUpper()));
                                break; // TODO: might not be correct. Was : Exit For
                            }
                            else
                            {
                                whereFiltro += string.Format(filterFormatString, filtros.groupOp, "(" + filtro.field, (totalParametro[0].ToString().Trim().ToUpper()));
                            }
                        }
                        else
                        {
                            if (contador == totalParametro.Count)
                            {
                                whereFiltro += string.Format(filterFormatString, "OR", filtro.field, (totalParametro[index].ToString().Trim().ToUpper())) + ")";
                                break; // TODO: might not be correct. Was : Exit For
                            }
                            else
                            {
                                whereFiltro += string.Format(filterFormatString, "OR", filtro.field, (totalParametro[index].ToString().Trim().ToUpper()));
                            }
                        }
                        contador = contador + 1;
                    }
                }
                else
                {
                    whereFiltro += string.Format(filterFormatString, filtros.groupOp, filtro.field, valor);
                }
            }
            return filtros.rules.Count == 0 ? string.Empty : (string)whereFiltro.Substring(4);
        }

        public static string GetWhere(string filters, List<Css_Rule> rules)
        {
            var @where = string.Empty;
            var filtro = (string.IsNullOrEmpty(filters)) ? null : JsonConvert.DeserializeObject<Css_Filter>(filters);

            if ((filtro != null))
            {
                @where = ConvertToSql(filtro);
            }
            if ((rules != null))
            {
                foreach (var regla in rules)
                {
                    if ((regla != null))
                    {
                        if ((!string.IsNullOrEmpty(regla.data.ToUpper()) & regla.data != string.Empty & (regla.data != null)))
                        {
                            if (string.IsNullOrEmpty(regla.op))
                            {
                                //camio
                                regla.op = " ";
                            }
                            if (regla.op2 != null)
                            {
                                @where += " or " + regla.field + regla.op2 + regla.data.ToUpper();
                            }
                            else
                            {
                                @where += " and " + regla.field + regla.op + regla.data.ToUpper();
                            }

                        }
                    }
                }
            }

            return string.IsNullOrEmpty(@where) ? where : (where.StartsWith(" and ") ? where.Substring(4) : where);
        }

        public static GenericDouble<JQgrid, T> BuscarPaginador<T>( int page, int rows, int count , IList<T> list) where T : class
        {
            JQgrid jqgrid = new JQgrid();
            //IList<T> list;
            try
            {
                int totalPages = 0;
                //if (search)
                //{
                //    @where = (@where + GetWhere(searchField, searchOper, searchString));
                //}
              //  int count = 0;

                if (((count > 0) & (rows > 0)))
                {
                    decimal division = (count / rows);
                    int totalpaginas = (int)Math.Truncate(division);
                    //(count Mod rows) > 0
                    if ((count > (totalpaginas * rows)))
                    {
                        //totalPages = CType(Math.Ceiling(CType((count / rows), Decimal)), Integer)
                        //totalPages = (CType((count / rows), Integer) + 1)
                        totalPages = totalpaginas + 1;
                    }
                    else
                    {
                        totalPages = totalpaginas;
                        //CType((count / rows), Integer)
                    }
                    //totalPages = (int)Math.Ceiling((decimal)(count / rows));
                    totalPages = (totalPages == 0 ? 1 : totalPages);
                    //TODO: Warning!!!, inline IF is not supported ?
                }
                page = (page > totalPages ? totalPages : page);
                int start = ((rows * page) - rows);
                if ((start < 0))
                {
                    start = 0;
                }
                jqgrid.total = totalPages;
                jqgrid.page = page;
                jqgrid.records = count;
                jqgrid.start = start;
                //if (id == 1)
                //{
                //    list = logic.OrdenExpedienteBuscar(sidx, sord, rows, page, @where);
                //}
                //else
                //{
                //    list = logic.OrdenExpedienteBuscar_dup(sidx, sord, rows, page, @where);
                //}

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new GenericDouble<JQgrid, T>(jqgrid, list);
        }
    }

    public class GenericDouble<T, TQ> where T : class, new()
    {
        public GenericDouble(T value__1, IList<TQ> list__2)
        {
            Value = value__1;
            List = list__2;
        }

        public T Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        private T m_Value;
        public IList<TQ> List
        {
            get { return m_List; }
            set { m_List = value; }
        }
        private IList<TQ> m_List;
    }
}