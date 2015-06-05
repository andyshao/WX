//using Microsoft.Practices.Unity;
using System.Threading.Tasks;
//using XXY.WX.IBiz;
using XXY.WxApi;
using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Messages;
using XXY.WxApi.Entities.Requests;
using XXY.WxApi.Handlers;
using XXY.WxApi.Methods;

namespace Example.Handlers {
    public class SubscribeHandler : BaseSubscribeHandler {

        //[Dependency]
        //public ISubscribes Biz {
        //    get;
        //    set;
        //}

        public override Reply Handle(string tag, SubscribeRequest msg) {
            Task.Factory.StartNew(() => {
                //this.Biz.Subscribe(tag, msg.FromUserName);

                var method = new UserInfo() {
                    OpenID = msg.FromUserName
                };
                var client = ApiClient.GetInstance(tag);

                var user = client.Execute(method);
                if (!user.HasError) {
                    //this.Biz.UpdateInfo(tag, msg.FromUserName, user);
                }

                var method2 = new MessageSend() {
                    OpenID = msg.FromUserName,
                    Message = new TextMessage() {
                        Content = "谢谢关注"
                    }
                };
                client.Execute(method2);
            });
            return null;
        }
    }
}