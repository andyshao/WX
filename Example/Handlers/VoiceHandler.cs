using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Requests;
using XXY.WxApi.Handlers;

namespace Example.Handlers {
    public class VoiceHandler : BaseVoiceHandler {
        public override Reply Handle(string tag, VoiceRequest msg) {
            //string reason;
            //return Searcher.Search(msg.Recognition, out reason) ?? new Reply() {
            //    Content = string.Format("您的请求: {0} ,没有结果, {1}", msg.Recognition, reason)
            //};

            //按你的逻辑返回
            return null;
        }
    }
}