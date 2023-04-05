using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    public interface IVoucherRepository
    {
        public List<Voucher> GetAll();
        public Voucher GetById(int id);
        public void Save(Voucher voucher);
        public void Delete(Voucher voucher);
    }
}
