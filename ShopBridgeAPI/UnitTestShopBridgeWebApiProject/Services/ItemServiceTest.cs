
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestShopBridgeWebApiProject.Helpers;
using Moq;
using ShopBridgeAPI.DataAccess;
using ShopBridgeAPI.Services;
using System.Collections.Generic;
using System.Linq;
using ShopBridgeAPI.Models;

namespace UnitTestShopBridgeWebApiProject.Services
{
    [TestClass]
    public class ItemServiceTest
    {
        private Mock<Model1Container> mockModel1Container;
        private Item mockItem;
        private Mock<ItemService> mockItemService;

        public ItemServiceTest()
        {
            this.mockItem = new Fixture().Build<Item>().Create<Item>();
            this.mockModel1Container = new Mock<Model1Container>();
            this.mockItemService = new Mock<ItemService>();
        }

        [TestMethod]
        public void GetAllItems()
        {
            //arrange
            this.mockModel1Container.Setup(prop => prop.Items).Returns(DbSetData<Item>.Start().BuildData(this.mockItem));

            //act
            var itemService = new ItemService(this.mockModel1Container.Object);
            IEnumerable<Item> items = (itemService.GetAllItems() as IEnumerable<Item>).ToList();

            //assert
            Assert.AreNotEqual(0, items.Count());
        }

        [TestMethod]
        public void GetItemById()
        {
            //arrange
            this.mockItem.ItemId = 1;
            this.mockModel1Container.Setup(prop => prop.Items).Returns(DbSetData<Item>.Start().BuildData(this.mockItem));

            //act
            var itemService = new ItemService(this.mockModel1Container.Object);
            var item = (itemService.GetItemById(1) as IQueryable<Item>).FirstOrDefault();

            //assert
            Assert.AreEqual(1, item.ItemId);
        }

        [TestMethod]
        public void UpdateItem()
        {
            //arrange
            this.mockModel1Container.Setup(prop => prop.Items).Returns(DbSetData<Item>.Start().BuildData(this.mockItem));

            ItemModel itemModel = new ItemModel();
            itemModel.Name = this.mockItem.Name;

            //act
            var itemService = new ItemService(this.mockModel1Container.Object);
            var resultCount = itemService.UpdateItem(1, itemModel);

            //assert
            Assert.AreEqual(1, resultCount);
        }

        [TestMethod]
        public void Delete()
        {
            //arrange
            this.mockItem.ItemId = 1;
            this.mockModel1Container.Setup(prop => prop.Items).Returns(DbSetData<Item>.Start().BuildData(this.mockItem));

            //act
            var itemService = new ItemService(this.mockModel1Container.Object);
            var resultCount = itemService.DeleteItem(1);

            //assert
            Assert.AreEqual(1, resultCount);
        }

    }
}
