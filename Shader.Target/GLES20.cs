using Compiler;
using Mono.Cecil;
using System.Text;
using Lemon.Tools;

namespace Shader.BuildTarget
{
    public class GLES20 : GLSLBase
    {
        public override void WriteOut(StringBuilder sb)
        {
            if (Context.ShaderProgram.ProgramType == ProgramType.Fragment)
            {
                sb.AppendLine("precision mediump float;");
            }

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

            foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && !p.BuiltIn && p.Type == VarType.Attribute))
            {
                sb.AppendLine($"attribute {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
            }

            sb.AppendLine();

            foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && p.Type == VarType.Varying))
            {
                sb.AppendLine($"varying {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
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
    }
}
