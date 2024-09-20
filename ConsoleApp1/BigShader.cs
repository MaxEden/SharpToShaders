using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using _ConsoleApp1;
using GenMath;

namespace Shaders
{
    struct ShaderFeature
    {
        public static implicit operator bool(ShaderFeature shaderFeature)
        {
            return true;
        }
    }

    [AssumeUniformScaling]
    internal class BigShader : IShader
    {
        //#pragma multi_compile_fog
        //#pragma multi_compile_instancing
        ShaderFeature _UNITY_LIGHTING;
        ShaderFeature _SMOOTH_FADE;

        //#pragma vertex vertGrass
        //#pragma fragment fragGrass

        struct appdataGrass
        {
            [POSITION] public float3 vertex;
            [TEXCOORD0] public float4 uv;
            [NORMAL] public fixed3 normal;
            [TANGENT] public fixed3 normal2;
            [COLOR] public fixed4 color;
            [TEXCOORD1] public fixed4 color2;

            public UNITY_VERTEX_INPUT_INSTANCE_ID UNITY_VERTEX_INPUT_INSTANCE_ID;
        };

        struct v2fGrass
        {
            [POSITION] public float4 vertex;
            [COLOR] public fixed4 color;
            [NORMAL] public fixed3 normal;
            [POSITION1] public float3 worldPos;

            public UNITY_VERTEX_INPUT_INSTANCE_ID UNITY_VERTEX_INPUT_INSTANCE_ID;
            public UNITY_VERTEX_OUTPUT_STEREO UNITY_VERTEX_OUTPUT_STEREO;
            public UNITY_FOG_COORDS UNITY_FOG_COORDS;
        };

        fixed4 _ColorLight;
        fixed4 _ColorShadow;
        float _Wind;
        float _Softness;
        float _DistMin;
        float _DistMax;
        fixed4 _DebugColor;

        private fixed4[] Positions;

        v2fGrass vertGrass(appdataGrass v)
        {
            v2fGrass o;

            Unity.UNITY_SETUP_INSTANCE_ID(ref v.UNITY_VERTEX_INPUT_INSTANCE_ID);
            Unity.UNITY_TRANSFER_INSTANCE_ID(ref v.UNITY_VERTEX_INPUT_INSTANCE_ID, ref o.UNITY_VERTEX_INPUT_INSTANCE_ID);
            Unity.UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(ref o.UNITY_VERTEX_OUTPUT_STEREO);


            float3 worldPos = math.mul(Unity.unity_ObjectToWorld, new float4(v.vertex.xyz, 1.0f));
            float3 normal = Unity.UnityObjectToWorldNormal(v.normal);

            float t = 0;

            if (_SMOOTH_FADE)
            {

                float3 centerPos = math.mul(Unity.unity_ObjectToWorld, new float4(0, 0, 0, 1.0f));
                float dist = math.distance(centerPos, Unity._WorldSpaceCameraPos.xyz);
                t = (dist - _DistMin) / (_DistMax - _DistMin);
                t = math.saturate(t);

                float3 worldPos2 = math.mul(Unity.unity_ObjectToWorld, new float4(v.uv.xyz, 1.0f));
                float3 normal2 = Unity.UnityObjectToWorldNormal(v.normal2);

                worldPos = math.lerp(worldPos, worldPos2, t);
                normal = math.lerp(normal, normal2, t);

            }

            o.normal = normal;

            float intensOr = (1 - v.color.a);

            float intens = intensOr;

            float val = math.lerp(0.01f, 0.5f, _Wind); //0.01 - 0.5
            float _Wavelength = 3.3f; //7;
            float _Speed = 15;
            float _Amplitude = 0.2f;

            intens *= val;
            _Speed *= val;

            float windForce = 0.5f + math.sin(0.75f * Unity._Time.y);
            float4 wave = new float4(0.3f, -0.15f, 0.31f, 0) * windForce;
            float tShift = math.saturate(windForce - 0.75f) * 0.01f;


            float3 p = worldPos;
            float k = 2 * Unity.UNITY_PI / _Wavelength;
            float f = k * (p.x - _Speed * (Unity._Time.y + v.uv.w * tShift)); //

            float sr;
            float cr;
            math.sincos(f, out sr, out cr);
            p.x += _Amplitude * cr * intens;
            p.y += _Amplitude * sr * intens;

            p += wave.xyz * intensOr * 1.3f / 0.4f * val;
            worldPos = p;

            o.worldPos = worldPos;
            o.vertex = math.mul(Unity.UNITY_MATRIX_VP, new float4(worldPos.xyz, 1.0f));

            //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            //o.uv = v.uv;

            if (_SMOOTH_FADE)
            {
                o.color = math.lerp(v.color, v.color2, t);
            }
            else
            {
                o.color = v.color;
            }
            //o.color =v.uv+0.5;

            //if(tShift>0) o.color = float4(1,0,0,1);                 

            Unity.UNITY_TRANSFER_FOG(ref o.UNITY_FOG_COORDS, o.vertex);

            //o.color.w = t;
            return o;
        }

        [SV_Target]
        fixed4 fragGrass(v2fGrass i, [SV_IsFrontFace] bool facing)
        {

            bool v = true;
            int a = 1;
            int b = 2;
            while (v)
            {
                if (a>b)
                {
                    a = b;
                    if (b>0)
                    {
                        return default;
                    }
                }
                
                if(a>10) break;
            }

            //return default;

            Unity.UNITY_SETUP_INSTANCE_ID(ref i.UNITY_VERTEX_INPUT_INSTANCE_ID);
            Unity.UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(ref i.UNITY_VERTEX_OUTPUT_STEREO);

            //return i.normal.xyzz;

            float sum = 0;
            for (int j = 0; j < 10; j++)
            {
                var pos = Positions[j];
                sum += pos.x;
            }

            sum += 12345;


            float sum2 = 0;

            int k = 0;
            while (k < 10)
            {
                k /= 2;
                var t = k + 1;
                if (t > 15) break;

                var pos = Positions[k];
                sum2 += pos.x;

                k++;

                if (facing)
                {
                    if (sum > 15)
                    {
                        for (int j = 0; j < 13; j++)
                        {
                            sum += j * 2;
                        }
                    }

                    if (sum2 < sum)
                    {
                        return default;
                    }
                }
            }

            sum2 += 5678;

            //return default;


            var dir = facing ? Unity._WorldSpaceLightPos0.xyz * 2 : i.worldPos.xyz - Unity._WorldSpaceCameraPos.xyz;

            float3 norm = math.normalize((facing ? 1.0f : -1.0f) * i.normal);

            float3 lightDir = -math.normalize(Unity._WorldSpaceLightPos0.xyz);
            float3 viewDir = math.normalize(i.worldPos.xyz - Unity._WorldSpaceCameraPos.xyz);

            float flare = math.saturate(math.dot(viewDir, -lightDir));
            flare = flare * flare * flare;
            float transl = math.saturate(1.5f * (1 - i.color.a));
            float fnorm = math.saturate(math.dot(norm, lightDir) - 0.5f);

            float sun = math.dot(norm, -lightDir);
            float sunL = (0.5f + 0.5f * sun);
            sun = math.saturate(sun);
            sun = math.lerp(sun, sunL, _Softness);

            float3 coeff = new float3(1, 0.75f, 0);
            float through = math.saturate(coeff.x * flare + coeff.y * fnorm) * transl;

            float4 colorLight;
            float4 colorShadow;

            if (_UNITY_LIGHTING)
            {
                colorLight = Unity._LightColor0;
                colorShadow = 1; //float4(unity_SHAr.w, unity_SHAg.w, unity_SHAb.w, 1);
                colorShadow.xyz = Unity.ShadeSH9(new float4(norm, 1.0f));

                //float4 res = float4(ShadeSH9(float4(norm, 1.0)),1);
                //return res;
            }
            else
            {
                colorLight = _ColorLight;
                colorShadow = _ColorShadow;
            }

            float4 lcolor = (colorShadow + colorLight);
            float4 sunColor = math.lerp(colorShadow, lcolor, sun);

            //return sunColor;

            if (_UNITY_LIGHTING)
            {
                sunColor = math.saturate(sunColor);
            }

            //i.color = float4(1,1,1,1);

            float4 color = i.color * sunColor;


            if (_UNITY_LIGHTING)
            {
                color += 0.11f * math.max(0, sunColor - 1);
            }

            float4 throughColor = 1.25f * i.color * colorLight + colorLight * 0.25f;
            float4 result = math.lerp(color, throughColor, through);

            result = math.saturate(result);

            //result = color;

            Unity.UNITY_APPLY_FOG(ref i.UNITY_FOG_COORDS, ref result);
            return result;//*_DebugColor;//*i.color.w;

            return default;
        }

    }
}
