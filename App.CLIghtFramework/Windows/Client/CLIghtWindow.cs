using System;
using System.Collections.Generic;
using App.CLIghtFramework.Windows.Context;

namespace App.CLIghtFramework.Windows
{
    public abstract class CLIghtWindow : ICLIghtContext
    {
        private Dictionary<string,Type> _clientWindows { get; set; }
        public abstract void OnWindowLoad();
    }
}