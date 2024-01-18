using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Net.Http;
using System.Data.SQLite;
using System.Security.Policy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Remoting;

namespace eczaneotomasyon
{
    public partial class recete : Form
    {
        public recete()
        {
            InitializeComponent();
        }
        List<ilaclar> ilac = new List<ilaclar>();
        private async void recete_Load(object sender, EventArgs e)
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
        List<string> eklenen = new List<string>();
        private void button2_Click(object sender, EventArgs e)
        {
            if (tcvarmı(textBox2.Text) == true)
            {

                SQLiteConnection bag = new SQLiteConnection("Data Source="+System.Windows.Forms.Application.StartupPath+"\\data.db;Version=3;");
                SQLiteCommand cmd = new SQLiteCommand();cmd.Connection = bag;
                bag.Open();
               foreach(var item in eklenen)
                {
                    cmd.CommandText = "insert into veri (tc,barcode)values('" + textBox2.Text + "','" + item + "')";
                    cmd.ExecuteNonQuery();

                }bag.Close();
                    this.Close();


                //98765432109
            }
            else
            {
                MessageBox.Show("TC Bulunamadı");
            }





        }

        Boolean tcvarmı(string tc)
        {
            bool deger = false;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Web sitesinin içeriğini indir
                    string content = client.GetStringAsync("https://mocki.io/v1/6d783363-e33f-4b5a-b4a4-16a518a69f6a").Result;


                 
                    JObject dataObject = JObject.Parse(content);

                    
                    foreach (var item in dataObject["Tcno"])
                    {
                        if (tc == item.ToString())
                        {
                            deger = true;
                            break;
                        }
                            

                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }







                return deger;

            }


          
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try

            {
               listView1.Items.Clear(); 

                var bulunanUrunler = ilac.Where(ilaclar => ilaclar.ilacad.Contains(textBox1.Text)).ToList();



                foreach (var item in bulunanUrunler)
                {
                    listView1.Items.Add(new ListViewItem(item.veri()));
                }


            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            eklenen.RemoveAt(listBox1.SelectedIndex); listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var eleman = listView1.SelectedItems[0].SubItems[1].Text;
                eklenen.Add(eleman);
                listBox1.Items.Add(listView1.SelectedItems[0].SubItems[0].Text);
            }
            catch
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    } 
}