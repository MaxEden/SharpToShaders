using Compiler;

namespace Shader.BuildTarget
{
    public class Context
    {
        public ShaderProgram ShaderProgram { get; internal set; }

        public MethodBuilder Builder { get; internal set; }
    }
}
