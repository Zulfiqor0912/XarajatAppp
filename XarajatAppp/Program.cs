using XarajatAppp.Data;
using XarajatAppp.Exensions;
using XarajatAppp.Repositories;

public class Program
{
    IUserRepository userRepository = new UserRepository();
    ITeamRepository teamRepository = new TeamRepository();
    private string userN;

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
                userN = userName;
                var b = await userRepository.Register(userName, fullname);

                if (b) Console.WriteLine("Registratsiya muvaffaqiyatli bo'ldi");
                else { Console.WriteLine("Registratsiyadan o'tmadingiz!!!"); }
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
                Console.WriteLine("Guruh nomini kiriting: ");
                var teamName = Console.ReadLine();

                Console.WriteLine("Guruhga uchun mahfiy kod kiriting");
                var password = Console.ReadLine();

                await teamRepository.CreateTeam(teamName, password);
                break;
            case 2:
                Console.WriteLine("Guruh nomini kiriting: ");
                var teamName1 = Console.ReadLine();

                Console.WriteLine("Maxfiy kodni kiriting!!!");
                var password2 = Console.ReadLine();

                await teamRepository.AddTeam(teamName1!, userN, password2!);
                break;
            default:
                break;
        }
    }
}