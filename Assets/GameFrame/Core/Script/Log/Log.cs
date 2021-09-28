namespace GameFrame.Core
{
    public static class Log
    {
        public static void Debug(object msg)
        {
            GameFrameLog.Debug(msg);
        }
        
        public static void Debug(string msg)
        {
            GameFrameLog.Debug(msg);
        }
        
        public static void Debug(string msg,string format)
        {
            GameFrameLog.Debug(msg,format);
        }
        
        public static void Debug(string format,params object[] args)
        {
            GameFrameLog.Debug(format,args);
        }
        
        
        
        
        public static void Info(object msg)
        {
            GameFrameLog.Info(msg);
        }
        
        public static void Info(string msg)
        {
            GameFrameLog.Info(msg);
        }
        
        public static void Info(string msg,string format)
        {
            GameFrameLog.Info(msg,format);
        }
        
        public static void Info(string format,params object[] args)
        {
            GameFrameLog.Info(format,args);
        }
        
        
        
        
        public static void Warning(object msg)
        {
            GameFrameLog.Warning(msg);
        }
        
        public static void Warning(string msg)
        {
            GameFrameLog.Warning(msg);
        }
        
        public static void Warning(string msg,string format)
        {
            GameFrameLog.Warning(msg,format);
        }
        
        public static void Warning(string format,params object[] args)
        {
            GameFrameLog.Warning(format,args);
        }
        
        
        
        
        public static void Error(object msg)
        {
            GameFrameLog.Error(msg);
        }
        
        public static void Error(string msg)
        {
            GameFrameLog.Error(msg);
        }
        
        public static void Error(string msg,string format)
        {
            GameFrameLog.Error(msg,format);
        }
        
        public static void Error(string format,params object[] args)
        {
            GameFrameLog.Error(format,args);
        }
        
        
        
        public static void Fatal(object msg)
        {
            GameFrameLog.Fatal(msg);
        }
        
        public static void Fatal(string msg)
        {
            GameFrameLog.Fatal(msg);
        }
        
        public static void Fatal(string msg,string format)
        {
            GameFrameLog.Fatal(msg,format);
        }
        
        public static void Fatal(string format,params object[] args)
        {
            GameFrameLog.Error(format,args);
        }
    }
}