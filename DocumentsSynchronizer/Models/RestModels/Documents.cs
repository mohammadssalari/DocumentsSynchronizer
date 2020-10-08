
using System;

namespace DocumentsSynchronizer.Models
{
    public class Fields
    {
        public string GGUID { get; set; }
        public string KEYWORD { get; set; }
        public DateTime UPDATETIMESTAMP { get; set; }
        public string UPDATEUSER { get; set; }
    }
    public class Links
    {
        public string self { get; set; }
        public string dossier { get; set; }
    }
    public class Documents
    {
        public string objectType { get; set; }
        public string id { get; set; }
        public string ETAG { get; set; }
        public Fields fields { get; set; }
        public Links links { get; set; }
    }
}