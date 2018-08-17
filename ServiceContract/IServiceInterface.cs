using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using chananet.DataContract;

namespace chananet.ServiceContract
{
    [ServiceContract]
    public interface IServiceInterface
    {
        [OperationContract]
        LineProgram getlineProgram();

        [OperationContract]
        ProcessResoult LineRegisterUser(int User_id);

        [OperationContract]
        ProcessResoult sentNotify(string Message);

        [OperationContract]
        ProcessResoult getlineUser(out List<LineUser> List_LineUser, int? id, string UserName, string Password);

        [OperationContract]
        ProcessResoult getAccesTokenUser(string code, int? id);

        [OperationContract]
        ProcessResoult test();
    }
}
