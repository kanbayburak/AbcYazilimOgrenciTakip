using AbcYazilim.OgrenciTakip.Ul.Win.Interfaces;
using DevExpress.XtraEditors;
using System.ComponentModel;
using System.Drawing;

namespace AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Controls
{
    [ToolboxItem(true)] //Toolbox da Controlümüz gözükmüş olacak
    public class MyTextEdit:TextEdit, IStatusBarAciklama
    {
        public MyTextEdit()
        {
            //focuslandığı zaman arka plan rengi değişecek
            Properties.AppearanceFocused.BackColor = Color.LightCyan;
            //karakter sayısını sınırlama
            Properties.MaxLength = 50;
        }
        //Enter a basıldığında bir sonraki controller a geçmesi 
        //normalde default değeri false dur biz true ya dönüştürüyoruz.
        public override bool EnterMoveNextControl { get; set; } = true;
        public string StatusBarAciklama { get; set; }
    }
}
