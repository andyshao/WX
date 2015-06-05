using System;
using XXY.WxApi;
using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Requests;
using XXY.WxApi.Handlers;

namespace Example.Handlers {
    public class ClickHandler : BaseClickHandler {

        public static readonly string HelpText =
@"
请按照下面格式进行提问：
盐田
广东
5/1从盐田到新加坡的船期
5/1从盐田到新加坡的运价
地点可用中文、英文、拼音代替；可以是港口名称/代码、城市、省份。
日期格式支持：5/1、5月1日、5.1、2015.5.1、2015/5/1
";

        public override Reply Handle(string tag, ClickRequest msg) {
            var ctx = "报歉，目前还不能处理您的请求。";
            switch (msg.EventKey.ToUpper()) {
                case "HELP":
                    ctx = HelpText;
                    break;
            }

            var reply = new Reply() {
                Content = ctx,
                CreateTime = DateTime.Now.ToUnixTimestamp(),
                FromUserName = msg.ToUserName,
                MsgId = DateTime.Now.Ticks,
                MsgType = "text",
                ToUserName = msg.FromUserName
            };
            return reply;
        }
    }
}