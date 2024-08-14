using System.Threading.Tasks;
using CitizenFX.Core;
using XCore.Client.Resources.XMenu;

namespace XCore.Client
{
    public class ClientMain : BaseScript
    {
        private readonly XMenu _menu;

        public ClientMain()
        {
            _menu = new XMenu();
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            await _menu.OnTick();
        }
    }
}