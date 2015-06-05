using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using XXY.WxApi;

namespace Example.Controllers {
    public class HomeController : Controller {
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
    }
}