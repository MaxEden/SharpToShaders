﻿using ConsoleApp1;
using Shader.Lib;
using Shader.Vectors;
using System.Diagnostics;

namespace _ConsoleApp1
{
    class StampShader : IShader
    {
        public Blend Blend => new(BlendOp.Zero, BlendOp.SrcColor);
        public bool ZWrite => false;
        public bool Cull => false;
        public bool Lighting => false;
        struct appdata_t
        {
            [POSITION] public float4 vertex;
            [COLOR] public float4 color;
            [TEXCOORD0] public float2 texcoord;
        };

        struct v2f
        {
            [POSITION] public float4 vertex;
            [COLOR] public fixed4 color;
            [TEXCOORD0] public half2 texcoord;
            [TEXCOORD1] public float3 worldPos;
        };

        string vertName => nameof(vert);

        v2f vert(appdata_t IN)
        {
            v2f OUT;
            OUT.worldPos = math.mul(Global.ObjectToWorld, IN.vertex);
            OUT.vertex = Global.ObjectToClipPos(IN.vertex);
            OUT.texcoord = IN.texcoord;
            OUT.color = IN.color;
            return OUT;
        }

        sampler2D _MainTex;
        sampler2D _PatternTex;
        fixed4 _ScalePattern;
        float _AlphaAdd;

        [SV_Target]
        fixed4 frag(v2f IN)
        {
            float2 uv = IN.worldPos.xy * _ScalePattern.xy;
            fixed4 pattern = Global.tex2D(_PatternTex, uv);
            fixed4 c = Global.tex2D(_MainTex, IN.texcoord);
            float l = Global.Luminance(c.rgb);
            l -= _ScalePattern.z;
            l *= _ScalePattern.w;
            c.rgb = math.saturate(l) + IN.color;
            c.a = c.a * (pattern.a + _AlphaAdd + (IN.color.a - 1));// add brackets to stack
            c = math.saturate(c);
            c.rgb *= c.a;
            c.rgb += (1 - c.a);
            c = math.saturate(c);
            return c;
        }
    }
}



