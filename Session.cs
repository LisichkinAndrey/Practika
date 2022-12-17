using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class Session
    {
        [Key]
        public int IDSession { get; set; }
        public int IDclient { get; set; }
        public int IDZapis { get; set; }
        public int IDcard { get; set; }
        public int IDPayment { get; set; }
        Payment Payment { get; set; }
        public int ServiceNumber { get; set; }
        public DateTime dayTime { get; set; }
        public DateTime Duration { get; set; }

    }
}
