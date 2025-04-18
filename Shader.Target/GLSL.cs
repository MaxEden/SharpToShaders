﻿using Compiler;
using Mono.Cecil;
using System.Text;
using Lemon.Tools;

namespace Shader.BuildTarget
{
    public class GLSL : GLSLBase
    {
        public override void WriteOut(StringBuilder sb)
        {
            sb.AppendLine("#version 330");

            foreach (var field in Context.ShaderProgram.Uniforms.Values.Where(p => p.IsUsed && !p.BuiltIn && p.Type == VarType.Uniform))
            {
                sb.AppendLine($"uniform {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
            }
            sb.AppendLine();

            if (Context.ShaderProgram.TargetMethods != null)
            {
                foreach (var meth in Context.ShaderProgram.BuiltMethods.Reverse())
                {
                    if (meth.Key == Context.ShaderProgram.MainMethod) continue;

                    sb.AppendLine(meth.Value.Header);
                    sb.AppendLine("{");
                    sb.Append(meth.Value.Body.ToString());
                    sb.AppendLine("}");
                    sb.AppendLine();
                }
            }

            int location = 0;
            foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && !p.BuiltIn && p.Type == VarType.Attribute))
            {
                sb.AppendLine($"layout(location = {location}) in {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
                location++;
            }

            sb.AppendLine();

            foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && p.Type == VarType.Varying))
            {
                if (Context.ShaderProgram.ProgramType == ProgramType.Vertex)
                {
                    sb.AppendLine($"out {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
                }
                else
                {
                    sb.AppendLine($"in {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
                }
            }
            sb.AppendLine();
            sb.AppendLine($"void main(){{");

            foreach (var local in Context.Builder.Locals)
            {
                if (local.canBeOmitted) continue;
                if (local.canBeRef) continue;
                if (local.canInline) continue;
                sb.AppendLine(
                    $"{Context.Builder.MapTypeName(local.definition.VariableType)} {local};"
                );
            }

            foreach (var named in Context.Builder.NamedStackLocals)
            {
                sb.AppendLine(
                    $"{named.expectedType} {named.name};"
                );
            }

            sb.Append(Context.Builder.Body.ToString());
            sb.AppendLine("}");
        }

        public override bool MapMethod(MethodReference methodRef, Parameters call, out string result, out bool needsBrackets)
        {
            result = default;
            needsBrackets = false;

            if (methodRef.Name == "op_Implicit")
            {
                if (methodRef.DeclaringType.Name == "float4" && methodRef.ReturnType.Name == "float3")
                {
                    if (call == null) return true;

                    result = call.List[0].btext + ".xyz";
                    needsBrackets = false;
                    return true;
                }

                if (methodRef.DeclaringType.Name == "float3" && methodRef.ReturnType.Name == "float2")
                {
                    if (call == null) return true;

                    result = call.List[0].btext + ".xy";
                    needsBrackets = false;
                    return true;
                }

                if (methodRef.DeclaringType.Name == "float2" && methodRef.ReturnType.Name == "float")
                {
                    if (call == null) return true;

                    result = call.List[0].btext + ".x";
                    needsBrackets = false;
                    return true;
                }
            }

            return base.MapMethod(methodRef, call, out result, out needsBrackets);
        }
    }
}
