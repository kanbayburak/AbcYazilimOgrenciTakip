using AbcYazilim.OgrenciTakip.Ul.Win.Interfaces;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.ComponentModel;
using System.Drawing;

namespace AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Controls
{
    public class MyToggleSwitch: ToggleSwitch, IStatusBarAciklama
    {
        [ToolboxItem(true)]
        public MyToggleSwitch()
        {
            //her formda en fazla 1 tane toggleSwitch kullanacağız o yüzden buna default bir isim atayabilriz.
            Name = "tglDurum";
            Properties.OffText = "Pasif";
            Properties.OnText = "Aktif";
            // hem false hem de true dememizin nedeni DataLayoutControl kullandığımız zaman bu kondtrol o hücreye girdiğinde boyutu kadar büyümesini istiyoruz. Yüksekliğinde müdahale edilmesini istemiyoruz. 
            Properties.AutoHeight = false;
            Properties.AutoWidth = true;
            //textin toggleswicth in sağında mı solunda mı olması için 
            Properties.GlyphAlignment = HorzAlignment.Far; // solunda
            //textlere renk vereceğiz 
            Properties.Appearance.ForeColor = Color.Maroon;
        }
        public override bool EnterMoveNextControl { get; set; } = true;
        public string StatusBarAciklama { get; set; } = "Kartın Kullanım Durumunu Seçiniz.";
    }
}
