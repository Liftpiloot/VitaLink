using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitaLink
{
    public class HealthDataItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Data { get; set; }
        public string Type { get; set; }
    }

    public class HealthDataResponse
    {
        public List<HealthDataItem> Data { get; set;}
    }
}
