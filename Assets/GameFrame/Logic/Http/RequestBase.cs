using System;

namespace GameFrame.Logic
{
    public abstract class RequestBase
    {
        public enum RequestType
        {
            Http,
            Socket,
        }

        private static long _serialId = 0;

        protected static string NextRequestSerialId()
        {
            if (_serialId == 0)
            {
                long utcOffset = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                _serialId = -utcOffset;
            }

            return $"{--_serialId}";
        }
        public string SerialId { get; internal set; }
        public string Url { get; private set; }
        public RequestType Type { get; set; }
        public DateTime SendTime { get; set; }
        public bool WithInputBlock { get; set; }
        public string Tag { get; set; }

        protected RequestBase(string url)
        {
            this.Url = url;
            this.SerialId = NextRequestSerialId();
        }

        protected RequestBase(string url,string serialId)
        {
            this.Url = url;
            this.SerialId = serialId;
        }
        
        public virtual void Send(bool sendWithBlock = true, float delay = 2.0f)
        {
            if (string.IsNullOrEmpty(Url))
            {
                return;
            }

            WithInputBlock = sendWithBlock;
            if (WithInputBlock)
            {
                //TODO:Input Block
            }

            SendTime = DateTime.Now;
        }
    }
}