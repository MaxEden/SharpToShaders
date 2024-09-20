

using System;
using System.Runtime.InteropServices;
namespace GenMath{
[StructLayout(LayoutKind.Explicit)]
public struct  float2
{
[FieldOffset(0)] public float x;
[FieldOffset(4)] public float y;
[FieldOffset(0)] public float r;
[FieldOffset(4)] public float g;
public float2(float x,float y)
{
this.r = default;
this.g = default;

this.x = x;
this.y = y;
}
public float2 xy {
get => new float2();
set{ }
 }
public float2 rg {
get => new float2();
set{ }
 }
public static float2 operator +(float2 vector, float scalar)
{
return new float2(
vector.x + scalar,
vector.y + scalar
);
}
public static float2 operator +(float scalar, float2 vector) => vector + scalar;
public static float2 operator +(float2 vector, float2 scalar)
{
return new float2(
vector.x + scalar.x,
vector.y + scalar.y
);
}
public static float2 operator +(float2 vector, fixed2 scalar) { return default;}
public static float2 operator +(float2 vector, half2 scalar) { return default;}
public static float2 operator -(float2 vector, float scalar)
{
return new float2(
vector.x - scalar,
vector.y - scalar
);
}
public static float2 operator -(float scalar, float2 vector) => vector - scalar;
public static float2 operator -(float2 vector, float2 scalar)
{
return new float2(
vector.x - scalar.x,
vector.y - scalar.y
);
}
public static float2 operator -(float2 vector, fixed2 scalar) { return default;}
public static float2 operator -(float2 vector, half2 scalar) { return default;}
public static float2 operator *(float2 vector, float scalar)
{
return new float2(
vector.x * scalar,
vector.y * scalar
);
}
public static float2 operator *(float scalar, float2 vector) => vector * scalar;
public static float2 operator *(float2 vector, float2 scalar)
{
return new float2(
vector.x * scalar.x,
vector.y * scalar.y
);
}
public static float2 operator *(float2 vector, fixed2 scalar) { return default;}
public static float2 operator *(float2 vector, half2 scalar) { return default;}
public static float2 operator /(float2 vector, float scalar)
{
return new float2(
vector.x / scalar,
vector.y / scalar
);
}
public static float2 operator /(float scalar, float2 vector) => vector / scalar;
public static float2 operator /(float2 vector, float2 scalar)
{
return new float2(
vector.x / scalar.x,
vector.y / scalar.y
);
}
public static float2 operator /(float2 vector, fixed2 scalar) { return default;}
public static float2 operator /(float2 vector, half2 scalar) { return default;}
public static float2 operator %(float2 vector, float scalar)
{
return new float2(
vector.x % scalar,
vector.y % scalar
);
}
public static float2 operator %(float scalar, float2 vector) => vector % scalar;
public static float2 operator %(float2 vector, float2 scalar)
{
return new float2(
vector.x % scalar.x,
vector.y % scalar.y
);
}
public static float2 operator %(float2 vector, fixed2 scalar) { return default;}
public static float2 operator %(float2 vector, half2 scalar) { return default;}
public static float2 operator -(float2 vector)
{
return new float2(
-vector.x,
-vector.y
);
}
public static implicit operator fixed2(float2 v) { throw new NotImplementedException(); }
public static implicit operator half2(float2 v) { throw new NotImplementedException(); }
public static implicit operator float2(float v) { throw new NotImplementedException(); }
}
[StructLayout(LayoutKind.Explicit)]
public struct  float3
{
[FieldOffset(0)] public float x;
[FieldOffset(4)] public float y;
[FieldOffset(8)] public float z;
[FieldOffset(0)] public float r;
[FieldOffset(4)] public float g;
[FieldOffset(8)] public float b;
public float3(float x,float y,float z)
{
this.xy = default;
this.rg = default;
this.r = default;
this.g = default;
this.b = default;

this.x = x;
this.y = y;
this.z = z;
}
public float3(float2 xy, float z)
{
this.xy = default;
this.rg = default;
this.r = default;
this.g = default;
this.b = default;

this.x = xy.x;
this.y = xy.y;
this.z = z;
}
[FieldOffset(0)]
public float2 xy;
public float3 xyz {
get => new float3();
set{ }
 }
[FieldOffset(0)]
public float2 rg;
public float3 rgb {
get => new float3();
set{ }
 }
public static float3 operator +(float3 vector, float scalar)
{
return new float3(
vector.x + scalar,
vector.y + scalar,
vector.z + scalar
);
}
public static float3 operator +(float scalar, float3 vector) => vector + scalar;
public static float3 operator +(float3 vector, float3 scalar)
{
return new float3(
vector.x + scalar.x,
vector.y + scalar.y,
vector.z + scalar.z
);
}
public static float3 operator +(float3 vector, fixed3 scalar) { return default;}
public static float3 operator +(float3 vector, half3 scalar) { return default;}
public static float3 operator -(float3 vector, float scalar)
{
return new float3(
vector.x - scalar,
vector.y - scalar,
vector.z - scalar
);
}
public static float3 operator -(float scalar, float3 vector) => vector - scalar;
public static float3 operator -(float3 vector, float3 scalar)
{
return new float3(
vector.x - scalar.x,
vector.y - scalar.y,
vector.z - scalar.z
);
}
public static float3 operator -(float3 vector, fixed3 scalar) { return default;}
public static float3 operator -(float3 vector, half3 scalar) { return default;}
public static float3 operator *(float3 vector, float scalar)
{
return new float3(
vector.x * scalar,
vector.y * scalar,
vector.z * scalar
);
}
public static float3 operator *(float scalar, float3 vector) => vector * scalar;
public static float3 operator *(float3 vector, float3 scalar)
{
return new float3(
vector.x * scalar.x,
vector.y * scalar.y,
vector.z * scalar.z
);
}
public static float3 operator *(float3 vector, fixed3 scalar) { return default;}
public static float3 operator *(float3 vector, half3 scalar) { return default;}
public static float3 operator /(float3 vector, float scalar)
{
return new float3(
vector.x / scalar,
vector.y / scalar,
vector.z / scalar
);
}
public static float3 operator /(float scalar, float3 vector) => vector / scalar;
public static float3 operator /(float3 vector, float3 scalar)
{
return new float3(
vector.x / scalar.x,
vector.y / scalar.y,
vector.z / scalar.z
);
}
public static float3 operator /(float3 vector, fixed3 scalar) { return default;}
public static float3 operator /(float3 vector, half3 scalar) { return default;}
public static float3 operator %(float3 vector, float scalar)
{
return new float3(
vector.x % scalar,
vector.y % scalar,
vector.z % scalar
);
}
public static float3 operator %(float scalar, float3 vector) => vector % scalar;
public static float3 operator %(float3 vector, float3 scalar)
{
return new float3(
vector.x % scalar.x,
vector.y % scalar.y,
vector.z % scalar.z
);
}
public static float3 operator %(float3 vector, fixed3 scalar) { return default;}
public static float3 operator %(float3 vector, half3 scalar) { return default;}
public static float3 operator -(float3 vector)
{
return new float3(
-vector.x,
-vector.y,
-vector.z
);
}
public static implicit operator float2(float3 v) { throw new NotImplementedException(); }
public static implicit operator fixed3(float3 v) { throw new NotImplementedException(); }
public static implicit operator half3(float3 v) { throw new NotImplementedException(); }
public static implicit operator float3(float v) { throw new NotImplementedException(); }
}
[StructLayout(LayoutKind.Explicit)]
public struct  float4
{
[FieldOffset(0)] public float x;
[FieldOffset(4)] public float y;
[FieldOffset(8)] public float z;
[FieldOffset(12)] public float w;
[FieldOffset(0)] public float r;
[FieldOffset(4)] public float g;
[FieldOffset(8)] public float b;
[FieldOffset(12)] public float a;
public float4(float x,float y,float z,float w)
{
this.xy = default;
this.xyz = default;
this.rg = default;
this.rgb = default;
this.r = default;
this.g = default;
this.b = default;
this.a = default;

this.x = x;
this.y = y;
this.z = z;
this.w = w;
}
public float4(float3 xyz, float w)
{
this.xy = default;
this.xyz = default;
this.rg = default;
this.rgb = default;
this.r = default;
this.g = default;
this.b = default;
this.a = default;

this.x = xyz.x;
this.y = xyz.y;
this.z = xyz.z;
this.w = w;
}
[FieldOffset(0)]
public float2 xy;
[FieldOffset(0)]
public float3 xyz;
public float4 xyzw {
get => new float4();
set{ }
 }
[FieldOffset(0)]
public float2 rg;
[FieldOffset(0)]
public float3 rgb;
public float4 rgba {
get => new float4();
set{ }
 }
public static float4 operator +(float4 vector, float scalar)
{
return new float4(
vector.x + scalar,
vector.y + scalar,
vector.z + scalar,
vector.w + scalar
);
}
public static float4 operator +(float scalar, float4 vector) => vector + scalar;
public static float4 operator +(float4 vector, float4 scalar)
{
return new float4(
vector.x + scalar.x,
vector.y + scalar.y,
vector.z + scalar.z,
vector.w + scalar.w
);
}
public static float4 operator +(float4 vector, fixed4 scalar) { return default;}
public static float4 operator +(float4 vector, half4 scalar) { return default;}
public static float4 operator -(float4 vector, float scalar)
{
return new float4(
vector.x - scalar,
vector.y - scalar,
vector.z - scalar,
vector.w - scalar
);
}
public static float4 operator -(float scalar, float4 vector) => vector - scalar;
public static float4 operator -(float4 vector, float4 scalar)
{
return new float4(
vector.x - scalar.x,
vector.y - scalar.y,
vector.z - scalar.z,
vector.w - scalar.w
);
}
public static float4 operator -(float4 vector, fixed4 scalar) { return default;}
public static float4 operator -(float4 vector, half4 scalar) { return default;}
public static float4 operator *(float4 vector, float scalar)
{
return new float4(
vector.x * scalar,
vector.y * scalar,
vector.z * scalar,
vector.w * scalar
);
}
public static float4 operator *(float scalar, float4 vector) => vector * scalar;
public static float4 operator *(float4 vector, float4 scalar)
{
return new float4(
vector.x * scalar.x,
vector.y * scalar.y,
vector.z * scalar.z,
vector.w * scalar.w
);
}
public static float4 operator *(float4 vector, fixed4 scalar) { return default;}
public static float4 operator *(float4 vector, half4 scalar) { return default;}
public static float4 operator /(float4 vector, float scalar)
{
return new float4(
vector.x / scalar,
vector.y / scalar,
vector.z / scalar,
vector.w / scalar
);
}
public static float4 operator /(float scalar, float4 vector) => vector / scalar;
public static float4 operator /(float4 vector, float4 scalar)
{
return new float4(
vector.x / scalar.x,
vector.y / scalar.y,
vector.z / scalar.z,
vector.w / scalar.w
);
}
public static float4 operator /(float4 vector, fixed4 scalar) { return default;}
public static float4 operator /(float4 vector, half4 scalar) { return default;}
public static float4 operator %(float4 vector, float scalar)
{
return new float4(
vector.x % scalar,
vector.y % scalar,
vector.z % scalar,
vector.w % scalar
);
}
public static float4 operator %(float scalar, float4 vector) => vector % scalar;
public static float4 operator %(float4 vector, float4 scalar)
{
return new float4(
vector.x % scalar.x,
vector.y % scalar.y,
vector.z % scalar.z,
vector.w % scalar.w
);
}
public static float4 operator %(float4 vector, fixed4 scalar) { return default;}
public static float4 operator %(float4 vector, half4 scalar) { return default;}
public static float4 operator -(float4 vector)
{
return new float4(
-vector.x,
-vector.y,
-vector.z,
-vector.w
);
}
public static implicit operator float2(float4 v) { throw new NotImplementedException(); }
public static implicit operator float3(float4 v) { throw new NotImplementedException(); }
public static implicit operator fixed4(float4 v) { throw new NotImplementedException(); }
public static implicit operator half4(float4 v) { throw new NotImplementedException(); }
public static implicit operator float4(float v) { throw new NotImplementedException(); }
}
[StructLayout(LayoutKind.Explicit)]
public struct  fixed2
{
[FieldOffset(0)] public float x;
[FieldOffset(4)] public float y;
[FieldOffset(0)] public float r;
[FieldOffset(4)] public float g;
public fixed2(float x,float y)
{
this.r = default;
this.g = default;

this.x = x;
this.y = y;
}
public fixed2 xy {
get => new fixed2();
set{ }
 }
public fixed2 rg {
get => new fixed2();
set{ }
 }
public static fixed2 operator +(fixed2 vector, float scalar)
{
return new fixed2(
vector.x + scalar,
vector.y + scalar
);
}
public static fixed2 operator +(float scalar, fixed2 vector) => vector + scalar;
public static fixed2 operator +(fixed2 vector, fixed2 scalar)
{
return new fixed2(
vector.x + scalar.x,
vector.y + scalar.y
);
}
public static fixed2 operator +(fixed2 vector, float2 scalar) { return default;}
public static fixed2 operator +(fixed2 vector, half2 scalar) { return default;}
public static fixed2 operator -(fixed2 vector, float scalar)
{
return new fixed2(
vector.x - scalar,
vector.y - scalar
);
}
public static fixed2 operator -(float scalar, fixed2 vector) => vector - scalar;
public static fixed2 operator -(fixed2 vector, fixed2 scalar)
{
return new fixed2(
vector.x - scalar.x,
vector.y - scalar.y
);
}
public static fixed2 operator -(fixed2 vector, float2 scalar) { return default;}
public static fixed2 operator -(fixed2 vector, half2 scalar) { return default;}
public static fixed2 operator *(fixed2 vector, float scalar)
{
return new fixed2(
vector.x * scalar,
vector.y * scalar
);
}
public static fixed2 operator *(float scalar, fixed2 vector) => vector * scalar;
public static fixed2 operator *(fixed2 vector, fixed2 scalar)
{
return new fixed2(
vector.x * scalar.x,
vector.y * scalar.y
);
}
public static fixed2 operator *(fixed2 vector, float2 scalar) { return default;}
public static fixed2 operator *(fixed2 vector, half2 scalar) { return default;}
public static fixed2 operator /(fixed2 vector, float scalar)
{
return new fixed2(
vector.x / scalar,
vector.y / scalar
);
}
public static fixed2 operator /(float scalar, fixed2 vector) => vector / scalar;
public static fixed2 operator /(fixed2 vector, fixed2 scalar)
{
return new fixed2(
vector.x / scalar.x,
vector.y / scalar.y
);
}
public static fixed2 operator /(fixed2 vector, float2 scalar) { return default;}
public static fixed2 operator /(fixed2 vector, half2 scalar) { return default;}
public static fixed2 operator %(fixed2 vector, float scalar)
{
return new fixed2(
vector.x % scalar,
vector.y % scalar
);
}
public static fixed2 operator %(float scalar, fixed2 vector) => vector % scalar;
public static fixed2 operator %(fixed2 vector, fixed2 scalar)
{
return new fixed2(
vector.x % scalar.x,
vector.y % scalar.y
);
}
public static fixed2 operator %(fixed2 vector, float2 scalar) { return default;}
public static fixed2 operator %(fixed2 vector, half2 scalar) { return default;}
public static fixed2 operator -(fixed2 vector)
{
return new fixed2(
-vector.x,
-vector.y
);
}
public static implicit operator float2(fixed2 v) { throw new NotImplementedException(); }
public static implicit operator half2(fixed2 v) { throw new NotImplementedException(); }
public static implicit operator fixed2(float v) { throw new NotImplementedException(); }
}
[StructLayout(LayoutKind.Explicit)]
public struct  fixed3
{
[FieldOffset(0)] public float x;
[FieldOffset(4)] public float y;
[FieldOffset(8)] public float z;
[FieldOffset(0)] public float r;
[FieldOffset(4)] public float g;
[FieldOffset(8)] public float b;
public fixed3(float x,float y,float z)
{
this.xy = default;
this.rg = default;
this.r = default;
this.g = default;
this.b = default;

this.x = x;
this.y = y;
this.z = z;
}
public fixed3(fixed2 xy, float z)
{
this.xy = default;
this.rg = default;
this.r = default;
this.g = default;
this.b = default;

this.x = xy.x;
this.y = xy.y;
this.z = z;
}
[FieldOffset(0)]
public fixed2 xy;
public fixed3 xyz {
get => new fixed3();
set{ }
 }
[FieldOffset(0)]
public fixed2 rg;
public fixed3 rgb {
get => new fixed3();
set{ }
 }
public static fixed3 operator +(fixed3 vector, float scalar)
{
return new fixed3(
vector.x + scalar,
vector.y + scalar,
vector.z + scalar
);
}
public static fixed3 operator +(float scalar, fixed3 vector) => vector + scalar;
public static fixed3 operator +(fixed3 vector, fixed3 scalar)
{
return new fixed3(
vector.x + scalar.x,
vector.y + scalar.y,
vector.z + scalar.z
);
}
public static fixed3 operator +(fixed3 vector, float3 scalar) { return default;}
public static fixed3 operator +(fixed3 vector, half3 scalar) { return default;}
public static fixed3 operator -(fixed3 vector, float scalar)
{
return new fixed3(
vector.x - scalar,
vector.y - scalar,
vector.z - scalar
);
}
public static fixed3 operator -(float scalar, fixed3 vector) => vector - scalar;
public static fixed3 operator -(fixed3 vector, fixed3 scalar)
{
return new fixed3(
vector.x - scalar.x,
vector.y - scalar.y,
vector.z - scalar.z
);
}
public static fixed3 operator -(fixed3 vector, float3 scalar) { return default;}
public static fixed3 operator -(fixed3 vector, half3 scalar) { return default;}
public static fixed3 operator *(fixed3 vector, float scalar)
{
return new fixed3(
vector.x * scalar,
vector.y * scalar,
vector.z * scalar
);
}
public static fixed3 operator *(float scalar, fixed3 vector) => vector * scalar;
public static fixed3 operator *(fixed3 vector, fixed3 scalar)
{
return new fixed3(
vector.x * scalar.x,
vector.y * scalar.y,
vector.z * scalar.z
);
}
public static fixed3 operator *(fixed3 vector, float3 scalar) { return default;}
public static fixed3 operator *(fixed3 vector, half3 scalar) { return default;}
public static fixed3 operator /(fixed3 vector, float scalar)
{
return new fixed3(
vector.x / scalar,
vector.y / scalar,
vector.z / scalar
);
}
public static fixed3 operator /(float scalar, fixed3 vector) => vector / scalar;
public static fixed3 operator /(fixed3 vector, fixed3 scalar)
{
return new fixed3(
vector.x / scalar.x,
vector.y / scalar.y,
vector.z / scalar.z
);
}
public static fixed3 operator /(fixed3 vector, float3 scalar) { return default;}
public static fixed3 operator /(fixed3 vector, half3 scalar) { return default;}
public static fixed3 operator %(fixed3 vector, float scalar)
{
return new fixed3(
vector.x % scalar,
vector.y % scalar,
vector.z % scalar
);
}
public static fixed3 operator %(float scalar, fixed3 vector) => vector % scalar;
public static fixed3 operator %(fixed3 vector, fixed3 scalar)
{
return new fixed3(
vector.x % scalar.x,
vector.y % scalar.y,
vector.z % scalar.z
);
}
public static fixed3 operator %(fixed3 vector, float3 scalar) { return default;}
public static fixed3 operator %(fixed3 vector, half3 scalar) { return default;}
public static fixed3 operator -(fixed3 vector)
{
return new fixed3(
-vector.x,
-vector.y,
-vector.z
);
}
public static implicit operator fixed2(fixed3 v) { throw new NotImplementedException(); }
public static implicit operator float3(fixed3 v) { throw new NotImplementedException(); }
public static implicit operator half3(fixed3 v) { throw new NotImplementedException(); }
public static implicit operator fixed3(float v) { throw new NotImplementedException(); }
}
[StructLayout(LayoutKind.Explicit)]
public struct  fixed4
{
[FieldOffset(0)] public float x;
[FieldOffset(4)] public float y;
[FieldOffset(8)] public float z;
[FieldOffset(12)] public float w;
[FieldOffset(0)] public float r;
[FieldOffset(4)] public float g;
[FieldOffset(8)] public float b;
[FieldOffset(12)] public float a;
public fixed4(float x,float y,float z,float w)
{
this.xy = default;
this.xyz = default;
this.rg = default;
this.rgb = default;
this.r = default;
this.g = default;
this.b = default;
this.a = default;

this.x = x;
this.y = y;
this.z = z;
this.w = w;
}
public fixed4(fixed3 xyz, float w)
{
this.xy = default;
this.xyz = default;
this.rg = default;
this.rgb = default;
this.r = default;
this.g = default;
this.b = default;
this.a = default;

this.x = xyz.x;
this.y = xyz.y;
this.z = xyz.z;
this.w = w;
}
[FieldOffset(0)]
public fixed2 xy;
[FieldOffset(0)]
public fixed3 xyz;
public fixed4 xyzw {
get => new fixed4();
set{ }
 }
[FieldOffset(0)]
public fixed2 rg;
[FieldOffset(0)]
public fixed3 rgb;
public fixed4 rgba {
get => new fixed4();
set{ }
 }
public static fixed4 operator +(fixed4 vector, float scalar)
{
return new fixed4(
vector.x + scalar,
vector.y + scalar,
vector.z + scalar,
vector.w + scalar
);
}
public static fixed4 operator +(float scalar, fixed4 vector) => vector + scalar;
public static fixed4 operator +(fixed4 vector, fixed4 scalar)
{
return new fixed4(
vector.x + scalar.x,
vector.y + scalar.y,
vector.z + scalar.z,
vector.w + scalar.w
);
}
public static fixed4 operator +(fixed4 vector, float4 scalar) { return default;}
public static fixed4 operator +(fixed4 vector, half4 scalar) { return default;}
public static fixed4 operator -(fixed4 vector, float scalar)
{
return new fixed4(
vector.x - scalar,
vector.y - scalar,
vector.z - scalar,
vector.w - scalar
);
}
public static fixed4 operator -(float scalar, fixed4 vector) => vector - scalar;
public static fixed4 operator -(fixed4 vector, fixed4 scalar)
{
return new fixed4(
vector.x - scalar.x,
vector.y - scalar.y,
vector.z - scalar.z,
vector.w - scalar.w
);
}
public static fixed4 operator -(fixed4 vector, float4 scalar) { return default;}
public static fixed4 operator -(fixed4 vector, half4 scalar) { return default;}
public static fixed4 operator *(fixed4 vector, float scalar)
{
return new fixed4(
vector.x * scalar,
vector.y * scalar,
vector.z * scalar,
vector.w * scalar
);
}
public static fixed4 operator *(float scalar, fixed4 vector) => vector * scalar;
public static fixed4 operator *(fixed4 vector, fixed4 scalar)
{
return new fixed4(
vector.x * scalar.x,
vector.y * scalar.y,
vector.z * scalar.z,
vector.w * scalar.w
);
}
public static fixed4 operator *(fixed4 vector, float4 scalar) { return default;}
public static fixed4 operator *(fixed4 vector, half4 scalar) { return default;}
public static fixed4 operator /(fixed4 vector, float scalar)
{
return new fixed4(
vector.x / scalar,
vector.y / scalar,
vector.z / scalar,
vector.w / scalar
);
}
public static fixed4 operator /(float scalar, fixed4 vector) => vector / scalar;
public static fixed4 operator /(fixed4 vector, fixed4 scalar)
{
return new fixed4(
vector.x / scalar.x,
vector.y / scalar.y,
vector.z / scalar.z,
vector.w / scalar.w
);
}
public static fixed4 operator /(fixed4 vector, float4 scalar) { return default;}
public static fixed4 operator /(fixed4 vector, half4 scalar) { return default;}
public static fixed4 operator %(fixed4 vector, float scalar)
{
return new fixed4(
vector.x % scalar,
vector.y % scalar,
vector.z % scalar,
vector.w % scalar
);
}
public static fixed4 operator %(float scalar, fixed4 vector) => vector % scalar;
public static fixed4 operator %(fixed4 vector, fixed4 scalar)
{
return new fixed4(
vector.x % scalar.x,
vector.y % scalar.y,
vector.z % scalar.z,
vector.w % scalar.w
);
}
public static fixed4 operator %(fixed4 vector, float4 scalar) { return default;}
public static fixed4 operator %(fixed4 vector, half4 scalar) { return default;}
public static fixed4 operator -(fixed4 vector)
{
return new fixed4(
-vector.x,
-vector.y,
-vector.z,
-vector.w
);
}
public static implicit operator fixed2(fixed4 v) { throw new NotImplementedException(); }
public static implicit operator fixed3(fixed4 v) { throw new NotImplementedException(); }
public static implicit operator float4(fixed4 v) { throw new NotImplementedException(); }
public static implicit operator half4(fixed4 v) { throw new NotImplementedException(); }
public static implicit operator fixed4(float v) { throw new NotImplementedException(); }
}
[StructLayout(LayoutKind.Explicit)]
public struct  half2
{
[FieldOffset(0)] public float x;
[FieldOffset(4)] public float y;
[FieldOffset(0)] public float r;
[FieldOffset(4)] public float g;
public half2(float x,float y)
{
this.r = default;
this.g = default;

this.x = x;
this.y = y;
}
public half2 xy {
get => new half2();
set{ }
 }
public half2 rg {
get => new half2();
set{ }
 }
public static half2 operator +(half2 vector, float scalar)
{
return new half2(
vector.x + scalar,
vector.y + scalar
);
}
public static half2 operator +(float scalar, half2 vector) => vector + scalar;
public static half2 operator +(half2 vector, half2 scalar)
{
return new half2(
vector.x + scalar.x,
vector.y + scalar.y
);
}
public static half2 operator +(half2 vector, float2 scalar) { return default;}
public static half2 operator +(half2 vector, fixed2 scalar) { return default;}
public static half2 operator -(half2 vector, float scalar)
{
return new half2(
vector.x - scalar,
vector.y - scalar
);
}
public static half2 operator -(float scalar, half2 vector) => vector - scalar;
public static half2 operator -(half2 vector, half2 scalar)
{
return new half2(
vector.x - scalar.x,
vector.y - scalar.y
);
}
public static half2 operator -(half2 vector, float2 scalar) { return default;}
public static half2 operator -(half2 vector, fixed2 scalar) { return default;}
public static half2 operator *(half2 vector, float scalar)
{
return new half2(
vector.x * scalar,
vector.y * scalar
);
}
public static half2 operator *(float scalar, half2 vector) => vector * scalar;
public static half2 operator *(half2 vector, half2 scalar)
{
return new half2(
vector.x * scalar.x,
vector.y * scalar.y
);
}
public static half2 operator *(half2 vector, float2 scalar) { return default;}
public static half2 operator *(half2 vector, fixed2 scalar) { return default;}
public static half2 operator /(half2 vector, float scalar)
{
return new half2(
vector.x / scalar,
vector.y / scalar
);
}
public static half2 operator /(float scalar, half2 vector) => vector / scalar;
public static half2 operator /(half2 vector, half2 scalar)
{
return new half2(
vector.x / scalar.x,
vector.y / scalar.y
);
}
public static half2 operator /(half2 vector, float2 scalar) { return default;}
public static half2 operator /(half2 vector, fixed2 scalar) { return default;}
public static half2 operator %(half2 vector, float scalar)
{
return new half2(
vector.x % scalar,
vector.y % scalar
);
}
public static half2 operator %(float scalar, half2 vector) => vector % scalar;
public static half2 operator %(half2 vector, half2 scalar)
{
return new half2(
vector.x % scalar.x,
vector.y % scalar.y
);
}
public static half2 operator %(half2 vector, float2 scalar) { return default;}
public static half2 operator %(half2 vector, fixed2 scalar) { return default;}
public static half2 operator -(half2 vector)
{
return new half2(
-vector.x,
-vector.y
);
}
public static implicit operator float2(half2 v) { throw new NotImplementedException(); }
public static implicit operator fixed2(half2 v) { throw new NotImplementedException(); }
public static implicit operator half2(float v) { throw new NotImplementedException(); }
}
[StructLayout(LayoutKind.Explicit)]
public struct  half3
{
[FieldOffset(0)] public float x;
[FieldOffset(4)] public float y;
[FieldOffset(8)] public float z;
[FieldOffset(0)] public float r;
[FieldOffset(4)] public float g;
[FieldOffset(8)] public float b;
public half3(float x,float y,float z)
{
this.xy = default;
this.rg = default;
this.r = default;
this.g = default;
this.b = default;

this.x = x;
this.y = y;
this.z = z;
}
public half3(half2 xy, float z)
{
this.xy = default;
this.rg = default;
this.r = default;
this.g = default;
this.b = default;

this.x = xy.x;
this.y = xy.y;
this.z = z;
}
[FieldOffset(0)]
public half2 xy;
public half3 xyz {
get => new half3();
set{ }
 }
[FieldOffset(0)]
public half2 rg;
public half3 rgb {
get => new half3();
set{ }
 }
public static half3 operator +(half3 vector, float scalar)
{
return new half3(
vector.x + scalar,
vector.y + scalar,
vector.z + scalar
);
}
public static half3 operator +(float scalar, half3 vector) => vector + scalar;
public static half3 operator +(half3 vector, half3 scalar)
{
return new half3(
vector.x + scalar.x,
vector.y + scalar.y,
vector.z + scalar.z
);
}
public static half3 operator +(half3 vector, float3 scalar) { return default;}
public static half3 operator +(half3 vector, fixed3 scalar) { return default;}
public static half3 operator -(half3 vector, float scalar)
{
return new half3(
vector.x - scalar,
vector.y - scalar,
vector.z - scalar
);
}
public static half3 operator -(float scalar, half3 vector) => vector - scalar;
public static half3 operator -(half3 vector, half3 scalar)
{
return new half3(
vector.x - scalar.x,
vector.y - scalar.y,
vector.z - scalar.z
);
}
public static half3 operator -(half3 vector, float3 scalar) { return default;}
public static half3 operator -(half3 vector, fixed3 scalar) { return default;}
public static half3 operator *(half3 vector, float scalar)
{
return new half3(
vector.x * scalar,
vector.y * scalar,
vector.z * scalar
);
}
public static half3 operator *(float scalar, half3 vector) => vector * scalar;
public static half3 operator *(half3 vector, half3 scalar)
{
return new half3(
vector.x * scalar.x,
vector.y * scalar.y,
vector.z * scalar.z
);
}
public static half3 operator *(half3 vector, float3 scalar) { return default;}
public static half3 operator *(half3 vector, fixed3 scalar) { return default;}
public static half3 operator /(half3 vector, float scalar)
{
return new half3(
vector.x / scalar,
vector.y / scalar,
vector.z / scalar
);
}
public static half3 operator /(float scalar, half3 vector) => vector / scalar;
public static half3 operator /(half3 vector, half3 scalar)
{
return new half3(
vector.x / scalar.x,
vector.y / scalar.y,
vector.z / scalar.z
);
}
public static half3 operator /(half3 vector, float3 scalar) { return default;}
public static half3 operator /(half3 vector, fixed3 scalar) { return default;}
public static half3 operator %(half3 vector, float scalar)
{
return new half3(
vector.x % scalar,
vector.y % scalar,
vector.z % scalar
);
}
public static half3 operator %(float scalar, half3 vector) => vector % scalar;
public static half3 operator %(half3 vector, half3 scalar)
{
return new half3(
vector.x % scalar.x,
vector.y % scalar.y,
vector.z % scalar.z
);
}
public static half3 operator %(half3 vector, float3 scalar) { return default;}
public static half3 operator %(half3 vector, fixed3 scalar) { return default;}
public static half3 operator -(half3 vector)
{
return new half3(
-vector.x,
-vector.y,
-vector.z
);
}
public static implicit operator half2(half3 v) { throw new NotImplementedException(); }
public static implicit operator float3(half3 v) { throw new NotImplementedException(); }
public static implicit operator fixed3(half3 v) { throw new NotImplementedException(); }
public static implicit operator half3(float v) { throw new NotImplementedException(); }
}
[StructLayout(LayoutKind.Explicit)]
public struct  half4
{
[FieldOffset(0)] public float x;
[FieldOffset(4)] public float y;
[FieldOffset(8)] public float z;
[FieldOffset(12)] public float w;
[FieldOffset(0)] public float r;
[FieldOffset(4)] public float g;
[FieldOffset(8)] public float b;
[FieldOffset(12)] public float a;
public half4(float x,float y,float z,float w)
{
this.xy = default;
this.xyz = default;
this.rg = default;
this.rgb = default;
this.r = default;
this.g = default;
this.b = default;
this.a = default;

this.x = x;
this.y = y;
this.z = z;
this.w = w;
}
public half4(half3 xyz, float w)
{
this.xy = default;
this.xyz = default;
this.rg = default;
this.rgb = default;
this.r = default;
this.g = default;
this.b = default;
this.a = default;

this.x = xyz.x;
this.y = xyz.y;
this.z = xyz.z;
this.w = w;
}
[FieldOffset(0)]
public half2 xy;
[FieldOffset(0)]
public half3 xyz;
public half4 xyzw {
get => new half4();
set{ }
 }
[FieldOffset(0)]
public half2 rg;
[FieldOffset(0)]
public half3 rgb;
public half4 rgba {
get => new half4();
set{ }
 }
public static half4 operator +(half4 vector, float scalar)
{
return new half4(
vector.x + scalar,
vector.y + scalar,
vector.z + scalar,
vector.w + scalar
);
}
public static half4 operator +(float scalar, half4 vector) => vector + scalar;
public static half4 operator +(half4 vector, half4 scalar)
{
return new half4(
vector.x + scalar.x,
vector.y + scalar.y,
vector.z + scalar.z,
vector.w + scalar.w
);
}
public static half4 operator +(half4 vector, float4 scalar) { return default;}
public static half4 operator +(half4 vector, fixed4 scalar) { return default;}
public static half4 operator -(half4 vector, float scalar)
{
return new half4(
vector.x - scalar,
vector.y - scalar,
vector.z - scalar,
vector.w - scalar
);
}
public static half4 operator -(float scalar, half4 vector) => vector - scalar;
public static half4 operator -(half4 vector, half4 scalar)
{
return new half4(
vector.x - scalar.x,
vector.y - scalar.y,
vector.z - scalar.z,
vector.w - scalar.w
);
}
public static half4 operator -(half4 vector, float4 scalar) { return default;}
public static half4 operator -(half4 vector, fixed4 scalar) { return default;}
public static half4 operator *(half4 vector, float scalar)
{
return new half4(
vector.x * scalar,
vector.y * scalar,
vector.z * scalar,
vector.w * scalar
);
}
public static half4 operator *(float scalar, half4 vector) => vector * scalar;
public static half4 operator *(half4 vector, half4 scalar)
{
return new half4(
vector.x * scalar.x,
vector.y * scalar.y,
vector.z * scalar.z,
vector.w * scalar.w
);
}
public static half4 operator *(half4 vector, float4 scalar) { return default;}
public static half4 operator *(half4 vector, fixed4 scalar) { return default;}
public static half4 operator /(half4 vector, float scalar)
{
return new half4(
vector.x / scalar,
vector.y / scalar,
vector.z / scalar,
vector.w / scalar
);
}
public static half4 operator /(float scalar, half4 vector) => vector / scalar;
public static half4 operator /(half4 vector, half4 scalar)
{
return new half4(
vector.x / scalar.x,
vector.y / scalar.y,
vector.z / scalar.z,
vector.w / scalar.w
);
}
public static half4 operator /(half4 vector, float4 scalar) { return default;}
public static half4 operator /(half4 vector, fixed4 scalar) { return default;}
public static half4 operator %(half4 vector, float scalar)
{
return new half4(
vector.x % scalar,
vector.y % scalar,
vector.z % scalar,
vector.w % scalar
);
}
public static half4 operator %(float scalar, half4 vector) => vector % scalar;
public static half4 operator %(half4 vector, half4 scalar)
{
return new half4(
vector.x % scalar.x,
vector.y % scalar.y,
vector.z % scalar.z,
vector.w % scalar.w
);
}
public static half4 operator %(half4 vector, float4 scalar) { return default;}
public static half4 operator %(half4 vector, fixed4 scalar) { return default;}
public static half4 operator -(half4 vector)
{
return new half4(
-vector.x,
-vector.y,
-vector.z,
-vector.w
);
}
public static implicit operator half2(half4 v) { throw new NotImplementedException(); }
public static implicit operator half3(half4 v) { throw new NotImplementedException(); }
public static implicit operator float4(half4 v) { throw new NotImplementedException(); }
public static implicit operator fixed4(half4 v) { throw new NotImplementedException(); }
public static implicit operator half4(float v) { throw new NotImplementedException(); }
}
}
	