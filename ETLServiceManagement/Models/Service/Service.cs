using System.ComponentModel.DataAnnotations;

namespace ETLServiceManagement.Models.Service
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string RefreshTime { get; set; }
        public string DataSource { get; set; }
        public string Mapping { get; set; }
        public string Status { get; set; }
    }
}
