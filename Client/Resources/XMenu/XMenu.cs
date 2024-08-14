using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core.UI;
using XCore.Client.Resources.Helpers;

namespace XCore.Client.Resources.XMenu
{

    public class XMenu : BaseScript
    {
        private readonly List<string> _menuItems;
        private int _selectedIndex;
        private bool _isMenuOpen;
        private int _lastInputCheckTime;

        public XMenu()
        {
            _menuItems = new List<string> { "Option 1", "Option 2", "Option 3" };
            _selectedIndex = 0;
            _isMenuOpen = false;
            _lastInputCheckTime = 0;
            Tick += OnTick;
            API.RegisterCommand("openmenu", new Action(OpenMenu), false);
            API.RegisterCommand("closemenu", new Action(CloseMenu), false);
        }

        private void OpenMenu()
        {
            _isMenuOpen = true;
        }

        private void CloseMenu()
        {
            _isMenuOpen = false;
        }

        public async Task OnTick()
        {
            CheckMenuToggle();

            if (_isMenuOpen)
            {
                DrawMenu();
                HandleInput();
            }

            await Task.FromResult(0);
        }

        private void CheckMenuToggle()
        {
            if (!Game.IsControlJustPressed(0, Control.InteractionMenu)) return;
            if (_isMenuOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }

        private void DrawMenu()
        {
            const float menuX = 0.5f;
            const float menuY = 0.3f;
            const float menuWidth = 0.2f;
            const float menuHeight = 0.2f;
            const float itemHeight = 0.05f;

            // Draw background
            DrawRect(menuX, menuY, menuWidth, menuHeight, 0, 0, 0, 200);

            // Draw menu items
            for (var i = 0; i < _menuItems.Count; i++)
            {
                var color = i == _selectedIndex ? XColors.White : XColors.Gray;
                DrawText(_menuItems[i], menuX - menuWidth / 2 + 0.01f, menuY - menuHeight / 2 + (i * itemHeight) + 0.02f, 0.35f, color);
            }
        }

        private static void DrawRect(float x, float y, float width, float height, int r, int g, int b, int a)
        {
            API.DrawRect(x, y, width, height, r, g, b, a);
        }

        private static void DrawText(string text, float x, float y, float scale, XColors color)
        {
            API.SetTextFont(0);
            API.SetTextProportional(true);
            API.SetTextScale(scale, scale);
            API.SetTextColour(color.R, color.G, color.B, color.A);
            API.SetTextDropshadow(0, 0, 0, 0, 255);
            API.SetTextEdge(2, 0, 0, 0, 150);
            API.SetTextDropShadow();
            API.SetTextOutline();
            API.SetTextEntry("STRING");
            API.AddTextComponentString(text);
            API.DrawText(x, y);
        }

        private void HandleInput()
        {
            if (Game.IsControlJustPressed(0, Control.PhoneUp))
            {
                _selectedIndex--;
                if (_selectedIndex < 0) _selectedIndex = _menuItems.Count - 1;
            }
            if (Game.IsControlJustPressed(0, Control.PhoneDown))
            {
                _selectedIndex++;
                if (_selectedIndex >= _menuItems.Count) _selectedIndex = 0;
            }
            if (Game.IsControlJustPressed(0, Control.PhoneSelect))
            {
                SelectOption();
            }
        }

        private void SelectOption()
        {
            switch (_selectedIndex)
            {
                case 0:
                    Screen.ShowNotification("Option 1 Selected");
                    break;
                case 1:
                    Screen.ShowNotification("Option 2 Selected");
                    break;
                case 2:
                    Screen.ShowNotification("Option 3 Selected");
                    break;
            }
            CloseMenu();
        }
    }
}
