using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Persondirectory.Models
{
    public class StatusModel
    {
        public string Status { get; set; }
        public string Perid { get; set; }
        public string Statusmsg { get; set; }

    }
}
