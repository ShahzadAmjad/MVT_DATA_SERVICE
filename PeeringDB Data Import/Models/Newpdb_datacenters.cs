using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeeringDB_Data_Import.Models
{
    public class Newpdb_datacenters
    {
        public string _id { get; set; }
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public pdb_datacenters properties { get; set; }
    }

    public class Geometry2
    {
        public string type { get; set; }
        public List<double?> coordinates { get; set; }
    }

    //public class Meta2
    //{
    //}

    //public class Org2
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public string aka { get; set; }
    //    public string name_long { get; set; }
    //    public string website { get; set; }
    //    public string notes { get; set; }
    //    public List<int> net_set { get; set; }
    //    public List<int> fac_set { get; set; }
    //    public List<int> ix_set { get; set; }
    //    public string address1 { get; set; }
    //    public string address2 { get; set; }
    //    public string city { get; set; }
    //    public string country { get; set; }
    //    public string state { get; set; }
    //    public string zipcode { get; set; }
    //    public string floor { get; set; }
    //    public string suite { get; set; }
    //    public double latitude { get; set; }
    //    public double longitude { get; set; }
    //    public DateTime created { get; set; }
    //    public DateTime updated { get; set; }
    //    public string status { get; set; }
    //}

    //public class Properties
    //{
    //    public int id { get; set; }
    //    public int org_id { get; set; }
    //    public string org_name { get; set; }
    //    public Org org { get; set; }
    //    public string name { get; set; }
    //    public string aka { get; set; }
    //    public string name_long { get; set; }
    //    public string website { get; set; }
    //    public string clli { get; set; }
    //    public string rencode { get; set; }
    //    public string npanxx { get; set; }
    //    public string notes { get; set; }
    //    public int net_count { get; set; }
    //    public int ix_count { get; set; }
    //    public string sales_email { get; set; }
    //    public string sales_phone { get; set; }
    //    public string tech_email { get; set; }
    //    public string tech_phone { get; set; }
    //    public List<object> available_voltage_services { get; set; }
    //    public object diverse_serving_substations { get; set; }
    //    public object property { get; set; }
    //    public string region_continent { get; set; }
    //    public object status_dashboard { get; set; }
    //    public DateTime created { get; set; }
    //    public DateTime updated { get; set; }
    //    public string status { get; set; }

        

    //    public string address1 { get; set; }
    //    public string address2 { get; set; }
    //    public string city { get; set; }
    //    public string country { get; set; }
    //    public string state { get; set; }
    //    public string zipcode { get; set; }
    //    public string floor { get; set; }
    //    public string suite { get; set; }
    //    public double latitude { get; set; }
    //    public double longitude { get; set; }

        
    //}

    public class NewRoot
    {
        public List<Newpdb_datacenters> data { get; set; }
        public Meta meta { get; set; }
    }
}
