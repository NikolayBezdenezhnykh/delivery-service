using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class ReserveCourier
    {
        public int Id { get ; set; }

        public DateTime? Date { get; set; }

        public string Address { get; set; }

        public DateTime? DateReserve { get; set; }

        public int? Status { get; set; }
    }
}
