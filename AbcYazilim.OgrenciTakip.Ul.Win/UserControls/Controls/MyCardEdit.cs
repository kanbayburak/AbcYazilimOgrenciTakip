using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using System.ComponentModel;

namespace AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Controls
{
    public class MyCardEdit: MyTextEdit
    {
        [ToolboxItem(true)]
        public MyCardEdit()
        {
            //kart numarası text in ortasında yazsın 
            Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //4 erli olarak yazıp arasına tire atmasını ve 16 haneyle sınrlı olmasını istiyoruz
            Properties.Mask.MaskType = MaskType.Regular;
            //üstekini yaptıktan sonra artık Mask ımızı tanımlamamız gerekecek
            Properties.Mask.EditMask = @"\d?\d?\d?\d?-\d?\d?\d?\d?-\d?\d?\d?\d?-\d?\d?\d?\d?"; //d ifadesi bize, bir rakamı ifade ediyor ve bu Null da olabileceğini ifade ediyor
            //Üsteki kodda d lerin yerini 0 alıyor yazdıkça değişiyor. böyle olmasını istemiyoruz.
            Properties.Mask.AutoComplete = AutoCompleteType.None;
            //Açıklama yapar
            StatusBarAciklama = "Kart No Giriniz.";
        }
    }
}
