
using System.Numerics;

namespace _ConsoleApp1
{
    public struct UNITY_VERTEX_INPUT_INSTANCE_ID
    {

    }

    public struct UNITY_VERTEX_OUTPUT_STEREO
    {

    }

    public struct UNITY_FOG_COORDS
    {

    }

    internal class Unity
    {
        internal static float4 _WorldSpaceCameraPos;
        internal static int UNITY_PI;
        internal static float4 _Time;
        internal static Matrix4x4 UNITY_MATRIX_VP;
        internal static float4 _WorldSpaceLightPos0;
        internal static float4 _LightColor0;
        internal static Matrix4x4 unity_ObjectToWorld;

        internal static float3 ShadeSH9(float4 float4)
        {
            throw new NotImplementedException();
        }

        internal static float3 UnityObjectToWorldNormal(fixed3 normal)
        {
            throw new NotImplementedException();
        }

        internal static void UNITY_APPLY_FOG(ref UNITY_FOG_COORDS uNITY_FOG_COORDS, ref float4 result)
        {
            throw new NotImplementedException();
        }

        internal static void UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(ref UNITY_VERTEX_OUTPUT_STEREO uNITY_VERTEX_OUTPUT_STEREO)
        {
        }

        internal static void UNITY_SETUP_INSTANCE_ID(ref UNITY_VERTEX_INPUT_INSTANCE_ID uNITY_VERTEX_INPUT_INSTANCE_ID)
        {
            throw new NotImplementedException();
        }

        internal static void UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(ref UNITY_VERTEX_OUTPUT_STEREO uNITY_VERTEX_OUTPUT_STEREO)
        {
            throw new NotImplementedException();
        }

        internal static void UNITY_TRANSFER_FOG(ref UNITY_FOG_COORDS uNITY_FOG_COORDS, float4 vertex)
        {
            throw new NotImplementedException();
        }

        internal static void UNITY_TRANSFER_INSTANCE_ID(ref UNITY_VERTEX_INPUT_INSTANCE_ID uNITY_VERTEX_INPUT_INSTANCE_ID1, ref UNITY_VERTEX_INPUT_INSTANCE_ID uNITY_VERTEX_INPUT_INSTANCE_ID2)
        {
            throw new NotImplementedException();
        }
    }
}