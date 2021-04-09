using ShopBridgeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using ShopBridgeAPI.DataAccess;
using ShopBridgeAPI.Services;
using System.Net.Http;

namespace ShopBridgeAPI.Controller
{
    //obul subbranch 2
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ItemController : ApiController
    {
        Model1Container _repository;
        ItemService itemService = new ItemService();

        public ItemController() { }
        public ItemController(Model1Container _model1Container, ItemService _itemService)
        {
            _repository = _model1Container;
            itemService = _itemService;
        }

        public IHttpActionResult Get()
        {
            var items = itemService.GetAllItems();
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        public IHttpActionResult Get(int? id)
        {
            var item = itemService.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        public IHttpActionResult Post(ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {

                var result = itemService.AddNewItem(itemModel);
                if (result == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }

            return Ok(itemModel);
        }

        public IHttpActionResult Put(int id, ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {

                var result = itemService.UpdateItem(id, itemModel);
                if (result == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }

            return Ok(itemModel);
        }

        public IHttpActionResult Delete(int id)
        {
            var result = 0;
            if (ModelState.IsValid)
            {

                result = itemService.DeleteItem(id);
                if (result == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }

            return Ok(result);
        }


        //public IEnumerable<Item> Get()
        //{
        //    //https://docs.microsoft.com/en-us/aspnet/web-api/overview/testing-and-debugging/unit-testing-controllers-in-web-api

        //    try
        //    {
        //        var context = new Model1Container();

        //        return context.Items;
        //    }
        //    catch (Exception ex)
        //    {
        //        //track exception
        //        return Enumerable.Empty<Item>();
        //    }
        //}



        //public IEnumerable<Item> Get(int? id)
        //{
        //    try
        //    {
        //        var context = new Model1Container();
        //        if (id.HasValue)
        //        {
        //            var item = context.Items.Where(a => a.ItemId == id);

        //            return item != null ? item : Enumerable.Empty<Item>();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //track exception
        //        return Enumerable.Empty<Item>();
        //    }

        //    return Enumerable.Empty<Item>();
        //}

        //public IHttpActionResult Post(ItemModel itemModel)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            using (var context = new Model1Container())
        //            {
        //                Item item = new Item();
        //                item.Name = itemModel.Name;
        //                item.Description = itemModel.Description;
        //                item.Price = itemModel.Price;
        //                item.IsActive = true;
        //                item.ItemImageBase64 = itemModel.ItemImageBase64;

        //                context.Items.Add(item);

        //                context.SaveChanges();
        //            }

        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        //track exception
        //    }

        //    return Ok();
        //}


        //public IHttpActionResult Put(int id, Item item)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("Not a valid model");

        //    using (var context = new Model1Container())
        //    {
        //        var _item = context.Items.FirstOrDefault(a => a.ItemId == id);

        //        if (_item != null)
        //        {
        //            _item.Name = item.Name;
        //            _item.Description = item.Description;
        //            _item.Price = item.Price;
        //            _item.ItemImageBase64 = item.ItemImageBase64;

        //            context.SaveChanges();
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }

        //    return Ok();
        //}

        //public IHttpActionResult Delete(int id)
        //{
        //    try
        //    {
        //        using (var context = new Model1Container())
        //        {
        //            var item = context.Items.FirstOrDefault(a => a.ItemId == id);

        //            if (item != null)
        //            {
        //                context.Items.Remove(item);
        //                context.SaveChanges();
        //            }
        //            else
        //            {
        //                return NotFound();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //track exception
        //    }

        //    return Ok();
        //}
    }
}
