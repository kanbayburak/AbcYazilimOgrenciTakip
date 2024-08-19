using AbcYazilim.OgrenciTakip.Bll.Interfaces;
using AbcYazilim.OgrenciTakip.Bll.InterFaces;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Common.Message;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using AbcYazilim.OgrenciTakip.Model.Entities.Base.Interfaces;
using AbcYazilim.OgrenciTakip.Ul.Win.Functions;
using AbcYazilim.OgrenciTakip.Ul.Win.Interfaces;
using AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Controls;
using AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Grid;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native;
using System;
using System.Windows.Forms;

namespace AbcYazilim.OgrenciTakip.Ul.Win.Forms.BaseForms
{
    public partial class BaseEditForm :RibbonForm
    {
        #region Variables

        private bool _formSablonKayitEdilecek;  // eğer şablonda bir değişiklik olacaksa buradaki değişkeni true ya çekecek. burada xml dosyası için değişken tanımladık. //bu değişkeni kullanarak 2 tane function kullanacağız sablonkaydet, sablonyukle
        protected MyDataLayoutControl DataLayoutControl;
        protected MyDataLayoutControl[] DataLayoutControls;
        protected IBaseBll Bll;
        protected KartTuru BaseKartTuru;
        protected BaseEntity OldEntity;
        protected BaseEntity CurrentEntity;
        protected bool IsLoaded;
        protected bool KayitSonrasiFormuKapat = true;
        protected BarItem[] ShowItems;
        protected BarItem[] HideItems;
        protected internal IslemTuru BaseIslemTuru;
        protected internal long Id;
        protected internal bool RefresYapilacak;

        #endregion
        public BaseEditForm()
        {
            InitializeComponent();
        }

        protected void EventsLoad()
        {
            //Buttton Events
            foreach (BarItem button in ribbonControl.Items)
                button.ItemClick += Button_ItemClick;


            //Form Events
            LocationChanged += BaseEditForm_LocationChanged;
            SizeChanged += BaseEditForm_SizeChanged;
            Load += BasedEditForm_Load;
            FormClosing += BasedEditForm_FormClosing;


            void ControlEvents(Control control)
            {
                control.KeyDown += Control_Keydown;
                control.GotFocus += Control_GotFocus;
                control.Leave += Control_Leave;

                switch (control)  //bu kısımda sıralama önemli MyButtonEdit sonrasında BaseEdit olmalı çünkü MyButtonEdit, BaseEditten implament olduğu için
                {
                    case FilterControl edt:
                        edt.FilterChanged += Control_EditValueChanged;
                        break;
                    case ComboBoxEdit edt:
                        edt.EditValueChanged += Control_EditValueChanged;
                        edt.SelectedValueChanged += Control_SelectedValueChanged;
                        break;
                    case MyButtonEdit edt:
                        edt.IdChanged += Control_IdChanged;
                        edt.EnabledChange += Control_EnabledChange;
                        edt.ButtonClick += Control_ButtonClick;
                        edt.DoubleClick += Control_DoubleClick;
                        break;

                    case BaseEdit edt:    //burada formda değişiklik yapıldığı vakit kaydettiğinde güncelleme işlemi yapar
                        edt.EditValueChanged += Control_EditValueChanged;
                        break;
                }
            }

            if (DataLayoutControls ==null)
            {
                if (DataLayoutControl == null) return;
                foreach (Control ctrl in DataLayoutControl.Controls)
                    ControlEvents(ctrl);
            }
            else
                foreach (var layout in DataLayoutControls)
                    foreach (Control ctrl in layout.Controls)
                        ControlEvents(ctrl);

        }


        //Functions
        private void EntityDelete()
        {
            if (!((IBaseCommonBll)Bll).Delete(OldEntity)) return;
            RefresYapilacak = true;
            Close();
        }
        private void ButonGizleGoster()
        {
            ShowItems?.ForEach(x => x.Visibility = BarItemVisibility.Always);
            HideItems?.ForEach(x => x.Visibility = BarItemVisibility.Never);

        }
        private void Gerial()
        {
            if (Messages.HayirSeciliEvetHayir("Yapılan Değişiklikler Geri Alınacaktır. Onaylıyor musunuz?", "Geri Al, Onay") != DialogResult.Yes) return;

            if (BaseIslemTuru == IslemTuru.EntityUpdate)
                Yukle();
            else
                Close();
        }
        private bool Kaydet(bool kapanis)
        {
            bool KayitIslemi()
            {
                Cursor.Current = Cursors.WaitCursor;
                switch (BaseIslemTuru)
                {
                    case IslemTuru.EntityInsert:
                        if (EntityInsert())
                            return KayitSonrasiIslemler();
                        break;
                    case IslemTuru.EntityUpdate:
                        if (EntityUpdate())
                            return KayitSonrasiIslemler();
                        break;

                }
                bool KayitSonrasiIslemler()
                {
                    OldEntity = CurrentEntity;
                    RefresYapilacak = true;
                    ButonEnabledDurumu();

                    if (KayitSonrasiFormuKapat)
                    {
                        Close();
                    }
                    else
                        BaseIslemTuru = BaseIslemTuru == IslemTuru.EntityInsert ? IslemTuru.EntityUpdate : BaseIslemTuru;
                    return true;
                }
                return false;
            }
            var result = kapanis ? Messages.KapanisMesaj() : Messages.KayitMesaj();

            switch (result) //tyhyth
            {
                case DialogResult.Yes:
                    return KayitIslemi();

                case DialogResult.No:
                    if (kapanis)
                    {
                        btnKaydet.Enabled = false;
                    }
                    return true;

                case DialogResult.Cancel:
                    return false;
            }
            return false;
        }
        private void SablonYukle()
        {
            Name.FormSablonYukle(this);
        }
        private void FarkliKaydet()
        {
            if (Messages.EvetSeciliEvetHayir("Bu filtre Referasn alınarak Yeni Bir Filtre Oluşturulacaktır. Onaylıyor Musunuz?", "Kayıt Onay") != DialogResult.Yes) return;
            BaseIslemTuru = IslemTuru.EntityInsert;
            Yukle();
            if (Kaydet(true))
                Close();
        }
        protected void SablonKaydet()
        {
            if (_formSablonKayitEdilecek)
                Name.FormSablonKaydet(Left, Top, Width, Height, WindowState);
        }
        protected virtual void FiltreUygula() { }
        protected virtual void BaskiOnizleme() { }
        protected virtual void Yazdir() { }
        protected virtual void SecimYap(object sender) { }     
        protected virtual bool EntityInsert()
        {
            return ((IBaseGenelBll)Bll).Insert(CurrentEntity);
        }
        protected virtual bool EntityUpdate()
        {
            return ((IBaseGenelBll)Bll).UpDate(OldEntity, CurrentEntity);
        }
        protected virtual void NesneyiKontrollereBagla() { }
        protected virtual void GuncelNesneOlustur() { }
        protected internal virtual void ButonEnabledDurumu()
        {
            if (!IsLoaded) return;
            GeneralFunctions.ButtonEnabledDurumu(btnYeni, btnKaydet, btnGerial, btnSil, OldEntity, CurrentEntity);  //buttonların enabled durumunu değiştirecek
        }
        protected internal virtual void Yukle() { }   //override edebilmek için virtual koymamız lazım 
        protected internal virtual IBaseEntity ReturnEntity() { return null; }


        //Events
        private void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (e.Item == btnYeni)
            {

                BaseIslemTuru = IslemTuru.EntityInsert;
                Yukle();
            }
            else if (e.Item == btnKaydet)
            {
                Kaydet(false);
            }

            else if (e.Item == btnFarkliKaydet)
            {
                //yetki kontrolü
                FarkliKaydet();
            }
            else if (e.Item == btnGerial)
            {
                Gerial();
            }
            else if (e.Item == btnSil)
            {
                EntityDelete();
            }
            else if (e.Item == btnUygula)
            {
                FiltreUygula();
            }
            else if (e.Item == btnYazdir)
                Yazdir();

            else if (e.Item == btnBaskiOnizleme)
                BaskiOnizleme();

            else if (e.Item == btnCikis)
            {
                Close();
            }

            Cursor.Current = DefaultCursor;
        }
        private void BaseEditForm_LocationChanged(object sender, EventArgs e)
        {
            if (!IsMdiChild)
                _formSablonKayitEdilecek = true;
        }
        private void BaseEditForm_SizeChanged(object sender, EventArgs e)
        {
            if (!IsMdiChild)
                _formSablonKayitEdilecek = true;
        }
        private void BasedEditForm_Load(object sender, EventArgs e)
        {
            IsLoaded = true;
            GuncelNesneOlustur();
            SablonYukle();
            ButonGizleGoster();
            //Id = BaseIslemTuru.IdOlustur(OldEntity);


            //güncelleme yapılacak

        }
        private void BasedEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SablonKaydet();

            if (btnKaydet.Visibility == BarItemVisibility.Never || btnKaydet.Enabled) return;
            if (!Kaydet(true))
                e.Cancel = true;
        }
        private void Control_Keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            if (sender is MyButtonEdit edt)
                switch (e.KeyCode)
                {
                    case Keys.Delete when e.Control && e.Shift: // burada ctrlshift delete basında şu işlmei yaparsın diye tanımladık
                        edt.Id = null;
                        edt.EditValue = null;
                        break;

                    case Keys.F4:
                    case Keys.Down when e.Modifiers == Keys.Alt:
                        SecimYap(edt);
                        break;
                }
        }
        private void Control_GotFocus(object sender, EventArgs e)
        {
            var type = sender.GetType();

            if (type == typeof(MyButtonEdit) || type == typeof(MyGridView) || type == typeof(MyPictureEdit) || type == typeof(MyComboBoxEdit) || type == typeof(MyDateEdit) || type == typeof(MyCalcEdit))
            {
                statusBarKisaYol.Visibility = BarItemVisibility.Always;
                statusBarAciklama.Visibility = BarItemVisibility.Always;

                statusBarAciklama.Caption = ((IStatusBarAciklama)sender).StatusBarAciklama;
                statusBarKisaYol.Caption = ((IStatusBarKisaYol)sender).StatusBarKisaYol;
                statusBarKisaYolAciklama.Caption = ((IStatusBarKisaYol)sender).StatusBarAciklama;
            }
            else if (sender is IStatusBarAciklama ctrl)
                statusBarAciklama.Caption = ctrl.StatusBarAciklama;
        }
        private void Control_Leave(object sender, EventArgs e)
        {
            statusBarKisaYol.Visibility = BarItemVisibility.Never;
            statusBarKisaYolAciklama.Visibility = BarItemVisibility.Never;
        }
        protected virtual void Control_EditValueChanged(object sender, EventArgs e)
        {
            if (!IsLoaded) return;
            GuncelNesneOlustur();
        }
        private void Edt_EditValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        protected virtual void Control_SelectedValueChanged(object sender, EventArgs e) { }
        private void Control_IdChanged(object sender, IdChangedEvenetArgs e)
        {
            if (!IsLoaded) return;
            GuncelNesneOlustur();
        }
        protected virtual void Control_EnabledChange(object sender, EventArgs e) { }
        private void Control_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SecimYap(sender);
        }
        private void Control_DoubleClick(object sender, EventArgs e)
        {
            SecimYap(sender);
        }
    }
}