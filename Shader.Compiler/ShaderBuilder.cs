﻿using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System.Globalization;
using System.Text;
using Lemon.Tools;
using FlowControl = Mono.Cecil.Cil.FlowControl;
using OpCodes = Mono.Cecil.Cil.OpCodes;
using Shader.BuildTarget;

namespace Compiler
{
    public class ShaderBuilder
    {
        public void Build(IBuildTarget buildTarget, string path, TypeDefinition type)
        {
            var vertProg = new ShaderProgram()
            {
                ProgramType = ProgramType.Vertex,
                MainMethod = type.GetMethods().First(p => p.Name.StartsWith("Vert", StringComparison.InvariantCultureIgnoreCase)),
                MainType = type,
                Path = path,
                BuildTarget = buildTarget
            };

            Process(vertProg);

            var fragProg = new ShaderProgram()
            {
                ProgramType = ProgramType.Fragment,
                MainMethod = type.GetMethods().First(p => p.Name.StartsWith("Frag", StringComparison.InvariantCultureIgnoreCase)),
                MainType = type,
                Path = path,
                BuildTarget = buildTarget
            };
            Process(fragProg);

        }

        void Process(ShaderProgram ShaderProgram)
        {
            ShaderProgram.BuildTarget.Context ??= new Context();
            ShaderProgram.BuildTarget.Context.ShaderProgram = ShaderProgram;

            CollectCalls(ShaderProgram, ShaderProgram.ProgramType, ShaderProgram.MainMethod);

            foreach (var call in ShaderProgram.TargetMethods)
            {
                var builder = new MethodBuilder();
                var programType = call == ShaderProgram.MainMethod ? ShaderProgram.ProgramType : ProgramType.SubFunction;
                builder.Build(ShaderProgram, call.Resolve(), programType);

                ShaderProgram.BuiltMethods.Add(call, builder);
            }

            var sb = new StringBuilder();
            ShaderProgram.BuildTarget.Context.Builder = ShaderProgram.BuiltMethods[ShaderProgram.MainMethod];
            ShaderProgram.BuildTarget.WriteOut(sb);

            var name = ShaderProgram.MainType.Name
                + "." + ShaderProgram.MainMethod.Name
                + "." + ShaderProgram.BuildTarget.GetType().Name.ToLowerInvariant()
                + ".shader.txt";

            File.WriteAllText(ShaderProgram.Path + name, sb.ToString());
        }

        private void CollectCalls(ShaderProgram shaderProgram, ProgramType programType, Mono.Cecil.MethodDefinition method)
        {
            shaderProgram.TargetMethods.Add(method);

            foreach (var field in method.DeclaringType.Resolve().Fields)
            {
                if (shaderProgram.Uniforms.ContainsKey(field)) continue;
                shaderProgram.Uniforms.Add(field, new Var()
                {
                    FieldType = field.FieldType,
                    Name = field.Name,
                    Type = VarType.Uniform,
                    InputType = InputType.In
                });
            }

            var methods = method.GetCalledMethods();
            foreach (var item in methods)
            {
                if (shaderProgram.TargetMethods.Contains(item)) continue;
                if (shaderProgram.BuildTarget.MapMethod(item, "", out _)) continue;

                if (item.DeclaringType.Implements("Shader.Lib.IShader"))
                {
                    var resolved = item.Resolve();
                    if (!HasBody(resolved)) continue;
                    if (shaderProgram.TargetMethods.Contains(item)) continue;

                    CollectCalls(shaderProgram, ProgramType.SubFunction, resolved);
                }
                else if (item.DeclaringType.Name == "Global")
                {
                    var resolved = item.Resolve();
                    if (!HasBody(resolved)) continue;
                    if (!resolved.HasThis)
                    {
                        if (shaderProgram.TargetMethods.Contains(item)) continue;
                        CollectCalls(shaderProgram, ProgramType.SubFunction, resolved);
                    }
                }
            }
        }

        public bool HasBody(MethodDefinition method)
        {
            if (!method.HasBody) return false;
            foreach (var item in method.Body.Instructions)
            {
                if (item.OpCode == OpCodes.Nop) continue;
                if (item.OpCode == OpCodes.Throw) continue;
                if (item.OpCode == OpCodes.Newobj
                    && item.Operand is MethodReference meth
                    && meth.DeclaringType.Name == nameof(NotImplementedException)) continue;
                return true;
            }

            return false;
        }
    }
}
