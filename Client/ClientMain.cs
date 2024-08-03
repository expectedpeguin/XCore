using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;

namespace XCore.Client
{
    public class Client  : BaseScript
    {
        public Client()
        {
            API.RegisterCommand("square", new Action<int, List<object>, string>((source, args, raw) =>
            {
                if (args.Count <= 0) return;
                if (int.TryParse(args[0].ToString(), out var num))
                {
                    TriggerServerEvent("requestSquare", num);
                }
                else
                {
                    Debug.WriteLine("Please provide a valid number.");
                }
            }), false);
        }
    }
}