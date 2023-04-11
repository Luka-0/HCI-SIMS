using InitialProject.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class ReservationReschedulingResponseDto
    {
        public RequestState State { get; set; }

        public string Comment { get; set; }

        public int RequestId { get; set; }

        public ReservationReschedulingResponseDto(RequestState state, string comment, int requestId)
        {
            State = state;
            Comment = comment;
            RequestId = requestId;
        }
    }
}
