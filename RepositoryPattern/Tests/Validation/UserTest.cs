// <copyright file="UserTest.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Tests.Validation
{
    using System.Collections.Generic;
    using System.Configuration;
    using AuctionProject.Enum;
    using AuctionProject.Helper;
    using AuctionProject.Models;
    using AuctionProject.Models.Validator;
    using NUnit.Framework;

    /// <summary>
    ///  Class with test for User.
    /// </summary>
    public class UserTest
    {
        /// <summary>
        /// Check a valid user.
        /// </summary>
        [Test]
        public void TestUserValid()
        {
            User user = new User
            {
                Name = "Elena",
                Role = Enum.Role.Offerer,
                Score = 5,
            };

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Check a user without name.
        /// </summary>
        [Test]
        public void TestUserInvalidWithoutName()
        {
            User user = new User();

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check a user with score default.
        /// </summary>
        [Test]
        public void TestUserValidWitScoreDefault()
        {
            User user = new User
            {
                Name = "Ana",
            };

            UserValidator userValidator = new UserValidator();
            bool isAdd = userValidator.Validate(user);

            Helper helper = new Helper();

            Assert.IsTrue(isAdd);
            Assert.AreEqual(user.Score, helper.Score);
        }

        /// <summary>
        /// Check a user with negative score.
        /// </summary>
        [Test]
        public void TestUserInvalidNegativeScore()
        {
            User user = new User
            {
                Name = "Ana",
                Score = -1,
            };

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check a user with name too long.
        /// </summary>
        [Test]
        public void TestUserInvalidLongName()
        {
            User user = new User
            {
                Name = new string('a', 101),
                Score = 9,
            };

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check a user with name too short.
        /// </summary>
        [Test]
        public void TestUserInvalidShortName()
        {
            User user = new User
            {
                Name = "A",
                Score = 9,
            };

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check a user with name with digit.
        /// </summary>
        [Test]
        public void TestUserInvalidNameWithDigit()
        {
            User user = new User
            {
                Name = "Ana4",
                Score = 9,
            };

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check a user with name with symbol.
        /// </summary>
        [Test]
        public void TestUserInvalidNameWithSymbol()
        {
            User user = new User
            {
                Name = "Ana@",
                Score = 9,
            };

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check a user with name with space.
        /// </summary>
        [Test]
        public void TestUserInvalidNameWithSpace()
        {
            User user = new User
            {
                Name = "Ana Maria",
                Score = 9,
            };

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Check a user with name with dash.
        /// </summary>
        [Test]
        public void TestUserInvalidNameWithDash()
        {
            User user = new User
            {
                Name = "Ana-Maria",
                Score = 9,
            };

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Check a user with name with lowercase.
        /// </summary>
        [Test]
        public void TestUserInvalidNameWithLowercase()
        {
            User user = new User
            {
                Name = "ana",
                Score = 9,
            };

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check a user with name with lowercase last name.
        /// </summary>
        [Test]
        public void TestUserInvalidNameWithLowercaseLastName()
        {
            User user = new User
            {
                Name = "Ana maria",
                Score = 9,
            };

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check a user null.
        /// </summary>
        [Test]
        public void TestInvalidUserNull()
        {
            User user = null;

            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if score is set default.
        /// </summary>
        [Test]
        public void TestScoreDefault()
        {
            User user = new User
            {
                Name = "Ana",
            };

            Helper helper = new Helper();

            Assert.IsTrue(user.Score == helper.Score);
        }

        /// <summary>
        /// Test get and set user's attributes.
        /// </summary>
        [Test]
        public void TestUserAtribute()
        {
            User user = new User()
            {
                Id = 1,
                Name = "Elena",
                Products = new List<Product>(),
                Auctions = new List<Auction>(),
                Role = Role.Bidder,
                Score = 7,
            };

            Assert.IsTrue(user.Id == 1);
            Assert.IsTrue(user.Name == "Elena");
            Assert.IsTrue(user.Products.Count == 0);
            Assert.IsTrue(user.Auctions.Count == 0);
            Assert.IsTrue(user.Role == Role.Bidder);
            Assert.IsTrue(user.Score == 7);
        }
    }
}
