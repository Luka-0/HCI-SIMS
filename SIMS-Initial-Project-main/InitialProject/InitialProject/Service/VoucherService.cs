using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace InitialProject.Service
{
    public class VoucherService
    {
        VoucherRepository voucherRepository = new VoucherRepository();

        public List <Voucher> GetAll()
        {
            return voucherRepository.GetAll();  
        }

        public Voucher GetById(int id)
        {
            return voucherRepository.GetById(id);
        }

        public void Save(Voucher voucher) 
        {
            voucherRepository.Save(voucher);
        }

        public void Delete(Voucher voucher) 
        {
            voucherRepository.Delete(voucher);
        }

        public bool IsAvailable(Voucher voucher)
        {
            if(voucher.ExpirationDate >= DateTime.Now)
            {
                return true;
            }

            return false;
        }

        public List<Voucher> GetAllAvailable()
        {
            List<Voucher> vouchers = new List<Voucher>();

            foreach(Voucher v in vouchers)
            {
                if(IsAvailable(v))
                    vouchers.Add(v);
            }
            return vouchers;
        }
        





    }
}
