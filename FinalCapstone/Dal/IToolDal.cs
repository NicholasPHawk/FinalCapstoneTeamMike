using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal
{
    public interface IToolDal
    {
        IList<Tool> GetTools();
        Tool GetToolDetails(int id);
    }
}
