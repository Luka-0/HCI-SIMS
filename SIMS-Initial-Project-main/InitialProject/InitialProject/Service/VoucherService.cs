using InitialProject.Interface;
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
        //VoucherRepository voucherRepository = new VoucherRepository();

        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository repository)
        {
            _voucherRepository = repository;
        }

        public List <Voucher> GetAll()
        {
            return _voucherRepository.GetAll();  
        }

        public Voucher GetById(int id)
        {
            return _voucherRepository.GetById(id);
        }

        public void Save(Voucher voucher) 
        {
            _voucherRepository.Save(voucher);
        }

        public void Delete(Voucher voucher) 
        {
            _voucherRepository.Delete(voucher);
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


        public void GiveOut(List<User?> users)
        {
            List<Voucher> vouchers = users.Select(user => new Voucher(user)).ToList();
            
            foreach (User? user in users)
            {
                Voucher voucher = new Voucher(user);
                _voucherRepository.Save(voucher);
                _voucherRepository.Update(voucher, user);
            }
        }
    }
}
