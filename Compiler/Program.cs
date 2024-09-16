﻿using Lemon;
using Lemon.Tools;
using Mono.Cecil;
using Mono.Cecil.Rocks;
using Watchers;

namespace Compiler
{
    internal class Program
    {
        private static string _srcPath;

        static void Main(string[] args)
        {
            _srcPath = Path.GetFullPath("../../../../ConsoleApp1");
            var dllPath = Path.GetFullPath("../../../../ConsoleApp1\\bin\\Debug\\net8.0");
            Console.WriteLine(_srcPath);

            foreach (var item in Directory.GetFiles(_srcPath, "*.dll"))
            {
                Console.WriteLine(item);
            }

            var lemon = new LemonWeaver(p => Console.WriteLine(p));
            lemon.Read(new[] { dllPath }, p => true, Read);
        }

        private static void Read(List<AssemblyDefinition> list, Action<string> action)
        {
            foreach (var asmDef in list)
            {
                var types = asmDef.MainModule.GetAllTypes().ToArray();

                foreach (var item in types)
                {
                    if (item.Implements("ConsoleApp1.IShader"))
                    {
                        var builder = new ShaderBuilder();
                        builder.Build(_srcPath + "/", item);

                       
                    }
                }
            }
        }
    }
}
