namespace GameFrame.Core
{
    public static class GameFrameLog
    {
        private static ILogHelper logHelper;

        public static void SetLogHelper(ILogHelper _helper)
        {
            logHelper = _helper;
        }

        public static void Debug(object msg)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Debug,msg);
        }

        public static void Debug(string msg)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Debug,msg);
        }

        public static void Debug(string msg, object arg0)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Debug,Utility.Text.Format(msg,arg0));
        }
        
        public static void Debug(string format, params object[] args)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Debug,Utility.Text.Format(format,args));
        } 
        
        
        
        
        public static void Info(object msg)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Info,msg);
        }

        public static void Info(string msg)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Info,msg);
        }

        public static void Info(string msg, object arg0)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Info,Utility.Text.Format(msg,arg0));
        }
        
        public static void Info(string format, params object[] args)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Info,Utility.Text.Format(format,args));
        } 
        
        
        
        public static void Warning(object msg)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Warning,msg);
        }

        public static void Warning(string msg)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Warning,msg);
        }

        public static void Warning(string msg, object arg0)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Warning,Utility.Text.Format(msg,arg0));
        }
        
        public static void Warning(string format, params object[] args)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Warning,Utility.Text.Format(format,args));
        } 
        
        
        
        
        public static void Error(object msg)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Error,msg);
        }

        public static void Error(string msg)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Error,msg);
        }

        public static void Error(string msg, object arg0)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Error,Utility.Text.Format(msg,arg0));
        }
        
        public static void Error(string format, params object[] args)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Error,Utility.Text.Format(format,args));
        } 
        
        
        
        
        public static void Fatal(object msg)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Fatal,msg);
        }

        public static void Fatal(string msg)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Fatal,msg);
        }

        public static void Fatal(string msg, object arg0)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Fatal,Utility.Text.Format(msg,arg0));
        }
        
        public static void Fatal(string format, params object[] args)
        {
            if(logHelper == null)
                return;
            logHelper.Log(LogLevel.Fatal,Utility.Text.Format(format,args));
        } 
    }
}