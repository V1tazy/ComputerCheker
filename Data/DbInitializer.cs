using ComputerCheker.DAL.Context;
using ComputerCheker.DAL.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace ComputerCheker.Data
{
    public class DbInitializer
    {
        private readonly ComputerDB _db;
        private readonly ILogger _Logger;

        public DbInitializer(ComputerDB dB, ILogger<DbInitializer> Logger)
        {
            _db = dB;
            _Logger = Logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация БД...");


            //_Logger.LogInformation("Удаление существующей БД...");
           // await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);

           // _Logger.LogInformation("Удаление существующей БД выполнено за {0} mc", timer.ElapsedMilliseconds);


            _Logger.LogInformation("Миграция БД...");

            await _db.Database.MigrateAsync();

            _Logger.LogInformation("Миграция выполнена за {0}", timer.ElapsedMilliseconds);

            if (await _db.Computers.AnyAsync()) return;

            await InitializeRole();
            await InitializeUser();
            await InitializeProgramType();
            await InitializePrograms();
            await InitializeComputer();
            await InitializeSetupMode();
            await InitializeSetups();

            _Logger.LogInformation("Инициализация БД выполнена за {0} c", timer.Elapsed.TotalSeconds);
        }


        #region ProgramType

        private ProgramType[] _ProgramTypes { get; set; }
        private static readonly string[] _ProgramTypeName = { "текстовый редактор", "операционная система", "СУБД" };

        public async Task InitializeProgramType()
        {
            var timer = Stopwatch.StartNew();


            _Logger.LogInformation("Инициализация Типов...");
            _ProgramTypes = Enumerable.Range(0, _ProgramTypeName.Length)
                .Select(i => new ProgramType 
                {
                    Name = _ProgramTypeName[i],
                })
                .ToArray();

            await _db.ProgramTypes.AddRangeAsync(_ProgramTypes);

            await _db.SaveChangesAsync();

            _Logger.LogInformation("Инициализация Типов выполнена за {0} mc", timer.ElapsedMilliseconds);
        }
        #endregion

        #region Role
        private Role[] _Roles { get; set; }

        private static readonly string[] _RoleNames = { "Администратор", "Директор", "Пользователи" };

        public async Task InitializeRole()
        {
            var timer = Stopwatch.StartNew();

            _Logger.LogInformation("Инициализация Ролей...");
            _Roles = Enumerable.Range(0, _RoleNames.Length)
                .Select(i => new Role
                {
                    Name = _RoleNames[i],
                    
                }).ToArray();

            await _db.Roles.AddRangeAsync(_Roles);

            await _db.SaveChangesAsync();

            _Logger.LogInformation("Инициализация Ролей выполнена за {0} c", timer.Elapsed.TotalSeconds);
        }
        #endregion

        #region SetupModel
        private SetupMode[] _SetupModes { get; set; }

        private static readonly string[] _SetupModeName = { "сетевой", "локальный", "полный", "демо" };

        public async Task InitializeSetupMode()
        {
            var timer = Stopwatch.StartNew();

            _Logger.LogInformation("Инициализация Режима Установок...");

            _SetupModes = Enumerable.Range(0, _SetupModeName.Length)
                .Select(i => new SetupMode
                {
                    Name = _SetupModeName[i],
                    
                }).ToArray();

            _Logger.LogInformation("Инициализация Режима Установок выполнена за {0} c", timer.Elapsed.TotalSeconds);
        }
        #endregion

        #region Computer
        private const int _ComputerCounter = 6;

        private Computer[] _Computers;

        public async Task InitializeComputer()
        {
            Random rnd = new Random();

            var timer = Stopwatch.StartNew();

            _Logger.LogInformation("Инициализация Компьютеров...");


            _Computers = Enumerable.Range(1, _ComputerCounter)
                .Select(i => new Computer
                {
                    IpAddress = $"192.168.0.{i}",
                    Office = rnd.Next(1, 5),
                    Parameters = $"Характеристика компа {i + 1}"
                }).ToArray();


            await _db.Computers.AddRangeAsync(_Computers);

            await _db.SaveChangesAsync();

            _Logger.LogInformation("Инициализация Компьютеров выполнена за {0} c", timer.Elapsed.TotalSeconds);
        }
        #endregion

        #region Program
        private const int _ProgramsCounter = 7;

        private ComputerProgram[] _Programs;
        public async Task InitializePrograms()
        {
            Random rnd = new Random();

            var timer = Stopwatch.StartNew();

            _Logger.LogInformation("Инициализация Программ...");

            _Programs = Enumerable.Range(1, _ProgramsCounter)
                .Select(i => new ComputerProgram
                {
                    Name = $"Программа {i}",
                    programType = rnd.NextItem(_ProgramTypes),
                    Version = $"0.0.{i+1}",
                    Volume = rnd.Next(100000)
                }).ToArray();


            await _db.ComputerPrograms.AddRangeAsync(_Programs);

            await _db.SaveChangesAsync();

            _Logger.LogInformation("Инициализация Программ выполнена за {0} c", timer.Elapsed.TotalSeconds);
        }
        #endregion

        #region Setup
        private const int _SetupCounter = 20;
        private Setup[] _Setups;

        public async Task InitializeSetups()
        {
            Random rnd = new Random();


            var timer = Stopwatch.StartNew();

            _Logger.LogInformation("Инициализация установок...");

            _Setups = Enumerable.Range(1, _SetupCounter)
                .Select(i => new Setup 
                {
                    Computer = rnd.NextItem(_Computers),
                    Programm = rnd.NextItem(_Programs),
                    SetupMode = rnd.NextItem(_SetupModes),
                    Date = DateTime.Now,
                }).ToArray();

            await _db.Setups.AddRangeAsync(_Setups);

            await _db.SaveChangesAsync();


            _Logger.LogInformation("Инициализация Установок выполнена за {0} c", timer.Elapsed.TotalSeconds);
        }
        #endregion

        #region User
        const int _UserCount = 5;
        private User[] _Users;

        public async Task InitializeUser()
        {
            Random rnd = new Random();


            var timer = Stopwatch.StartNew();

            _Logger.LogInformation("Инициализация Пользователей...");

            _Users = Enumerable.Range(1, _UserCount)
                .Select(i => new User 
                { 
                    Name = $"Пользователь{1}",
                    Login = $"User{i}",
                    Password = $"admin{i}",
                    Role = rnd.NextItem(_Roles)
                }).ToArray();

            await _db.Users.AddRangeAsync(_Users);
            await _db.SaveChangesAsync();

            _Logger.LogInformation("Инициализация Пользователей выполнена за {0} c", timer.Elapsed.TotalSeconds);
        }
        #endregion
    }
}
