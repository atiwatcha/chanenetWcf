using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chananet.DataContract
{
    public class LineProgram
    {
        public int id { get; set; }

        public string AccessToken { get; set; }

        public string ClientSecret { get; set; }

        public string redirect_uri { get; set; }
    }
}
