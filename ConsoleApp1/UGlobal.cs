
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

    internal class UGlobal
    {         

        internal static float3 ShadeSH9(float4 float4)
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
    }
}