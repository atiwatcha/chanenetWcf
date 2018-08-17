using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWcf.chanenetWcf;

namespace TestWcf
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceInterfaceClient client = new ServiceInterfaceClient();
            ProcessResoult pr = client.sentNotify("ทดสอบนะ");
            if(pr.Success){

            }
        }
    }
}
