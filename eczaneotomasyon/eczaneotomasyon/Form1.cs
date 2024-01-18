using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eczaneotomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
      
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recete ac=new recete();
            ac.Show();  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ilacbak ac=new ilacbak();ac.Show(); 
        }
    }
}
