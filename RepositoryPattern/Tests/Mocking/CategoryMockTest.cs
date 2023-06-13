// <copyright file="CategoryMockTest.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Tests.Mocking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AuctionProject.DataMapper;
    using AuctionProject.Enum;
    using AuctionProject.Models;
    using AuctionProject.Repository;
    using AuctionProject.Service;
    using NUnit.Framework;
    using Telerik.JustMock.EntityFramework;

    /// <summary>
    /// Test category using mock.
    /// </summary>
    public class CategoryMockTest
    {
        /// <summary>
        /// The library database mock <see cref="MyContext"/>.
        /// </summary>
        private MyContext myContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="categoryService"/>.
        /// </summary>
        private CategoryService categoryService;

        /// <summary>
        /// The category.
        /// </summary>
        private Category category;

        /// <summary>
        /// Category's children.
        /// </summary>
        private List<Category> children;

        /// <summary>
        /// Category's parents.
        /// </summary>
        private List<Category> parents;

        /// <summary>
        /// Sets up for tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.myContext = EntityFrameworkMock.Create<MyContext>();
            this.categoryService = new CategoryService(new CategoryRepository(this.myContext));

            Category parent1 = new Category()
            {
                Name = "Haine",
            };

            Category parent2 = new Category()
            {
                Name = "Haine pt femei",
            };

            this.parents = new List<Category>() { parent1, parent2 };

            Category child1 = new Category()
            {
                Name = "Fuste",
            };

            Category child2 = new Category()
            {
                Name = "Bluze",
            };

            this.children = new List<Category>() { child1, child2 };

            this.category = new Category()
            {
                Id = 1,
                Name = "Haine de vara",
            };
        }

        /// <summary>
        /// Add a valid category in database.
        /// </summary>
        [Test]
        public void TestAddCategory()
        {
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsTrue(result);
            Assert.True(this.myContext.Categories.Count() == 1);
        }

        /// <summary>
        /// Add a invalid category in database.
        /// </summary>
        [Test]
        public void TestAddCategoryInvalidNameNull()
        {
            this.category.Name = null;
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Categories.Count() == 0);
        }

        /// <summary>
        /// Add a invalid category in database.
        /// </summary>
        [Test]
        public void TestAddCategoryInvalidNameEmpty()
        {
            this.category.Name = string.Empty;
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Categories.Count() == 0);
        }

        /// <summary>
        /// Add a invalid category in database.
        /// </summary>
        [Test]
        public void TestAddCategoryInvalidShortName()
        {
            this.category.Name = "H";
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Categories.Count() == 0);
        }

        /// <summary>
        /// Add a invalid category in database.
        /// </summary>
        [Test]
        public void TestAddCategoryInvalidLongName()
        {
            this.category.Name = new string('a', 101);
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Categories.Count() == 0);
        }

        /// <summary>
        /// Add a invalid category in database.
        /// </summary>
        [Test]
        public void TestAddCategoryInvalidNameWithDigit()
        {
            this.category.Name = "H324";
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Categories.Count() == 0);
        }

        /// <summary>
        /// Add a invalid category in database.
        /// </summary>
        [Test]
        public void TestAddCategoryInvalidNameWithSymbols()
        {
            this.category.Name = "@#$";
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Categories.Count() == 0);
        }

        /// <summary>
        /// Add a invalid category in database.
        /// </summary>
        [Test]
        public void TestAddCategoryInvalidNameWithSpace()
        {
            this.category.Name = "Masini de epoca";
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsTrue(result);
            Assert.True(this.myContext.Categories.Count() == 1);
        }

        /// <summary>
        /// Add a invalid category in database.
        /// </summary>
        [Test]
        public void TestAddCategoryNull()
        {
            bool result = this.categoryService.AddCategory(null);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Categories.Count() == 0);
        }

        /// <summary>
        /// Add a valid category with children in database.
        /// </summary>
        [Test]
        public void TestAddCategoryWithChildren()
        {
            this.category.CategoryChildren = this.children;
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsTrue(result);
            Assert.True(this.myContext.Categories.Count() == 3);
        }

        /// <summary>
        /// Add a valid category with children in database.
        /// </summary>
        [Test]
        public void TestAddCategoryWithParent()
        {
            this.category.CategoryParents = this.parents;
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsTrue(result);
            Assert.True(this.myContext.Categories.Count() == 3);
        }

        /// <summary>
        /// Add a valid category with children and parents in database.
        /// </summary>
        [Test]
        public void TestAddCategoryWithParentAndChildren()
        {
            this.category.CategoryChildren = this.children;
            this.category.CategoryParents = this.parents;
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsTrue(result);
            Assert.True(this.myContext.Categories.Count() == 5);
        }

        /// <summary>
        /// Add a valid category with children and invalid parents in database.
        /// </summary>
        [Test]
        public void TestAddCategoryWithInvalidParentAndChildren()
        {
            this.parents[0].Name = null;
            this.category.CategoryChildren = this.children;
            this.category.CategoryParents = this.parents;
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Categories.Count() == 0);
        }

        /// <summary>
        /// Add a valid category with invalid children and parents in database.
        /// </summary>
        [Test]
        public void TestAddCategoryWithParentAndInvalidChildren()
        {
            this.children[1].Name = null;
            this.category.CategoryChildren = this.children;
            this.category.CategoryParents = this.parents;
            bool result = this.categoryService.AddCategory(this.category);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Categories.Count() == 0);
        }

        /// <summary>
        /// Get category.
        /// </summary>
        [Test]
        public void TestGetCategory()
        {
            bool result = this.categoryService.AddCategory(this.category);
            Category categoryDB = this.categoryService.GetCategory(this.category.Id);
            Assert.IsTrue(result);
            Assert.NotNull(categoryDB);
        }

        /// <summary>
        /// Get category with non-existent id.
        /// </summary>
        [Test]
        public void TestGetCategoryNonExistentId()
        {
            Category categoryDB = this.categoryService.GetCategory(1);
            Assert.Null(categoryDB);
        }

        /// <summary>
        /// Get category with negative id.
        /// </summary>
        [Test]
        public void TestGetCategoryNegativeId()
        {
            Category categoryDB = this.categoryService.GetCategory(-1);
            Assert.Null(categoryDB);
        }

        /// <summary>
        /// Test update category's name.
        /// </summary>
        [Test]
        public void TestUpdateCategoryNameWithSpace()
        {
            this.categoryService.AddCategory(this.category);

            this.category.Name = "Update Name";
            bool result = this.categoryService.Update(this.category);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test update category's name invalid with digit.
        /// </summary>
        [Test]
        public void TestUpdateCategoryNameWithDigit()
        {
            this.categoryService.AddCategory(this.category);

            this.category.Name = "Update Name85";
            bool result = this.categoryService.Update(this.category);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test update category's name invalid with symbols.
        /// </summary>
        [Test]
        public void TestUpdateCategoryNameWithSymbols()
        {
            this.categoryService.AddCategory(this.category);

            this.category.Name = "Update#Name";
            bool result = this.categoryService.Update(this.category);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test update category's name invalid with dash.
        /// </summary>
        [Test]
        public void TestUpdateCategoryNameWithDash()
        {
            this.categoryService.AddCategory(this.category);

            this.category.Name = "Update-Name";
            bool result = this.categoryService.Update(this.category);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test update category's name null.
        /// </summary>
        [Test]
        public void TestUpdateCategoryNameNull()
        {
            this.categoryService.AddCategory(this.category);

            this.category.Name = null;
            bool result = this.categoryService.Update(this.category);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test update category's name empty.
        /// </summary>
        [Test]
        public void TestUpdateCategoryNameEmpty()
        {
            this.categoryService.AddCategory(this.category);

            this.category.Name = string.Empty;
            bool result = this.categoryService.Update(this.category);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test update category's parent.
        /// </summary>
        [Test]
        public void TestUpdateCategoryParent()
        {
            this.category.CategoryParents = this.parents;
            this.categoryService.AddCategory(this.category);

            Category newParent = new Category()
            {
                Name = "New Parent",
            };

            this.category.CategoryParents.Add(newParent);

            bool result = this.categoryService.Update(this.category);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test update category's invalid parent.
        /// </summary>
        [Test]
        public void TestUpdateCategoryInvalidParent()
        {
            this.category.CategoryParents = this.parents;
            this.categoryService.AddCategory(this.category);

            Category newParent = new Category()
            {
                Name = "Ne234",
            };

            this.category.CategoryParents.Add(newParent);

            bool result = this.categoryService.Update(this.category);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test add category with products.
        /// </summary>
        [Test]
        public void TestAddCategoryWithProducts()
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
                Description = "Bluza marca C&A, Marimea L",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 7, 1),
            };

            Category category = new Category()
            {
                Name = "Bluze",
                Products = new List<Product>() { p1, p2 },
            };

            this.categoryService.AddCategory(category);

            Assert.IsTrue(this.myContext.Categories.Count() == 1);
        }
    }
}