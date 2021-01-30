using System;
using System.Collections.Generic;

namespace App.Core.Scenes
{
    public abstract class CLIWindow : ICLIContext
    {
        private Dictionary<string,Type> _clientWindows { get; set; }
        public abstract void OnWindowLoad();
    }
}