using System;

namespace GameFrame.Core
{
    public partial class ResourceComponent
    {
        public class Loader
        {
            public string path;
            public object param;
            public Action<string, object, object> successCallback;
            public Action<string, object> failedCallback;

            public Loader(string _path, object _param, Action<string, object, object> _successCallback,
                Action<string, object> _failedCallback)
            {
                path = _path;
                param = _param;
                successCallback = _successCallback;
                failedCallback = _failedCallback;
            }
        }
    }
}