using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public interface ILibrarianDal
    {
        Librarian GetLibrarian(string username);
        bool RegisterLibrarian(Librarian librarian);
        bool RegisterUser(User user);
    }
}
