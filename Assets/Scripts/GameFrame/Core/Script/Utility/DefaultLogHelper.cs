using UnityEngine;

namespace GameFrame.Core
{
    public class DefaultLogHelper : ILogHelper
    {
        public void Log(LogLevel level, object msg)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    Debug.Log(Utility.Text.Format("<color=#90EE90>[Debug]{0}</color>", msg.ToString()));
                    break;
                case LogLevel.Info:
                    Debug.Log(Utility.Text.Format("<color=#C0C0C0>{0}</color>", msg.ToString()));
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(msg.ToString());
                    break;
                case LogLevel.Error:
                    Debug.LogError(msg.ToString());
                    break;
                case LogLevel.Fatal:
                    Debug.LogError(Utility.Text.Format("<color=#FF0000>【Fatal】{0}</color>", msg.ToString()));
                    break;
                default:
                    throw new GameFrameException(msg.ToString());
            }
        }
    }
}