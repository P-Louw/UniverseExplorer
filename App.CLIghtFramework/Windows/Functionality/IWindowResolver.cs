using System.Reflection;

namespace App.CLIghtFramework.Windows.Context
{
    public interface IWindowResolver
    {
        public IWindowResolver Resolve(string windowName);
    }
}