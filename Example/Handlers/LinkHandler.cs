using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Requests;
using XXY.WxApi.Handlers;

namespace Example.Handlers {
    public class LinkHandler : BaseLinkHandler {
        public override Reply Handle(string tag, LinkRequest request) {
            return new Reply() {
                Content = "LINK RESULT"
            };
        }
    }
}