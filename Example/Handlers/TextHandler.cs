using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XXY.WxApi;
using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Messages;
using XXY.WxApi.Entities.Requests;
using XXY.WxApi.Handlers;
using XXY.WxApi.Methods;

namespace Example.Handlers {
    public class TextHandler : BaseTextHandler {
        public override Reply Handle(string tag, TextRequest msg) {
            if (msg.Content.Equals("test", StringComparison.OrdinalIgnoreCase)) {
                Task.Factory.StartNew(() => {
                    this.SendLink(tag, msg);
                });
                return null;
            } else {
                string reason;
                //return Searcher.Search(msg.Content, out reason) ?? new Reply() {
                //    Content = reason
                //};

                //这里请按你的逻逻返回。
                return null;
            }
        }

        private void SendLink(string tag, BaseRequest msg) {
            var client = ApiClient.GetInstance(tag);
            if (client != null) {
                var method = new MessageSend() {
                    OpenID = msg.FromUserName,
                    Message = new NewsMessage() {
                        Articles = new List<Article>() {
                        new Article(){
                             Title = "test",
                             Url = "http://www.56cargo.com"
                        }
                     }
                    }
                };

                client.Execute(method);
            }
        }
    }
}