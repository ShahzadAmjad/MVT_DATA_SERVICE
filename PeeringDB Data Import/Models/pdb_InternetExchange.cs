using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeeringDB_Data_Import.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class pdb_InternetExchange
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
        public List<FacSet_pdb_InternetExchange> fac_set { get; set; }
        public List<IxlanSet> ixlan_set { get; set; }
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

    public class FacSet_pdb_InternetExchange
    {
        public int id { get; set; }
        public int org_id { get; set; }
        public string org_name { get; set; }
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

    public class IxlanSet
    {
        public int id { get; set; }
        public string name { get; set; }
        public string descr { get; set; }
        public object mtu { get; set; }
        public bool dot1q_support { get; set; }
        public int rs_asn { get; set; }
        public object arp_sponge { get; set; }
        public string ixf_ixp_member_list_url_visible { get; set; }
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
    //    public double? latitude { get; set; }
    //    public double? longitude { get; set; }
    //    public DateTime created { get; set; }
    //    public DateTime updated { get; set; }
    //    public string status { get; set; }
    //}

    public class Root_pdb_InternetExchange
    {
        public List<pdb_InternetExchange> data { get; set; }
        public Meta meta { get; set; }
    }




}
