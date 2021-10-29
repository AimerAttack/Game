using GameFrame.Core;

namespace GameFrame.Logic
{
    public class ProcedureInit : GameFrameProcedureBase
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            
            ChangeState<ProcedureLogin>();
        }
    }
}