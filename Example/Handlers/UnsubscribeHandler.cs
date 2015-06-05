//using Microsoft.Practices.Unity;
using System.Threading.Tasks;
//using XXY.WX.IBiz;
using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Requests;
using XXY.WxApi.Handlers;

namespace Example.Handlers {
    public class UnsubscribeHandler : BaseUnsubscribeHandler {

        //[Dependency]
        //public ISubscribes Biz {
        //    get;
        //    set;
        //}

        public override Reply Handle(string tag, UnsubscribeRequest msg) {
            Task.Factory.StartNew(() => {
                //this.Biz.Unsubscribe(tag, msg.FromUserName);
            });

            return null;
        }
    }
}