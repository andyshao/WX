using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.WxApi;
using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Messages;
using XXY.WxApi.Methods;

namespace UnitTestProject1 {
    [TestClass]
    public class UserTest {

        [TestInitialize]
        public void Init() {
            var cfgs = new List<ApiConfig>() {
                new ApiConfig("tag1","your appID 1", "your secret code 1", "your aes key 1 , if not set , can be null", "your token 1"),
                new ApiConfig("tag2","your appID 2", "your secret code 2", "your aes key 2 , if not set , can be null", "your token 2")
            };
            ApiClient.Init(cfgs);
        }


        [TestMethod]
        public void GetUserInfo() {
            var method = new UserInfo() {
                OpenID = "replace with your User OpenID"
            };
            var client = ApiClient.GetInstance("test");
            var user = client.Execute(method);
        }

        [TestMethod]
        public void MessageSend() {
            var method = new MessageSend() {
                OpenID = "replace with your User OpenID",
                Message = new TextMessage() {
                    Content = "tttt"
                }
            };

            var method2 = new MessageSend() {
                OpenID = "replace with your User OpenID",
                Message = new NewsMessage() {
                    Articles = new List<Article>() {
                         new Article(){
                            Title = "文章1",
                            Url = "http://mp.weixin.qq.com/wiki/1/70a29afed17f56d537c833f89be979c9.html",
                            Description = "Description",
                            PicUrl = "http://e.hiphotos.baidu.com/image/pic/item/b999a9014c086e06312ffb4200087bf40ad1cb30.jpg"
                        },
                        new Article(){
                            Title = "文章2",
                            Url = "http://mp.weixin.qq.com/wiki/1/70a29afed17f56d537c833f89be979c9.html",
                            Description = "Description",
                            PicUrl = "http://e.hiphotos.baidu.com/image/pic/item/b999a9014c086e06312ffb4200087bf40ad1cb30.jpg"
                        }
                    }
                }
            };

            var client = ApiClient.GetInstance("tag1");
            //var obj = client.Execute(method);
            var obj2 = client.Execute(method2);
        }

    }
}
