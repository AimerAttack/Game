using GameFrame.Core;

namespace GameFrame.Logic
{
    public class ProcedureInit : GameFrameProcedureBase
    {
        protected override void OnEnter()
        {
            base.OnEnter();

            Entry.UI.Open<MainUI>();
            
            ChangeState<ProcedureLogin>();
        }
    }
}