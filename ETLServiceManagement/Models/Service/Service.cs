using System.ComponentModel.DataAnnotations;

namespace ETLServiceManagement.Models.Service
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string RefreshTime { get; set; }
        [Required]
        public string DataSource { get; set; }
        [Required]
        public string SourceFolder { get; set; }
        [Required]
        public string Mapping { get; set; }
        [Required]
        public string DestinationDb { get; set; }
        [Required]
        public string DbUrl { get; set; }
        [Required]
        public string DbName { get; set; }
        [Required]
        public string TableName { get; set; }
        public string Status { get; set; }
    }
}
