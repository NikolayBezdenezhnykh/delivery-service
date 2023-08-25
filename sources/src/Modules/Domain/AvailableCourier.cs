using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class AvailableCourier
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public int? Count { get; set; }
    }
}
