using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace WorkingObjSpc
{

    public class DrawObj
    {
        public string mark { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string section { get; set; }
    }

    public class ObjSpc
    {

        public string format;   // 1 - формат
        public string zone;     // 2 - зона
        public string pos;      // 3 - позиция
        public string mark;     // 4 - обозначение
        public string name;     // 5 - наименование
        public string count;    // 6 - количество
        public string note;     // 7 - примечание
        public string section;  // номер раздела
        public uint[] mcount;
        public bool viewObj;    //вид объекта спецификации (false-базовый, true-вспомогательный)
        

        public ObjSpc(int CountIsp, uint[] arr)
        {
            mcount = new uint[CountIsp];
            if (arr != null)
            {
                arr.CopyTo(mcount, 0);
            }
            else
               for (int i = 0; i < CountIsp; i++)
                {
                    mcount[i] = 0;
                }
        }

        public string DoMarkName()
        {
            return mark + " _ " + name;
        }
    }

    public class WorkingRegFile
    {
        /*Класс работы с файлом RegFile, реализованным по принципу HTML*/

        private List<DrawObj> drawobj;

        string PathRegFile;  // ссылка на regFile

        int countRegFile;  //содержит количество объектов в Реестре изделий (RegFile)

        public DrawObj Drawobj
        {
            set
            {
                if(drawobj == null)
                {
                    drawobj = new List<DrawObj>();
                }

                drawobj.Add(new DrawObj
                {
                    mark = value.mark,
                    name = value.name,
                    path = value.path,
                    section = value.section
                });

            }            
        }


        //функция возвращает ссылки из RegFile
        public List<string> PathDevice()
        {
            List<string> path = new List<string>();

            foreach (var v in drawobj)
                path.Add(v.path);

            return path;
        }

        public WorkingRegFile()
        {

        }

        public WorkingRegFile(string PathRegFile)
        {
            PathRegFile = PathRegFile + "\\RegFile";
            ReadRegFile(PathRegFile);               
        }

        public string WriteSpcFile(List<DrawObj> draw_obj, string PathFile) //функция создания файла типа Reg для специфицированного изделия 
        {
            int count = draw_obj.Count;           
            string STR = "";
            string str = "";


            using (StreamWriter outputFile = new StreamWriter(PathFile, false))
            {

                foreach (var varSpc in draw_obj)
                {
                    switch (varSpc.section)
                    {
                        case "Документация":
                            {
                                int flag = STR.IndexOf("</Документация>");

                                str = "<Объект>\n";

                                str += "<Обозначение_изделия>";
                                str += varSpc.mark;
                                str += "</Обозначение_изделия>\n";

                                str += "<Наименование_изделия>";
                                str += varSpc.name;
                                str += "</Наименование_изделия>\n";

                                str += "<Путь_к_файлу>";
                                str += varSpc.path;
                                str += "</Путь_к_файлу>\n";

                                str += "</Объект>\n\n";


                                if (flag == -1)
                                {
                                    STR += "<Документация>\n\n";

                                    STR += str;

                                    STR += "</Документация>\n\n";
                                }
                                else
                                    STR = STR.Insert(flag, str);


                            }
                            break;

                        case "Комплексы":
                            break;
                        case "Сборочные единицы":
                            {
                                int flag = STR.IndexOf("</Сборочные единицы>");

                                str = "<Объект>\n";

                                str += "<Обозначение_изделия>";
                                str += varSpc.mark;
                                str += "</Обозначение_изделия>\n";

                                str += "<Наименование_изделия>";
                                str += varSpc.name;
                                str += "</Наименование_изделия>\n";

                                str += "<Путь_к_файлу>";
                                str += varSpc.path;
                                str += "</Путь_к_файлу>\n";

                                str += "</Объект>\n\n";


                                if (flag == -1)
                                {
                                    STR += "<Сборочные единицы>\n\n";

                                    STR += str;

                                    STR += "</Сборочные единицы>\n\n";
                                }
                                else
                                    STR = STR.Insert(flag, str);
                            }
                            break;
                        case "Детали":
                            {
                                int flag = STR.IndexOf("</Детали>");

                                str = "<Объект>\n";

                                str += "<Обозначение_изделия>";
                                str += varSpc.mark;
                                str += "</Обозначение_изделия>\n";

                                str += "<Наименование_изделия>";
                                str += varSpc.name;
                                str += "</Наименование_изделия>\n";

                                str += "<Путь_к_файлу>";
                                str += varSpc.path;
                                str += "</Путь_к_файлу>\n";

                                str += "</Объект>\n\n";


                                if (flag == -1)
                                {
                                    STR += "<Детали>\n\n";

                                    STR += str;

                                    STR += "</Детали>\n\n";
                                }
                                else
                                    STR = STR.Insert(flag, str);
                            }
                            break;
                        case "Стандартные изделия":
                            {
                                int flag = STR.IndexOf("</Стандартные изделия>");

                                str = "<Объект>\n";

                                str += "<Обозначение_изделия>";
                                str += varSpc.mark;
                                str += "</Обозначение_изделия>\n";

                                str += "<Наименование_изделия>";
                                str += varSpc.name;
                                str += "</Наименование_изделия>\n";


                                str += "</Объект>\n\n";


                                if (flag == -1)
                                {
                                    STR += "<Стандартные изделия>\n\n";

                                    STR += str;

                                    STR += "</Стандартные изделия>\n\n";
                                }
                                else
                                    STR = STR.Insert(flag, str);
                            }
                            break;
                        case "Прочие изделия":
                            break;
                        case "Материалы":
                            break;
                    }

                }
                outputFile.WriteLine(STR);
            }

            return PathFile;

        }

        //Здесь будет функция для записи разработанных изделий в (регистр файлов) RegFile

        public void ReadRegFile(string PathFile) //Функция читает файл с данными. Далее преобразует эти данные в класс DrawObj
        {
            //открыть файл на чтение
            //функция читает файл и возвращает все данные из него типом данных DrawOjc
            //Подумать на хранением данных в бинарном файле
            if (File.Exists(PathFile))
            {
                using (StreamReader sr = File.OpenText(PathFile))
                {
                    string STR = sr.ReadToEnd();
                    PathRegFile = PathFile;
                    drawobj = new SpcRegFile().SetDrawObj(STR);
                    countRegFile = drawobj.Count;
                }
            }
            else
            {
                //создадим файл RegFile
                using (StreamWriter outputFile = new StreamWriter(PathFile, false))
                {
                    PathRegFile = PathFile;
                    countRegFile = 0;
                }
            }


        }

        public bool WriteRegFile(string path, bool ReadAll)  //Переменная ReadAll отвечает за то какие строки записывать в RegFile. Если она true, то записывать все строки. Иначе только те строки, которые были добавлены
        {

            string str = "";

            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Append);               
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    if(ReadAll)
                    {
                        countRegFile = 0;
                    }

                    int count = drawobj.Count;

                    for (int i = countRegFile; i < count; i++)
                    {
                        str += "<Объект>\n";

                        str += "<Обозначение_изделия>";
                        str += drawobj[i].mark;
                        str += "</Обозначение_изделия>\n";

                        str += "<Наименование_изделия>";
                        str += drawobj[i].name;
                        str += "</Наименование_изделия>\n";

                        str += "<Путь_к_файлу>";
                        str += drawobj[i].path;
                        str += "</Путь_к_файлу>\n";

                        str += "</Объект>\n\n";
                    }
                                        
                    writer.Write(str);
                }
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
                return true;
        }

        public bool WriteRegFile()
        {

            string str = "";

            FileStream fs = null;
            try
            {
                fs = new FileStream(PathRegFile, FileMode.Append);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    int count = drawobj.Count;

                    for (int i = countRegFile; i < count; i++)
                    {
                        str += "<Объект>\n";

                        str += "<Обозначение_изделия>";
                        str += drawobj[i].mark;
                        str += "</Обозначение_изделия>\n";

                        str += "<Наименование_изделия>";
                        str += drawobj[i].name;
                        str += "</Наименование_изделия>\n";

                        str += "<Путь_к_файлу>";
                        str += drawobj[i].path;
                        str += "</Путь_к_файлу>\n";

                        str += "</Объект>\n\n";
                    }

                    writer.Write(str);
                }
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
            return true;
        }

        //Функция проверяет существуют ли файлы, на который ссылаются ссылки в файле реестра. Функция возвращает False если файл не существует и True если существует
        public bool CheckLink()
        {
            string path = Path.GetDirectoryName(PathRegFile);
            if (drawobj != null)
                foreach (var draw in drawobj)
                {
                    if (File.Exists(path + "\\" + draw.path))
                    { }
                    else
                    {
                        string name = Path.GetFileNameWithoutExtension(draw.path);
                        MessageBox.Show("Не существует файл " + name, "Ошибка при копировании");
                        return false; //Если хоть один файл не существует, то не копируем 
                    }
                }
            else
                return false;

            return true;
        }

        public string SearchDrawObj(string str) //Функция выполняет поиск обозначения str в списке объектов
        {
           if(drawobj != null)
                foreach (var draw in drawobj)
                {
                    if (draw.mark.Contains(str) || str.Contains(draw.mark))
                    return draw.path;
                }
           return null;
        }

        public List<DrawObj> SearchDevice(string str, bool MarkName) //функция возвращает все имена или обозначения изделий, которые соответствуют строке str
        {
            List<DrawObj> listDevice = new List<DrawObj>();
            if (drawobj.Count > 0)
            {                
                if (MarkName)
                {
                    foreach (var draw in drawobj)
                    {
                        if (draw.mark.ToLower().Contains(str.ToLower()) || str.ToLower().Contains(draw.mark.ToLower()))
                            listDevice.Add(draw);

                    }
                }
                else
                {
                    foreach (var draw in drawobj)
                    {
                        if (draw.name.ToLower().Contains(str.ToLower()) || str.ToLower().Contains(draw.name.ToLower()))
                            listDevice.Add(draw);
                    }
                }
                return listDevice;
            }
            else
                return listDevice;

        }

        public List<DrawObj> transform(List<ObjSpc> list_obj_spc)  //функция преобразует объект List<ObjSpc> в List<DrawObj>
        {
            var list_draw_obj = new List<DrawObj>();

            foreach (var Los in list_obj_spc)
            {
                list_draw_obj.Add(new DrawObj()
                {
                    mark = Los.mark,
                    name = Los.name,
                    section = Los.section
                    //Возможно нужно будет добавить количество изделий
                });
            }
            return list_draw_obj;
        }

        public List<ObjSpc> transform(List<DrawObj> list_draw_obj)
        {
            var list_obj_spc = new List<ObjSpc>();


            foreach (var Los in list_draw_obj)
            {
                list_obj_spc.Add(new ObjSpc(1, null)
                {
                    mark = Los.mark,
                    name = Los.name,
                });
            }
            return list_obj_spc;
        }       

        public void ToRankingFunction(ref List<DrawObj> ListDrawObj)
        {           
            int count=0;
            foreach(var DrawObj in ListDrawObj)
            {
                if (DrawObj.section == "Документация")
                {
                    count++;
                }
                else
                    break;
            }
            if(count!=0)
            {
                var Temp = new List<DrawObj>();
                Temp.AddRange(ListDrawObj.Take(count)); //Копируем объекты во временную переменную
                ListDrawObj.RemoveRange(0, count); //Удаляем из начала списка
                ListDrawObj.AddRange(Temp); //Добавляем в конец списка
            }
        }

        public void CopyPasteRegFile(string PathFrom, string PathTo)
        {

            WriteRegFile(PathTo, false);
        }


    }

    public class SpcRegFile
    {
        readonly string mark_begin = "<Обозначение_изделия>";
        readonly string mark_end = "</Обозначение_изделия>";
        readonly string name_begin = "<Наименование_изделия>";
        readonly string name_end = "</Наименование_изделия>";
        readonly string path_begin = "<Путь_к_файлу>";
        readonly string path_end = "</Путь_к_файлу>";
        readonly string product_begin = "<Объект>";
        readonly string product_end = "</Объект>";

        //Функция выделяет текст заключенный между <..> и </..>
        public string select(string str, string substr_begin, string substr_end) 
        {

            try
            {
                int index_begin = str.IndexOf(substr_begin) + substr_begin.Length; //Индекс начала строки
                int index_end = str.IndexOf(substr_end); //Индекс конца строки

                int lenght = index_end - index_begin; //Длина выделяемой строки

                return str.Substring(index_begin, lenght); // выделить строку
            }
            catch
            {
                return null;
            }

        }

        //функция выделяет подстроки <Объект>...</Объект> из файла и возвращает их в виде объектов List<string>
        private List<string> SelectBlock(string STR, string substr_begin, string substr_end) 
        {
            var list = new List<string>();
            int index_begin;
            int index_end;
            int lenght;

            int substr_end_length = substr_end.Length;

            while (STR.IndexOf(substr_begin) >= 0)
            {
                index_begin = STR.IndexOf(substr_begin);
                index_end = STR.IndexOf(substr_end) + substr_end_length;
                lenght = index_end - index_begin;

                list.Add(STR.Substring(index_begin, lenght));


                STR = STR.Remove(index_begin, lenght);

            }

            return list;
        }

        //Функция создает объект DrawObj, заполняет данными и возвращает его
        public List<DrawObj> SetDrawObj(string STR) 
        {
            var list = new List<string>();
            var theproduct = new List<DrawObj>();  //создание списка изделий                     

            list = SelectBlock(STR, product_begin, product_end);

            for (int i = 0; i < list.Count; i++)
            {
                theproduct.Add(new DrawObj()
                {
                    mark = select(list[i].ToString(), mark_begin, mark_end),
                    name = select(list[i].ToString(), name_begin, name_end),
                    path = select(list[i].ToString(), path_begin, path_end)
                });

            }

            return theproduct;

        }


        public TreeNode ReadToTree(string pathFile, ref TreeNode treeNode)
        {
            string mark, name, path;
            
            var list = new List<string>();
                       
            if (File.Exists(pathFile))
            {
                using (StreamReader sr = File.OpenText(pathFile))
                {
                    //Данные две строки получают директорию в которой хранятся издели
                    //т.е. директорию архива или временную директорию 
                    string pathDirectory = Path.GetDirectoryName(pathFile);
                    pathDirectory = Path.GetDirectoryName(pathDirectory);

                    string STR = sr.ReadToEnd();

                    list = SelectBlock(STR, product_begin, product_end);

                    

                    for (int i = 0; i < list.Count; i++)
                    {
                        mark = select(list[i].ToString(), mark_begin, mark_end);
                        name = select(list[i].ToString(), name_begin, name_end);
                        path = select(list[i].ToString(), path_begin, path_end);

                        string PathColor = path; //Если PathColor пустая то изменим цвет на серый
                        

                        path = pathDirectory + "\\" + path;
                        

                        try
                        {
                            string ext = Path.GetExtension(path);
                            if (ext != string.Empty && ext == ".SpcFile")
                            {
                                string NameNode = Path.GetFileNameWithoutExtension(path);

                                TreeNode nod = new TreeNode(NameNode); //Создадим новый лист если изделие имеет сборочные единицы                                

                                ReadToTree(path, ref nod); //.Nodes.Add(NameNode)

                                treeNode.Nodes.Add(nod);
                                
                            }
                            else
                            {
                                int count = 0;

                                if (i==0)
                                {
                                    count = treeNode.Nodes.Count;
                                    string NameNode = Path.GetFileNameWithoutExtension(pathFile);
                                    treeNode.Nodes.Add(NameNode); // Добавление спецификации
                                    NameNode += ".spw";
                                    treeNode.Nodes[count].Tag = Path.GetDirectoryName(pathFile) + "\\" + NameNode + ".pdf";
                                }
                                count = treeNode.Nodes.Count;
                                treeNode.Nodes.Add(mark + " _ " + name);

                                if (PathColor == "") //Меняем цвет
                                {
                                    treeNode.Nodes[count].ForeColor = Color.Gray;
                                }

                                treeNode.Nodes[count].Tag = path + ".pdf";                                
                            }
                        }
                        catch
                        {
                            continue;
                        }


                    }

                    return treeNode;

                }
            }
            else
                return null;






            

        }
    }

    public class SpcFile
    {

        public void GetTreeNod(string path, ref TreeNode nod)
        {
            //path - ссылка на SpcFile            
            ReadSpcFile(path, ref nod);            
        }
   

        private void ReadSpcFile(string path, ref TreeNode nod)
        {           
            new SpcRegFile().ReadToTree(path, ref nod);
        }
    }




}


