
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeeringDB_Data_Import.Models
{
    public class pdb_NetworkPOC
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public int id { get; set; }
        public int net_id { get; set; }
        public Net net { get; set; }
        public string role { get; set; }
        public string visible { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string url { get; set; }
        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
        public string status { get; set; }
    }

    public class Meta
    {
    }

<<<<<<< HEAD
    //public class Org
    //    {
    //        public int id { get; set; }
    //        public string name { get; set; }
    //        public string aka { get; set; }
    //        public string name_long { get; set; }
    //        public string website { get; set; }
    //        public string notes { get; set; }
    //        public string address1 { get; set; }
    //        public string address2 { get; set; }
    //        public string city { get; set; }
    //        public string country { get; set; }
    //        public string state { get; set; }
    //        public string zipcode { get; set; }
    //        public string floor { get; set; }
    //        public string suite { get; set; }
    //        public double? latitude { get; set; }
    //        public double? longitude { get; set; }
    //        public DateTime created { get; set; }
    //        public DateTime updated { get; set; }
    //        public string status { get; set; }
    //    }
=======
    public class Net
    {
        public int id { get; set; }
        public int org_id { get; set; }
        public Org org { get; set; }
        public string name { get; set; }
        public string aka { get; set; }
        public string name_long { get; set; }
        public string website { get; set; }
        public int asn { get; set; }
        public string looking_glass { get; set; }
        public string route_server { get; set; }
        public string irr_as_set { get; set; }
        public string info_type { get; set; }
        public int info_prefixes4 { get; set; }
        public int info_prefixes6 { get; set; }
        public string info_traffic { get; set; }
        public string info_ratio { get; set; }
        public string info_scope { get; set; }
        public bool info_unicast { get; set; }
        public bool info_multicast { get; set; }
        public bool info_ipv6 { get; set; }
        public bool info_never_via_route_servers { get; set; }
        public int ix_count { get; set; }
        public int fac_count { get; set; }
        public string notes { get; set; }
        public object netixlan_updated { get; set; }
        public DateTime? netfac_updated { get; set; }
        public DateTime? poc_updated { get; set; }
        public string policy_url { get; set; }
        public string policy_general { get; set; }
        public string policy_locations { get; set; }
        public bool policy_ratio { get; set; }
        public string policy_contracts { get; set; }
        public List<int> netfac_set { get; set; }
        public List<object> netixlan_set { get; set; }
        public List<int> poc_set { get; set; }
        public bool allow_ixp_update { get; set; }
        public object status_dashboard { get; set; }
        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
        public string status { get; set; }
    }
>>>>>>> 5bc26f4bf18c6c9eeafceb11bbbc643ced29cc98

    public class Org
    {
        public int id { get; set; }
        public string name { get; set; }
        public string aka { get; set; }
        public string name_long { get; set; }
        public string website { get; set; }
        public string notes { get; set; }
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
        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
        public string status { get; set; }
    }

    public class Root
    {
        public List<pdb_NetworkPOC> data { get; set; }
        public Meta meta { get; set; }
    }



}
