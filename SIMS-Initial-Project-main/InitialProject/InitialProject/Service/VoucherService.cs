using InitialProject.Contexts;
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

        public List<Voucher> GetAll()
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
            if (voucher.ExpirationDate >= DateTime.Now)
            {
                return true;
            }

            return false;
        }

        public List<Voucher> GetAllAvailable()
        {
            List<Voucher> vouchers = new List<Voucher>();

            foreach (Voucher v in vouchers)
            {
                if (IsAvailable(v))
                    vouchers.Add(v);
            }

            return vouchers;
        }

        public void Save(Voucher voucher, User user)
        {
            _voucherRepository.Save(voucher, user);
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

        public void GiveOutToSingleUSer(User user)
        {
            Voucher voucher = new Voucher(user);
            _voucherRepository.Save(voucher);
            _voucherRepository.Update(voucher, user);
        }
    

    public void SendVouchers(List<TourReservation> reservations, User guide)
    {
        List<TourReservation> leftReservations = new List<TourReservation>();
        leftReservations.AddRange(reservations);
        foreach (TourReservation reservation in reservations)
        {
            if (_voucherRepository.HasVoucherForGuide(reservation.BookingGuest, guide))
            {
                GiveOutToSingleUSer(reservation.BookingGuest);
                leftReservations.Remove(reservation);
            }
        }
        List<User> tourists = new List<User>();
           tourists.AddRange(getTouristsFromReservations(leftReservations));
        if (tourists.Count > 0)
        {
            GiveOutForGuide(tourists, guide);
        }
        
    }

    private List<User> getTouristsFromReservations(List<TourReservation> leftReservations)
    {
        List<User> tourists = new List<User>();
        foreach (TourReservation reservation in leftReservations)
        {
            tourists.Add(reservation.BookingGuest);
        }
        return tourists;
    }

    private void GiveOutForGuide(List<User> tourists, User guide)
    {
        foreach (User? user in tourists)
        {
            Voucher voucher = new Voucher(user);
            _voucherRepository.Save(voucher);
            _voucherRepository.Update(voucher, user);
            _voucherRepository.UpdateGuide(voucher, guide);
            }

        }
    }

}
