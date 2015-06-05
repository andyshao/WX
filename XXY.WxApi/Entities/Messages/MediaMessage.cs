using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.WxApi.Entities.Messages {
    public abstract class MediaMessage : Message {

        /// <summary>
        /// 发送的图片/语音/视频的媒体ID
        /// </summary>
        [JsonProperty("media_id")]
        public string MediaID {
            get;
            set;
        }

        /// <summary>
        /// 缩略图的媒体ID
        /// </summary>
        [JsonProperty("thumb_media_id")]
        public string ThumbMediaID {
            get;
            set;
        }

    }
}
