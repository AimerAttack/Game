using System;

namespace GameFrame.Core
{
    public interface ITaskAgent<T> where T : ITask
    {
        T Task
        {
            get;
        }

        void Initialize();
        
        void Update(float elapseSeconds, float realElapseSeconds);

        void Shutdown();

        void Start(T Task);

        /// <summary>
        /// 停止正在处理的任务并重置任务代理。
        /// </summary>
        void Reset();
    }
}