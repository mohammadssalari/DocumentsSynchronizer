using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentsSynchronizer.Models.RestModels
{
    public class DocumentDossier
    {
        public string id { get; set; }
        public Fields fields { get; set; }
        public Links links { get; set; }
    }

    public class Fields
    {
        public string GGUID { get; set; }
        public string GGUID1 { get; set; }
        public string GGUID2 { get; set; }
        public string OBJECTTYPE1 { get; set; }
        public string OBJECTTYPE2 { get; set; }
        public string KEYWORD { get; set; }
        public string PLUSINFO { get; set; }
        public DateTime SORTDATE { get; set; }
        public string DOCEXT { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
        public string object1 { get; set; }
        public string object2 { get; set; }
    }






}
