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
        bool CheckToolAvailability(int id);
    }
}