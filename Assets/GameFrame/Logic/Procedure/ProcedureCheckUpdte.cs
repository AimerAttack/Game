using GameFrame.Core;

namespace GameFrame.Logic
{
    public class ProcedureCheckUpdte : GameFrameProcedureBase
    {
        protected override void OnEnter()
        {
            base.OnEnter();

            GetVersionData();
            
            bool needClientUpdate = false;
            if (needClientUpdate)
            {
                ChangeState<ProcedureUpdateClient>();
            }
            else
            {
                ChangeState<ProcedureUpdateRes>();
            }
        }

        void GetVersionData()
        {
            
        }

        void OnServerResponce()
        {
            
        }
    }
}