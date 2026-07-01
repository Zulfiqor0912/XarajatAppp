using XarajatAppp.Data;
using XarajatAppp.Exensions;
using XarajatAppp.Repositories;

public class Program
{
    UserRepository userRepository = new UserRepository();

    private static void Main(string[] args)
    {
        Console.WriteLine("1-> register\n2-> login");
        int n = int.Parse(Console.ReadLine());

        Program p = new Program();
        p.UserController(n);
        
    }
    private async void UserController(int n)
    {
        switch (n)
        {
            case 1:
                Console.WriteLine("Username: ");
                var userName = Console.ReadLine();
                Console.WriteLine("Fullname: ");
                var fullname = Console.ReadLine();

                await userRepository.Register(userName, fullname);
                break;
            case 2:
                Console.WriteLine("Username: ");
                var userName1 = Console.ReadLine();

                var result =await userRepository.Login(userName1);
                if (result)
                {
                    Console.WriteLine("Login tasdiqlandi");
                    Console.WriteLine("1-> guruh yaratish\n2-guruhga qo'shilish");

                    var teamMenu = int.Parse(Console.ReadLine());
                    TeamController(teamMenu);
                }
                else { Console.WriteLine("Bunday foydalanuvchi mavjud emas"); }
                break;
            default:
                break;
        }
    }

    public async void TeamController(int n)
    {
        switch (n)
        {
            case 1:
                 
                break;
            case 2:
                
                break;
            default:
                break;
        }
    }
}