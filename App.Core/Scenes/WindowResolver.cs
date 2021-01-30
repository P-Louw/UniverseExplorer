using System;
using System.Reflection;
using App.Core.Scenes;

namespace App.Core.Scenes
{
    public class WindowResolver : IWindowResolver
    {
        public IWindowResolver Resolve(string windowName)
        {
            var type = Assembly.GetAssembly(typeof(WindowResolver)).GetType($"{windowName}Window");
            var instance = Activator.CreateInstance(type);
            
            return instance as IWindowResolver;
        }
    }
}