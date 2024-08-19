using AbcYazilim.Dal.InterFaces;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Common.Functions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AbcYazilim.Dal.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Variables
        private readonly DbContext _context; //Database imizi temsil ediyor //_yerelde kullanılacak olduğubu gösteriyor // readonly de aynı class içerisindeki constructer larda değer ataması yapılabiliyor ama fasklı bir class lardan sadece okunabiliyor
        private readonly DbSet<T> _dbSet; //Table(Entity) larımızı temsil eden bir değişken

        #endregion


        //IRepository implemente ettik ve aşağıdakiler geldi 
        //çeşitli işler yapmak için constructer oluşturduk 
        //öncelikle bize işlem yapabilecek database lazım işte onu temsilen bir tane DbContext gönderiyoruz Repository a ve bu context üzerinden delete, update işlemlerini yapacağız 
        public Repository(DbContext context) //DbContext tipinde bir parametre gelmeli context adında
        {
            {
                if (context == null) return;
                _context = context;            // private readonly DbContext _context 
                _dbSet = context.Set<T>();     //private readonly DbSet<T> _dbSet;
            }
        }


        //DataBase e kayıt işlemi 
        public void Insert(T entity)
        {
            _context.Entry(entity).State = EntityState.Added; //yeni bir kayıt olarak database e ekleyecek 
        }

        //birden çokverinin DataBase e kayıt işlemi 
        public void Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Added; //yeni bir kayıt olarak database e ekleyecek 
            }

            //test
        }



        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;   //burada entity i update et var aşağıda sadece entity nin field larnı
        }

        //şöyle düşün okul tablosu gelecek ve bazı kısımları değişecek orasıda field olarak gelecek ve değişme işlemi yapılacak bu şekilde tamamıyla tüm tabloyu update yapmış olmayacağız 
        public void Update(T entity, IEnumerable<string> fields)
        {
            _dbSet.Attach(entity);                        //öncelikle hangi bdset ile çalışacağımızı belirledik
            var entry = _context.Entry(entity);           //sonrasında entitynin filed lar arasında dolaşıp update işlemini yapacağımızı belirtmek için entry oluşturduk
            foreach (var field in fields)                 //hangisinde değişiklik yapılacaksa onları true yap
            {
                entry.Property(field).IsModified = true;
            }
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }






        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }



        public TResult Find<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return filter == null ? _dbSet.Select(selector).FirstOrDefault() : _dbSet.Where(filter).Select(selector).FirstOrDefault();
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return filter == null ? _dbSet.Select(selector) : _dbSet.Where(filter).Select(selector);
        }
    
        public int Count(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? _dbSet.Count() : _dbSet.Count(filter);
        }

        public string YeniKodVer(KartTuru kartTuru, Expression<Func<T, string>> filter, Expression<Func<T, bool>> where = null)
        {
            string Kod()
            {
                string kod = null;
                var kodDizi = kartTuru.ToName().Split(' ');

                for (int i = 0; i < kodDizi.Length-1; i++)  //okul kartı,  sınıf grup kartı olabilir 
                {
                    kod += kodDizi[i];

                    if (i + 1 < kodDizi.Length - 1)
                        kod += " ";
                }
                return kod += "-0001";
            }
            string YeniKodVer(string kod) //Okul-0001
            {
                var sayisalDegerler = "";
                foreach (var karakter in kod)
                {
                    if (char.IsDigit(karakter)) //0001
                    {
                        sayisalDegerler += karakter;
                    }
                    else
                        sayisalDegerler = "";
                }
                var artisSonrasiDeger = (int.Parse(sayisalDegerler) + 1).ToString(); //0049 //50 
                var fark = kod.Length - artisSonrasiDeger.Length;
                if (fark<0)
                {
                    fark = 0;
                }
                var yeniDeger = kod.Substring(0, fark);
                yeniDeger += artisSonrasiDeger; //Okul-0050
                return yeniDeger;
            }
            var maxKod = where == null ? _dbSet.Max(filter) : _dbSet.Where(where).Max(filter);
            return maxKod == null ? Kod() : YeniKodVer(maxKod);
        }



        //burası default olarak geldi sadece bir kaç ekleme yaptık ama çok net kavrayamadım ne yaptık

        #region Dispose
        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       
        #endregion
    }
}
