using System;
using System.Text;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using XCore.Server.Utilities;
namespace XCore.Server
{
    public class ServerMain : BaseScript
    {
        private readonly ConsoleUtility _logger = new ConsoleUtility();
        private readonly IniFile _conf = new IniFile("config.ini");
        private readonly XCoreEvents _eventHandler = new XCoreEvents();
        public ServerMain()
        {
           
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
        }
        private async void OnResourceStart(string resourceName)
        {
            if (resourceName != API.GetCurrentResourceName()) return;
            var versioning = await VersionChecker.CheckLatestVersion() != VersionChecker.CheckCurrentVersion()
                ? $"{ConsoleUtility.GetColorCode(ConsoleColor.Red)}v{VersionChecker.CheckCurrentVersion()}"
                : $"{ConsoleUtility.GetColorCode(ConsoleColor.Green)}You've the latest version v{VersionChecker.CheckCurrentVersion()}";
            Debug.WriteLine($"{ConsoleUtility.GetColorCode(ConsoleColor.Magenta)}____  __________________________________\n{ConsoleUtility.GetColorCode(ConsoleColor.Magenta)}__  |/ /_  ____/_  __ \\__  __ \\__  ____/\n{ConsoleUtility.GetColorCode(ConsoleColor.Magenta)}__    /_  /    _  / / /_  /_/ /_  __/   \n{ConsoleUtility.GetColorCode(ConsoleColor.Magenta)}_    | / /___  / /_/ /_  _, _/_  /___   \n{ConsoleUtility.GetColorCode(ConsoleColor.Magenta)}/_/|_| \\____/  \\____/ /_/ |_| /_____/\x1B[0m");
            Debug.WriteLine(versioning);
            Debug.WriteLine(await VersionChecker.CompareVersions());
            _logger.Log("Info",$"{resourceName} starting up....", ConsoleColor.Cyan,ConsoleColor.Blue);
            _eventHandler.XCoreEventHandlerInitializer(_conf,resourceName);
            _logger.Log("Info",$"{resourceName} EventHandler initialized....", ConsoleColor.Cyan,ConsoleColor.Blue);
        }
        private void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            _logger.Log("Info",$"Player {playerName} is connecting to server....", ConsoleColor.Cyan,ConsoleColor.Blue);
            _eventHandler.RegisterXCoreEvent("fivem");
            _eventHandler.FireXCoreEvent("fivem","Example message pre-alpha!");
        }
    }
}
