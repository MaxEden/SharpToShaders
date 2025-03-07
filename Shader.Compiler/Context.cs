using Compiler;

namespace Shader.BuildTarget
{
    public class Context
    {
        public ShaderBuilder.ProgramType ProgramType;

        public ShaderBuilder Builder { get; internal set; }
    }
}
