using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Automatas
{
    public partial class Automatas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cargar(object sender, EventArgs e)
        {
            divuno.Visible = true;
            divdos.Visible = true;
            string carpeta = "~/archivos/";
            if (file.PostedFile != null && file.PostedFile.ContentLength > 0)
            {
                //Se valida que exista carpeta y si no existe se crea
                if (!Directory.Exists(Server.MapPath(carpeta)))
                    Directory.CreateDirectory(Server.MapPath(carpeta));
                //Guarda el archivo en carpeta archivos
                file.PostedFile.SaveAs(String.Concat(Server.MapPath(carpeta), file.PostedFile.FileName));
                //Convierte cadena de archivo en arreglo por linea
                String[] arregloArchivo = File.ReadAllLines(Server.MapPath(carpeta) + file.PostedFile.FileName);
                arregloArchivo = arregloArchivo.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                //Manipulacion de informacion
                Manipular_Data(arregloArchivo);
            }
        }
        private void Manipular_Data(String[] arregloArchivo) {
            //Se obtienen las cadenas de acuerdo a la letra
            String cadenaQ = arregloArchivo[0];
            String cadenaF = arregloArchivo[1];
            String cadenaI = arregloArchivo[2];
            String cadenaA = arregloArchivo[3];
            String cadenaW = arregloArchivo[4];
            //Se llena area donde se muestra el contenido del archivo
            //Q
            areaArchivo.InnerText = cadenaQ + Environment.NewLine;
            //F
            areaArchivo.InnerText += cadenaF + Environment.NewLine;
            //I
            areaArchivo.InnerText += cadenaI + Environment.NewLine;
            //A
            areaArchivo.InnerText += cadenaA + Environment.NewLine;
            //W
            areaArchivo.InnerText += cadenaW + Environment.NewLine;
            //Se limpian las cadenas, se obtienen unicamente los valores a trabajar
            cadenaQ = cadenaQ.Substring((cadenaQ.IndexOf("{") + 1), cadenaQ.IndexOf("}") - (cadenaQ.IndexOf("{") + 1));
            cadenaF = cadenaF.Substring((cadenaF.IndexOf("{") + 1), cadenaF.IndexOf("}") - (cadenaF.IndexOf("{") + 1));
            cadenaI = cadenaI.Substring(cadenaI.IndexOf(":") + 1, 1);
            cadenaA = cadenaA.Substring((cadenaA.IndexOf("{") + 1), cadenaA.IndexOf("}") - (cadenaA.IndexOf("{") + 1));
            cadenaW = cadenaW.Substring((cadenaW.IndexOf("{") + 1), cadenaW.IndexOf("}") - (cadenaW.IndexOf("{") + 1));
            //se convierten las cadenas de valores en arreglos
            String[] arregloQ = cadenaQ.Split(',');
            String[] arregloF = cadenaF.Split(',');
            String[] arregloW = cadenaW.Split(';');
            //Removiendo espacios en blanco
            arregloQ = arregloQ.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            arregloF = arregloF.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            arregloW = arregloW.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //Llenando area Q estados
            areaQEstados.InnerText = "";
            foreach (string valor in arregloQ)
            {
                areaQEstados.InnerText += valor + Environment.NewLine;
            }
            //Llenando area F alfabeto
            areaFAlfabeto.InnerText = "";
            foreach (string valor in arregloF)
            {
                areaFAlfabeto.InnerText += valor + Environment.NewLine;
            }
            //Llenando area A aceptacion
            areaAAceptacion.InnerText = cadenaA;
            //Se valida si existe epsilon
            bool banderaEpsilon = false;
            if (cadenaW.Contains("e")) {
                banderaEpsilon = true;
            }
            //Se llena tabla AFN
            DataTable tablaAFN = new DataTable();
            tablaAFN = generarAFN(arregloW, arregloF,banderaEpsilon);
            GridViewAFN.DataSource = tablaAFN;
            GridViewAFN.DataBind();
            //Se llena tabla AFD
            DataTable tablaAFD = new DataTable();
            //tablaAFD = generarAFD(tablaAFN, cadenaI, arregloF, banderaEpsilon);
            GridViewAFD.DataSource = tablaAFN;
            GridViewAFD.DataBind();
            //Se arma quintupla
            //armarQuintuplaAFD(A, AFD);
            //Session["GlobalAFD"] = AFD;

        }

        private DataTable generarAFN(String[] arregloW, String[] arregloF, bool banderaEpsilon)
        {
            DataTable tablaAFN = new DataTable();
            //Se arma el encabezado de tabla
            tablaAFN.Columns.Add("N");
            //Se recorre el alfabeto para crear las columnas correspondientes
            foreach (string letra in arregloF)
            {
                tablaAFN.Columns.Add(letra);
            }
            //Si existe epsilon se agrega columna epsilon
            if (banderaEpsilon)
            {
                tablaAFN.Columns.Add("e");
            }
            //Se recorre la tabla de transicion
            foreach (String estadoTran in arregloW)
            {
                String transicion = estadoTran;
                //Eliminando parentesis de linea
                transicion = transicion.Replace("(","");
                transicion = transicion.Replace(")", "");
                //Se crea arreglo de valores, separandolos por coma
                //estado inicial, alfabeto, estado destino
                String[] valores = transicion.Split(',');
                bool bandera = false;
                //Se recorre tabla para validar si estado ya existe
                foreach (DataRow fila in tablaAFN.Rows)
                {
                    //Si estado inicial ya existe en tabla, se activa la bandera indicando que ya existe
                    if (valores[0].Equals(fila["N"]))
                    {
                        bandera = true;
                        //Concatena valor
                        if (fila[valores[1]].ToString().Equals(""))
                            fila[valores[1]] = valores[2];
                        else
                            fila[valores[1]] += "," + valores[2];
                    }
                }
                //Si bandera false, es decir que el estado no existe
                if (!bandera)
                {
                    //Lo agrega a la tabla
                    DataRow filaTabla = tablaAFN.NewRow();
                    filaTabla["N"] = valores[0];
                    filaTabla[valores[1]] = valores[2];
                    tablaAFN.Rows.Add(filaTabla);
                }
            }
            return tablaAFN;
        }

        private DataTable generarAFD(DataTable tablaAFN, String inicial, String[] alfabeto, bool banderaEpsilon)
        {
            DataTable tablaAFD = new DataTable();
            if (tablaAFN.Rows[0]["N"].Equals(inicial))
            {
                //Se arma encabezado de tabla AFN
                tablaAFD.Columns.Add("ESTADO");
                //Se recorre el alfabeto para crear las columnas correspondientes
                foreach (string letra in alfabeto)
                {
                    tablaAFD.Columns.Add(letra);
                }
                //Se agrega columna composicion
                tablaAFD.Columns.Add("COMPOSICION");

                DataRow dr = tablaAFD.NewRow();
                dr["ESTADO"] = "A";
                String e = "";
                if (banderaEpsilon) {
                    e = cerraduraEpsilon(tablaAFN.Rows[0]["e"].ToString(), tablaAFN);
                }
                if (e.Equals(""))
                {
                    e = "0";
                    dr["COMPOSICION"] = clean((tablaAFN.Rows[0]["N"].ToString()));
                }
                else
                {
                    dr["COMPOSICION"] = clean((tablaAFN.Rows[0]["N"].ToString() + "," + e));
                }
                tablaAFD.Rows.Add(dr);

                tablaAFD = armarComposiciones(tablaAFD, tablaAFN);
                tablaAFD = armarComposiciones(tablaAFD, tablaAFN);
            }
            return tablaAFD;
        }

        private DataTable armarComposiciones(DataTable AFD, DataTable AFN)
        {
            for (int i = 0; i < AFD.Rows.Count; i++)
            {
                foreach (DataColumn column in AFD.Columns)
                {
                    if (!column.ColumnName.Equals("ESTADO") && !column.ColumnName.Equals("COMPOSICION"))
                    {
                        String composicion = clean(remove(cerraduraEpsilon(move(column.ColumnName, AFN, AFD.Rows[i]["COMPOSICION"].ToString()), AFN)));
                        if (composicion.Equals(""))
                            composicion = "0";
                        bool flag = false;
                        string lastLetter = "";
                        foreach (DataRow r in AFD.Rows)
                        {
                            if (r["COMPOSICION"].Equals(composicion))
                            {
                                flag = true;
                                AFD.Rows[i][column.ColumnName] = r["ESTADO"].ToString();
                            }
                            lastLetter = r["ESTADO"].ToString();
                        }
                        if (!flag)
                        {
                            DataRow dr = AFD.NewRow();
                            dr["ESTADO"] = nextLetter(lastLetter);
                            dr["COMPOSICION"] = composicion;
                            AFD.Rows.Add(dr);
                        }
                    }
                }
            }
            return AFD;
        }

        private String remove(String texto)
        {
            String[] tmp = texto.Split(',');
            tmp = tmp.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            tmp = tmp.Distinct().ToArray();

            return string.Join(",", tmp);
        }

        private string nextLetter(String value)
        {

            char letter = value[0];
            char next;

            if (letter == 'z')
                next = 'a';
            else if (letter == 'Z')
                next = 'A';
            else
                next = (char)(((int)letter) + 1);

            return next.ToString();
        }

        private string move(String letra, DataTable AFN, String composicion)
        {
            string[] tmp = composicion.Split(',');
            String cadena = "";
            foreach (string item in tmp)
            {
                foreach (DataRow row in AFN.Rows)
                {
                    if (row["N"].Equals(item))
                    {
                        if (!row[letra].ToString().Equals(""))
                            if (cadena.Equals(""))
                                cadena = row[letra].ToString();
                            else
                                cadena += "," + row[letra].ToString();
                    }
                }
            }

            return cadena;
        }

        private String cerraduraEpsilon(String cadena, DataTable dt)
        {

            String[] aux;

            if (cadena.Contains(","))
                aux = cadena.Split(',');
            else
                aux = new string[1] { cadena };

            foreach (string item in aux)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["N"].Equals(item))
                    {
                        if (!dr["e"].ToString().Equals(""))
                            if (cadena.Equals(""))
                                cadena = cerraduraEpsilon(dr["e"].ToString(), dt);
                            else
                                cadena += "," + cerraduraEpsilon(dr["e"].ToString(), dt);
                        break;
                    }
                }
            }
            return cadena;
        }

        private String clean(String text)
        {
            if (text.Contains(","))
            {
                string[] tmp = text.Split(',');
                int quantity = tmp.Count();
                int[] nums = new int[tmp.Length];

                for (int i = 0; i < tmp.Length; i++)
                {
                    nums[i] = int.Parse(tmp[i]);
                }

                Comparison<int> comparador = new Comparison<int>((numero1, numero2) => numero1.CompareTo(numero2));
                Array.Sort<int>(nums, comparador);
                int pos = -1;
                tmp = new string[quantity];
                foreach (int numero in nums)
                {
                    pos++;
                    tmp[pos] = numero.ToString();

                }
                return String.Join(",", tmp);
            }
            else
            {
                return text;
            }
        }
    }
}