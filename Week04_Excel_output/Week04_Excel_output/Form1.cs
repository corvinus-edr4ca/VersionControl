using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Week04_Excel_output
{
    public partial class Form1 : Form
    {

        List<Flat> Flats;
        RealEstateEntities context = new RealEstateEntities();

        Excel.Application x1App;
        Excel.Workbook x1WB;
        Excel.Worksheet x1Sheet;

        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
        }

        private void LoadData()
        {
            Flats = context.Flat.ToList();
        }

        private void CreateExcel()
        {
            try
            {
                x1App = new Excel.Application();
                x1WB = x1App.Workbooks.Add(Missing.Value);
                x1Sheet = x1WB.ActiveSheet;

                //CreateTable();

                x1App.Visible = true;
                x1App.UserControl = true;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                x1WB.Close(false, Type.Missing, Type.Missing);
                x1App.Quit();
                x1WB = null;
                x1App = null;
            }

        }
    }
}
