using DevExpress.XtraDataLayout;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyDataLayoutControl: DataLayoutControl
    {
        public MyDataLayoutControl()
        {
            //bizim istediğimiz index düzeninde sıralanması için 
            OptionsFocus.EnableAutoTabOrder = false;

        }

        //bizim yapacağımız tanımlamaları bu override ettiğimiz fnvtion ile yapacağız 
        protected override LayoutControlImplementor CreateILayoutControlImplementorCore()
        {
            //burada kendi LayoutControlümüzü oluşturacağız
            return new MyLayoutControlImplementor(this);
        }
    }

    internal class MyLayoutControlImplementor : LayoutControlImplementor
    {
        public MyLayoutControlImplementor(ILayoutControlOwner controlOwner) : base(controlOwner)
        {
        }

        //kendi grup ve item larımızı oluşturabileceğiz
        public override BaseLayoutItem CreateLayoutItem(LayoutGroup parent)
        {
            //oluşturacağımız CreateLayoutItem sürükleyip bırakacağımız item ların captionların rengini burada ayarlayacağız 
            var item = base.CreateLayoutItem(parent);
            item.AppearanceItemCaption.ForeColor = Color.Maroon;
            return item;
        }
        public override LayoutGroup CreateLayoutGroup(LayoutGroup parent)
        {
            var grp = base.CreateLayoutGroup(parent);
            grp.LayoutMode = LayoutMode.Table;
            // tablonun satır ve sütun ayarlarını yapacağız 
            //column eklemesi yaptık
            grp.OptionsTableLayoutGroup.ColumnDefinitions[0].SizeType = SizeType.Absolute;
            grp.OptionsTableLayoutGroup.ColumnDefinitions[0].Width = 200;
            grp.OptionsTableLayoutGroup.ColumnDefinitions[1].SizeType = SizeType.Percent;
            grp.OptionsTableLayoutGroup.ColumnDefinitions[1].Width = 100;
            grp.OptionsTableLayoutGroup.ColumnDefinitions.Add(new ColumnDefinition { SizeType = SizeType.Absolute, Width = 99 });


            //row eklemesi 10 tane ekleyecek
            grp.OptionsTableLayoutGroup.RowDefinitions.Clear();
            for (int i = 0; i < 9; i++)
            {
                grp.OptionsTableLayoutGroup.RowDefinitions.Add(new RowDefinition
                {
                    SizeType = SizeType.Absolute,
                    Height = 24
                });
                if (i + 1 != 9) continue;
                grp.OptionsTableLayoutGroup.RowDefinitions.Add(new RowDefinition
                {
                    SizeType = SizeType.Percent,
                    Height = 100
                });
            }
            return grp;
        }
    }
}
