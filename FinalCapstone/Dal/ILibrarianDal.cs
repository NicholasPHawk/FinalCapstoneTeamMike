using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal
{
    public interface ILibrarianDal
    {
        Librarian GetLibrarian(string username);
        bool RegisterLibrarian(Librarian librarian);
        bool RegisterUser(User user);
    }
}
