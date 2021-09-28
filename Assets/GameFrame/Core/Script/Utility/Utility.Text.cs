using System;
using System.Text;

namespace GameFrame.Core
{
    public partial class Utility
    {
        public static class Text
        {
            [ThreadStatic]
            private static StringBuilder builder = new StringBuilder(1024);
            
            public static string Format(string format, object arg0)
            {
                if (format == null)
                    throw new GameFrameException("format is invalid");
                if(arg0 == null)
                    throw new GameFrameException("arg is invalid");
                builder.Length = 0;
                builder.AppendFormat(format, arg0);
                return builder.ToString();
            }

            public static string Format(string format, params object[] args)
            {
                if (format == null)
                    throw new GameFrameException("format is invalid");
                if (args == null)
                    throw new GameFrameException("args is invalid");
                builder.Length = 0;
                builder.AppendFormat(format, args);
                return builder.ToString();
            }
            
            public static string GetFullName<T>(string name)
            {
                return GetFullName(typeof(T), name);
            }

            public static string GetFullName(Type type, string name)
            {
                if (type == null)
                {
                    throw new GameFrameException("Type is invalid.");
                }

                string typeName = type.FullName;
                return string.IsNullOrEmpty(name) ? typeName : Utility.Text.Format("{0}.{1}", typeName, name);
            }
        }
    }
}