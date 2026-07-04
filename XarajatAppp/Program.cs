using XarajatAppp.Data;
using XarajatAppp.Exensions;
using XarajatAppp.Repositories;

public class Program
{
    UserRepository userRepository = new UserRepository();
    TeamRepository teamRepository;
    ExpenditureRepository expenditure;
    public static string username;
    public static string fullname;

    private string userN;

    public Program()
    {
        teamRepository = new TeamRepository(userRepository);
        expenditure = new ExpenditureRepository(teamRepository);
    }
    private static void Main(string[] args)
    {
        Program p = new Program();
        p.UserController();
        
    }
    private async void UserController()
    {
        bool uc = true;
        while (uc)
        {
            Console.WriteLine("1-> register\n2-> login\n3-> foydalanuvchilarin ko'rish");
            int n = int.Parse(Console.ReadLine());
            switch (n)
            {
                case 1:
                    Console.WriteLine("Username: ");
                    username = Console.ReadLine();
                    Console.WriteLine("Fullname: ");
                    fullname = Console.ReadLine();
                    userN = username;
                    var b = await userRepository.Register(username, fullname);

                    if (b)
                    {
                        Console.WriteLine("Registratsiya muvaffaqiyatli bo'ldi");
                        TeamController();
                    }
                    else { Console.WriteLine("Registratsiyadan o'tmadingiz!!!"); }
                    break;
                case 2:
                    Console.WriteLine("Username: ");
                    var userName1 = Console.ReadLine();

                    var result = await userRepository.Login(userName1);
                    if (result)
                    {
                        Console.WriteLine("Login tasdiqlandi");

                        TeamController();
                    }
                    else { Console.WriteLine("Bunday foydalanuvchi mavjud emas"); }
                    break;
                case 3:
                    var users = await userRepository.GetAllUsers();
                    if (users.Count != 0)
                    {
                        foreach (var u in users)
                        {
                            Console.WriteLine($"{u.Id}\n{u.Username}\n{u.Fullname}\n{u.CreatedDate}");
                        }
                    }
                    else { Console.WriteLine("Foydalanuvchilar mavjud emas"); }
                    break;
                default:

                    break;
            }
        }
    }

    public async void TeamController()
    {
        bool a = true;
        while (a)
        {
            Console.WriteLine("1-> guruh yaratish\n2-guruhga qo'shilish\n3-orqaga");
            var teamMenu = int.Parse(Console.ReadLine());
            switch (teamMenu)
            {
                case 1:
                    Console.WriteLine("Guruh nomini kiriting: ");
                    var teamName = Console.ReadLine();

                    Console.WriteLine("Guruh uchun mahfiy kod kiriting");
                    var password = Console.ReadLine();
                    await teamRepository.CreateTeam(teamName, password);
                    break;
                case 2:

                    Console.WriteLine("Guruh nomini kiriting: ");
                    var teamName1 = Console.ReadLine();

                    Console.WriteLine("Maxfiy kodni kiriting!!!");
                    var password2 = Console.ReadLine();

                    var b = await teamRepository.AddTeam(teamName1!, userN, password2!);
                    if (b)
                        ExpenditureController();
                    break;
                case 3:
                    a = false;
                    teamMenu = 3;
                    break;
                default:
                    break;
            }
        }
        
    }

    public async Task ExpenditureController()
    {
        bool a = true;
        while (a)
        {
            Console.WriteLine("1-> xarajat qo'shish\n2-Guruh hisoboti");
            var teamMenu = int.Parse(Console.ReadLine());
            switch (teamMenu)
            {
                case 1:
                    Console.WriteLine("Kim xarajat qildi (Username): ");
                    var u = Console.ReadLine();

                    Console.WriteLine("Qancha xarajat qildingiz: ");
                    var cost = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("Nima xarid qildingiz: ");
                    var description = Console.ReadLine();

                    await expenditure.AddCost(u, fullname, cost, description);
                    break;
                case 2:
                    Console.WriteLine("Guruh nomini kiriting");
                    var teamName = Console.ReadLine();
                    var teams = await expenditure.Calculate(teamName!);
                    foreach (var t in teams)
                    {
                        Console.WriteLine($" ID: {t.Id}\nUsername: {t.Username}\nFullname: {t.Fullname}\nUmumiy xarajati: {t.TotalCost}\nQancha pul berishi kerak: {t.ToGetMoney}\nQancha pul olish kk: {t.ToGiveMoney}\nGuruh harajati: {t.TotalCostTeamMoney}");
                    }
                    break;
                case 3:
                    a = false;
                    teamMenu = 3;
                    break;
                default:
                    break;
            }
        }
    }
}