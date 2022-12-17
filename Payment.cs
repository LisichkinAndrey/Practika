using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class Payment
    {

        public int IDSession { get; set; }
        [Key]
        public int IDPayment { get; set; }
        public float Result { get; set; }
        public float Entered { get; set; }
        public float Change { get; set; }
        public DateTime DatePayment { get; set; }
    }
}
