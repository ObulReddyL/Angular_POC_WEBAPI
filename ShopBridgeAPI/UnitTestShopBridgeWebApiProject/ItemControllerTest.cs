using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridgeAPI.Controller;
using System.Collections.Generic;
using ShopBridgeAPI.DataAccess;
using System.Linq;
using Moq;
using AutoFixture;
using System.Web.Http.Results;
using UnitTestShopBridgeWebApiProject.Helpers;
using ShopBridgeAPI.Models;
using ShopBridgeAPI.Services;

namespace UnitTestShopBridgeWebApiProject
{
    [TestClass]
    public class ItemControllerTest
    {
        private Mock<Model1Container> mockModel1Container ;
        private Item mockItem ;
        private Mock<ItemService> mockItemService;
        public ItemControllerTest()
        {
            this.mockItem = new Fixture().Build<Item>().Create<Item>();
            this.mockModel1Container = new Mock<Model1Container>();
            this.mockItemService = new Mock<ItemService>();
        }

        [TestMethod]
        public void Get_ShouldReturnItemLists()
        {
            //arrange
            this.mockModel1Container.Setup(prop => prop.Items).Returns(DbSetData<Item>.Start().BuildData(this.mockItem));

            //act
            var controller = new ItemController(this.mockModel1Container.Object, mockItemService.Object);
            IEnumerable<Item> items = (controller.Get() as IEnumerable<Item>);

            //assert
            Assert.AreNotEqual(0, items.Count());
        }

        [TestMethod]
        public void Get_ShouldReturnItem()
        {
            //arrange
            this.mockItem.ItemId = 1;
            this.mockModel1Container.Setup(prop => prop.Items).Returns(DbSetData<Item>.Start().BuildData(this.mockItem));

            //act
            var controller = new ItemController(this.mockModel1Container.Object, mockItemService.Object);
            var actionResult = controller.Get(1);

            //assert
            var response = actionResult as NegotiatedContentResult<Item>;
            Assert.IsNotNull(response);
            var item = response.Content;
            Assert.AreEqual(1, item.ItemId);
        }

        [TestMethod]
        public void Post_ReturnInsertedRecords()
        {
            //arrange
            this.mockModel1Container.Setup(prop => prop.Items).Returns(DbSetData<Item>.Start().BuildData(this.mockItem));

            ItemModel itemModel = new ItemModel();
            itemModel.Name = this.mockItem.Name;

            //act
            var controller = new ItemController(this.mockModel1Container.Object, mockItemService.Object);
            var actionResult = controller.Post(itemModel);
            var response = actionResult as NegotiatedContentResult<Item>;

            //assert
           Assert.AreEqual(this.mockItem.Name, response.Content.Name);
        }


        [TestMethod]
        public void Patch_ReturnUpdatedRecord()
        {
            //arrange
            this.mockItem.ItemId = 1;
            this.mockModel1Container.Setup(prop => prop.Items).Returns(DbSetData<Item>.Start().BuildData(this.mockItem));

            ItemModel itemModel = new ItemModel();
            itemModel.Name = this.mockItem.Name;
            //act
            var controller = new ItemController(this.mockModel1Container.Object, mockItemService.Object);
            var actionResult = controller.Put(1, itemModel);
            var response = actionResult as NegotiatedContentResult<Item>;
            //assert
            Assert.AreEqual(this.mockItem.Name, response.Content.Name);
        }

        [TestMethod]
        public void Delete()
        {
            //arrange
            this.mockItem.ItemId = 1;
            this.mockModel1Container.Setup(prop => prop.Items).Returns(DbSetData<Item>.Start().BuildData(this.mockItem));

            //act
            var controller = new ItemController(this.mockModel1Container.Object, mockItemService.Object);
            var actionResult = controller.Delete(1);
            var response = actionResult as NegotiatedContentResult<int>;
            //assert
            Assert.AreEqual(1, response.Content);
        }
    }
}
