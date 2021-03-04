using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System;
using Ext.Net;
using Microsoft.AspNetCore.Mvc;
using CalismaWebapiExtnet.Pages.Model2;
using Ext.Net.Core;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Diagnostics;
using CalismaWebapiExtnet.Model;

namespace CalismaWebapiExtnet.Pages
{
    //[BindProperties]
    [DirectModel]
    public class LayoutModel : PageModel
    {
        public List<object> Categories_Model { get; set; }
        public List<object> Shippers_Model { get; set; }
        public List<object> Partners_Model { get; set; }
        public List<object> Employees_Model { get; set; }
        public List<object> Customers_Model { get; set; }
        public List<object> Chart_Data_Model { get; set; }

        public List<Order> OrderSummary { get; set; }

        // public List<object> OrderSummaryTree{ get; set; }

        public TreeNode TreeNode1 { get; set; }


        public void OnGet()
        {
            var TaskList = new List<Task>();
            TaskList.Add(Task<object>.Factory.StartNew(() => this.Categories_Model = Get_Categories().ToList()));
            TaskList.Add(Task<object>.Factory.StartNew(() => this.Shippers_Model = Get_Shippers().ToList()));
            TaskList.Add(Task<object>.Factory.StartNew(() => this.Partners_Model = Get_Partners().ToList()));
            TaskList.Add(Task<object>.Factory.StartNew(() => this.Employees_Model = Get_Employees().ToList()));
            TaskList.Add(Task<object>.Factory.StartNew(() => this.Customers_Model = Get_Customer().ToList()));
            TaskList.Add(Task<object>.Factory.StartNew(() => this.Chart_Data_Model = Get_ChartData().ToList()));
            TaskList.Add(Task<object>.Factory.StartNew(() => this.TreeNode1 = OrdersTree()));
            //TaskList.Add(Task<object>.Factory.StartNew(() => this.OrderSummaryTree = Get_OrderSummaryTree().ToList()));
            //TaskList.Add(Task<object>.Factory.StartNew(() => this.Root_Node = Root()));

            Task.WaitAll(TaskList.ToArray());
        }
        public IEnumerable<object> Get_ChartData()
        {
            Api.X w3 = new Api.X();
            var retval = new List<object>();
            foreach (var item in w3.Get_OrderSummaryByPartners())
                retval.Add(new object[] { item.Type, item.Name, item.OrderCount });
            return retval;
        }

        public IEnumerable<object> Get_Categories()
        {
            var retval = new List<object>();
            Api.ApiCategory.X w3 = new Api.ApiCategory.X();
            foreach (var item in w3.List())
                retval.Add(new object[] { item.CategoryName, item.Description, item.CategoryID, item.EnDis });
            return retval;
        }

        public IEnumerable<object> Get_Shippers()
        {
            var retval = new List<object>();
            Api.ApiShipper.X w3 = new Api.ApiShipper.X();
            foreach (var item in w3.List())
                retval.Add(new object[] { item.ShipperName, item.Phone, item.ShipperID, item.EnDis });
            return retval;
        }

        public IEnumerable<object> Get_Partners()
        {
            Api.X w3 = new Api.X();
            var retval = new List<object>();
            foreach (var item in w3.Get_Partners())
                retval.Add(new object[] { item.Type, item.Name, item.PostalCode, item.Address, item.City, item.ContactName, item.Country, item.EnDis, item.ID });
            return retval;
        }

        public IEnumerable<object> Get_Employees()
        {
            Api.ApiEmployee.X w3 = new Api.ApiEmployee.X();
            var retval = new List<object>();
            foreach (var item in w3.List())
                retval.Add(new object[] { item.FirstName, item.LastName });
            return retval;
        }

        public IEnumerable<object> Get_Customer()
        {
            Api.ApiCustomer.X w3 = new Api.ApiCustomer.X();

            var retval = new List<object>();
            foreach (var item in w3.List())
                retval.Add(new object[] { item.CustomerName });
            return retval;
        }

        public IActionResult OnPostCard_Click(int index, Panel panel)
        {
            panel.ActiveItem = index;
            return this.Direct();
        }

        public IActionResult OnPostSave(string Mode, string Data, string DataType, bool IsCustomer, string record_index)
        {
            switch (DataType)
            {
                case "WIN_CNT_CAT":
                    if (Mode == "N")
                    {
                        Api.ApiCategory.X cat = new Api.ApiCategory.X();
                        var objcat = JsonConvert.DeserializeObject<Categories_Model>(Data);
                        cat.Create(objcat.CategoryName, objcat.Description);
                    }
                    else if (Mode == "E")
                    {
                        Api.ApiCategory.X cat = new Api.ApiCategory.X();
                        var objcat = JsonConvert.DeserializeObject<Categories_Model>(Data);
                        cat.Update(objcat.CategoryName, objcat.Description, objcat.CategoryID);
                    }
                    break;

                case "WIN_CNT_SHP":
                    if (Mode == "N")
                    {
                        Api.ApiShipper.X shp = new Api.ApiShipper.X();
                        var objshp = JsonConvert.DeserializeObject<Shippers_Model>(Data);
                        shp.Create(objshp.ShipperName, objshp.Phone);
                    }
                    else if (Mode == "E")
                    {
                        Api.ApiShipper.X shp = new Api.ApiShipper.X();
                        var objshp = JsonConvert.DeserializeObject<Shippers_Model>(Data);
                        shp.Update(objshp.ShipperName, objshp.Phone, objshp.ShipperID);
                    }
                    break;

                case "WIN_FRM_PARTNERS":
                    var objpartners = JsonConvert.DeserializeObject<Partners_Model>(Data);
                    Api.ApiCustomer.X cs = new Api.ApiCustomer.X();
                    Api.ApiSupplier.X sp = new Api.ApiSupplier.X();
                    if (IsCustomer)
                    {
                        if (Mode == "N")
                        {
                            cs.Create(objpartners.Name, objpartners.ContactName, objpartners.Address,
                       objpartners.City, objpartners.PostalCode, objpartners.Country);
                        }
                        else if (Mode == "E")
                        {
                            cs.Update(objpartners.ContactName, objpartners.Address,
                       objpartners.City, objpartners.PostalCode, objpartners.Country, objpartners.ID);
                        }
                    }
                    else
                    {
                        if (Mode == "N")
                        {
                            sp.Create(objpartners.Name, objpartners.ContactName, objpartners.Address,
                            objpartners.City, objpartners.PostalCode, objpartners.Country);
                        }
                        else if (Mode == "E")
                        {
                            sp.Update(objpartners.ContactName, objpartners.Address,
                           objpartners.City, objpartners.PostalCode, objpartners.Country, objpartners.ID);
                        }
                    }
                    break;
                default:
                    break;
            }
            return this.Direct();
        }

        public TreeNode OrdersTree()
        {

            Api.X t = new Api.X();
            var rootNode = new TreeNode()
            {
                Text = "Orders",
                Expanded = true,
                Children = new List<TreeNode>()
            };

            foreach (var item in t.Get_OrderSummaryByTree())
            {
                var OrderNode = new TreeNode()
                {
                    Text = item.OrderID.ToString(),
                    Children = new List<TreeNode>()
                };
                OrderNode.CustomConfig = new JsObject();
                OrderNode.CustomConfig.Add("IsHeader", true);
                OrderNode.CustomConfig.Add("CustomerName", item.CustomerName);
                OrderNode.CustomConfig.Add("FirstName", item.FirstName);
                OrderNode.CustomConfig.Add("ShipperName", item.ShipperName);
                OrderNode.CustomConfig.Add("Order", item.OrderID);

                rootNode.Children.Add(OrderNode);

                foreach (var x in item.Details)
                {
                    var OrderDetail = new TreeNode()
                    {
                        Leaf = true,
                        Text = x.OrderDetailID.ToString(),
                    };

                    OrderDetail.CustomConfig = new JsObject();
                    OrderDetail.CustomConfig.Add("IsHeader", false);
                    OrderDetail.CustomConfig.Add("ProductName", x.ProductName);
                    OrderDetail.CustomConfig.Add("Order", x.OrderDetailID);
                    OrderDetail.CustomConfig.Add("SupplierName", x.SupplierName);
                    OrderDetail.CustomConfig.Add("Unit", x.Unit);
                    OrderDetail.CustomConfig.Add("Price", x.Price);
                    OrderDetail.CustomConfig.Add("Amount", x.Amount);


                    OrderNode.Children.Add(OrderDetail);
                }

            }
            return rootNode;
        }

        [Direct]
        public IActionResult OnPostCategoryEnable(bool EnDis, int id)
        {
            Api.ApiCategory.X cat = new Api.ApiCategory.X();
            cat.EnableDisable(EnDis, id);
            return this.Direct();
        }
        [Direct]
        public IActionResult OnPostShipperEnable(bool EnDis, int id)
        {
            Api.ApiShipper.X shp = new Api.ApiShipper.X();
            shp.EnableDisable(EnDis, id);
            return this.Direct();
        }

        [Direct]
        public IActionResult OnPostPartnerEnable(bool EnDis, int ID, string Type)
        {
            if (Type == "C") { 
            Api.ApiCustomer.X cus = new Api.ApiCustomer.X();
            cus.EnableDisable(EnDis, ID);
            }
            else 
            {
            Api.ApiSupplier.X sup = new Api.ApiSupplier.X();
            sup.EnableDisable(EnDis, ID);
            
            }
            return this.Direct();
        }

        [Direct]
        public IActionResult OnPostCategoryDelete(int CategoryID)
        {
            Api.ApiCategory.X api = new Api.ApiCategory.X();
            api.Delete(CategoryID);

            return this.Direct();
        }

        [Direct]
        public IActionResult OnPostShipperDelete(int ShipperID)
        {
            Api.ApiShipper.X api = new Api.ApiShipper.X();
            api.Delete(ShipperID);

            return this.Direct();
        }

        [Direct]
        public IActionResult OnPostPartnerDelete(int ID, string Type)
        {
            if (Type == "C")
            {
                Api.ApiCustomer.X api = new Api.ApiCustomer.X();
                api.Delete(ID);
            }
            else
            {
                Api.ApiSupplier.X api2 = new Api.ApiSupplier.X();
                api2.Delete(ID);
            }
            return this.Direct();
        }

        //[Direct]
        //public IActionResult OnPostEdit(string CategoryName, string Description, int CategoryID)
        //{
        //    Db.Tables.Categories categories = new Db.Tables.Categories();
        //    categories.CategoryName = CategoryName;
        //    categories.Description = Description;
        //    categories.CategoryID = CategoryID;

        //    Api.ApiCategory.X api = new Api.ApiCategory.X();

        //    api.Update(categories);

        //    return this.Direct();
        //}

        //[Direct]
        //public IActionResult OnPostEditShipper(string ShipperName, string Phone, int ShipperID)
        //{
        //    Db.Tables.Shippers shippers = new Db.Tables.Shippers();
        //    shippers.ShipperName = ShipperName;
        //    shippers.Phone = Phone;
        //    shippers.ShipperID = ShipperID;

        //    Api.ApiShipper.X api = new Api.ApiShipper.X();

        //    api.Update(shippers);

        //    return this.Direct();
        //}
    }
}
