using ComputerCheker.DAL.Context;
using ComputerCheker.DAL.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


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
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);

            await _db.Database.MigrateAsync();

            if (await _db.Computers.AnyAsync()) return;
        }


        #region ProgramType

        private ProgramType[] _ProgramTypes;
        private static readonly string[] _ProgramTypeName = { "текстовый редактор", "операционная система", "СУБД" };

        public async Task InitializeProgramType()
        {
            _ProgramTypes = Enumerable.Range(0, _ProgramTypeName.Length)
                .Select(i => new ProgramType 
                {
                    Name = _ProgramTypeName[i],
                })
                .ToArray();

            await _db.ProgramTypes.AddRangeAsync(_ProgramTypes);

            await _db.SaveChangesAsync();
        }
        #endregion

        #region Role
        private Role[] _Roles { get; set; }

        private static readonly string[] _RoleNames = { "Администратор", "Директор", "Пользователи" };

        public async Task InitializeRole()
        {
            _Roles = Enumerable.Range(0, _RoleNames.Length)
                .Select(i => new Role
                {
                    Name = _RoleNames[i],
                    
                }).ToArray();

            await _db.Roles.AddRangeAsync(_Roles);

            await _db.SaveChangesAsync();
        }
        #endregion

        #region SetupModel
        private SetupMode[] _SetupModes { get; set; }

        private static readonly string[] _SetupModeName = { "сетевой", "локальный", "полный", "демо" };

        public async Task InitializeSetupMode()
        {
            _SetupModes = Enumerable.Range(0, _SetupModeName.Length)
                .Select(i => new SetupMode
                {
                    Name = _SetupModeName[i],
                    
                }).ToArray();
        }
        #endregion

        #region Computer
        private const int _ComputerCounter = 6;

        private Computer[] _Computers;

        public async Task InitializeComputer()
        {
            Random rnd = new Random();

            _Computers = Enumerable.Range(1, _ComputerCounter)
                .Select(i => new Computer
                {
                    IpAddress = $"192.168.0.{i}",
                    Office = rnd.Next(1, 5),
                    Parameters = $"Характеристика компа {i + 1}"
                }).ToArray();


            await _db.Computers.AddRangeAsync(_Computers);

            await _db.SaveChangesAsync();
        }
        #endregion

        #region Program
        private const int _ProgramsCounter = 7;

        private ComputerProgram[] _Programs;
        public async Task InitializePrograms()
        {
            Random rnd = new Random();

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
        }
        #endregion

        #region Setup
        private const int _SetupCounter = 20;
        private Setup[] _Setups;

        public async Task InitializeSetups()
        {
            Random rnd = new Random();

            _Setups = Enumerable.Range(1, _SetupCounter)
                .Select(i => new Setup 
                {
                    Computer = rnd.NextItem(_Computers),
                    Programm = rnd.NextItem(_Programs),
                    SetupMode = rnd.NextItem(_SetupModes),
                    Date = new DateTime(rnd.Next()).Date
                }).ToArray();

            await _db.Setups.AddRangeAsync(_Setups);

            await _db.SaveChangesAsync();
        }
        #endregion

        #region User
        const int _UserCount = 5;
        private User[] _Users;

        public async Task InitializeUser()
        {
            Random rnd = new Random();

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
        }
        #endregion
    }
}
