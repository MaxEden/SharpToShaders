

## Sharp to Shaders

Yet another C# to multi target shader transpiler.
Inspired by Unity's Shader Lab language.
Made with **Mono.Cecil**, [**Lemon**](https://github.com/MaxEden/Lemon) and of course❤️ 

### Why
Extensive tools, autocomplete, refactoring tools, code reuse etc. 
C# is a relatively pleasant language which although lacks specific features such as swizzling operators, in general is pretty suitable for shaders. It also allows you to write your layout structures once and catch any mismatch at compile time.

### How

Create a class that implements **IShader**.  
***Uniforms*** - class fields  
***Vertex program*** - any method which name starts with "Vert"  
***Fragment program*** - any method which name starts with "Frag"  
***Semantics*** - common attributes  
Everything else is basically HLSL

```c#
    class TestStampShader : IShader
    {        
        sampler2D _PatternTex;
        float4 _ScalePattern;
        ...
        
        struct vertex_layout
        {
            [POSITION] public float4 vertex;
            [COLOR] public float4 color;
            [TEXCOORD0] public float2 texcoord;
        };
    
        struct v2f
        {
            [POSITION] public float4 vertex;
            ...
        };
        
        v2f vert(vertex_layout IN)
        {
            v2f OUT;
            OUT.worldPos = math.mul(Global.ObjectToWorld, IN.vertex);
            ...
            return OUT;
        }
       
        float4 frag(v2f IN)
        {
            float2 uv = IN.worldPos.xy * _ScalePattern.xy;
            float4 pattern = Global.tex2D(_PatternTex, uv);
            ...
            c = math.saturate(c);
            return c;
        }
    
        float local_func(float4 color)
        {
           return (color.r + color.g + color.b)/3;
        }
    }
```

### Code Reuse

You can call any other static methods outside your main shader class.
Any field that is used in this static method will be added to the uniforms. That allows you to create helper functions that reuse the same matrices and any other global values such as lighting inputs. The methods itself will be added before the main method in final output.

### Targets
Currently there are three build targets: **hlsl**, **glsl** and **gles20**.
To implement a new output target you need to implement **IBuildTarget** interface, which is basically a huge mapper between C# fields and calls and the target output. 

### What else
Generics? Virtual calls? Not right now but eh why not? PRs are welcome!