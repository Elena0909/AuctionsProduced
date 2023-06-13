// <copyright file="UserMockTest.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Tests.Mocking
{
    using System.Linq;
    using AuctionProject.DataMapper;
    using AuctionProject.Enum;
    using AuctionProject.Models;
    using AuctionProject.Repository;
    using AuctionProject.Service;
    using NUnit.Framework;
    using Telerik.JustMock.EntityFramework;

    /// <summary>
    /// Test user using mock.
    /// </summary>
    public class UserMockTest
    {
        /// <summary>
        /// The library database mock <see cref="MyContext"/>.
        /// </summary>
        private MyContext myContext;

        /// <summary>
        /// User service <see cref="UserService"/>.
        /// </summary>
        private UserService userService;

        /// <summary>
        /// The user.
        /// </summary>
        private User user;

        /// <summary>
        /// Sets up for tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.myContext = EntityFrameworkMock.Create<MyContext>();
            this.userService = new UserService(new UserRespository(this.myContext));
            this.user = new User()
            {
                Name = "Elena",
                Role = Role.Offerer,
            };
        }

        /// <summary>
        /// Test the add user.
        /// </summary>
        [Test]
        public void TestAddUser()
        {
            bool result = this.userService.AddUser(this.user);
            Assert.True(result);
            Assert.True(this.myContext.Users.Count() == 1);
        }

        /// <summary>
        /// Tests the add null user.
        /// </summary>
        [Test]
        public void TestAddNullUser()
        {
            bool result = this.userService.AddUser(null);
            Assert.False(result);
            Assert.True(this.myContext.Users.Count() == 0);
        }

        /// <summary>
        /// Tests the add null name user.
        /// </summary>
        [Test]
        public void TestAddNullNameUser()
        {
            this.user.Name = null;
            bool result = this.userService.AddUser(this.user);
            Assert.False(result);
            Assert.True(this.myContext.Users.Count() == 0);
        }

        /// <summary>
        /// Tests the add user with negative score.
        /// </summary>
        [Test]
        public void TestAddNegativScoreUser()
        {
            this.user.Score = -1;
            Assert.False(this.userService.AddUser(this.user));
            Assert.True(this.myContext.Users.Count() == 0);
        }

        /// <summary>
        /// Tests the add empty name user.
        /// </summary>
        [Test]
        public void TestAddEmptyNameUser()
        {
            this.user.Name = string.Empty;
            bool result = this.userService.AddUser(this.user);
            Assert.False(result);
            Assert.True(this.myContext.Users.Count() == 0);
        }

        /// <summary>
        /// Tests the add smaller name user.
        /// </summary>
        [Test]
        public void TestAddSmallerNameUser()
        {
            this.user.Name = "a";
            bool result = this.userService.AddUser(this.user);

            Assert.False(result);
            Assert.True(this.myContext.Users.Count() == 0);
        }

        /// <summary>
        /// Tests the add longer name user.
        /// </summary>
        [Test]
        public void TestAddLongerNameUser()
        {
            this.user.Name = new string('a', 101);
            bool result = this.userService.AddUser(this.user);

            Assert.False(result);
            Assert.True(this.myContext.Users.Count() == 0);
        }

        /// <summary>
        /// Add a user with name with digit.
        /// </summary>
        [Test]
        public void TestAddUserInvalidNameWithDigit()
        {
            this.user.Name = "Ana4";

            this.userService.AddUser(this.user);

            Assert.True(this.myContext.Users.Count() == 0);
        }

        /// <summary>
        /// Add a user with name with symbol.
        /// </summary>
        [Test]
        public void TestAddUserInvalidNameWithSymbol()
        {
            this.user.Name = "Ana#";

            this.userService.AddUser(this.user);

            Assert.True(this.myContext.Users.Count() == 0);
        }

        /// <summary>
        /// Add a user with name with space.
        /// </summary>
        [Test]
        public void TestAddUserInvalidNameWithSpace()
        {
            this.user.Name = "Ana Maria";

            this.userService.AddUser(this.user);

            Assert.True(this.myContext.Users.Count() == 1);
        }

        /// <summary>
        /// Add a user with name with dash.
        /// </summary>
        [Test]
        public void TestAddUserInvalidNameWithDash()
        {
            this.user.Name = "Ana-Maria";

            this.userService.AddUser(this.user);

            Assert.True(this.myContext.Users.Count() == 1);
        }

        /// <summary>
        /// Add a user with name with lowercase.
        /// </summary>
        [Test]
        public void TestAddUserInvalidNameWithLowercase()
        {
            this.user.Name = "maria";
            this.userService.AddUser(this.user);

            Assert.True(this.myContext.Users.Count() == 0);
        }

        /// <summary>
        /// Add a user with name with lowercase last name.
        /// </summary>
        [Test]
        public void TestAddUserInvalidNameWithLowercaseLastName()
        {
            this.user.Name = "Ana maria";

            this.userService.AddUser(this.user);

            Assert.True(this.myContext.Users.Count() == 0);
        }

        /// <summary>
        /// Tests to get user.
        /// </summary>
        [Test]
        public void TestGetUser()
        {
            User user = this.user;
            bool result = this.userService.AddUser(user);
            user = this.userService.GetUser(user.Id);

            Assert.True(result);
            Assert.NotNull(user);
        }

        /// <summary>
        /// Tests to get negative id value for user.
        /// </summary>
        [Test]
        public void TestGetUserWithNegativeIdValue()
        {
            User user = this.userService.GetUser(-1);
            Assert.Null(user);
        }

        /// <summary>
        /// Tests to get zero id value user.
        /// </summary>
        [Test]
        public void TestGetUserZeroValueId()
        {
            User user = this.userService.GetUser(0);
            Assert.Null(user);
        }

        /// <summary>
        /// Tests to get non-existent id value user.
        /// </summary>
        [Test]
        public void TestGetUserNonExistentId()
        {
            User user = this.userService.GetUser(2);
            Assert.Null(user);
        }

        /// <summary>
        /// Update user's name.
        /// </summary>
        [Test]
        public void TestUpdateNameUser()
        {
            this.userService.AddUser(this.user);
            this.user.Name = "Alexandra";
            this.userService.UpdateUser(this.user);

            User updateUser = this.userService.GetUser(this.user.Id);

            Assert.AreEqual("Alexandra", updateUser.Name);
        }

        /// <summary>
        /// Update user's name.
        /// </summary>
        [Test]
        public void TestUpdateUserDontExist()
        {
            this.user.Name = "Alexandra";
            bool resul = this.userService.UpdateUser(this.user);

            Assert.IsFalse(resul);
        }

        /// <summary>
        /// /// Update user's score.
        /// </summary>
        [Test]
        public void TestUpdateScoreUser()
        {
            this.userService.AddUser(this.user);
            User user = this.user;
            user.Score = 10;

            this.userService.UpdateUser(user);

            User updateUser = this.userService.GetUser(this.user.Id);

            Assert.AreEqual(10, updateUser.Score);
        }

        /// <summary>
        /// Update null user.
        /// </summary>
        [Test]
        public void TestUpdateNullUser()
        {
            bool result = this.userService.UpdateUser(null);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Update user with null name.
        /// </summary>
        [Test]
        public void TestUpdateNullNameUser()
        {
            this.userService.AddUser(this.user);
            this.user.Name = null;
            bool result = this.userService.UpdateUser(this.user);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Update user with empty name.
        /// </summary>
        [Test]
        public void TestUpdateEmptyNameUser()
        {
            this.userService.AddUser(this.user);

            this.user.Name = string.Empty;
            bool result = this.userService.UpdateUser(this.user);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Update user with smaller name.
        /// </summary>
        [Test]
        public void TestUpdateSmallerNameUser()
        {
            this.userService.AddUser(this.user);
            this.user.Name = "a";
            bool result = this.userService.UpdateUser(this.user);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Update user with longer name.
        /// </summary>
        [Test]
        public void TestUpdateLongerNameUser()
        {
            this.userService.AddUser(this.user);

            this.user.Name = new string('a', 101);
            bool result = this.userService.UpdateUser(this.user);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Update user with negative score.
        /// </summary>
        [Test]
        public void TestUpdateNegativScoreUser()
        {
            this.userService.AddUser(this.user);

            this.user.Score = -5;
            Assert.IsFalse(this.userService.UpdateUser(this.user));
        }

        /// <summary>
        /// Update user with name with digit.
        /// </summary>
        [Test]
        public void TestUpdatteUserInvalidNameWithDigit()
        {
            this.userService.AddUser(this.user);

            this.user.Name = "Ana4";
            bool result = this.userService.UpdateUser(this.user);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Update user with name with symbol.
        /// </summary>
        [Test]
        public void TestUpdateUserInvalidNameWithSymbol()
        {
            this.userService.AddUser(this.user);

            this.user.Name = "Ana#";
            bool result = this.userService.UpdateUser(this.user);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Update user with name with space.
        /// </summary>
        [Test]
        public void TestUpdateUserInvalidNameWithSpace()
        {
            this.userService.AddUser(this.user);

            this.user.Name = "Ana Maria";
            bool result = this.userService.UpdateUser(this.user);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Update user with name with dash.
        /// </summary>
        [Test]
        public void TestUpdateUserInvalidNameWithDash()
        {
            this.userService.AddUser(this.user);

            this.user.Name = "Ana-Maria";
            bool result = this.userService.AddUser(this.user);

            Assert.True(result);
        }

        /// <summary>
        /// Update a user with name with lowercase.
        /// </summary>
        [Test]
        public void TestUpdateUserInvalidNameWithLowercase()
        {
            this.userService.AddUser(this.user);

            this.user.Name = "maria";
            bool result = this.userService.AddUser(this.user);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Update user with name with lowercase last name.
        /// </summary>
        [Test]
        public void TestUpdateUserInvalidNameWithLowercaseLastName()
        {
            this.userService.AddUser(this.user);

            this.user.Name = "Ana maria";
            bool result = this.userService.AddUser(this.user);

            Assert.IsFalse(result);
        }
    }
}
