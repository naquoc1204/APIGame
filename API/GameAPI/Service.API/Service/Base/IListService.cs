using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.API.Base
{
    public interface IListService
    {
        //void ChangeState(int id, int state);
        //void ChangeState(string[] arrayID, int state);
        //void ChangeState(int id, int state, int stateby);
        //void ChangeState(int id, int state, bool approve, int stateby);
        //void ChangeState(string[] arrayID, int state, int stateby);
        //void ChangeUnApprove(int id, bool approve, int userID);
        void ChangeStatus(int id, int status);
        int ToggleStatus(int id);
        void UpdateOrder(int id, int order);
        void UpdateType(int id, string type);
        int GetOrderLast(int plus, string type);
    }
}