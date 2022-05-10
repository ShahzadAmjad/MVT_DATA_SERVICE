using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeeringDB_Data_Import.Models
{
    public class pdb_internet_exchange_networks
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        
            public int id { get; set; }
            public int ix_id { get; set; }
            public Ix ix { get; set; }
            public string name { get; set; }
            public string descr { get; set; }
            public object mtu { get; set; }
            public bool dot1q_support { get; set; }
            public int rs_asn { get; set; }
            public object arp_sponge { get; set; }
            public List<NetSet> net_set { get; set; }
            public List<IxpfxSet> ixpfx_set { get; set; }
            public string ixf_ixp_member_list_url_visible { get; set; }
            public DateTime created { get; set; }
            public DateTime updated { get; set; }
            public string status { get; set; }
        }

        public class Ix2
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

        public class IxpfxSet
        {
            public int id { get; set; }
            public string protocol { get; set; }
            public string prefix { get; set; }
            public bool in_dfz { get; set; }
            public DateTime created { get; set; }
            public DateTime updated { get; set; }
            public string status { get; set; }
        }

        //public class Meta
        //{
        //}

        public class NetSet
        {
            public int id { get; set; }
            public int org_id { get; set; }
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
            public DateTime netixlan_updated { get; set; }
            public DateTime? netfac_updated { get; set; }
            public DateTime poc_updated { get; set; }
            public string policy_url { get; set; }
            public string policy_general { get; set; }
            public string policy_locations { get; set; }
            public bool policy_ratio { get; set; }
            public string policy_contracts { get; set; }
            public bool allow_ixp_update { get; set; }
            public string status_dashboard { get; set; }
            public DateTime created { get; set; }
            public DateTime updated { get; set; }
            public string status { get; set; }
        }

        public class Org4
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
            public DateTime created { get; set; }
            public DateTime updated { get; set; }
            public string status { get; set; }
        }

        //public class Root
        //{
        //    public List<Datum> data { get; set; }
        //    public Meta meta { get; set; }
        //}



    }
