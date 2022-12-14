using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeeringDB_Data_Import.Models
{
    public class Cpdb_tranformation
    {
        public string _id { get; set; }
        public string type { get; set; }
        public Geometry geometry { get; set; }

        //6th change
        public pdb_datacenters properties { get; set; }

    }
    public class Geometry
    {
        public string type { get; set; }
        public List<double?> coordinates { get; set; }
    }

}
