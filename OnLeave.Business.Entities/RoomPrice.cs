using System;
using System.Collections.Generic;
using System.Text;

namespace OnLeave.Business.Entities
{
    public class RoomPrice
    {
        public int RoomTypeId { get; set; }

        public DateTime PeriodStartDate { get; set; }

        public DateTime PeriodEndDate { get; set; }

        public decimal Amount { get; set; }
    }
}
