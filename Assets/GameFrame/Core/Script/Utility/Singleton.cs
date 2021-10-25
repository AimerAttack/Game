namespace GameFrame.Core
{
    public class Singleton<T> where T : class,IBaseSingleton, new()
    {
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                    _instance.OnInit();
                }

                return _instance;
            }
        }

        private static T _instance;
    }
}