using practice1.Models;  //If you won't write this line compiler will throw and error because User class is inside the Model folder 

namespace practice1.Repository
{
    public interface IUserRepository
    {   
        User Login(User user);

        bool Register(User user);
    }
}
