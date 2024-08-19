using DevExpress.XtraEditors;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors.Controls;
using AbcYazilim.OgrenciTakip.Ul.Win.Interfaces;

namespace AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    class MyPictureEdit: PictureEdit, IStatusBarKisaYol
    {
        public MyPictureEdit()
        {
            Properties.AppearanceFocused.BackColor = Color.LightCyan;

            //Yazdığımız metnin rengi
            Properties.Appearance.ForeColor = Color.Maroon;

            //resim silindiğinde yazması gereken metin
            Properties.NullText = "Resim Yok";

            //Resmimizin size modunu ayarlama
            Properties.SizeMode = PictureSizeMode.Stretch;

            //menü geliyor onun gelmesini istemiyoruz
            Properties.ShowMenu = false;
        }
        public override bool EnterMoveNextControl { get; set; } = true;
        public string StatusBarKisaYol { get; set; } = "F4";
        public string StatusBarKisaYolAciklama { get; set; }
        public string StatusBarAciklama { get; set; }
    }
}
