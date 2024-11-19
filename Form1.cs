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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkingObjSpc;
using WorkingKompas;
//using Adder;

using System.IO;

namespace PDMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            treeView2.ContextMenuStrip = contextMenuStrip1;
            g_object.Initialize();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            treeView2.Nodes.Clear();

            g_object.Initialize();

            bool MarkName = SearchMarkName.CheckState == CheckState.Checked;

            g_object.SearchDevice(treeView2, textBox2.Text, MarkName); 

            //g_object.ShowDevice(treeView2);
        }

        private void AdderItem_Click(object sender, EventArgs e)
        {
            g_object.Initialize(); //В каком месте лучше проинициализировать переменную
            g_object.AddDevice();
            g_object.ShowDevice(treeView2);

            // WorkingRegFile regFile = new WorkingRegFile();
        }

        private void CloseItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string treeNodeTag = treeView2.SelectedNode.Tag.ToString();

                if (OpenKompas.CheckState != CheckState.Checked)
                {

                    if (PDF_Viewer.CheckState == CheckState.Checked)
                    {
                        System.Diagnostics.Process.Start(treeNodeTag);
                        //axAcroPDF2.

                    }

                    else
                        axAcroPDF2.LoadFile(treeNodeTag);
                }
                else
                {
                    Kompas kompas = new Kompas();
                    string pathDirectory = Path.GetDirectoryName(treeNodeTag); //Директория
                    string name = Path.GetFileNameWithoutExtension(treeNodeTag);

                    string path = pathDirectory + "\\" + name;

                    //MessageBox.Show(path);

                    kompas.OpenFile(path, true);
                }

            }
            catch
            {

            }
           
            
        }

        private void AboutApp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данную прогрмму разработал Рябов Алексей", "О программе");
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                Search.PerformClick(); // Создаем событие нажатие кнопки "Поиск"
        }

        private void CorrectionDevice_Click(object sender, EventArgs e)
        {
            string treeNodeTag = treeView2.SelectedNode.Tag.ToString();
            Kompas kompas = new Kompas();
            string pathDirectory = Path.GetDirectoryName(treeNodeTag); //Директория
            string name = Path.GetFileNameWithoutExtension(treeNodeTag);

            string path = pathDirectory + "\\" + name;

            //MessageBox.Show(path);

            kompas.OpenFile(path, true);
        }

        private void MoveDeviceItem_Click(object sender, EventArgs e)
        {
            g_object.MoveDeviceInArhiv();
        }

        private void AdderDraw(object sender, EventArgs e)
        {
            //Копировать файл в папку Temp
            //Получить вторичное представление файла
        }

        private void UpDateLink(object sender, EventArgs e)
        {
            //Функция для обновления ссылок 
        }
    }
}
