using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ReschedulingRequestsHandler.xaml
    /// </summary>
    public partial class ReschedulingRequestsHandler : Page
    {
        private string OwnerUsername;
        private ReservationReschedulingRequestController ReservationReschedulingRequestController;
        public ReschedulingRequestsHandler(string ownerUsername)
        {
            ReservationReschedulingRequestController = new ReservationReschedulingRequestController();
            OwnerUsername = ownerUsername;

            InitializeComponent();

            InitializeReschedulingRequests();
        }

        public void InitializeReschedulingRequests() {

            reschedulingRequests.ItemsSource = ReservationReschedulingRequestController.GetAllBy(OwnerUsername);
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            ReservationReschedulingRequestDto selectedRequest = reschedulingRequests.SelectedItem as ReservationReschedulingRequestDto;

            ReservationReschedulingResponseDto ownersResponse = new ReservationReschedulingResponseDto(RequestState.Approved, comment.Text, selectedRequest.RequestId);
            ReservationReschedulingRequestController.Respond(ownersResponse);

        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            ReservationReschedulingRequestDto selectedRequest = reschedulingRequests.SelectedItem as ReservationReschedulingRequestDto;

            ReservationReschedulingResponseDto ownersResponse = new ReservationReschedulingResponseDto(RequestState.Rejected, comment.Text, selectedRequest.RequestId);
            ReservationReschedulingRequestController.Respond(ownersResponse);
        }
    }
}
