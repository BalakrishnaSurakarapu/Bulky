﻿namespace BulkyWeb.Service
{
    public class CustomAssemblyLoadContext : System.Runtime.Loader.AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllPath)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllPath);
        }

        protected override System.Reflection.Assembly Load(System.Reflection.AssemblyName assemblyName)
        {
            return null;
        }
    }
}