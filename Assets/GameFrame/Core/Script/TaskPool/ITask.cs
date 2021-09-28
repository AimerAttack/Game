namespace GameFrame.Core
{
    public interface ITask
    {
        int SerialId
        {
            get;
        }

        int Priority
        {
            get;
        }

        bool Done
        {
            get;
        }
    }
}