using ShopBridgeAPI.DataAccess;
using ShopBridgeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopBridgeAPI.Services
{
    public class ItemService
    {
        Model1Container context ;
        public ItemService() {
            context = new Model1Container();
        }
        public ItemService(Model1Container _Model1Container)
        {
            context = _Model1Container;
        }

        public IQueryable<Item> GetAllItems()
        {
            try
            {
                return context.Items;
            }
            catch (Exception ex)
            {
                //track exception
                return Enumerable.Empty<Item>().AsQueryable();
            }
        }

        public IQueryable<Item> GetItemById(int? id)
        {
            var result = Enumerable.Empty<Item>().AsQueryable();
            try
            {
                if (id.HasValue)
                {
                    result = context.Items.Where(a => a.ItemId == id);
                }
            }
            catch (Exception ex)
            {
                //track exception
                return result;
            }

            return result;
        }

        public int AddNewItem(ItemModel itemModel)
        {
            int resultCount = 0;
            try
            {
                //using (var context = new Model1Container())
                //{
                    Item item = new Item();
                    item.Name = itemModel.Name;
                    item.Description = itemModel.Description;
                    item.Price = itemModel.Price;
                    item.IsActive = true;
                    item.ItemImageBase64 = itemModel.ItemImageBase64;

                    context.Items.Add(item);

                    resultCount = context.SaveChanges();
               // }

            }
            catch (Exception ex)
            {
                //track exception
            }

            return resultCount;
        }

        public int UpdateItem(int id, ItemModel itemModel)
        {
            int resultCount = 0;
            try
            {
                    var _item = context.Items.FirstOrDefault(a => a.ItemId == id);

                    if (_item != null)
                    {
                        _item.Name = itemModel.Name;
                        _item.Description = itemModel.Description;
                        _item.Price = itemModel.Price;
                        _item.ItemImageBase64 = itemModel.ItemImageBase64;

                        resultCount = context.SaveChanges();
                    }
            }
            catch (Exception ex)
            {
                //track exception
            }

            return resultCount;
        }

        public int DeleteItem(int id)
        {
            int resultCount = 0;
            try
            {
                    var item = context.Items.FirstOrDefault(a => a.ItemId == id);

                    if (item != null)
                    {
                        context.Items.Remove(item);
                        resultCount = context.SaveChanges();
                    }
            }
            catch (Exception ex)
            {
                //track exception
            }

            return resultCount;
        }
    }
}