﻿using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        void RemoveATool(Tool tool);
        IList<Tool> RemoveAToolList();
        bool AddTool(Tool tool);
    }
}
