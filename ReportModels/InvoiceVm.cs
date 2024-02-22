using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.ReportModels
{
    public class InvoiceVm
    {
        public DateTime Date { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTowill { get; set; }
        public int? NumberofHorses { get; set; }
        public bool ispaid { get; set; }
        public string CustomerNameEn { get; set; }
        public double ServiceCost { get; set; }
        public string AdditionalTypes { get; set; }
        public double TotalAdditionalCost { get; set; }
        public double TotalCost { get; set; }
        public string ServiceTitle { get; set; }
        public int? AppointmentsId { get; set; }
        public string CustomerPhone { get; set; }
        public string OrderSerialNumber { get; set; }
        public int? TotalNumberofHorses { get; set; }




    }
}
