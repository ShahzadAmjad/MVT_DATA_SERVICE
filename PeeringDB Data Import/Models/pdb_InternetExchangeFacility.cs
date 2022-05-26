using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeeringDB_Data_Import.Models
{
    public class pdb_InternetExchangeFacility
    {

        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public int ix_id { get; set; }
        public Ix ix { get; set; }
        public int fac_id { get; set; }
        public Fac fac { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public string status { get; set; }
    }

    public class Fac
    {
        public int id { get; set; }
        public int org_id { get; set; }
        public string org_name { get; set; }
        public Org org { get; set; }
        public string name { get; set; }
        public string aka { get; set; }
        public string name_long { get; set; }
        public string website { get; set; }
        public string clli { get; set; }
        public string rencode { get; set; }
        public string npanxx { get; set; }
        public string notes { get; set; }
        public int net_count { get; set; }
        public int ix_count { get; set; }
        public string sales_email { get; set; }
        public string sales_phone { get; set; }
        public string tech_email { get; set; }
        public string tech_phone { get; set; }
        public List<object> available_voltage_services { get; set; }
        public object diverse_serving_substations { get; set; }
        public object property { get; set; }
        public string region_continent { get; set; }
        public object status_dashboard { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public string status { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string floor { get; set; }
        public string suite { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }

    public class Ix
    {
        public int id { get; set; }
        public int org_id { get; set; }
        public Org org { get; set; }
        public string name { get; set; }
        public string aka { get; set; }
        public string name_long { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string region_continent { get; set; }
        public string media { get; set; }
        public string notes { get; set; }
        public bool proto_unicast { get; set; }
        public bool proto_multicast { get; set; }
        public bool proto_ipv6 { get; set; }
        public string website { get; set; }
        public string url_stats { get; set; }
        public string tech_email { get; set; }
        public string tech_phone { get; set; }
        public string policy_email { get; set; }
        public string policy_phone { get; set; }
        public string sales_phone { get; set; }
        public string sales_email { get; set; }
        public List<int> fac_set { get; set; }
        public List<int> ixlan_set { get; set; }
        public int net_count { get; set; }
        public int fac_count { get; set; }
        public int ixf_net_count { get; set; }
        public object ixf_last_import { get; set; }
        public object ixf_import_request { get; set; }
        public string ixf_import_request_status { get; set; }
        public string service_level { get; set; }
        public string terms { get; set; }
        public object status_dashboard { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public string status { get; set; }
    }

    //public class Meta
    //{
    //}

    //public class Org
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public string aka { get; set; }
    //    public string name_long { get; set; }
    //    public string website { get; set; }
    //    public string notes { get; set; }
    //    public string address1 { get; set; }
    //    public string address2 { get; set; }
    //    public string city { get; set; }
    //    public string country { get; set; }
    //    public string state { get; set; }
    //    public string zipcode { get; set; }
    //    public string floor { get; set; }
    //    public string suite { get; set; }
    //    public double? latitude { get; set; }
    //    public double? longitude { get; set; }
    //    public DateTime created { get; set; }
    //    public DateTime updated { get; set; }
    //    public string status { get; set; }
    //}

    public class Root_pdb_InternetExchangeFacility
    {
        public List<pdb_InternetExchangeFacility> data { get; set; }
        public Meta meta { get; set; }
    }



}
