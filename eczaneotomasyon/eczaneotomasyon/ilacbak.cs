using Microsoft.Office.Interop.Excel;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eczaneotomasyon
{
    public partial class ilacbak : Form
    {
        public ilacbak()
        {
            InitializeComponent();
        }
        List<ilaclar> ilac = new List<ilaclar>();
        private void ilacbak_Load(object sender, EventArgs e)
        {
            listedoldur();
            
        }



        void listedoldur()



        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();


            Workbook workbook = excelApp.Workbooks.Open(System.Windows.Forms.Application.StartupPath + "\\ilac.xlsx");

            // İlk çalışma sayfasını seç
            Worksheet worksheet = (Worksheet)workbook.Sheets[1];
            Random sayi = new Random();
            //sağdaki sütun soldaki satır

            int sayac = 3;
            while (true)
            {

                try
                {
                    sayac++;
                    Range cell = worksheet.Cells[sayac, 1];
                    string ilac_isim = cell.Value2.ToString();



                    if (sayac > 100)
                    {
                        break;
                    }
                    cell = worksheet.Cells[sayac, 2];
                    string barcode = cell.Value2.ToString();

                    cell = worksheet.Cells[sayac, 3];
                    string atc_code = cell.Value2.ToString();

                    cell = worksheet.Cells[sayac, 6];


                    string recetetur = cell.Value2.ToString();
                    ilac.Add(new ilaclar(ilac_isim, barcode, recetetur, atc_code, sayi.Next(0, 100)));
                }
                catch
                {

                }
            }




            workbook.Close(false);
            excelApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);





        }

        private void button1_Click(object sender, EventArgs e)
        {

            SQLiteConnection bag = new SQLiteConnection("Data Source=" + System.Windows.Forms.Application.StartupPath + "\\data.db;Version=3;");
            SQLiteCommand cmd = new SQLiteCommand(); cmd.Connection = bag;
            bag.Open();

            int Toplam = 0;
            cmd.CommandText="select * from veri where tc='"+textBox1.Text+"'";

            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string veri = reader["barcode"].ToString();


                ilaclar bulunanUrun = ilac.FirstOrDefault(ilaclar => ilaclar.barcode.Equals(veri, StringComparison.OrdinalIgnoreCase));
                
                    Toplam += bulunanUrun.fiyat;


                listView1.Items.Add(new ListViewItem(bulunanUrun.veri()));
              
            }
            label2.Text = Toplam.ToString();

        }
    }
}
