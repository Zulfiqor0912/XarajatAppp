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
    private static string teamName;

    private string userN;

    public Program()
    {
        teamRepository = new TeamRepository(userRepository);
        expenditure = new ExpenditureRepository(teamRepository);
    }
    private static async Task Main(string[] args)
    {
        Program p = new Program();
        await p.UserController();
        
    }
    private async Task UserController()
    {
        bool uc = true;
        while (uc)
        {
            Console.WriteLine("\n    1 -> register\n    2 -> login\n    3 -> foydalanuvchilarni ko'rish");
            Console.Write("->");
            int n = int.Parse(Console.ReadLine());
            switch (n)
            {
                case 1:
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                    Console.Write("Fullname: ");
                    fullname = Console.ReadLine();
                    Console.Write("Password: ");
                    var loginPassword = Console.ReadLine();
                    userN = username;
                    var b = await userRepository.Register(username, fullname, loginPassword);

                    if (b)
                    {
                        Console.WriteLine("Registratsiya muvaffaqiyatli bo'ldi");
                        await TeamController();
                    }
                    else { Console.Write("Registratsiyadan o'tmadingiz!!!: "); }
                    break;
                case 2:
                    Console.Write("Username: ");
                    var userName1 = Console.ReadLine();

                    Console.Write("Password");
                    var loginPassword1 = Console.ReadLine();
                    var result = await userRepository.Login(userName1, loginPassword1);

                    if (result)
                    {
                        Console.WriteLine("Login tasdiqlandi");
                        await TeamController();
                    }
                    else { Console.WriteLine("Bunday foydalanuvchi mavjud emas"); }
                    break;
                case 3:
                    var users = await userRepository.GetAllUsers();
                    if (users.Count != 0)
                    {
                        foreach (var u in users)
                        {
                            Console.WriteLine($"ID: {u.Id}\nUsername: {u.Username}\nFullname: {u.Fullname}\nYaratilgan sanasi: {u.CreatedDate}");
                        }
                    }
                    else { Console.WriteLine("Foydalanuvchilar mavjud emas"); }
                    break;
                default:

                    break;
            }
        }
    }

    public async Task TeamController()
    {
        bool a = true;
        while (a)
        {
            Console.WriteLine("\n    1 -> guruh yaratish\n    2 -> guruhga qo'shilish\n    3 -> guruhlar ro'yhati\n    4 -> orqaga");
            Console.Write("-> ");
            var teamMenu = int.Parse(Console.ReadLine());
            switch (teamMenu)
            {
                case 1:
                    Console.Write("Guruh nomini kiriting: ");
                    teamName = Console.ReadLine();

                    Console.Write("Guruh uchun mahfiy kod kiriting: ");
                    var password = Console.ReadLine();
                    var x = await teamRepository.CreateTeam(teamName, password);
                    if (x)
                        await ExpenditureController(teamName);
                    break;
                case 2:

                    Console.Write("Guruh nomini kiriting: ");
                    var teamName1 = Console.ReadLine();

                    Console.Write("Maxfiy kodni kiriting: ");
                    var password2 = Console.ReadLine();

                    var b = await teamRepository.AddTeam(teamName1!, userN, password2!);
                    if (b)
                        await ExpenditureController(teamName1);
                    break;
                case 3:
                    var teams = await teamRepository.GetAllTeam();
                    foreach (var item in teams)
                    {
                        Console.WriteLine($"" +
                            $"  Id: {item.Id}" +
                            $"  Teamname: {item.Name}" +
                            $"  Password: *********");
                    }
                    break;
                case 4:
                    a = false;
                    teamMenu = 4;
                    break;
                default:
                    break;
            }
        }
        
    }

    public async Task ExpenditureController(string teamname)
    {
        bool a = true;
        while (a)
        {
            Console.WriteLine("\n    1 -> xarajat qo'shish\n    2 -> Guruh hisoboti\n    3 -> orqaga");
            Console.Write("-> ");
            var teamMenu = int.Parse(Console.ReadLine());
            switch (teamMenu)
            {
                case 1:
                    Console.Write("Kim xarajat qildi (Username): ");
                    var u = Console.ReadLine();

                    Console.Write("Qancha xarajat qildingiz: ");
                    var cost = decimal.Parse(Console.ReadLine());

                    Console.Write("Nima xarid qildingiz: ");
                    var description = Console.ReadLine();

                    await expenditure.AddCost(u, fullname, cost, description);
                    break;
                case 2:
                    var teams = await expenditure.Calculate(teamname!);
                    foreach (var t in teams)
                    {
                        Console.WriteLine($"\nID: {t.Id}\nUsername: {t.Username}\nFullname: {t.Fullname}\nUmumiy xarajati: {t.TotalCost}\nQancha pul olish kk: {t.ToGetMoney}\nQancha pul berishi kerak: {t.ToGiveMoney}\nGuruh harajati: {t.TotalCostTeamMoney}");
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