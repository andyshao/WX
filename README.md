# WX
微信 Api

特色: 支持多个 AppCode

支写了部分功能.
对应的主动方法在: WxApi/Methods 下. 可参照 微信的 API 接口文档自行扩展.

WxApi/Entities/Requests 
下是被动接收到的消息(用户从微信向你的程序发送的消息/请求), 需要对应的 Handler 来处理这些请求.

WxApi/Entities/Messages 
是主动或被动向用户发送的消息.

WxApi/Entities/Handlers
定义的是 Request 的处理程序基类(目前只定义了几种,可自行扩展). 这里的 Hander 不参于具体的业务处理, 所以只是个定义而已.

Example/Handlers
Request 的业务处理程序, 



Api 配置 （在Global 中执行该方法， IApiConfig 是存储接口，可以替换成自己的实现）
~~~
    /// <summary>
    /// 微信ApiClient 配置
    /// </summary>
    public static class WXApiConfig {
        public static void Config() {
            //从数据库获取配置信息
            var cfgs = DependencyResolver.Current.GetService<IApiConfig>().GetConfigs()
                .Select(c => new ApiConfig(c.TAG, c.APPID, c.APPSECRET, c.AESKEY, c.TOKEN));
            ApiClient.Init(cfgs);

            //Dispatcher 的依赖注入
            RequestDispatcher.GetService = t => DependencyResolver.Current.GetService(t);

            #region 注册处理程序
            RequestDispatcher.Regist<TextHandler>(RequestTypes.Text);
            RequestDispatcher.Regist<VoiceHandler>(RequestTypes.Voice);
            RequestDispatcher.Regist<LinkHandler>(RequestTypes.Link);


            RequestDispatcher.Regist<ClickHandler>(RequestTypes.Event, EventTypes.Click);
            RequestDispatcher.Regist<SubscribeHandler>(RequestTypes.Event, EventTypes.Subscribe);
            RequestDispatcher.Regist<UnsubscribeHandler>(RequestTypes.Event, EventTypes.Unsubscribe);
            #endregion
        }

    }
~~~

被动回复与开发者认证入口 (MVC)
~~~
        /// <summary>
        /// 消息接收点
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="encrypt_type"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Token")]
        public ContentResult Post(string tag, string signature, string timestamp, string nonce, string encrypt_type = "") {
            var client = ApiClient.GetInstance(tag);
            if (client == null)
                return null;

            var useAes = encrypt_type.Equals("aes", StringComparison.OrdinalIgnoreCase);

            if (this.CheckSignature(useAes, client, signature, timestamp, nonce)) {
                using (StreamReader reader = new StreamReader(Request.InputStream)) {
                    var data = reader.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(data)) {

                        var msg = client.Parse(data, useAes);
                        if (msg != null) {
                            var reply = RequestDispatcher.Dispatch(tag, msg);
                            if (reply != null) {
                                var str = client.Encrypt(reply, nonce, useAes);
                                return Content(str, "text/xml", UTF8Encoding.UTF8);
                            }
                        }
                    }
                }
            }

            return Content("");
        }

        /// <summary>
        /// 开发者认证点
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="signature"></param>
        /// <param name="echostr"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="encrypt_type"></param>
        /// <returns></returns>
        [HttpGet, ActionName("Token")]
        public ContentResult Get(string tag, string signature, string echostr, string timestamp, string nonce, string encrypt_type = "") {
            var client = ApiClient.GetInstance(tag);
            if (client == null)
                return null;

            var useAes = encrypt_type.Equals("aes", StringComparison.OrdinalIgnoreCase);

            if (this.CheckSignature(useAes, client, signature, timestamp, nonce)) {
                return Content(echostr);
            } else {
                return null;
            }
        }

        private bool CheckSignature(bool useAes, ApiClient client, string signature, string timestamp, string nonce) {
            return useAes ? Cryptography.Signature(client.Config.Token, timestamp, nonce)
                .Equals(signature, StringComparison.OrdinalIgnoreCase) : true;
        }
~~~


