// <copyright file="CategoryTest.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Tests.Validation
{
    using System;
    using System.Collections.Generic;
    using AuctionProject.Enum;
    using AuctionProject.Models;
    using AuctionProject.Models.Validator;
    using AuctionProject.Service;
    using NUnit.Framework;

    /// <summary>
    /// Class with test for Category.
    /// </summary>
    internal class CategoryTest
    {
        /// <summary>
        /// check if the category is valid.
        /// </summary>
        [Test]
        public void TestCategoryValid()
        {
            Category category = new Category
            {
                Name = "Haine",
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// check if the category is valid.
        /// </summary>
        [Test]
        public void TestInvalidCategoryShortName()
        {
            Category category = new Category
            {
                Name = "H",
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// check if the category is valid.
        /// </summary>
        [Test]
        public void TestInvalidCategoryLongName()
        {
            Category category = new Category
            {
                Name = new string('a', 101),
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// check if the category is valid.
        /// </summary>
        [Test]
        public void TestInvalidCategoryWithoutName()
        {
            Category category = new Category { };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// check if the category is valid.
        /// </summary>
        [Test]
        public void TestInvalidCategoryNameWithDigit()
        {
            Category category = new Category()
            {
                Name = "Haine5",
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// check if the category is valid.
        /// </summary>
        [Test]
        public void TestInvalidCategoryNameWithSpace()
        {
            Category category = new Category()
            {
                Name = "Haine de vara",
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// check if the category is valid.
        /// </summary>
        [Test]
        public void TestInvalidCategoryNameWithSymbols()
        {
            Category category = new Category()
            {
                Name = "Haine @#$ vara",
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// check if the category is valid.
        /// </summary>
        [Test]
        public void TestNullCategory()
        {
            Category category = null;

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check a category with valid parent.
        /// </summary>
        [Test]
        public void TestValidCategoryWithParent()
        {
            Category categoryParent = new Category
            {
                Name = "Haine",
            };

            List<Category> categoryParents = new List<Category>()
            {
               categoryParent,
            };

            Category category = new Category
            {
                Name = "Pantaloni",
                CategoryParents = categoryParents,
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Check a category with invalid parent.
        /// </summary>
        [Test]
        public void TestValidCategoryWithInvalidParent()
        {
            Category categoryParent = new Category
            {
                Name = "H",
            };

            List<Category> categoryParents = new List<Category>
            {
                categoryParent,
            };

            Category category = new Category
            {
                Name = "Pantaloni",
                CategoryParents = categoryParents,
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check a category with child.
        /// </summary>
        [Test]
        public void TestValidCategoryWithChild()
        {
            Category categoryChild = new Category
            {
                Name = "Pantaloni",
            };

            List<Category> categoryChildren = new List<Category>
            {
                categoryChild,
            };

            Category category = new Category
            {
                Name = "Haine",
                CategoryChildren = categoryChildren,
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Check a category with invalid child.
        /// </summary>
        [Test]
        public void TestValidCategoryWithInvalidChild()
        {
            Category categoryChild = new Category
            {
                Name = "P",
            };

            List<Category> categoryChildren = new List<Category>
            {
                categoryChild,
            };

            Category category = new Category
            {
                Name = "Haine",
                CategoryChildren = categoryChildren,
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check category with children and parent.
        /// </summary>
        [Test]
        public void TestValidCategoryWithParentAndChild()
        {
            Category categoryParent = new Category
            {
                Name = "Haine",
            };

            List<Category> categoryParents = new List<Category>
            {
                categoryParent,
            };

            Category categoryChild1 = new Category
            {
                Name = "Bluze",
            };

            Category categoryChild2 = new Category
            {
                Name = "Fuste",
            };

            List<Category> categoryChildern = new List<Category>
            {
                categoryChild1,
                categoryChild2,
            };

            Category category = new Category
            {
                Name = "Produse femei",
                CategoryParents = categoryParents,
                CategoryChildren = categoryChildern,
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Check category with children and  invalid parent in database.
        /// </summary>
        [Test]
        public void TestValidCategoryWithInvalidParentAndChild()
        {
            Category categoryParent = new Category
            {
                Name = "H",
            };

            List<Category> categoryParents = new List<Category>
            {
                categoryParent,
            };

            Category categoryChild1 = new Category
            {
                Name = "Bluze",
            };

            Category categoryChild2 = new Category
            {
                Name = "Fuste",
            };

            List<Category> categoryChildern = new List<Category>
            {
                categoryChild1,
                categoryChild2,
            };

            Category category = new Category
            {
                Name = "Produse femei",
                CategoryParents = categoryParents,
                CategoryChildren = categoryChildern,
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check category with parent and invalid children.
        /// </summary>
        [Test]
        public void TestValidCategoryWithParentAndInvalideChild()
        {
            Category categoryParent = new Category
            {
                Name = "Haine",
            };

            List<Category> categoryParents = new List<Category>
            {
                categoryParent,
            };

            Category categoryChild1 = new Category
            {
                Name = "B",
            };

            Category categoryChild2 = new Category
            {
                Name = "Fuste",
            };

            List<Category> categoryChildern = new List<Category>
            {
                categoryChild1,
                categoryChild2,
            };

            Category category = new Category
            {
                Name = "Produse femei",
                CategoryParents = categoryParents,
                CategoryChildren = categoryChildern,
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Test category with products.
        /// </summary>
        [Test]
        public void TestCategoryWithProducts()
        {
            User owner = new User
            {
                Name = "Valentina",
                Role = Role.Offerer,
            };

            Product p1 = new Product()
            {
                Name = "Bluza1",
                Owner = owner,
                Price = 10,
                Coins = Coins.Euro,
                Description = "Bluza marca Zara, Marimea M",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 7, 1),
            };

            Product p2 = new Product()
            {
                Name = "Bluza2",
                Owner = owner,
                Price = 10,
                Coins = Coins.Euro,
                Description = "Bluza marca Zara, Marimea M",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 7, 1),
            };

            Category category = new Category()
            {
                Name = "Bluze",
                Products = new List<Product>() { p1, p2 },
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Test category with invalid products.
        /// </summary>
        [Test]
        public void TestCategoryWithInvalidProducts()
        {
            User owner = new User
            {
                Name = "Valentina",
            };

            Product p1 = new Product()
            {
                Name = "Bluza1",
                Owner = owner,
                Price = 10,
                Coins = Coins.Euro,
                Description = "Bluza marca Zara, Marimea M",
                EndDateAction = new DateTime(2023, 7, 1),
                StartDateAction = new DateTime(2023, 7, 25),
            };

            Product p2 = new Product()
            {
                Name = "Bluza2",
                Owner = owner,
                Price = 10,
                Coins = Coins.Euro,
                Description = "Bluza marca Zara, Marimea M",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 7, 1),
            };

            Category category = new Category()
            {
                Name = "Bluze",
                Products = new List<Product>() { p1, p2 },
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            bool isValid = categoryValidator.Validate(category);

            Assert.IsFalse(isValid);
        }
    }
}
