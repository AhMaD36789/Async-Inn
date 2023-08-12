using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Inn_test
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly AsyncInnDBContext _db;
        protected readonly IAminity _am;
        protected readonly JwtTokenService _JwtTokenService;
        private readonly UserManager<User> _userManager;
        protected readonly IUser _user;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncInnDBContext(new DbContextOptionsBuilder<AsyncInnDBContext>().UseSqlite(_connection).Options);

            _db.Database.EnsureCreated();
            _user = new IdentityUserService(_userManager, _JwtTokenService);
        }

        protected static IUser SetupUserMock(UserDTO expectedResult)
        {
            var userMock = new Mock<IUser>();


            userMock.Setup(u => u.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(expectedResult);

            return userMock.Object;
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection.Dispose();

        }
    }
}