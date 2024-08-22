using AbcYazilim.OgrenciTakip.Ul.Win.Interfaces;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using System;
using System.ComponentModel;

namespace AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Controls
{
    public class MyButtonEdit : ButtonEdit, IStatusBarKisaYol
    {
        [ToolboxItem(true)] // buradaki kodları toolbox dan erişebilmemiz için bu attribute i yaptık
        public MyButtonEdit()
        {
            //1. özelleştirme= ButtonEdit da default olarak yazı yazılabiliyor biz bunun önüne geçmek DisableTextEditor kullandık.

            Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            //2. Özelleştirme= buttonEdit ini focuslandığı zaman arka plan renginin değişmesini istiyoruz.

            Properties.AppearanceFocused.BackColor = Color.LightCyan;


        }

        //3. özelleştirme = enter a basıldığında bir sonraki index sırasına göre controle focuslanması lazım, bu default olarak false gelir bunu true ya çekeceğiz, bu property olduğu için override yapılıyoruz.
        public override bool EnterMoveNextControl { get; set; } = true;

        //4. özelleştirme = textlerin  üzerine gelince açıklama yapması için
        public string StatusBarAciklama { get; set; }
        public string StatusBarKisaYol { get; set; } = "F4 :";
        public string StatusBarKisaYolAciklama { get; set; }

        //Evnetslerin tanımlandığı bölüm Ctrl+K+S yaparak
        #region Events 
        private long? _id; // alt tire vermemizin nedeni sadece bu classdan ulaşılabilmesi için 

        [Browsable(false)] //ıd değerini göstermemesi için attribute 
        public long? Id
        {
            get { return _id; }
            set
            {
                var oldValue = _id;
                var newValue = value;

                if (newValue == oldValue) return; // butonda Ankara yazılı ben gittim yine Ankara yazdım o zaman return boş dönecek yani yeni bir id atamayacak
                _id = value;
                IdChanged(this, new IdChangedEvenetArgs(oldValue, newValue));
                EnabledChange(this, EventArgs.Empty);
            }
        }


        //Bu event ile ıd değişikliklerini yakalamak için kullanılır.
        //bunu yapabilmemiz için IdChangedEvenetArgs constructur olarak tanımladık

        public event EventHandler<IdChangedEvenetArgs> IdChanged = delegate { };  //delegate dememizin nedeni hiç bir zaman null a düşmemesi için 
        public event EventHandler EnabledChange = delegate { };
        #endregion
    }

    //Bunu yapmamızın amacı textButton da önceden girilmiş bir value ya ait ıd var ve sonradan değiştireceğimiz value nun ıd si var, bu 2 değeri tutabilmek için kullanılır
    //Bu class 2 tane değer dönecek old-value ve new-value
    public class IdChangedEvenetArgs: EventArgs
    {
        public IdChangedEvenetArgs(long? oldValue, long? newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public long? OldValue { get; }
        public long? NewValue { get; }
    }
    //Songelişme Open Edit
    //5. işlem

    //6. işlem
}
