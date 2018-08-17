using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace chananet.DataContract
{
    public class LineUser
    {
        public int id { get; set; }

        public string AccessToken { get; set; }

        public int LineProgram_id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
