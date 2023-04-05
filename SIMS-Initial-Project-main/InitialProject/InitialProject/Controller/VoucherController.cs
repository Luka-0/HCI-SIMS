using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class VoucherController
    {
        VoucherService voucherService = new VoucherService(new VoucherRepository());

        public List<Voucher> GetAll()
        {
            return voucherService.GetAll();
        }

        public Voucher GetById(int id)
        {
            return voucherService.GetById(id);
        }

        public void Save(Voucher voucher)
        {
            voucherService.Save(voucher);
        }

        public void Delete(Voucher voucher)
        {
            voucherService.Delete(voucher);
        }
    }
}
