using UnityEngine;

namespace GameFrame.Logic
{
    public class HttpRequest : RequestBase
    {
        public delegate void RequestSuccessCallback(HttpRequest req, byte[] res);
        public delegate void RequestFailureCallback(HttpRequest req, byte[] res);
        
        public string ExtArgs { get; set; }
        public int RetryTimes { get; set; }
        public WWWForm WwwForm { get; private set; }
        public RequestSuccessCallback SuccessCallback { get; set; }
        public RequestFailureCallback FailureCallback { get; set; }
        
        public HttpRequest(string url) : base(url)
        {
            this.RetryTimes = 3;
            Type = RequestType.Http;
        }

        public void AddParam(string key, string val)
        {
            if (WwwForm == null)
            {
                WwwForm = new WWWForm();
            }
            WwwForm.AddField(key, val);
        }
 
        public override void Send(bool sendWithBlock = true, float delay = 2)
        {
            base.Send(sendWithBlock, delay);
            Entry.Http.EnqueueRequestAndSend(this);
        }
    }
}