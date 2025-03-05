using MKDir;

namespace GM.Test
{
    public class TestScript : MonoSingleton<TestScript>
    {
        public WarningPanel warningPanel;

        public void testText(string msg)
        {
            warningPanel.ShowText(msg);
        }
    }
}