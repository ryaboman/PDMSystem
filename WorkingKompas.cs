//#define __LIGHT_VERSION__
#if (__LIGHT_VERSION__)
	using Kompas6LTAPI5;
#else
using Kompas6API5;
#endif

using VCHATCHLib;
using Kompas6Constants3D;
using Kompas6Constants;
using KAPITypes;


using System;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using reference = System.Int32;

using WorkingObjSpc;

namespace WorkingKompas
{
    public class Kompas
    {

        #region Custom declarations
            static private KompasObject kompas;
        //int libraryId;                  // HANDLE загруженной библиотеки
        #endregion

        //конструктора для данного класса
        public Kompas()
        {
            ConnectionKompas();
        }

        private string ConnectionKompas() //функция подключения к компасу
        {
            if (kompas == null)
            {
                string progId = string.Empty;

                #if __LIGHT_VERSION__
					progId = "KOMPASLT.Application.5";
                #else
                    progId = "KOMPAS.Application.5";
                #endif

                kompas = (KompasObject)Marshal.GetActiveObject(progId);
                if (kompas != null)
                {
                    kompas.Visible = true;
                    kompas.ActivateControllerAPI();
                    return "Успешно подлючились";
                }
                else
                {
                    return "Не найден активный объект";
                }
            }
            else
            {
                return "Объект уже захвачен";
            }
        }
        
        public List<ObjSpc> ReadSpc() //Функция чтения спецификации
        {
            var objSpc = new List<ObjSpc>();
            const int countSpcColumn = 7;
            string[] obj_text = new string[countSpcColumn + 2];

            ksDocument2D doc = (ksDocument2D)kompas.Document2D();
            ksSpcDocument spc = (ksSpcDocument)kompas.SpcActiveDocument();

            //SpcObjParam Isp = (SpcObjParam)kompas.GetParamStruct((short)StructType2DEnum.ko_SpcObjParam);

            //SpcTuningStyleParam Isp = (SpcTuningStyleParam)kompas.GetParamStruct((short)StructType2DEnum.ko_SpcTuningStyleParam);
            //Console.WriteLine(Isp.countIspoln.ToString());

            if (doc != null && spc != null && spc.reference != 0)
            {
                ksSpecification specification = (ksSpecification)spc.GetSpecification();                                

                ksIterator iter = (ksIterator)kompas.GetIterator();
                iter.ksCreateSpcIterator(null, 0, 2); // Иттератор для каких объектов?
                if (iter.reference != 0 && specification != null)
                {
                    int obj = iter.ksMoveIterator("F");
                    if (obj != 0)
                    {
                        int count = 0;
                        do
                        {
                            string buf = string.Format("Объект спецификации №{0}", count);
                            ////kompas.ksMessage(buf);


                            // пройдем по всем колонкам
                            for (int i = 1; i <= countSpcColumn; i++)
                            {
                                // для текущего номера определим тип колонки, номер исполнения и блок
                                ksSpcColumnParam spcColPar = (ksSpcColumnParam)kompas.GetParamStruct((short)StructType2DEnum.ko_SpcColumnParam);
                                if (specification.ksGetSpcColumnType(obj,   //объект спецификации  (функция - по номеру колонки для данного объекта спецификации получить параметры колонки)
                                    i,                                      // номер колонки, начиная с 1
                                    spcColPar) == 1)
                                {
                                    // возьмем текст
                                    int columnType = spcColPar.columnType;
                                    int ispoln = spcColPar.ispoln;
                                    int blok = spcColPar.block;
                                    
                                    //Console.WriteLine(spcColPar.);

                                    //SpcTuningStyleParam StyleSpc = (SpcTuningStyleParam)kompas.GetParamStruct((short)StructType2DEnum.ko_SpcColumnParam);

                                    //Console.WriteLine(StyleSpc.countIspoln);

                                    // по типу колонки, номеру исполнения и блоку определим номер колонки
                                    int colNumb = specification.ksGetSpcColumnNumb(obj, //объект спецификации
                                        spcColPar.columnType, spcColPar.ispoln, spcColPar.block);

                                    string text = specification.ksGetSpcObjectColumnText(obj, columnType, ispoln, blok);

                                    if (colNumb == 4 || colNumb == 5)
                                    {
                                        text=text.Replace("@/", "");
                                        text = text.Replace("/", "-");
                                    }
                                    //Получить строку с текстом из определенной колонки объекта спецификации
                                    obj_text[colNumb - 1] = text;
                                    

                                }
                            }

                            

                            obj_text[7] = specification.ksGetSpcSectionName(obj);
                            
                            objSpc.Add(new ObjSpc(1, null)
                            {
                                format = obj_text[0],
                                zone = obj_text[1],
                                pos = obj_text[2],
                                mark = obj_text[3],
                                name = obj_text[4],
                                count = obj_text[5],
                                note = obj_text[6],
                                section = obj_text[7]
                            }); //Добавить сведения на ссылаемые данные

                            count++;
                        }
                        while ((obj = iter.ksMoveIterator("N")) != 0);
                    }
                }
            }
            else
                kompas.ksError("Спецификация должна быть текущей");

            return DeleteEmptyString(objSpc);
        }

        public List<ObjSpc> ReadSpcGroup()
        {
            
            const int countSpcColumn = 7;  //количество колонок в СП
            string[] obj_text = new string[countSpcColumn + 2];

            ksDocument2D doc = (ksDocument2D)kompas.Document2D();
            ksSpcDocument spc = (ksSpcDocument)kompas.SpcActiveDocument();                 
            

            if (doc != null && spc != null && spc.reference != 0)
            {
                ksSpecification specification = (ksSpecification)spc.GetSpecification();

                SpcTuningStyleParam StyleSpc = (SpcTuningStyleParam)kompas.GetParamStruct((short)StructType2DEnum.ko_SpcTuningStyleParam);

                specification.ksGetTuningSpcStyleParam(-1, StyleSpc);  // Считать параметры в переменную StyleSpc

                int CountIsp = StyleSpc.countIspoln; //Количество исполнений заданное в настройках (для групповой СП)

                uint[] NIspoln = new uint[CountIsp]; //массив в котором хранится количество изделий на конкретное исполнение


                var objSpc = new List<ObjSpc>();

                ksIterator iter = (ksIterator)kompas.GetIterator();
                iter.ksCreateSpcIterator(null, 0, 2); // Иттератор для каких объектов?
                if (iter.reference != 0 && specification != null)
                {
                    int obj = iter.ksMoveIterator("F");
                    if (obj != 0)
                    {
                        int count = 0;
                        do
                        {
                            string buf = string.Format("Объект спецификации №{0}", count);


                            //ksSpcColumnParam spcColPar = (ksSpcColumnParam)kompas.GetParamStruct((short)StructType2DEnum.ko_SpcColumnParam);
                            //if (specification.ksGetSpcColumnType(obj,   //объект спецификации  (функция - по номеру колонки для данного объекта спецификации получить параметры колонки)
                            //    3,                                      // номер колонки, начиная с 1
                            //    spcColPar) == 1)
                            //{
                            //    //Console.WriteLine(blok.ToString());
                            //    //Console.WriteLine(specification.ksGetSpcPerformanceName(15, 1, 9)); //ksGetTuningSpcStyleParam


                            

                            //    //specification.ksGetTuningSpcStyleParam

                            //}


                            // пройдем по всем колонкам
                            for (int i = 1; i <= countSpcColumn; i++)
                            {
                                // для текущего объекта определим тип колонки, номер исполнения и блок
                                ksSpcColumnParam spcColPar = (ksSpcColumnParam)kompas.GetParamStruct((short)StructType2DEnum.ko_SpcColumnParam);
                                if (specification.ksGetSpcColumnType(obj,   //объект спецификации  (функция - по номеру колонки для данного объекта спецификации получить параметры колонки)
                                    i,                                      // номер колонки, начиная с 1
                                    spcColPar) == 1)
                                {
                                    // возьмем текст
                                    int columnType = spcColPar.columnType;
                                    int ispoln = spcColPar.ispoln;
                                    int blok = spcColPar.block;                                                                     

                                    // по типу колонки, номеру исполнения и блоку определим номер колонки
                                    int colNumb = specification.ksGetSpcColumnNumb(obj, //объект спецификации
                                        spcColPar.columnType, spcColPar.ispoln, spcColPar.block);

                                    //Получить строку с текстом из определенной колонки объекта спецификации
                                    if(spcColPar.columnType == 6)
                                    {
                                        for(int j=0; j < CountIsp; j++)
                                        {
                                            try
                                            {
                                                NIspoln[j] = Convert.ToUInt32(specification.ksGetSpcObjectColumnText(obj, columnType, ispoln, j));
                                            }
                                            catch
                                            {
                                                NIspoln[j] = 0;
                                            }
                                        }
                                        
                                    }
                                    else
                                        obj_text[colNumb - 1] = specification.ksGetSpcObjectColumnText(obj, columnType, ispoln, blok);


                                }
                            }


                            //objSpc.Last<ObjSpc>().mcount[5]=10;
                            obj_text[7] = specification.ksGetSpcSectionName(obj);

                            

                            objSpc.Add(new ObjSpc(CountIsp, NIspoln)
                            {
                                format = obj_text[0],
                                zone = obj_text[1],
                                pos = obj_text[2],
                                mark = obj_text[3],
                                name = obj_text[4],
                                count = obj_text[5],
                                note = obj_text[6],
                                section = obj_text[7],                                
                            }); //Добавить сведения на ссылаемые данные


                            count++;
                        }
                        while ((obj = iter.ksMoveIterator("N")) != 0);

                    }
                }
                return DeleteEmptyString(objSpc);
            }
            else
            {
                Console.WriteLine("Спецификация должна быть текущей");
                return null;
            }
                

            
        }

        private List<ObjSpc> DeleteEmptyString(List<ObjSpc> objSpc)  //функция удаления пустых строк из спецификации
        {
            if(objSpc!=null)
            {
                int count = 0;
                //int objSpcCount = 0;
                while (true)
                {

                    if (objSpc[count].section == "Документация")
                    {
                        if (objSpc[count].mark == "" || objSpc[count].name == "")
                        {
                            objSpc.RemoveAt(count);
                            count--;
                        }
                    }
                    else // Сборочные единицы и детали, для стандарных изделий нужно сделать отдельный
                    {
                        if (objSpc[count].mark == "" || objSpc[count].name == "")
                        {
                            objSpc.RemoveAt(count);
                            count--;
                        }

                    }

                    //objSpcCount = objSpc.Count;
                    count++;

                    if (count == objSpc.Count)
                        break;

                }

                return objSpc;
            }
            return null;

            
        }


        public void SafeFileToPDF(string PathFile) //функция сохранения в pdf формат спецификации
        {
            ksSpcDocument spc = (ksSpcDocument)kompas.SpcActiveDocument();
            spc.ksSaveDocument(PathFile);
            spc.ksCloseDocument(); //нужно ли закрывать документ??
        }

        public void File2DSaveAs(string PathFile) //функция сохранения в pdf формат 2D документа - чертежа. Данная функция не проверена
        {
            ksDocument2D doc2d = (ksDocument2D)kompas.ActiveDocument2D();
            doc2d.ksSaveDocument(PathFile);
            doc2d.ksCloseDocument(); //нужно ли закрывать документ??
        }

        public void SafeFileToPDF(ksDocument3D doc3d) //функция сохранения в pdf формат 3D документа - модели. Данная функция не проверена
        {
            doc3d.SaveAs(@"C:\Users\aleksey\Desktop\экс\Спецификация.pdf");
            doc3d.close(); //нужно ли закрывать документ??
        }

        public void SafeFileToPDF(ksDocumentTxt docTxt) //функция сохранения в pdf формат текстового документа. Данная функция не проверена
        {
            docTxt.ksSaveDocument(@"C:\Users\aleksey\Desktop\экс\Спецификация.pdf");
            docTxt.ksCloseDocument(); //нужно ли закрывать документ??
        }

        public string ReadStampDraw(int NumbGraph)
        {
            ksDocument2D doc = (ksDocument2D)kompas.Document2D();

            string stampSTR = "";

            ksStamp stamp = (ksStamp)doc.GetStamp();
            if (stamp != null && stamp.ksOpenStamp() == 1)
            {
                int numb = 0;
                //в цикле будем брать все существующие графы
                stamp.ksColumnNumber(NumbGraph);
                ksDynamicArray arr = (ksDynamicArray)stamp.ksGetStampColumnText(ref numb);
                ksTextItemParam item = null;
                if (numb != 0 && arr != null)
                {
                    string buf = string.Empty;

                    ksTextLineParam itemLineText = (ksTextLineParam)kompas.GetParamStruct((short)StructType2DEnum.ko_TextLineParam);
                    if (itemLineText == null)
                        return "False";
                    itemLineText.Init();

                    for (int i = 0, count = arr.ksGetArrayCount(); i < count; i++)
                    {
                        arr.ksGetArrayItem(i, itemLineText);

                        ksDynamicArray arrpTextItem = (ksDynamicArray)itemLineText.GetTextItemArr();
                        item = (ksTextItemParam)kompas.GetParamStruct((short)StructType2DEnum.ko_TextItemParam);

                        if (item == null || arrpTextItem == null)
                            return "False";
                        item.Init();

                        for (int j = 0, count2 = arrpTextItem.ksGetArrayCount(); j < count2; j++)
                        {
                            arrpTextItem.ksGetArrayItem(j, item);
                            
                            //Определяем нужно ли добавлять пробелы                                                
                            if (NumbGraph == 1) //Если строка наименования изделия, то пробелы нужны!
                            {
                                
                                if (i != 0 || j != 0) //Если не начала i и j циклов то определяем последний элемент
                                {
                                    int len = stampSTR.Length-1;
                                    try
                                    {
                                        if (stampSTR.Substring(len) == " " || stampSTR.Substring(len) == "-"
                                                                                || item.s.Substring(0, 1) == " " || item.s.Substring(0, 1) == "-") //Если строка не начинается/заканчивается с пробела или дефиса
                                        {
                                            stampSTR += item.s;
                                        }
                                        else
                                            stampSTR += " " + item.s;
                                    }
                                    catch
                                    {
                                        stampSTR += item.s;
                                    }
                                }
                                else //Если начало цикла, то просто добавляем элемент
                                    stampSTR += item.s;
                            }
                            else
                                stampSTR += item.s;


                        }
                        arrpTextItem.ksDeleteArray();  //очистим массив компонент
                    }

                    arr.ksDeleteArray();

                }

                stamp.ksCloseStamp();
            }
            else
                kompas.ksError("Штамп не найден");


            stampSTR = stampSTR.Replace("/", "-");

            return stampSTR;

            //kompas.ksMessageBoxResult(); //Выдает сообщение об успешном или не успешном выполнении функции в Компасе


        }

        public string ReadStampSpc(int NumbGraph)
        {
            ksSpcDocument spc = (ksSpcDocument)kompas.SpcActiveDocument();

            string stampSTR = "";

            ksStamp stamp = (ksStamp)spc.GetStamp();
            if (stamp != null && stamp.ksOpenStamp() == 1)
            {
                int numb = 0;

                stamp.ksColumnNumber(NumbGraph);
                ksDynamicArray arr = (ksDynamicArray)stamp.ksGetStampColumnText(ref numb);
                ksTextItemParam item = null;
                if (numb != 0 && arr != null)
                {
                    string buf = string.Empty;

                    ksTextLineParam itemLineText = (ksTextLineParam)kompas.GetParamStruct((short)StructType2DEnum.ko_TextLineParam);
                    if (itemLineText == null)
                        return "False";
                    itemLineText.Init();

                    for (int i = 0, count = arr.ksGetArrayCount(); i < count; i++)
                    {
                        arr.ksGetArrayItem(i, itemLineText);

                        ksDynamicArray arrpTextItem = (ksDynamicArray)itemLineText.GetTextItemArr();
                        item = (ksTextItemParam)kompas.GetParamStruct((short)StructType2DEnum.ko_TextItemParam);

                        if (item == null || arrpTextItem == null)
                            return "False";
                        item.Init();

                        for (int j = 0, count2 = arrpTextItem.ksGetArrayCount(); j < count2; j++)
                        {
                            arrpTextItem.ksGetArrayItem(j, item);

                            //Определяем нужно ли добавлять пробелы                                                
                            if (NumbGraph == 1) //Если строка наименования изделия, то пробелы нужны!
                            {

                                if (i != 0 || j != 0) //Если не начала i и j циклов то определяем последний элемент
                                {
                                    int len = stampSTR.Length - 1;
                                    try
                                    {
                                        if (stampSTR.Substring(len) == " " || stampSTR.Substring(len) == "-"
                                            || item.s.Substring(0, 1) == " " || item.s.Substring(0, 1) == "-") //Если строка не начинается/заканчивается с пробела или дефиса
                                        {
                                            stampSTR += item.s;
                                        }
                                        else
                                            stampSTR += " " + item.s;
                                    }
                                    catch
                                    {
                                        stampSTR += item.s;
                                    }
                                    
                                }
                                else //Если начало цикла, то просто добавляем элемент
                                    stampSTR += item.s;
                            }
                            else
                                stampSTR += item.s;


                        }
                        arrpTextItem.ksDeleteArray();  //очистим массив компонент
                    }

                    arr.ksDeleteArray();

                }

                stamp.ksCloseStamp();
            }
            else
                kompas.ksError("Штамп не найден");

            stampSTR = stampSTR.Replace("/", "-");

            return stampSTR;

            //kompas.ksMessageBoxResult(); //Выдает сообщение об успешном или не успешном выполнении функции в Компасе


        }

        public void OpenFile(string PathFile, bool flag) // flag = true - открытие только на чтение, flag = false - открытие с полным доступом к файлу
        {       
            if (kompas != null)
            {                
                //добвать функцию сравнения открытого файла со СП
                if (true)
                {
                    // Открыть документ с диска
                    // первый параметр - имя открываемого файла
                    // второй параметр указывает на необходимость выдачи запроса "Файл изменен. Сохранять?" при закрытии файла
                    // третий параметр - указатель на IDispatch, по которому График вызывает уведомления об изменении своего состояния
                    // ф-ия возвращает HANDLE открытого документа

                    int type = kompas.ksGetDocumentTypeByName(PathFile);
                    ksDocument3D doc3D;
                    ksDocument2D doc2D;
                    ksSpcDocument docSpc;
                    ksDocumentTxt docTxt;
                    switch (type)
                    {
                        case (int)DocType.lt_DocPart3D:         //3d документы
                        case (int)DocType.lt_DocAssemble3D:
                            doc3D = (ksDocument3D)kompas.Document3D();
                            if (doc3D != null)
                                doc3D.Open(PathFile, false);
                            break;
                        case (int)DocType.lt_DocSheetStandart:  //2d документы
                        case (int)DocType.lt_DocFragment:
                            doc2D = (ksDocument2D)kompas.Document2D();
                            if (doc2D != null)
                                doc2D.ksOpenDocument(PathFile, false);
                            break;
                        case (int)DocType.lt_DocSpc:                //спецификации
                            docSpc = (ksSpcDocument)kompas.SpcDocument();
                            if (docSpc != null)
                                docSpc.ksOpenDocument(PathFile, 0);
                            break;
                        case (int)DocType.lt_DocTxtStandart:        //текстовые документы
                            docTxt = (ksDocumentTxt)kompas.DocumentTxt();
                            if (docTxt != null)
                                docTxt.ksOpenDocument(PathFile, 0);
                            break;
                    }
                    int err = kompas.ksReturnResult();
                    if (err != 0)
                        kompas.ksResultNULL();
                }
            }
            else
            {
                MessageBox.Show("Объект не захвачен", "Сообщение");

            }
        }

        public void Visible(bool swt)
        {
            try
            {
                if (kompas != null)
                {
                    kompas.Visible = swt;
                }
                else
                    Console.WriteLine("Объект не захвачен (нет подключения к Компасу!)");
            }
            catch (Exception ex)
            {
                ex.ToString();
                Console.WriteLine("Неизвестная ошибка");
                Marshal.ReleaseComObject(kompas);
                kompas = null;
            }
        }

        public string GetStyleSheet()
        {
            //SpcTuningStyleParam Isp = (SpcTuningStyleParam)kompas.GetParamStruct((short)StructType2DEnum.ko_SpcTuningStyleParam);

            //    ksSpcObjParam Isp = (ksSpcObjParam)kompas.GetParamStruct((short)StructType2DEnum.ko_SpcObjParam);

            ksDocument2D doc = (ksDocument2D)kompas.Document2D();
            //ksDocumentParam par = (ksDocumentParam)kompas.DocumentParam();
            ksSpcDocument spc = (ksSpcDocument)kompas.SpcActiveDocument();

            if (doc != null && spc != null && spc.reference != 0)
            {
                ksSpecification specification = (ksSpecification)spc.GetSpecification();

                //ksSpcStyleParam par = (ksSpcStyleParam)kompas.GetParamStruct((short)StructType2DEnum.ko_SpcStyleParam);

               ksSheetPar par = (ksSheetPar)kompas.GetParamStruct((short)StructType2DEnum.ko_SheetPar);
                //doc.Param
                //specification.GetTu(null, 5, par, 0);  // Считать параметры в переменную StyleSpc
                return par.layoutName;
            }
            else
                return null;

            //SpcTuningStyleParam StyleSpc = (SpcTuningStyleParam)kompas.GetParamStruct((short)StructType2DEnum.ko_SpcColumnParam);
        }

        private void ToGetInfoModel(ksFeature ipart)
        {
            //buf = string.Format("Имя компоненты {0}", part.name);
            //kompas.ksMessage(buf);
            //part.name = "Втулка";
            //part.fileName = "C:\\Users\\Aleksey\\Desktop\\Models\\Gear-shaft.m3d";
            //part.Update();                
            ksFeatureCollection iFeatureCollection = ipart.SubFeatureCollection(true, false);
            //Console.WriteLine(iFeatureCollection.GetCount());
            int featureCount = iFeatureCollection.GetCount();
            for (int i = 0; i < featureCount; i++)
            {
                //Console.WriteLine();
                //Console.WriteLine();
                ksFeature iFeature = iFeatureCollection.GetByIndex(i);
                if (iFeature.type == 104) //o3d_part - Деталь
                {

                    //int iEntity = iFeature.GetObject().type;
                    if (iFeature.GetObject() == null)
                    {
                        //Console.WriteLine(iEntity);
                        continue;
                    }
                    else
                    {
                        if (iFeature.GetObject().IsDetail())
                        {
                            Console.Write("Деталь - ");
                            Console.Write(iFeature.GetObject().marking);
                            Console.Write(" _ ");
                            Console.Write(iFeature.GetObject().name);
                            Console.Write(" _ ");
                            Console.WriteLine(iFeature.GetObject().filename);
                        }
                        else
                            Console.WriteLine("Сборка");
                    }
                }
                else if (iFeature.type == 105) //o3d_entity - Объект
                {
                    int iEntity = iFeature.GetObject().type;
                    if (iFeature.GetObject() == null)
                    {
                        //Console.WriteLine(iEntity);
                        continue;
                    }
                    else if (iEntity == 63)
                    {
                        //Console.WriteLine("МакроОбъект");
                        ToGetInfoDraw(iFeature);
                        //сделать рекурсию
                    }
                }
                //Console.WriteLine(iFeature.type);
            }

        }

        public void Read3DModel(string path) //Нужно две ссылки. Одна для открытия модели. Другая куда сохранить модель.
        {
            OpenFile(path, true); //Откыть документ

            ksDocument3D doc3D;
            doc3D = (ksDocument3D)kompas.ActiveDocument3D(); //Получить ссылку на документ 3D

            ksPart part = (ksPart)doc3D.GetPart(-1);  //верхний компонент сбоки/детали (short)Part_Type.pTop_Part
            if (part != null)
            {
                Console.WriteLine(part.name); //Также данное поле не должно быть пустым
                Console.WriteLine(part.marking); //Если обозначение пустое, нужна сообщить пользователю
                Console.WriteLine(part.fileName); //Проверить на существование ссылки
                //Здесь нужно проверить добавлена ли модель в архив
                //Если не добавлена нужно сохранить его по ссылки (чертеж и pdf- представление)
                ksFeature ipart = part.GetFeature(); //Получить объект дерева, связанный с данным телом. Получаем дерево
                ToGetInfoModel(ipart); //Обработаем полученное дерево модели на присутствие других моделей
            }
        }

        public void ToGetInfoDraw(string path) //Функция должна принимать путь по которому она должна сохранить 3D-модели. После чего заменяет модели в чертеже и перестраивает чертеж.
        {
            //Функция для получения из чертежа Компаса ссылки на модели
            ksAssociationViewParam doc;
            doc = (ksAssociationViewParam)kompas.GetParamStruct((short)StructType2DEnum.ko_AssociationViewParam); //Получаем ссылку на параметры? //Нужно переименовать объект
            doc.Init(); //Нужен ли данный оператор? нужно проверять.
            ksDocument2D doc2d = (ksDocument2D)kompas.ActiveDocument2D(); // Получаем ссылку на документ 2D

            ksIterator iter = (ksIterator)kompas.GetIterator(); //Определяем итератор (указатель)
            iter.ksCreateIterator(ldefin2d.VIEW_OBJ, 0); // Иттератор для каких объектов для видом чертежа.

            if (iter.reference != 0) //Если удалось инициализировать итератор
            {
                //Console.WriteLine("итератор работает");
                int obj = iter.ksMoveIterator("F"); //Переместить итератор на первый объект

                if (obj != 0)
                {
                    do
                    {
                        doc2d.ksGetObjParam(obj, doc, ldefin2d.ASSOCIATION_VIEW_PARAM); //Получаем параметры чертежа.
                        if (doc.fileName != "") //Т.е. если у вида есть 3D-модель, то обработаем ее
                        {
                            //Здесь нужно проверять не ссылается ли модель на другие модели. Возможно для этого необходимо окрыть модель, заодно можно получить ее pdf-представление
                            //Console.WriteLine("Имя файла");
                            //Console.WriteLine(doc.fileName);
                            Read3DModel(doc.fileName); //Обработаем модель
                            doc.fileName = "C:\\Users\\Aleksey\\Desktop\\Models\\Receptacle.m3d"; //Нужна переменная
                            doc2d.ksSetObjParam(obj, doc, ldefin2d.ASSOCIATION_VIEW_PARAM); // Применим заданные параметры
                            doc2d.ksRebuildDocument(); // Перестроить чертеж //Кстати такой оператор нужен и 3Д
                            //Console.WriteLine(doc.fileName);
                        }
                    }
                    while ((obj = iter.ksMoveIterator("N")) != 0);

                }
            }

        }

    }

 
}
