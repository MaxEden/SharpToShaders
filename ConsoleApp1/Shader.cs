using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    using System;
    using System.Numerics;
    public class Shader
    {
        public struct App
        {
            public Vector3 pos;
            public Vector4 color;
        }

        public struct ToFrag
        {
            public Vector3 pos;
            public Vector4 color;
        }

        public float Time;

        public ToFrag Vert(App app)
        {
            var result = new ToFrag();
            var pos = app.pos;
            pos.Y += MathF.Sin(Time);
            pos = pos * 0.5f;
            pos = pos / 1;
            pos.Z = pos.X % 3;
            pos -= Vector3.Zero;
            result.pos = pos;
            return result;
        }

        public Vector4 Frag(ToFrag toFrag)
        {
            return Vector4.Zero;
        }
    }

    //public static class math
    //{
    //    public static Vector3 mul(Vector3 vec, float scal)
    //    {
    //        return vec * scal;
    //    }
    //}

    public interface IShader
    {
        public bool Cull => true;
        public bool Lighting => true;
        public bool ZWrite => true;
    }
}
