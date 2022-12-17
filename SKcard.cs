using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Practice
{
    public class SKcard
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public int card { get; set; }
        public DateTime datecard { get; set; }
    }
}
