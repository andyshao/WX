using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XXY.WxApi.Entities;
using XXY.WxApi;
using XXY.WxApi.Methods;
using XXY.WxApi.Entities.Menus;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using XXY.WxApi.Entities.Requests;
using System.Collections;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace UnitTestProject1 {
    [TestClass]
    public class UnitTest1 {

        [TestInitialize]
        public void Init() {
            var cfgs = new List<ApiConfig>() {
                new ApiConfig("tag","your appID", "your secret code", "your aes key , if not set , can be null", "your token")
            };
            ApiClient.Init(cfgs);
        }

        [TestMethod]
        public void GetMenuTest() {
            var client = ApiClient.GetInstance("tag");
            var method = new MenuGet();
            var data = client.Execute(method);
        }

        [TestMethod]
        public void DeleteMenuTest() {
            var client = ApiClient.GetInstance("tag");
            var method = new MenuDelete();
            var data = client.Execute(method);
        }

        [TestMethod]
        public void CreateMenuTest() {
            var client = ApiClient.GetInstance("tag");
            var method = new MenuCreate() {
                Menu = new Menu() {
                    Buttons = new List<BaseMenu>() {
                        new ClickButton(){ Name = "查询说明",Key = "Help"},
                        //new ViewButton(){Name="百度", Url="http://www.baidu.com"},
                        //new SubMenus(){ Name = "abc", SubButtons = new List<ButtonMenu>(){
                        //    new ClickButton(){Name="A", Key = "A"},
                        //    new ClickButton(){Name="B", Key="B"},
                        //    new ClickButton(){Name="C", Key="C"}
                        //}}
                    }
                }
            };

            var data = client.Execute(method);
        }
    }
}
