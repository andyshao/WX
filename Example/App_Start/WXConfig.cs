using Example.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using XXY.WxApi;
using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Requests;

namespace Example {
    /// <summary>
    /// 微信ApiClient 配置
    /// </summary>
    public static class WXApiConfig {
        public static void Config() {
            //从数据库获取配置信息
            //var cfgs = DependencyResolver.Current.GetService<IApiConfig>().GetConfigs()
            //    .Select(c => new ApiConfig(c.TAG, c.APPID, c.APPSECRET, c.AESKEY, c.TOKEN));
            IEnumerable<ApiConfig> cfgs = new List<ApiConfig>{
                    new ApiConfig("test","your appID", "your Secret Code", "your aes key,if not set can be null", "your token")
            };
            ApiClient.Init(cfgs);

            //Dispatcher 的依赖注入
            RequestDispatcher.GetService = t => DependencyResolver.Current.GetService(t);

            #region 注册处理程序
            RequestDispatcher.Regist<TextHandler>();
            RequestDispatcher.Regist<VoiceHandler>();
            RequestDispatcher.Regist<LinkHandler>();


            RequestDispatcher.Regist<ClickHandler>();
            RequestDispatcher.Regist<SubscribeHandler>();
            RequestDispatcher.Regist<UnsubscribeHandler>();
            //RequestDispatcher.Regist<TextHandler>(RequestTypes.Text);
            //RequestDispatcher.Regist<VoiceHandler>(RequestTypes.Voice);
            //RequestDispatcher.Regist<LinkHandler>(RequestTypes.Link);


            //RequestDispatcher.Regist<ClickHandler>(RequestTypes.Event, EventTypes.Click);
            //RequestDispatcher.Regist<SubscribeHandler>(RequestTypes.Event, EventTypes.Subscribe);
            //RequestDispatcher.Regist<UnsubscribeHandler>(RequestTypes.Event, EventTypes.Unsubscribe);
            #endregion
        }

    }
}
