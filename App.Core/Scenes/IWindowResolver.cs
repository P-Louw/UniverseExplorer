using System.Reflection;

namespace App.Core.Scenes
{
    public interface IWindowResolver
    {
        public IWindowResolver Resolve(string windowName);
    }
}