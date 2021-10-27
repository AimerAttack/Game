namespace GameFrame.Logic
{
    public class MainUI : UIScreen<MainUIHolder>
    {
        public override string AssetPath { get; }

        public override EUILayer UILayer
        {
            get
            {
                return EUILayer.Main;
            }
        }

        protected override void OnOpen()
        {
            base.OnOpen();

        }
    }
}