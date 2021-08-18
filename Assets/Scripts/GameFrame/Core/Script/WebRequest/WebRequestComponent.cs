using UnityEngine;

namespace GameFrame.Core
{
    public class WebRequestComponent : GameFrameComponentBase
    {
        private const int DefaultPriority = 0;

        private WebRequestManager manager;
        
        protected override void OnAwake()
        {
            manager = CoreEntry.AddModule<WebRequestManager>();
        }

        public int AddWebRequest(string webRequestUri, WWWForm wwwForm)
        {
            return AddWebRequest(webRequestUri, null, wwwForm, DefaultPriority, null);
        }       
        
        /// <summary>
        /// 增加 Web 请求任务。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="postData">要发送的数据流。</param>
        /// <param name="wwwForm">WWW 表单。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        private int AddWebRequest(string webRequestUri, byte[] postData, WWWForm wwwForm, int priority, object userData)
        {
            return manager.AddWebRequest(webRequestUri, postData, priority, new WWWFormInfo(wwwForm, userData));
        } 
    }
}