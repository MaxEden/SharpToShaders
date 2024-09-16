using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _ConsoleApp1
{
    public struct float3
    {
        public float x, y, z;

        public float3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float3 xyz => this;

        public static float3 operator *(float3 vector, float scalar)
        {
            return new float3(
                vector.x * scalar,
                vector.y * scalar,
                vector.z * scalar
                );
        }

        public static float3 operator *(float scalar, float3 vector)
        {
            return vector * scalar;
        }

        public static float3 operator /(float3 vector, float scalar)
        {
            return new float3(
                vector.x / scalar,
                vector.y / scalar,
                vector.z / scalar
                );
        }

        public static float3 operator +(float3 vector, float3 scalar)
        {
            return new float3(
                vector.x + scalar.x,
                vector.y + scalar.y,
                vector.z + scalar.z
                );
        }

        public static float3 operator -(float3 vector, float3 scalar)
        {
            return new float3(
                vector.x - scalar.x,
                vector.y - scalar.y,
                vector.z - scalar.z
                );
        }

        public static float3 operator -(float3 vector)
        {
            return new float3(
                -vector.x,
                -vector.y,
                -vector.z
                );
        }
    }

    public struct float4
    {
        public float x, y, z, w;

        public float4(float3 xyz, float w) : this()
        {
            x = xyz.x;
            y = xyz.y;
            z = xyz.z;
            this.w = w;
        }

        public float4(float x, float y, float z, float w) : this()
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public float3 xyz
        {
            get => new float3(x, y, z);
            
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
            }
        }
    
        public static float4 operator *(float scalar, float4 vector)
        {
            return vector * scalar;
        }

        public static float4 operator *(float4 vector, float scalar)
        {
            return new float4(
                vector.x * scalar,
                vector.y * scalar,
                vector.z * scalar,
                vector.w * scalar
                );
        }

        public static float4 operator *(float4 vector, float4 scalar)
        {
            return new float4(
                vector.x * scalar.x,
                vector.y * scalar.y,
                vector.z * scalar.z,
                vector.w * scalar.w
                );
        }

        public static float4 operator *(float4 vector, fixed4 scalar)
        {
            return new float4(
                vector.x * scalar.x,
                vector.y * scalar.y,
                vector.z * scalar.z,
                vector.w * scalar.w
                );
        }
        public static float4 operator /(float4 vector, float scalar)
        {
            return new float4(
                vector.x / scalar,
                vector.y / scalar,
                vector.z / scalar,
                vector.w / scalar
                );
        }
        public static float4 operator +(float4 vector, float4 scalar)
        {
            return new float4(
                vector.x + scalar.x,
                vector.y + scalar.y,
                vector.z + scalar.z,
                vector.w + scalar.w
                );
        }
        public static float4 operator -(float4 vector, float4 scalar)
        {
            return new float4(
                vector.x - scalar.x,
                vector.y - scalar.y,
                vector.z - scalar.z,
                vector.w - scalar.w
                );
        }

        public static float4 operator -(float4 vector)
        {
            return new float4(
                -vector.x,
                -vector.y,
                -vector.z,
                -vector.w
                );
        }

        public static implicit operator float4(float3 v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator float3(float4 v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator float4(fixed4 v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator fixed4(float4 v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator float4(float v)
        {
            throw new NotImplementedException();
        }
    }

    public struct fixed3
    {
        private float x;
        private float y;
        private float z;

        public fixed3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator fixed3(float3 v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator fixed3(fixed4 v)
        {
            throw new NotImplementedException();
        }

        public static fixed3 operator *(fixed3 vector, float scalar)
        {
            return new fixed3(
                vector.x * scalar,
                vector.y * scalar,
                vector.z * scalar
                );
        }

        public static fixed3 operator *(float scalar, fixed3 vector)
        {
            return new fixed3(
                vector.x * scalar,
                vector.y * scalar,
                vector.z * scalar
                );
        }
    }

    public struct fixed4
    {
        public float x, y, z, w;

        public float r => x;
        public float g => y;
        public float b => z;
        public float a => w;

        public fixed4(float3 xyz, float w) : this()
        {
            x = xyz.x;
            y = xyz.y;
            z = xyz.z;
            this.w = w;
        }

        public fixed4(float x, float y, float z, float w) : this()
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public fixed3 xyz => new fixed3(x, y, z);

        public static fixed4 operator *(float scalar, fixed4 vector)
        {
            return vector * scalar;
        }

        public static fixed4 operator *(fixed4 vector, float scalar)
        {
            return new fixed4(
                vector.x * scalar,
                vector.y * scalar,
                vector.z * scalar,
                vector.w * scalar
                );
        }
    }

    public static class math
    {
        internal static float distance(float3 centerPos, float3 xyz)
        {
            throw new NotImplementedException();
        }

        internal static float dot(float3 viewDir, float3 float3)
        {
            throw new NotImplementedException();
        }

        internal static float3 lerp(float3 worldPos, float3 worldPos2, float t)
        {
            throw new NotImplementedException();
        }

        internal static float4 lerp(float4 worldPos, float4 worldPos2, float t)
        {
            throw new NotImplementedException();
        }

        internal static float lerp(float v1, float v2, float wind)
        {
            throw new NotImplementedException();
        }

        internal static fixed4 lerp(fixed4 color, fixed4 color2, float t)
        {
            throw new NotImplementedException();
        }

        internal static float4 max(float4 v, float4 float4)
        {
            throw new NotImplementedException();
        }

        internal static float4 mul(Matrix4x4 unity_ObjectToWorld, float4 float4)
        {
            throw new NotImplementedException();
        }

        internal static float3 normalize(float3 value)
        {
            throw new NotImplementedException();
        }

        internal static float3 normalize(fixed3 fixed3)
        {
            throw new NotImplementedException();
        }

        internal static float saturate(float t)
        {
            throw new NotImplementedException();
        }

        internal static float4 saturate(float4 sunColor)
        {
            throw new NotImplementedException();
        }

        internal static float sin(float value)
        {
            throw new NotImplementedException();
        }

        internal static void sincos(float f, out float sr, out float cr)
        {
            throw new NotImplementedException();
        }
    }


    [System.AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    sealed class POSITIONAttribute : Attribute {}

    [System.AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    sealed class POSITION1Attribute : Attribute { }


    [System.AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    sealed class TEXCOORD0Attribute : Attribute { }

    [System.AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    sealed class TEXCOORD1Attribute : Attribute { }


    [System.AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)] 
    sealed class NORMALAttribute : Attribute { }

    [System.AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    sealed class TANGENTAttribute : Attribute { }


    [System.AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    sealed class COLORAttribute : Attribute { }

    [System.AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    sealed class SV_IsFrontFaceAttribute : Attribute { }

    [System.AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    sealed class SV_TargetAttribute : Attribute { }


    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    sealed class AssumeUniformScalingAttribute : Attribute { }


}
