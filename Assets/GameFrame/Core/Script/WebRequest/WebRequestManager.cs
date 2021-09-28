using System;

namespace GameFrame.Core
{
    public partial class WebRequestManager : GameFrameModule
    {
        
        private readonly TaskPool<WebRequestTask> m_TaskPool;
        private float m_Timeout;
        private EventHandler<WebRequestStartEventArgs> m_WebRequestStartEventHandler;
        private EventHandler<WebRequestSuccessEventArgs> m_WebRequestSuccessEventHandler;
        private EventHandler<WebRequestFailureEventArgs> m_WebRequestFailureEventHandler;

        public WebRequestManager()
        {
            m_TaskPool = new TaskPool<WebRequestTask>();
            m_Timeout = 30f;
        }
        
        public int TotalAgentCount
        {
            get
            {
                return m_TaskPool.TotalAgentCount;
            }
        }
        
        /// <summary>
        /// Web 请求开始事件。
        /// </summary>
        public event EventHandler<WebRequestStartEventArgs> WebRequestStart
        {
            add
            {
                m_WebRequestStartEventHandler += value;
            }
            remove
            {
                m_WebRequestStartEventHandler -= value;
            }
        }

        /// <summary>
        /// Web 请求成功事件。
        /// </summary>
        public event EventHandler<WebRequestSuccessEventArgs> WebRequestSuccess
        {
            add
            {
                m_WebRequestSuccessEventHandler += value;
            }
            remove
            {
                m_WebRequestSuccessEventHandler -= value;
            }
        }

        /// <summary>
        /// Web 请求失败事件。
        /// </summary>
        public event EventHandler<WebRequestFailureEventArgs> WebRequestFailure
        {
            add
            {
                m_WebRequestFailureEventHandler += value;
            }
            remove
            {
                m_WebRequestFailureEventHandler -= value;
            }
        }

        
        /// <summary>
        /// 增加 Web 请求任务。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="postData">要发送的数据流。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public int AddWebRequest(string webRequestUri, byte[] postData, int priority, object userData)
        {
            if (string.IsNullOrEmpty(webRequestUri))
            {
                throw new GameFrameException("Web request uri is invalid.");
            }

            if (TotalAgentCount <= 0)
            {
                throw new GameFrameException("You must add web request agent first.");
            }

            WebRequestTask webRequestTask = new WebRequestTask(webRequestUri, postData, priority, m_Timeout, userData);
            m_TaskPool.AddTask(webRequestTask);

            return webRequestTask.SerialId;
        }

        internal override void Update(float deltaTime, float realDeltaTime)
        {
            m_TaskPool.Update(deltaTime,realDeltaTime);
        }

        internal override void ShutDown()
        {
            m_TaskPool.Shutdown();
        }
        
        public void RemoveAllWebRequests()
        {
            m_TaskPool.RemoveAllTasks();
        }
        
        public bool RemoveWebRequest(int serialId)
        {
            return m_TaskPool.RemoveTask(serialId) != null;
        }
        
        public void AddWebRequestAgentHelper(IWebRequestAgentHelper webRequestAgentHelper)
        {
            WebRequestAgent agent = new WebRequestAgent(webRequestAgentHelper);
            agent.WebRequestAgentStart += OnWebRequestAgentStart;
            agent.WebRequestAgentSuccess += OnWebRequestAgentSuccess;
            agent.WebRequestAgentFailure += OnWebRequestAgentFailure;

            m_TaskPool.AddAgent(agent);
        }
        
        private void OnWebRequestAgentStart(WebRequestAgent sender)
        {
            if (m_WebRequestStartEventHandler != null)
            {
                m_WebRequestStartEventHandler(this, new WebRequestStartEventArgs(sender.Task.SerialId, sender.Task.WebRequestUri, sender.Task.UserData));
            }
        }

        private void OnWebRequestAgentSuccess(WebRequestAgent sender, byte[] webResponseBytes)
        {
            if (m_WebRequestSuccessEventHandler != null)
            {
                m_WebRequestSuccessEventHandler(this, new WebRequestSuccessEventArgs(sender.Task.SerialId, sender.Task.WebRequestUri, webResponseBytes, sender.Task.UserData));
            }
        }

        private void OnWebRequestAgentFailure(WebRequestAgent sender, string errorMessage, byte[] webResponseBytes)
        {
            if (m_WebRequestFailureEventHandler != null)
            {
                m_WebRequestFailureEventHandler(this, new WebRequestFailureEventArgs(sender.Task.SerialId, sender.Task.WebRequestUri, errorMessage, sender.Task.UserData, webResponseBytes));
            }
        }
    }
}