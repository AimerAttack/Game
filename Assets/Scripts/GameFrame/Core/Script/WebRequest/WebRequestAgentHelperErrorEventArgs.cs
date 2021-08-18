namespace GameFrame.Core
{
    public sealed class WebRequestAgentHelperErrorEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// 初始化 Web 请求代理辅助器错误事件的新实例。
        /// </summary>
        /// <param name="errorMessage">错误信息。</param>
        /// <param name="webResponseBytes">Web 响应的数据流。</param>
        public WebRequestAgentHelperErrorEventArgs(string errorMessage, byte[] webResponseBytes = default(byte[]))
        {
            ErrorMessage = errorMessage;
            m_WebResponseBytes = webResponseBytes;
        }

        /// <summary>
        /// 获取错误信息。
        /// </summary>
        public string ErrorMessage
        {
            get;
            private set;
        }

        /// <summary>
        /// Edit by wcy 2018/12/15
        /// </summary>
        private readonly byte[] m_WebResponseBytes;

        /// <summary>
        /// 获取 Web 响应的数据流。
        /// </summary>
        /// <returns>Web 响应的数据流。</returns>
        public byte[] GetWebResponseBytes()
        {
            return m_WebResponseBytes;
        }
    }
}