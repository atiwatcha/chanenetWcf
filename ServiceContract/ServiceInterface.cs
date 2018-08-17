using chananet.BussinessLogic;
using chananet.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chananet.ServiceContract
{
    public class ServiceInterface : IServiceInterface
    {
        public LineProgram getlineProgram()
        {
            LineProgram lineProgram = (LineProgram)null;
            new ServiceAction().getlineProgram(out lineProgram);
            return lineProgram;
        }

        public ProcessResoult LineRegisterUser(int User_id)
        {
            ProcessResoult processResoult = new ProcessResoult();
            try
            {
                new ServiceAction().LineRegisterUser(User_id);
                processResoult.Success = true;
                return processResoult;
            }
            catch (Exception ex)
            {
                processResoult.Message = ex.Message;
                processResoult.Success = false;
                return processResoult;
            }
        }

        public ProcessResoult sentNotify(string Message)
        {
            ProcessResoult processResoult = new ProcessResoult();
            try
            {
                new ServiceAction().sentNotify(Message);
                processResoult.Success = true;
                processResoult.Message = "ส่งข้อความสำเร็จ";
                return processResoult;
            }
            catch (Exception ex)
            {
                processResoult.Message = ex.Message;
                processResoult.Success = false;
                return processResoult;
            }
        }

        public ProcessResoult getlineUser(out List<LineUser> List_LineUser, int? id, string UserName, string Password)
        {
            List_LineUser = new List<LineUser>();
            ProcessResoult processResoult = new ProcessResoult();
            try
            {
                ServiceAction serviceAction = new ServiceAction();
                List_LineUser = serviceAction.getlineUser(id, UserName, Password);
                processResoult.Success = true;
                return processResoult;
            }
            catch (Exception ex)
            {
                processResoult.Message = ex.Message;
                processResoult.Success = false;
                return processResoult;
            }
        }

        public ProcessResoult getAccesTokenUser(string code, int? id)
        {
            ProcessResoult processResoult = new ProcessResoult();
            try
            {
                new ServiceAction().getAccesTokenUser(code, id);
                processResoult.Success = true;
                return processResoult;
            }
            catch (Exception ex)
            {
                processResoult.Message = ex.Message;
                processResoult.Success = false;
                return processResoult;
            }
        }

        public ProcessResoult test()
        {
            ProcessResoult processResoult = new ProcessResoult();
            processResoult.Message = "test";
            processResoult.Success = true;
            return processResoult;
        }
    }
}