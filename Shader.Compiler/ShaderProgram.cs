using Mono.Cecil;
using Shader.BuildTarget;
using System.Collections.Generic;

namespace Compiler
{
    public class ShaderProgram
    {
        public ProgramType ProgramType;
        public MethodDefinition MainMethod;
        public string Path;
        public IBuildTarget BuildTarget;
        public TypeDefinition MainType;
        public HashSet<MethodDefinition> TargetMethods = new();
        public Dictionary<FieldReference, Var> Uniforms = new();
        public Dictionary<MethodDefinition, MethodBuilder> BuiltMethods = new();
    }
}
