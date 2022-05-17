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
<<<<<<< HEAD
        //6th change
        public pdb_Network properties { get; set; }
=======
        public pdb_NetworkPOC properties { get; set; }
>>>>>>> 5bc26f4bf18c6c9eeafceb11bbbc643ced29cc98
    }
    public class Geometry
    {
        public string type { get; set; }
        public List<double?> coordinates { get; set; }
    }
<<<<<<< HEAD

=======
>>>>>>> 5bc26f4bf18c6c9eeafceb11bbbc643ced29cc98
}
