using System.Collections.Generic;

namespace GameFrame.Core
{
    public class TaskPool<T> where T : ITask
    {
        private readonly Stack<ITaskAgent<T>> freeAgents;
        private readonly LinkedList<ITaskAgent<T>> workingAgents;
        private readonly LinkedList<T> waitingTasks;

        public TaskPool()
        {
            freeAgents = new Stack<ITaskAgent<T>>();
            workingAgents = new LinkedList<ITaskAgent<T>>();
            waitingTasks = new LinkedList<T>();
        }

        public int TotalAgentCount => FreeAgentCount + WorkingAgentCount;
        public int FreeAgentCount => freeAgents.Count;
        public int WorkingAgentCount => workingAgents.Count;
        public int WaitingTaskCount => waitingTasks.Count;

        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            var current = workingAgents.First;
            while (current != null)
            {
                if (current.Value.Task.Done)
                {
                    var next = current.Next;
                    current.Value.Reset();
                    freeAgents.Push(current.Value);
                    workingAgents.Remove(current);
                    current = next;
                    continue;
                }

                current.Value.Update(elapseSeconds,realElapseSeconds);
                current = current.Next;
            }

            while (freeAgents.Count > 0 && waitingTasks.Count > 0)
            {
                var agent = freeAgents.Pop();
                workingAgents.AddLast(agent);
                var task = waitingTasks.First.Value;
                waitingTasks.RemoveFirst();
                agent.Start(task);

                if (task.Done)
                {
                    agent.Reset();
                    freeAgents.Push(agent);
                    workingAgents.Remove(agent);
                }
            }            
        }

        public void Shutdown()
        {
            while (FreeAgentCount > 0)
            {
                freeAgents.Pop().Shutdown();
            }

            foreach (ITaskAgent<T> workingAgent in workingAgents)
            {
                workingAgent.Shutdown();
            }
            
            workingAgents.Clear();
            waitingTasks.Clear();
        }

        public void AddAgent(ITaskAgent<T> agent)
        {
            if(agent == null)
                return;
            agent.Initialize();
            freeAgents.Push(agent);
        }

        public void AddTask(T task)
        {
            var current = waitingTasks.First;
            while (current != null)
            {
                if(task.Priority > current.Value.Priority)
                    break;
                current = current.Next;
            }

            if (current != null)
            {
                waitingTasks.AddBefore(current, task);
            }
            else
            {
                waitingTasks.AddLast(task);
            }
        }

        public T RemoveTask(int SerialId)
        {
            foreach (var task in waitingTasks)
            {
                if (task.SerialId == SerialId)
                {
                    waitingTasks.Remove(task);
                    return task;
                }
            }

            foreach (var agent in workingAgents)
            {
                if (agent.Task.SerialId == SerialId)
                {
                    var task = agent.Task;
                    agent.Reset();
                    workingAgents.Remove(agent);
                    freeAgents.Push(agent);
                    return task;
                }
            }
            
            return default(T);
        }

        public void RemoveAllTasks()
        {
            waitingTasks.Clear();
            foreach (var agent in workingAgents)
            {
                agent.Reset();
                freeAgents.Push(agent);
            }
            workingAgents.Clear();
        }
    }
}