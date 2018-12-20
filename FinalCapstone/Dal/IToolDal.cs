using FinalCapstone.Models;
using System.Collections.Generic;

namespace FinalCapstone.Dal
{
    public interface IToolDal
    {
        IList<Tool> GetTools(bool checkedOut);
        Tool GetToolDetails(int id, bool checkedOut);
        IList<ToolLoanRecordSearchModel> GetLoanRecords(string searchOptions, string searchString);
        IList<User> GetUsers();
        bool ChangeCheckedOutStatus(int id, bool checkedOut);
        bool CheckOut(Cart cart);
        IList<Tool> RemoveAToolList();
        void RemoveATool(Tool tool);
        bool AddTool(Tool tool);
    }
}
