using System;
using System.Collections.Generic;
using App.Core.Gui;

namespace App.Core.GUI
{
    public class CLIWindow : ICLIContext
    {
        private Dictionary<string,Type> _clientWindows { get; set; }

        public virtual void OnWindowLoad()
        {
               
        }

        public CLIWindow()
        {
            
        }
    }
}