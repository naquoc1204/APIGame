/**
 * Project: Data.API 
 * FileName: IListRepository.cs 
 * EF Version: 6.1.0 - FR: 4.5
 * Description: Short description.
 * Last update: 2020-1-31
 * Author: NGUYỄN ANH QUỐC (ASUS)
 * Email: naquoc1204@gmail.com
 * Phone: 093.123.6699
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.API.Infrastructure
{
    public interface IListRepository
    {
        //void ChangeState(int id, int state);
        //void ChangeState(string[] arrayID, int state);
        //void ChangeState(int id, int state, int stateby);
        //void ChangeState(int id, int state, bool approve, int stateby);
        //void ChangeState(string[] arrayID, int state, int stateby);
        //void ChangeUnApprove(int id, bool approve, int userID);
        //void ChangeStatus(int id, int status);
        //void ToggleStatus(int id);
        //void UpdateOrder(int id, int order);
        //void UpdateType(int id, string type);
        void ChangeStatus(int id, int status);
        int ToggleStatus(int id);
        void UpdateOrder(int id, int order);
        void UpdateType(int id, string type);
        int GetOrderLast(int plus, string type);
    }
}
