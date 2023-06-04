using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using User = InitialProject.Model.User;

namespace InitialProject.Repository
{
    public class VoucherRepository:IVoucherRepository
    {
        public List<Voucher> GetAll()
        {
            List<Voucher> vouchers = new List<Voucher>();

            using (var dbContext = new UserContext())
            {
                vouchers = dbContext.voucher.ToList();
            }
            return vouchers;
        }

        public Voucher GetById(int id)
        {
            Voucher voucher = new Voucher();

            using (var dbContext = new UserContext())
            {
                voucher = (Voucher)dbContext.voucher
                                .Where(v => v.Id == id)
                                .SingleOrDefault();
            }
            return voucher;
        }

        public void Save(Voucher voucher)
        {
            var db = new UserContext();
            db.Add(voucher);
            db.SaveChanges();
        }

        public void Delete(Voucher voucher) 
        {
            var db = new UserContext();
            db.Remove(voucher);
            db.SaveChanges();
        }

        public void SaveMultiple(List<Voucher> vouchers)
        {
            var db = new UserContext();
            foreach (var voucher in vouchers)
            {
                User existingUser = db.users.Find(voucher.User.Id);
                db.users.Attach(existingUser);
                db.Add(voucher);
            }
            db.SaveChanges();
        }

        public void Update(Voucher voucher, User? user)
        {
            var db = new UserContext();
            Voucher tempVoucher = db.voucher.Find(voucher.Id);
            tempVoucher.User = user;
            tempVoucher.ExpirationDate = DateTime.Now.AddYears(2);
            db.SaveChanges();

        }

        public bool HasVoucherForGuide(User bookingGuest, User guide)
        {
            var db= new UserContext();
            return db.voucher.Any(v => v.Guide == guide && v.User == bookingGuest);
        }

        public void UpdateGuide(Voucher voucher, User guide)
        {
            var db = new UserContext();
            Voucher tempVoucher = db.voucher.Find(voucher.Id);
            tempVoucher.Guide = guide;
            tempVoucher.ExpirationDate = DateTime.Now.AddYears(2);
            db.SaveChanges();
        }

    }
}
