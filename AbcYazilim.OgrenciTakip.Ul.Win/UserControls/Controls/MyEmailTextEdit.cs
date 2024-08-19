using DevExpress.XtraEditors.Mask;
using System.ComponentModel;

namespace AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyEmailTextEdit: MyTextEdit
    {
        public MyEmailTextEdit()
        {
            Properties.Mask.MaskType = MaskType.RegEx;
            Properties.Mask.EditMask = @"((([0-9a-zA-Z_%-])+[.])+|([0-9a-zA-Z_%-])+)+@((([0-9a-zA-Z_-])+[.])+|([0-9a-zA-Z_-])+)+"; //devexpress T259527 yazarak iinternetten bulduk
            Properties.Mask.AutoComplete = AutoCompleteType.Strong; //strong default ile benzer ama ufak bir farkı var sadece 
            StatusBarAciklama = "Email Adresinizi Giriniz.";


        }
    }
}
