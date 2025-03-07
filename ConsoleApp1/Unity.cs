
using System.Numerics;
using Shader.Lib;
using Shader.Vectors;

namespace _ConsoleApp1
{
    public struct VERTEX_INPUT_INSTANCE_ID
    {

    }

    public struct VERTEX_OUTPUT_STEREO
    {

    }

    public struct FOG_COORDS
    {

    }

    internal class Unity
    {
        internal static float4 _WorldSpaceCameraPos;
        internal static int PI;
        internal static float4 _Time;
        internal static Matrix4x4 MATRIX_VP;
        internal static float4 _WorldSpaceLightPos0;
        internal static float4 _LightColor0;
        internal static Matrix4x4 ObjectToWorld;

        internal static float3 ShadeSH9(float4 float4)
        {
            throw new NotImplementedException();
        }

        internal static float3 ObjectToWorldNormal(fixed3 normal)
        {
            throw new NotImplementedException();
        }

        internal static void APPLY_FOG(ref FOG_COORDS FOG_COORDS, ref float4 result)
        {
            throw new NotImplementedException();
        }

        internal static void INITIALIZE_VERTEX_OUTPUT_STEREO(ref VERTEX_OUTPUT_STEREO VERTEX_OUTPUT_STEREO)
        {
        }

        internal static void SETUP_INSTANCE_ID(ref VERTEX_INPUT_INSTANCE_ID VERTEX_INPUT_INSTANCE_ID)
        {
            throw new NotImplementedException();
        }

        internal static void SETUP_STEREO_EYE_INDEX_POST_VERTEX(ref VERTEX_OUTPUT_STEREO uNITY_VERTEX_OUTPUT_STEREO)
        {
            throw new NotImplementedException();
        }

        internal static void TRANSFER_FOG(ref FOG_COORDS uNITY_FOG_COORDS, float4 vertex)
        {
            throw new NotImplementedException();
        }

        internal static void TRANSFER_INSTANCE_ID(ref VERTEX_INPUT_INSTANCE_ID uNITY_VERTEX_INPUT_INSTANCE_ID1, ref VERTEX_INPUT_INSTANCE_ID uNITY_VERTEX_INPUT_INSTANCE_ID2)
        {
            throw new NotImplementedException();
        }

        public static float4 ObjectToClipPos(float4 inVertex)
        {
            throw new NotImplementedException();
        }

        public static fixed4 tex2D(sampler2D mainTex, float2 texcoord)
        {
            throw new NotImplementedException();
        }

        public static float Luminance(float3 rgb)
        {
            throw new NotImplementedException();
        }
    }
}