using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eczaneotomasyon
{
    internal class ilaclar
    {



        public string ilacad { get; set; }
        public string barcode {  get; set; }
        public string recetetur {  get; set; }
        public string atckod {  get; set; }
        public int fiyat {  get; set; }
      public  ilaclar (string ilacad,string barcode,string recetetur,string atckod,int fiyat)
        
        {
            this.ilacad = ilacad;
            this.barcode = barcode;
                
            this.recetetur = recetetur;
                
            this.atckod = atckod;
            this.fiyat = fiyat;


        }   


        public string[] veri()
        {
            return new string[] { ilacad, barcode, recetetur, atckod };
        }
       
    }
}
