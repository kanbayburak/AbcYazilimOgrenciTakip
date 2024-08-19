using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.Drawing;
using DevExpress.XtraEditors.Mask;
using AbcYazilim.OgrenciTakip.Ul.Win.Interfaces;
using System.ComponentModel;

namespace AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Controls
{

    [ToolboxItem(true)]
    public class MyDateEdit: DateEdit, IStatusBarKisaYol
    {
        public MyDateEdit()
        {
            Properties.AppearanceFocused.BorderColor = Color.LightCyan;
            //tarih alanında hiç birşeylde Null değer olmaması lazım 
            Properties.AllowNullInput = DefaultBoolean.False;
            //tarih alanlarının ortada yazılması isteniyor
            Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //tarih kısmında gün yazdıktan sonra ay kısmına sonrasında yıl kısmına geçmesi için 
            Properties.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
        }
        public override bool EnterMoveNextControl { get; set; } = true;
        public string StatusBarKisaYol { get; set; } = "F4 :";
        public string StatusBarKisaYolAciklama { get; set; } = "Tarih Seç";
        public string StatusBarAciklama { get; set; }
    }
}
