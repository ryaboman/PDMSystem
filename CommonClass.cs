using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using WorkingKompas;
using WorkingObjSpc;

namespace PDMS
{
    class CommonClass
    {
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        //openFileDialog1.

        ///to do: сделать форму для фомирования состава изделия в ручную
        ///
        FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

        //WorkingRegFile regFile = new WorkingRegFile();

        string TempPathDirectory = null;  //Путь к временной директории
        string TempPathRegFile = null; //временый реестр

        string ArchivPathDirectory = null;
        string ArchivPathRegFile = null; //реестр в архиве


        string LinkFile = Directory.GetCurrentDirectory() + "\\link";
        

        //в данной функции наверное нужно читать RegFile
        public bool Initialize()
        {
            
            if (File.Exists(LinkFile))
            {
                //читаем файл
                using (StreamReader sr = File.OpenText(LinkFile))
                {
                    string STR = sr.ReadToEnd();

                    //WorkingRegFile LkFile = new WorkingRegFile();

                    TempPathDirectory = new SpcRegFile().select(STR, "<TempPathDirectory>", "</TempPathDirectory>");

                    ArchivPathDirectory = new SpcRegFile().select(STR, "<ArchivPathDirectory>", "</ArchivPathDirectory>");                
                }
            }
            else
            {

                //создаем файл и спрашиваем пользователя указать ссылки
                using (StreamWriter fs = new StreamWriter(LinkFile, false))
                {
                    //Здесь нужно будет попросить пользователя указать директории                    
                    MessageBox.Show("Необходимо указать директорию архива", "Отсутствует файл link");
                    DialogResult result = folderBrowserDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        ArchivPathDirectory = folderBrowserDialog1.SelectedPath;

                        fs.Write("<ArchivPathDirectory>");
                        fs.Write(ArchivPathDirectory);
                        fs.Write("</ArchivPathDirectory>");

                        MessageBox.Show("Необходимо указать временную директорию", "Отсутствует файл link");
                        result = folderBrowserDialog1.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            
                            TempPathDirectory = folderBrowserDialog1.SelectedPath;

                            fs.Write("<TempPathDirectory>");
                            fs.Write(TempPathDirectory);
                            fs.WriteLine("</TempPathDirectory>");
                            fs.WriteLine();
                            
                        }

                    }

                }
            }

            if (TempPathDirectory == null || ArchivPathDirectory == null)
            {
                MessageBox.Show("Неудалось инициализировать класс Adder. Обратитесь к разработчику программы.");
                return false;
            }

            ArchivPathRegFile = ArchivPathDirectory + "\\RegFile";
            TempPathRegFile = TempPathDirectory + "\\RegFile";

            return true;
        }

        public void AddDevice()
        {
            Kompas kompas = new Kompas();
            WorkingRegFile regFile = new WorkingRegFile(ArchivPathDirectory);

            //открыть окно выбора файла
            openFileDialog1.Filter = "Спецификация (*.spw)|*.spw";
            DialogResult result = openFileDialog1.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                string PathDevice = openFileDialog1.FileName;

                List<string> ListPath;

                //MessageBox.Show(kompas.GetStyleSheet());

                ListPath = FunMain(kompas, PathDevice, regFile);
                regFile.WriteRegFile(TempPathRegFile, false); //Запись данных в реестр RegFile, который находится в temp

                MessageBox.Show("Предварительный состав изделия сформирован. " +
                                "Пожалуйста проверьте его. " + 
                                "Если все добавилось корректно - добавьте изделия в архив, вызвав контекстное меню");
            }
            

        }

        public void SearchDevice(TreeView tree, string str, bool MarkName)
        {
            //переменная MarkName - это переключатель поиска между поиском по обозначению и наименованию
            //Kompas kompas = new Kompas();
            WorkingRegFile regFile = new WorkingRegFile(ArchivPathDirectory); //Где-то нужно убедиться существует ли RegFile в папке
            SpcFile spc = new SpcFile();
            List<DrawObj> ListDevice = new List<DrawObj>();

            ListDevice = regFile.SearchDevice(str, MarkName);

            for (int i = 0; i < ListDevice.Count; i++)
            {
                string pth = ArchivPathDirectory + "\\" + ListDevice[i].path;
                string NameNode = ListDevice[i].mark + " _ " + ListDevice[i].name;

                string ext = Path.GetExtension(ListDevice[i].path);
                if (ext == ".SpcFile") //если изделие - сборочная единица то читаем SpcFile
                {
                                        
                    TreeNode nod = new TreeNode(NameNode);

                    spc.GetTreeNod(pth, ref nod);
                    tree.Nodes.Add(nod);
                }
                else //если изделие - деталь
                {
                    TreeNode nod = new TreeNode(NameNode);
                    tree.Nodes.Add(nod);
                    tree.Nodes[i].Tag = pth + ".pdf";
                }

            }

            //public List<DrawObj> SearchDevice(string str, bool MarkName)

        }

        public void ShowDevice(TreeView tree)
        {
            Kompas kompas = new Kompas();
            WorkingRegFile regFile = new WorkingRegFile(TempPathDirectory);

            SpcFile spc = new SpcFile();

            List<string> path = regFile.PathDevice();

            for (int i = 0; i < path.Count; i++)
            {
                string ext = Path.GetExtension(path[i]);
                if (ext == ".SpcFile")
                {
                    string pth = TempPathDirectory + "\\" + path[i];
                    string NameNode = Path.GetFileNameWithoutExtension(pth);

                    TreeNode nod = new TreeNode(NameNode);

                    spc.GetTreeNod(pth, ref nod);
                    tree.Nodes.Add(nod);
                }

            }

        }

        public void MoveDeviceInArhiv()
        {
            string PathFrom = TempPathDirectory;
            string PathTo = ArchivPathDirectory;

            FinishingAddDevice(PathFrom, PathTo);
        }

        private List<string> FunMain(Kompas kompas, string path, WorkingRegFile regFile) //Функция возвращает относительные пути к спецификации добавляемым объектов
        {         

            string MarkName, FildName, FildMark; //строки для хранения (str = наименования + обозначения изделия) наименования и обозначения изделия

            List<string> ReturnValue = new List<string>();  //возвращаемое значение функции - ссылки на добавленные папки



            string PathDeviceDirectory = Path.GetDirectoryName(path); // директория добавляемого КД. Грубо говоря это директория где находится спецификация изделия

            string extension;
            extension = Path.GetExtension(path);  // Расширение указанного файла (СП? или чертеж?, а может 3Д?)

            if (true) //возможно нужно убедиться что это спецификация, а не, например ВП, прочитав шаблом документа ".spw"
            {
                kompas.OpenFile(path, true);   //открываем файл в компасе

                FildName = kompas.ReadStampSpc(1); //читаем поле наименование из основной надписи
                FildMark = kompas.ReadStampSpc(2); //читаем поле обозначение из основной надписи

                string Str = regFile.SearchDrawObj(FildMark);

                if (Str == null) // Выполняем поиск данного изделия в списке объектов (в RegFile), если не нашли, то продолжаем добавление
                {
                    MarkName = FildMark + " _ " + FildName; //переменная MarkName необходима для создания директории
                    
                    string PathDeviceTemp = CreateDirectoryMark(Path.Combine(TempPathDirectory, MarkName)); //создаем директорию (во временной папке PathDeviceDirectory) имя которой состоит из обозначения и наименования изделия                    

                    DeleteSpaseToPath(ref MarkName); // удалить пробелы в конце строки. Данная функция нужна для того, что иногда разработчик случайно вводит пробелы в конце строки наименования

                    string RelativePath = MarkName;  //относительный путь, который будет записывать в SpcFile                

                    List<DrawObj> DrawList = regFile.transform(kompas.ReadSpc()); // Лист дерева изделий из СП //Читаем спецификацию (Необходимо дополнить объект objspc путями к файлам)

                    regFile.ToRankingFunction(ref DrawList);    //Перемещаем объекты из начала списка, содержащиеся в секции "Документация", в конец списка
                    //Данное перемещение нужно для того чтобы модель-сбока сборочного чертежа обрабатывалась в конце процесса

                    string FileName = Path.GetFileName(path); //Что это? 

                    CopyFileDraw(path, PathDeviceTemp + "\\" + MarkName + extension); //копируем объект во временную папку PathDeviceTemp

                    kompas.SafeFileToPDF(PathDeviceTemp + "\\" + MarkName + extension + ".pdf");  //сохранить файл в pdf формате во временную папку PathDeviceTemp

                    foreach(var draw_obj in DrawList)
                    {
                        for (int i = 0, count = DrawList.Count; i < count; i++) // для каждого изделия из спецификации
                        {
                            switch (DrawList[i].section)
                            {
                                case "Документация":
                                    {
                                        string[] dirs = Directory.GetFiles(PathDeviceDirectory, DrawList[i].mark + " _ *");

                                        string DocMarkName = DrawList[i].mark + " _ " + DrawList[i].name;
                                        string DocNameMark = DrawList[i].name + " _ " + DrawList[i].mark;


                                        string Dir = null;

                                        foreach (string dir in dirs)
                                        {
                                            string ext = Path.GetExtension(dir);
                                            if (ext == ".spw" || ext == ".cdw")
                                            {
                                                Dir = dir;
                                                break;
                                            }
                                        }

                                        if (Dir == null)
                                        {
                                            MessageBox.Show("Не найден чертеж изделия " + DocNameMark + ". Укажите его вручную");

                                            openFileDialog1.Filter = "Чертеж (*.cdw)|*.cdw|ВП (*.spw)|*.spw";
                                            DialogResult result = openFileDialog1.ShowDialog();

                                            if (result == DialogResult.OK)
                                            {
                                                Dir = openFileDialog1.FileName;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        kompas.OpenFile(Dir, true);

                                        string ex = Path.GetExtension(Dir);
                                        string FileNameExt = DocMarkName + ex;
                                        if (ex == ".spw")
                                            kompas.SafeFileToPDF(Path.Combine(PathDeviceTemp, FileNameExt + ".pdf"));
                                        else
                                            kompas.File2DSaveAs(Path.Combine(PathDeviceTemp, FileNameExt + ".pdf"));

                                        DrawList[i].path = RelativePath + "\\" + FileNameExt;  //запишим путь к файлу для добавления в SpcFile нужно ли здесь расширение
                                        CopyFileDraw(Dir, Path.Combine(PathDeviceTemp, FileNameExt)); //копируем объект


                                    }
                                    break;
                                case "Комплексы":
                                    break;
                                case "Сборочные единицы": // Спецификация
                                    {
                                        string SDO = regFile.SearchDrawObj(DrawList[i].mark);

                                        if (SDO != null)
                                        {
                                            DrawList[i].path = SDO;  //Если изделие содержится в RegFile, то копируем ссылку на него
                                                                     //Здесь нужно дополнить его карточку применяемости
                                        }
                                        else
                                        {
                                            //Иначе пытаемся найти изделие в папках пользователя

                                            //нужно указать где искать, т.е. в какой папке проводить поиск
                                            string[] dirs = Directory.GetFiles(PathDeviceDirectory, DrawList[i].mark + " _ *");

                                            string Dir = null;

                                            foreach (string dir in dirs)  //Ищем спецификацию на изделие
                                            {
                                                string ext = Path.GetExtension(dir);
                                                if (ext == ".spw")
                                                {
                                                    Dir = dir;
                                                    break;
                                                }
                                            }

                                            if (Dir == null)
                                            {
                                                MessageBox.Show("Не найдена СП изделия " + DrawList[i].name + "  " + DrawList[i].mark + ". Укажите ее вручную");

                                                openFileDialog1.Filter = "Спецификация (*.spw)|*.spw";
                                                DialogResult result = openFileDialog1.ShowDialog();

                                                if (result == DialogResult.OK)
                                                {
                                                    Dir = openFileDialog1.FileName;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }

                                            ReturnValue.AddRange(FunMain(kompas, Dir, regFile));
                                            DrawList[i].path = ReturnValue.Last();

                                        }


                                    }
                                    break;
                                case "Детали":
                                    {
                                        ComponentSection(kompas, regFile, draw_obj, PathDeviceDirectory, PathDeviceTemp, RelativePath); //DrawList                                        
                                    }
                                    break;
                                case "Стандартные изделия":
                                    break;
                                case "Прочие изделия":
                                    break;
                                case "Материалы":
                                    break;
                                case "Комплекты":
                                    break;
                                default: break;
                            }
                        }
                    }
                    

                    string FileNameWithoutExt = MarkName;
                    regFile.WriteSpcFile(DrawList, PathDeviceTemp + "\\" + FileNameWithoutExt + ".SpcFile"); //Записываем все данные из спецификации в SpcFile, где str наименование файла с расширением .SpcFile


                    regFile.Drawobj = new DrawObj
                    {
                        mark = FildMark,
                        name = FildName,
                        path = RelativePath + "\\" + FileNameWithoutExt + ".SpcFile"
                    };                    

                    ReturnValue.Add(RelativePath + "\\" + FileNameWithoutExt + ".SpcFile");

                    return ReturnValue;
                }
                else // Если нашли то выдаем сообщение
                {
                    MessageBox.Show("Изделие уже находится в архиве!");
                    ReturnValue.Add(Str);
                    return ReturnValue;
                }
            }
            else
            {
                return null;
            }

        }

        void ComponentSection(Kompas kompas, WorkingRegFile regFile, DrawObj DrawObj, string PathDeviceDirectory, string PathDeviceTemp, string RelativePath)
        {
            string DocMarkName = DrawObj.mark + " _ " + DrawObj.name; //Формируем имя документа/файла
            string DocNameMark = DrawObj.name + " _ " + DrawObj.mark; //Формируем имя документа/файла

            string SDO = regFile.SearchDrawObj(DrawObj.mark); //Существует ли данное изделие в реестре

            if (SDO != null) //Если да то копируем ссылку
            {
                DrawObj.path = SDO;  //Копируем ссылку на изделие
                                         //Здесь нужно дополнить его карточку применяемости
            }
            else
            {
                //Иначе пытаемся найти изделие в папках пользователя

                string[] dirs = Directory.GetFiles(PathDeviceDirectory, DrawObj.mark + " _ *");

                string Dir = null;

                foreach (string dir in dirs)
                {
                    string ext = Path.GetExtension(dir);
                    if (ext == ".cdw")
                    {
                        Dir = dir; //Запомним путь к чертежу
                        break;
                    }
                }

                if (Dir == null)
                {
                    MessageBox.Show("Не найден чертеж изделия " + DrawObj.name + " " + DrawObj.mark + ". Укажите его вручную");

                    openFileDialog1.Filter = "Чертеж (*.cdw)|*.cdw";
                    DialogResult result = openFileDialog1.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        Dir = openFileDialog1.FileName;
                    }
                    else
                    {
                        return;
                    }
                }

                if (Dir != null) //Если переменная пути на файл не пустая то..
                {
                    //Нужно создавать директорию для детали в сборке.
                    //В ней будут храниться различные представления для детали: чертеж, 3D-модель, pdf-представление, технологическая документация и прочее (различные статьи, документы относящиеся к ее изготовлению).

                    string FileNameExt = DocMarkName + ".cdw";

                    kompas.OpenFile(Dir, true);

                    string FildName = kompas.ReadStampDraw(1); //читаем поле наименование из основной надписи
                    string FildMark = kompas.ReadStampDraw(2); //читаем поле обозначение из основной надписи
                    string MarkName = FildMark + " _ " + FildName;
                    //Нужно счить информацию из чертежа (разработчик, Проверяющий, Утверждающий, Материал [и материал-заменитель], масса и др.)                    

                    string PathComponentTemp = CreateDirectoryMark(Path.Combine(PathDeviceTemp, MarkName));

                    kompas.ToGetInfoDraw(PathComponentTemp); //Обработка модели детали

                    kompas.File2DSaveAs(Path.Combine(PathComponentTemp, FileNameExt)); //"Сохранить как ..." потому что мы будем менять ссылки на модели
                    kompas.File2DSaveAs(PathComponentTemp + "\\" + FileNameExt + ".pdf");

                    DrawObj.path = RelativePath + "\\" + FileNameExt;  //запишим путь к файлу для добавления в SpcFile //здесь возможно нужно без расширения                    
                                                                       //Где-то здесь нужно создавать карточку изделия, с информацией о применяемости данного изделия
                    regFile.Drawobj = DrawObj;
                }

            }
        }

        public string CreateDirectoryMark(string path)  //Создаем папку по указанному пути и все другие верхнии папки из пути если они не созданные
        {
            Console.WriteLine("Пытаемся создать папку");
            // Specify the directory you want to manipulate.
            
            try
            {
                // Determine whether the directory exists.

                if (Directory.Exists(path))
                {
                    DeleteSpaseToPath(ref path);
                    return path;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                return di.FullName;
                //Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                // Delete the directory.
                //di.Delete();
                //Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return "";
            }
            finally { }
        }

        public void CopyFileDraw(string sourceFileName, string destFileName)  //функция копирует указанный файл и все внешние файлы, на которые ссылается упомянутый файл
        {
            //Функция определения внешних файлов на которые ссылается копируемый файл
            //Для данной функции необходимо ловить исключения
            //sourceFileName - откуда копировать
            //destFileName - куда копировать
            try
            {
                File.Copy(sourceFileName, destFileName, true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Процесс завершился фатально!", e.ToString());
            }
            finally { }


        }

        void CopyFolderDraw(string sourceDir, string backupDir) //Функция копирует файлы из папки предварительного дерева изделия в папку архива
        {
            //string sourceDir = @"C:\Users\aleksey\AppData\Local\PDM-System\(folder)";
            //string backupDir = @"C:\Users\aleksey\YandexDisk\WORKS\PROJECT\PDM-System\Archiv\(folder)";

            try
            {
                //Проверить существую ли директории в данной директории
                //Если да то функция вызывает саму себя

                string[] Dir = Directory.GetDirectories(sourceDir); //сформировать массив папок

                if (Dir.Count() > 0)        
                {
                    foreach(string d in Dir)
                    {
                        //Здесь необходимо создать папку в архиве
                        string DirectoryName = Path.GetFileName(d); //Формируем имя директории

                        backupDir = Path.Combine(backupDir, DirectoryName);

                        CreateDirectoryMark(backupDir); //создаем папку в архиве

                        CopyFolderDraw(d, backupDir);
                    }
                }
                else
                {
                    string DirectoryName = Path.GetFileName(sourceDir); //Формируем имя директории

                    backupDir = Path.Combine(backupDir, DirectoryName);

                    CreateDirectoryMark(backupDir); //создаем папку в архиве

                    string[] DrawList = Directory.GetFiles(sourceDir); //сформировать массив файлов                             

                    // Copy picture files.
                    foreach (string f in DrawList)
                    {
                        // Remove path from the file name.
                        string fName = Path.GetFileName(f);

                        File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true); //Функция копирует файлы
                        //File.Delete(f);  // может быть не нужно пока удалять?))
                    }
                    //Нужно как-то удалять папку
                    //Directory.Delete(sourceDir);
                }

                
            }

            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
        }

        void DeleteSpaseToPath(ref string path)  //Функция удаляет пробелы из строк в конце
        {
            while(path.Last() == ' ')
            {
                path = path.Remove(path.Length-1);
            }
            
        }
        
        void ClearDirectory(string path)
        {
            string[] Dir = Directory.GetDirectories(path);
            foreach(var d in Dir)
            {
                Directory.Delete(d, true);
            }

            string[] FileList = Directory.GetFiles(path);
            foreach (var f in FileList)
            {
                File.Delete(f);
            }
        }
        void FinishingAddDevice(string PathFrom, string PathTo) //PathTo - RegFile в архиве, PathFrom - RegFile в temp
        {

            WorkingRegFile regFile = new WorkingRegFile(PathFrom);

            //проверяем ссылки на файлы. Существуют ли файлы.
            if (!regFile.CheckLink())  //Проверить каждую ссылку изделия, существует ли файл
            {
                return;
            }

            
            List<string> path = regFile.PathDevice();  //Получаем все ссылки из реестра файлов.
            
            foreach(string p in path)
            {
                string ext = Path.GetExtension(p);
                if (ext == ".SpcFile") //если изделие - сборочная единица то читаем SpcFile
                {
                    string pth = PathFrom + "\\" + Path.GetDirectoryName(p);
                    CopyFolderDraw(pth, PathTo);
                }
                    
            }
         

            regFile.WriteRegFile(PathTo + "\\RegFile", true); //Запись данных в реестр RegFile, который находится в архиве

            //здесь где-то нужно удалять все папки и RegFile
            ClearDirectory(PathFrom);
        }

    }
}
