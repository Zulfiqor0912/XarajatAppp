using XarajatAppp.Exensions;
using XarajatAppp.Repositories;

public class Program
{
    UserRepository userRepository = new UserRepository();

    private static void Main(string[] args)
    {
        Console.WriteLine("1-> register\n2-> login");
        int n = int.Parse(Console.ReadLine());
        
    }
    private void UserController(int n)
    {
        switch (n)
        {
            case 1:
                Console.WriteLine("Username: ");
                var userName = Console.ReadLine();
                Console.WriteLine("Fullname: ");
                var fullname = Console.ReadLine();

                userRepository.Register();
                break;
            case 2:
                break;
            default:
                break;
        }
    }
}