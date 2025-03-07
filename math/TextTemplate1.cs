


using System;
using System.Runtime.InteropServices;
namespace Shader.Vectors{
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
public float2 xy { get => this; set{ }}
public float2 xx {get => new float2(x,x);set{this.x= value.x;this.x= value.y;}}
public float2 yx {get => new float2(y,x);set{this.y= value.x;this.x= value.y;}}
public float2 yy {get => new float2(y,y);set{this.y= value.x;this.y= value.y;}}
public float3 xxx {get => new float3(x,x,x);set{this.x= value.x;this.x= value.y;this.x= value.z;}}
public float3 xxy {get => new float3(x,x,y);set{this.x= value.x;this.x= value.y;this.y= value.z;}}
public float3 xyx {get => new float3(x,y,x);set{this.x= value.x;this.y= value.y;this.x= value.z;}}
public float3 xyy {get => new float3(x,y,y);set{this.x= value.x;this.y= value.y;this.y= value.z;}}
public float3 yxx {get => new float3(y,x,x);set{this.y= value.x;this.x= value.y;this.x= value.z;}}
public float3 yxy {get => new float3(y,x,y);set{this.y= value.x;this.x= value.y;this.y= value.z;}}
public float3 yyx {get => new float3(y,y,x);set{this.y= value.x;this.y= value.y;this.x= value.z;}}
public float3 yyy {get => new float3(y,y,y);set{this.y= value.x;this.y= value.y;this.y= value.z;}}
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
[FieldOffset(0)]
public float2 rg;
public float3 xyz { get => this; set{ }}
public float2 xx {get => new float2(x,x);set{this.x= value.x;this.x= value.y;}}
public float2 xz {get => new float2(x,z);set{this.x= value.x;this.z= value.y;}}
public float2 yx {get => new float2(y,x);set{this.y= value.x;this.x= value.y;}}
public float2 yy {get => new float2(y,y);set{this.y= value.x;this.y= value.y;}}
public float2 yz {get => new float2(y,z);set{this.y= value.x;this.z= value.y;}}
public float2 zx {get => new float2(z,x);set{this.z= value.x;this.x= value.y;}}
public float2 zy {get => new float2(z,y);set{this.z= value.x;this.y= value.y;}}
public float2 zz {get => new float2(z,z);set{this.z= value.x;this.z= value.y;}}
public float3 xxx {get => new float3(x,x,x);set{this.x= value.x;this.x= value.y;this.x= value.z;}}
public float3 xxy {get => new float3(x,x,y);set{this.x= value.x;this.x= value.y;this.y= value.z;}}
public float3 xxz {get => new float3(x,x,z);set{this.x= value.x;this.x= value.y;this.z= value.z;}}
public float3 xyx {get => new float3(x,y,x);set{this.x= value.x;this.y= value.y;this.x= value.z;}}
public float3 xyy {get => new float3(x,y,y);set{this.x= value.x;this.y= value.y;this.y= value.z;}}
public float3 xzx {get => new float3(x,z,x);set{this.x= value.x;this.z= value.y;this.x= value.z;}}
public float3 xzy {get => new float3(x,z,y);set{this.x= value.x;this.z= value.y;this.y= value.z;}}
public float3 xzz {get => new float3(x,z,z);set{this.x= value.x;this.z= value.y;this.z= value.z;}}
public float3 yxx {get => new float3(y,x,x);set{this.y= value.x;this.x= value.y;this.x= value.z;}}
public float3 yxy {get => new float3(y,x,y);set{this.y= value.x;this.x= value.y;this.y= value.z;}}
public float3 yxz {get => new float3(y,x,z);set{this.y= value.x;this.x= value.y;this.z= value.z;}}
public float3 yyx {get => new float3(y,y,x);set{this.y= value.x;this.y= value.y;this.x= value.z;}}
public float3 yyy {get => new float3(y,y,y);set{this.y= value.x;this.y= value.y;this.y= value.z;}}
public float3 yyz {get => new float3(y,y,z);set{this.y= value.x;this.y= value.y;this.z= value.z;}}
public float3 yzx {get => new float3(y,z,x);set{this.y= value.x;this.z= value.y;this.x= value.z;}}
public float3 yzy {get => new float3(y,z,y);set{this.y= value.x;this.z= value.y;this.y= value.z;}}
public float3 yzz {get => new float3(y,z,z);set{this.y= value.x;this.z= value.y;this.z= value.z;}}
public float3 zxx {get => new float3(z,x,x);set{this.z= value.x;this.x= value.y;this.x= value.z;}}
public float3 zxy {get => new float3(z,x,y);set{this.z= value.x;this.x= value.y;this.y= value.z;}}
public float3 zxz {get => new float3(z,x,z);set{this.z= value.x;this.x= value.y;this.z= value.z;}}
public float3 zyx {get => new float3(z,y,x);set{this.z= value.x;this.y= value.y;this.x= value.z;}}
public float3 zyy {get => new float3(z,y,y);set{this.z= value.x;this.y= value.y;this.y= value.z;}}
public float3 zyz {get => new float3(z,y,z);set{this.z= value.x;this.y= value.y;this.z= value.z;}}
public float3 zzx {get => new float3(z,z,x);set{this.z= value.x;this.z= value.y;this.x= value.z;}}
public float3 zzy {get => new float3(z,z,y);set{this.z= value.x;this.z= value.y;this.y= value.z;}}
public float3 zzz {get => new float3(z,z,z);set{this.z= value.x;this.z= value.y;this.z= value.z;}}
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
public float2 rg;
[FieldOffset(0)]
public float3 xyz;
[FieldOffset(0)]
public float3 rgb;
public float4 xyzw { get => this; set{ }}
public float2 xx {get => new float2(x,x);set{this.x= value.x;this.x= value.y;}}
public float2 xz {get => new float2(x,z);set{this.x= value.x;this.z= value.y;}}
public float2 xw {get => new float2(x,w);set{this.x= value.x;this.w= value.y;}}
public float2 yx {get => new float2(y,x);set{this.y= value.x;this.x= value.y;}}
public float2 yy {get => new float2(y,y);set{this.y= value.x;this.y= value.y;}}
public float2 yz {get => new float2(y,z);set{this.y= value.x;this.z= value.y;}}
public float2 yw {get => new float2(y,w);set{this.y= value.x;this.w= value.y;}}
public float2 zx {get => new float2(z,x);set{this.z= value.x;this.x= value.y;}}
public float2 zy {get => new float2(z,y);set{this.z= value.x;this.y= value.y;}}
public float2 zz {get => new float2(z,z);set{this.z= value.x;this.z= value.y;}}
public float2 zw {get => new float2(z,w);set{this.z= value.x;this.w= value.y;}}
public float2 wx {get => new float2(w,x);set{this.w= value.x;this.x= value.y;}}
public float2 wy {get => new float2(w,y);set{this.w= value.x;this.y= value.y;}}
public float2 wz {get => new float2(w,z);set{this.w= value.x;this.z= value.y;}}
public float2 ww {get => new float2(w,w);set{this.w= value.x;this.w= value.y;}}
public float3 xxx {get => new float3(x,x,x);set{this.x= value.x;this.x= value.y;this.x= value.z;}}
public float3 xxy {get => new float3(x,x,y);set{this.x= value.x;this.x= value.y;this.y= value.z;}}
public float3 xxz {get => new float3(x,x,z);set{this.x= value.x;this.x= value.y;this.z= value.z;}}
public float3 xxw {get => new float3(x,x,w);set{this.x= value.x;this.x= value.y;this.w= value.z;}}
public float3 xyx {get => new float3(x,y,x);set{this.x= value.x;this.y= value.y;this.x= value.z;}}
public float3 xyy {get => new float3(x,y,y);set{this.x= value.x;this.y= value.y;this.y= value.z;}}
public float3 xyw {get => new float3(x,y,w);set{this.x= value.x;this.y= value.y;this.w= value.z;}}
public float3 xzx {get => new float3(x,z,x);set{this.x= value.x;this.z= value.y;this.x= value.z;}}
public float3 xzy {get => new float3(x,z,y);set{this.x= value.x;this.z= value.y;this.y= value.z;}}
public float3 xzz {get => new float3(x,z,z);set{this.x= value.x;this.z= value.y;this.z= value.z;}}
public float3 xzw {get => new float3(x,z,w);set{this.x= value.x;this.z= value.y;this.w= value.z;}}
public float3 xwx {get => new float3(x,w,x);set{this.x= value.x;this.w= value.y;this.x= value.z;}}
public float3 xwy {get => new float3(x,w,y);set{this.x= value.x;this.w= value.y;this.y= value.z;}}
public float3 xwz {get => new float3(x,w,z);set{this.x= value.x;this.w= value.y;this.z= value.z;}}
public float3 xww {get => new float3(x,w,w);set{this.x= value.x;this.w= value.y;this.w= value.z;}}
public float3 yxx {get => new float3(y,x,x);set{this.y= value.x;this.x= value.y;this.x= value.z;}}
public float3 yxy {get => new float3(y,x,y);set{this.y= value.x;this.x= value.y;this.y= value.z;}}
public float3 yxz {get => new float3(y,x,z);set{this.y= value.x;this.x= value.y;this.z= value.z;}}
public float3 yxw {get => new float3(y,x,w);set{this.y= value.x;this.x= value.y;this.w= value.z;}}
public float3 yyx {get => new float3(y,y,x);set{this.y= value.x;this.y= value.y;this.x= value.z;}}
public float3 yyy {get => new float3(y,y,y);set{this.y= value.x;this.y= value.y;this.y= value.z;}}
public float3 yyz {get => new float3(y,y,z);set{this.y= value.x;this.y= value.y;this.z= value.z;}}
public float3 yyw {get => new float3(y,y,w);set{this.y= value.x;this.y= value.y;this.w= value.z;}}
public float3 yzx {get => new float3(y,z,x);set{this.y= value.x;this.z= value.y;this.x= value.z;}}
public float3 yzy {get => new float3(y,z,y);set{this.y= value.x;this.z= value.y;this.y= value.z;}}
public float3 yzz {get => new float3(y,z,z);set{this.y= value.x;this.z= value.y;this.z= value.z;}}
public float3 yzw {get => new float3(y,z,w);set{this.y= value.x;this.z= value.y;this.w= value.z;}}
public float3 ywx {get => new float3(y,w,x);set{this.y= value.x;this.w= value.y;this.x= value.z;}}
public float3 ywy {get => new float3(y,w,y);set{this.y= value.x;this.w= value.y;this.y= value.z;}}
public float3 ywz {get => new float3(y,w,z);set{this.y= value.x;this.w= value.y;this.z= value.z;}}
public float3 yww {get => new float3(y,w,w);set{this.y= value.x;this.w= value.y;this.w= value.z;}}
public float3 zxx {get => new float3(z,x,x);set{this.z= value.x;this.x= value.y;this.x= value.z;}}
public float3 zxy {get => new float3(z,x,y);set{this.z= value.x;this.x= value.y;this.y= value.z;}}
public float3 zxz {get => new float3(z,x,z);set{this.z= value.x;this.x= value.y;this.z= value.z;}}
public float3 zxw {get => new float3(z,x,w);set{this.z= value.x;this.x= value.y;this.w= value.z;}}
public float3 zyx {get => new float3(z,y,x);set{this.z= value.x;this.y= value.y;this.x= value.z;}}
public float3 zyy {get => new float3(z,y,y);set{this.z= value.x;this.y= value.y;this.y= value.z;}}
public float3 zyz {get => new float3(z,y,z);set{this.z= value.x;this.y= value.y;this.z= value.z;}}
public float3 zyw {get => new float3(z,y,w);set{this.z= value.x;this.y= value.y;this.w= value.z;}}
public float3 zzx {get => new float3(z,z,x);set{this.z= value.x;this.z= value.y;this.x= value.z;}}
public float3 zzy {get => new float3(z,z,y);set{this.z= value.x;this.z= value.y;this.y= value.z;}}
public float3 zzz {get => new float3(z,z,z);set{this.z= value.x;this.z= value.y;this.z= value.z;}}
public float3 zzw {get => new float3(z,z,w);set{this.z= value.x;this.z= value.y;this.w= value.z;}}
public float3 zwx {get => new float3(z,w,x);set{this.z= value.x;this.w= value.y;this.x= value.z;}}
public float3 zwy {get => new float3(z,w,y);set{this.z= value.x;this.w= value.y;this.y= value.z;}}
public float3 zwz {get => new float3(z,w,z);set{this.z= value.x;this.w= value.y;this.z= value.z;}}
public float3 zww {get => new float3(z,w,w);set{this.z= value.x;this.w= value.y;this.w= value.z;}}
public float3 wxx {get => new float3(w,x,x);set{this.w= value.x;this.x= value.y;this.x= value.z;}}
public float3 wxy {get => new float3(w,x,y);set{this.w= value.x;this.x= value.y;this.y= value.z;}}
public float3 wxz {get => new float3(w,x,z);set{this.w= value.x;this.x= value.y;this.z= value.z;}}
public float3 wxw {get => new float3(w,x,w);set{this.w= value.x;this.x= value.y;this.w= value.z;}}
public float3 wyx {get => new float3(w,y,x);set{this.w= value.x;this.y= value.y;this.x= value.z;}}
public float3 wyy {get => new float3(w,y,y);set{this.w= value.x;this.y= value.y;this.y= value.z;}}
public float3 wyz {get => new float3(w,y,z);set{this.w= value.x;this.y= value.y;this.z= value.z;}}
public float3 wyw {get => new float3(w,y,w);set{this.w= value.x;this.y= value.y;this.w= value.z;}}
public float3 wzx {get => new float3(w,z,x);set{this.w= value.x;this.z= value.y;this.x= value.z;}}
public float3 wzy {get => new float3(w,z,y);set{this.w= value.x;this.z= value.y;this.y= value.z;}}
public float3 wzz {get => new float3(w,z,z);set{this.w= value.x;this.z= value.y;this.z= value.z;}}
public float3 wzw {get => new float3(w,z,w);set{this.w= value.x;this.z= value.y;this.w= value.z;}}
public float3 wwx {get => new float3(w,w,x);set{this.w= value.x;this.w= value.y;this.x= value.z;}}
public float3 wwy {get => new float3(w,w,y);set{this.w= value.x;this.w= value.y;this.y= value.z;}}
public float3 wwz {get => new float3(w,w,z);set{this.w= value.x;this.w= value.y;this.z= value.z;}}
public float3 www {get => new float3(w,w,w);set{this.w= value.x;this.w= value.y;this.w= value.z;}}
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
public fixed2 xy { get => this; set{ }}
public fixed2 xx {get => new fixed2(x,x);set{this.x= value.x;this.x= value.y;}}
public fixed2 yx {get => new fixed2(y,x);set{this.y= value.x;this.x= value.y;}}
public fixed2 yy {get => new fixed2(y,y);set{this.y= value.x;this.y= value.y;}}
public fixed3 xxx {get => new fixed3(x,x,x);set{this.x= value.x;this.x= value.y;this.x= value.z;}}
public fixed3 xxy {get => new fixed3(x,x,y);set{this.x= value.x;this.x= value.y;this.y= value.z;}}
public fixed3 xyx {get => new fixed3(x,y,x);set{this.x= value.x;this.y= value.y;this.x= value.z;}}
public fixed3 xyy {get => new fixed3(x,y,y);set{this.x= value.x;this.y= value.y;this.y= value.z;}}
public fixed3 yxx {get => new fixed3(y,x,x);set{this.y= value.x;this.x= value.y;this.x= value.z;}}
public fixed3 yxy {get => new fixed3(y,x,y);set{this.y= value.x;this.x= value.y;this.y= value.z;}}
public fixed3 yyx {get => new fixed3(y,y,x);set{this.y= value.x;this.y= value.y;this.x= value.z;}}
public fixed3 yyy {get => new fixed3(y,y,y);set{this.y= value.x;this.y= value.y;this.y= value.z;}}
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
[FieldOffset(0)]
public fixed2 rg;
public fixed3 xyz { get => this; set{ }}
public fixed2 xx {get => new fixed2(x,x);set{this.x= value.x;this.x= value.y;}}
public fixed2 xz {get => new fixed2(x,z);set{this.x= value.x;this.z= value.y;}}
public fixed2 yx {get => new fixed2(y,x);set{this.y= value.x;this.x= value.y;}}
public fixed2 yy {get => new fixed2(y,y);set{this.y= value.x;this.y= value.y;}}
public fixed2 yz {get => new fixed2(y,z);set{this.y= value.x;this.z= value.y;}}
public fixed2 zx {get => new fixed2(z,x);set{this.z= value.x;this.x= value.y;}}
public fixed2 zy {get => new fixed2(z,y);set{this.z= value.x;this.y= value.y;}}
public fixed2 zz {get => new fixed2(z,z);set{this.z= value.x;this.z= value.y;}}
public fixed3 xxx {get => new fixed3(x,x,x);set{this.x= value.x;this.x= value.y;this.x= value.z;}}
public fixed3 xxy {get => new fixed3(x,x,y);set{this.x= value.x;this.x= value.y;this.y= value.z;}}
public fixed3 xxz {get => new fixed3(x,x,z);set{this.x= value.x;this.x= value.y;this.z= value.z;}}
public fixed3 xyx {get => new fixed3(x,y,x);set{this.x= value.x;this.y= value.y;this.x= value.z;}}
public fixed3 xyy {get => new fixed3(x,y,y);set{this.x= value.x;this.y= value.y;this.y= value.z;}}
public fixed3 xzx {get => new fixed3(x,z,x);set{this.x= value.x;this.z= value.y;this.x= value.z;}}
public fixed3 xzy {get => new fixed3(x,z,y);set{this.x= value.x;this.z= value.y;this.y= value.z;}}
public fixed3 xzz {get => new fixed3(x,z,z);set{this.x= value.x;this.z= value.y;this.z= value.z;}}
public fixed3 yxx {get => new fixed3(y,x,x);set{this.y= value.x;this.x= value.y;this.x= value.z;}}
public fixed3 yxy {get => new fixed3(y,x,y);set{this.y= value.x;this.x= value.y;this.y= value.z;}}
public fixed3 yxz {get => new fixed3(y,x,z);set{this.y= value.x;this.x= value.y;this.z= value.z;}}
public fixed3 yyx {get => new fixed3(y,y,x);set{this.y= value.x;this.y= value.y;this.x= value.z;}}
public fixed3 yyy {get => new fixed3(y,y,y);set{this.y= value.x;this.y= value.y;this.y= value.z;}}
public fixed3 yyz {get => new fixed3(y,y,z);set{this.y= value.x;this.y= value.y;this.z= value.z;}}
public fixed3 yzx {get => new fixed3(y,z,x);set{this.y= value.x;this.z= value.y;this.x= value.z;}}
public fixed3 yzy {get => new fixed3(y,z,y);set{this.y= value.x;this.z= value.y;this.y= value.z;}}
public fixed3 yzz {get => new fixed3(y,z,z);set{this.y= value.x;this.z= value.y;this.z= value.z;}}
public fixed3 zxx {get => new fixed3(z,x,x);set{this.z= value.x;this.x= value.y;this.x= value.z;}}
public fixed3 zxy {get => new fixed3(z,x,y);set{this.z= value.x;this.x= value.y;this.y= value.z;}}
public fixed3 zxz {get => new fixed3(z,x,z);set{this.z= value.x;this.x= value.y;this.z= value.z;}}
public fixed3 zyx {get => new fixed3(z,y,x);set{this.z= value.x;this.y= value.y;this.x= value.z;}}
public fixed3 zyy {get => new fixed3(z,y,y);set{this.z= value.x;this.y= value.y;this.y= value.z;}}
public fixed3 zyz {get => new fixed3(z,y,z);set{this.z= value.x;this.y= value.y;this.z= value.z;}}
public fixed3 zzx {get => new fixed3(z,z,x);set{this.z= value.x;this.z= value.y;this.x= value.z;}}
public fixed3 zzy {get => new fixed3(z,z,y);set{this.z= value.x;this.z= value.y;this.y= value.z;}}
public fixed3 zzz {get => new fixed3(z,z,z);set{this.z= value.x;this.z= value.y;this.z= value.z;}}
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
public fixed2 rg;
[FieldOffset(0)]
public fixed3 xyz;
[FieldOffset(0)]
public fixed3 rgb;
public fixed4 xyzw { get => this; set{ }}
public fixed2 xx {get => new fixed2(x,x);set{this.x= value.x;this.x= value.y;}}
public fixed2 xz {get => new fixed2(x,z);set{this.x= value.x;this.z= value.y;}}
public fixed2 xw {get => new fixed2(x,w);set{this.x= value.x;this.w= value.y;}}
public fixed2 yx {get => new fixed2(y,x);set{this.y= value.x;this.x= value.y;}}
public fixed2 yy {get => new fixed2(y,y);set{this.y= value.x;this.y= value.y;}}
public fixed2 yz {get => new fixed2(y,z);set{this.y= value.x;this.z= value.y;}}
public fixed2 yw {get => new fixed2(y,w);set{this.y= value.x;this.w= value.y;}}
public fixed2 zx {get => new fixed2(z,x);set{this.z= value.x;this.x= value.y;}}
public fixed2 zy {get => new fixed2(z,y);set{this.z= value.x;this.y= value.y;}}
public fixed2 zz {get => new fixed2(z,z);set{this.z= value.x;this.z= value.y;}}
public fixed2 zw {get => new fixed2(z,w);set{this.z= value.x;this.w= value.y;}}
public fixed2 wx {get => new fixed2(w,x);set{this.w= value.x;this.x= value.y;}}
public fixed2 wy {get => new fixed2(w,y);set{this.w= value.x;this.y= value.y;}}
public fixed2 wz {get => new fixed2(w,z);set{this.w= value.x;this.z= value.y;}}
public fixed2 ww {get => new fixed2(w,w);set{this.w= value.x;this.w= value.y;}}
public fixed3 xxx {get => new fixed3(x,x,x);set{this.x= value.x;this.x= value.y;this.x= value.z;}}
public fixed3 xxy {get => new fixed3(x,x,y);set{this.x= value.x;this.x= value.y;this.y= value.z;}}
public fixed3 xxz {get => new fixed3(x,x,z);set{this.x= value.x;this.x= value.y;this.z= value.z;}}
public fixed3 xxw {get => new fixed3(x,x,w);set{this.x= value.x;this.x= value.y;this.w= value.z;}}
public fixed3 xyx {get => new fixed3(x,y,x);set{this.x= value.x;this.y= value.y;this.x= value.z;}}
public fixed3 xyy {get => new fixed3(x,y,y);set{this.x= value.x;this.y= value.y;this.y= value.z;}}
public fixed3 xyw {get => new fixed3(x,y,w);set{this.x= value.x;this.y= value.y;this.w= value.z;}}
public fixed3 xzx {get => new fixed3(x,z,x);set{this.x= value.x;this.z= value.y;this.x= value.z;}}
public fixed3 xzy {get => new fixed3(x,z,y);set{this.x= value.x;this.z= value.y;this.y= value.z;}}
public fixed3 xzz {get => new fixed3(x,z,z);set{this.x= value.x;this.z= value.y;this.z= value.z;}}
public fixed3 xzw {get => new fixed3(x,z,w);set{this.x= value.x;this.z= value.y;this.w= value.z;}}
public fixed3 xwx {get => new fixed3(x,w,x);set{this.x= value.x;this.w= value.y;this.x= value.z;}}
public fixed3 xwy {get => new fixed3(x,w,y);set{this.x= value.x;this.w= value.y;this.y= value.z;}}
public fixed3 xwz {get => new fixed3(x,w,z);set{this.x= value.x;this.w= value.y;this.z= value.z;}}
public fixed3 xww {get => new fixed3(x,w,w);set{this.x= value.x;this.w= value.y;this.w= value.z;}}
public fixed3 yxx {get => new fixed3(y,x,x);set{this.y= value.x;this.x= value.y;this.x= value.z;}}
public fixed3 yxy {get => new fixed3(y,x,y);set{this.y= value.x;this.x= value.y;this.y= value.z;}}
public fixed3 yxz {get => new fixed3(y,x,z);set{this.y= value.x;this.x= value.y;this.z= value.z;}}
public fixed3 yxw {get => new fixed3(y,x,w);set{this.y= value.x;this.x= value.y;this.w= value.z;}}
public fixed3 yyx {get => new fixed3(y,y,x);set{this.y= value.x;this.y= value.y;this.x= value.z;}}
public fixed3 yyy {get => new fixed3(y,y,y);set{this.y= value.x;this.y= value.y;this.y= value.z;}}
public fixed3 yyz {get => new fixed3(y,y,z);set{this.y= value.x;this.y= value.y;this.z= value.z;}}
public fixed3 yyw {get => new fixed3(y,y,w);set{this.y= value.x;this.y= value.y;this.w= value.z;}}
public fixed3 yzx {get => new fixed3(y,z,x);set{this.y= value.x;this.z= value.y;this.x= value.z;}}
public fixed3 yzy {get => new fixed3(y,z,y);set{this.y= value.x;this.z= value.y;this.y= value.z;}}
public fixed3 yzz {get => new fixed3(y,z,z);set{this.y= value.x;this.z= value.y;this.z= value.z;}}
public fixed3 yzw {get => new fixed3(y,z,w);set{this.y= value.x;this.z= value.y;this.w= value.z;}}
public fixed3 ywx {get => new fixed3(y,w,x);set{this.y= value.x;this.w= value.y;this.x= value.z;}}
public fixed3 ywy {get => new fixed3(y,w,y);set{this.y= value.x;this.w= value.y;this.y= value.z;}}
public fixed3 ywz {get => new fixed3(y,w,z);set{this.y= value.x;this.w= value.y;this.z= value.z;}}
public fixed3 yww {get => new fixed3(y,w,w);set{this.y= value.x;this.w= value.y;this.w= value.z;}}
public fixed3 zxx {get => new fixed3(z,x,x);set{this.z= value.x;this.x= value.y;this.x= value.z;}}
public fixed3 zxy {get => new fixed3(z,x,y);set{this.z= value.x;this.x= value.y;this.y= value.z;}}
public fixed3 zxz {get => new fixed3(z,x,z);set{this.z= value.x;this.x= value.y;this.z= value.z;}}
public fixed3 zxw {get => new fixed3(z,x,w);set{this.z= value.x;this.x= value.y;this.w= value.z;}}
public fixed3 zyx {get => new fixed3(z,y,x);set{this.z= value.x;this.y= value.y;this.x= value.z;}}
public fixed3 zyy {get => new fixed3(z,y,y);set{this.z= value.x;this.y= value.y;this.y= value.z;}}
public fixed3 zyz {get => new fixed3(z,y,z);set{this.z= value.x;this.y= value.y;this.z= value.z;}}
public fixed3 zyw {get => new fixed3(z,y,w);set{this.z= value.x;this.y= value.y;this.w= value.z;}}
public fixed3 zzx {get => new fixed3(z,z,x);set{this.z= value.x;this.z= value.y;this.x= value.z;}}
public fixed3 zzy {get => new fixed3(z,z,y);set{this.z= value.x;this.z= value.y;this.y= value.z;}}
public fixed3 zzz {get => new fixed3(z,z,z);set{this.z= value.x;this.z= value.y;this.z= value.z;}}
public fixed3 zzw {get => new fixed3(z,z,w);set{this.z= value.x;this.z= value.y;this.w= value.z;}}
public fixed3 zwx {get => new fixed3(z,w,x);set{this.z= value.x;this.w= value.y;this.x= value.z;}}
public fixed3 zwy {get => new fixed3(z,w,y);set{this.z= value.x;this.w= value.y;this.y= value.z;}}
public fixed3 zwz {get => new fixed3(z,w,z);set{this.z= value.x;this.w= value.y;this.z= value.z;}}
public fixed3 zww {get => new fixed3(z,w,w);set{this.z= value.x;this.w= value.y;this.w= value.z;}}
public fixed3 wxx {get => new fixed3(w,x,x);set{this.w= value.x;this.x= value.y;this.x= value.z;}}
public fixed3 wxy {get => new fixed3(w,x,y);set{this.w= value.x;this.x= value.y;this.y= value.z;}}
public fixed3 wxz {get => new fixed3(w,x,z);set{this.w= value.x;this.x= value.y;this.z= value.z;}}
public fixed3 wxw {get => new fixed3(w,x,w);set{this.w= value.x;this.x= value.y;this.w= value.z;}}
public fixed3 wyx {get => new fixed3(w,y,x);set{this.w= value.x;this.y= value.y;this.x= value.z;}}
public fixed3 wyy {get => new fixed3(w,y,y);set{this.w= value.x;this.y= value.y;this.y= value.z;}}
public fixed3 wyz {get => new fixed3(w,y,z);set{this.w= value.x;this.y= value.y;this.z= value.z;}}
public fixed3 wyw {get => new fixed3(w,y,w);set{this.w= value.x;this.y= value.y;this.w= value.z;}}
public fixed3 wzx {get => new fixed3(w,z,x);set{this.w= value.x;this.z= value.y;this.x= value.z;}}
public fixed3 wzy {get => new fixed3(w,z,y);set{this.w= value.x;this.z= value.y;this.y= value.z;}}
public fixed3 wzz {get => new fixed3(w,z,z);set{this.w= value.x;this.z= value.y;this.z= value.z;}}
public fixed3 wzw {get => new fixed3(w,z,w);set{this.w= value.x;this.z= value.y;this.w= value.z;}}
public fixed3 wwx {get => new fixed3(w,w,x);set{this.w= value.x;this.w= value.y;this.x= value.z;}}
public fixed3 wwy {get => new fixed3(w,w,y);set{this.w= value.x;this.w= value.y;this.y= value.z;}}
public fixed3 wwz {get => new fixed3(w,w,z);set{this.w= value.x;this.w= value.y;this.z= value.z;}}
public fixed3 www {get => new fixed3(w,w,w);set{this.w= value.x;this.w= value.y;this.w= value.z;}}
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
public half2 xy { get => this; set{ }}
public half2 xx {get => new half2(x,x);set{this.x= value.x;this.x= value.y;}}
public half2 yx {get => new half2(y,x);set{this.y= value.x;this.x= value.y;}}
public half2 yy {get => new half2(y,y);set{this.y= value.x;this.y= value.y;}}
public half3 xxx {get => new half3(x,x,x);set{this.x= value.x;this.x= value.y;this.x= value.z;}}
public half3 xxy {get => new half3(x,x,y);set{this.x= value.x;this.x= value.y;this.y= value.z;}}
public half3 xyx {get => new half3(x,y,x);set{this.x= value.x;this.y= value.y;this.x= value.z;}}
public half3 xyy {get => new half3(x,y,y);set{this.x= value.x;this.y= value.y;this.y= value.z;}}
public half3 yxx {get => new half3(y,x,x);set{this.y= value.x;this.x= value.y;this.x= value.z;}}
public half3 yxy {get => new half3(y,x,y);set{this.y= value.x;this.x= value.y;this.y= value.z;}}
public half3 yyx {get => new half3(y,y,x);set{this.y= value.x;this.y= value.y;this.x= value.z;}}
public half3 yyy {get => new half3(y,y,y);set{this.y= value.x;this.y= value.y;this.y= value.z;}}
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
[FieldOffset(0)]
public half2 rg;
public half3 xyz { get => this; set{ }}
public half2 xx {get => new half2(x,x);set{this.x= value.x;this.x= value.y;}}
public half2 xz {get => new half2(x,z);set{this.x= value.x;this.z= value.y;}}
public half2 yx {get => new half2(y,x);set{this.y= value.x;this.x= value.y;}}
public half2 yy {get => new half2(y,y);set{this.y= value.x;this.y= value.y;}}
public half2 yz {get => new half2(y,z);set{this.y= value.x;this.z= value.y;}}
public half2 zx {get => new half2(z,x);set{this.z= value.x;this.x= value.y;}}
public half2 zy {get => new half2(z,y);set{this.z= value.x;this.y= value.y;}}
public half2 zz {get => new half2(z,z);set{this.z= value.x;this.z= value.y;}}
public half3 xxx {get => new half3(x,x,x);set{this.x= value.x;this.x= value.y;this.x= value.z;}}
public half3 xxy {get => new half3(x,x,y);set{this.x= value.x;this.x= value.y;this.y= value.z;}}
public half3 xxz {get => new half3(x,x,z);set{this.x= value.x;this.x= value.y;this.z= value.z;}}
public half3 xyx {get => new half3(x,y,x);set{this.x= value.x;this.y= value.y;this.x= value.z;}}
public half3 xyy {get => new half3(x,y,y);set{this.x= value.x;this.y= value.y;this.y= value.z;}}
public half3 xzx {get => new half3(x,z,x);set{this.x= value.x;this.z= value.y;this.x= value.z;}}
public half3 xzy {get => new half3(x,z,y);set{this.x= value.x;this.z= value.y;this.y= value.z;}}
public half3 xzz {get => new half3(x,z,z);set{this.x= value.x;this.z= value.y;this.z= value.z;}}
public half3 yxx {get => new half3(y,x,x);set{this.y= value.x;this.x= value.y;this.x= value.z;}}
public half3 yxy {get => new half3(y,x,y);set{this.y= value.x;this.x= value.y;this.y= value.z;}}
public half3 yxz {get => new half3(y,x,z);set{this.y= value.x;this.x= value.y;this.z= value.z;}}
public half3 yyx {get => new half3(y,y,x);set{this.y= value.x;this.y= value.y;this.x= value.z;}}
public half3 yyy {get => new half3(y,y,y);set{this.y= value.x;this.y= value.y;this.y= value.z;}}
public half3 yyz {get => new half3(y,y,z);set{this.y= value.x;this.y= value.y;this.z= value.z;}}
public half3 yzx {get => new half3(y,z,x);set{this.y= value.x;this.z= value.y;this.x= value.z;}}
public half3 yzy {get => new half3(y,z,y);set{this.y= value.x;this.z= value.y;this.y= value.z;}}
public half3 yzz {get => new half3(y,z,z);set{this.y= value.x;this.z= value.y;this.z= value.z;}}
public half3 zxx {get => new half3(z,x,x);set{this.z= value.x;this.x= value.y;this.x= value.z;}}
public half3 zxy {get => new half3(z,x,y);set{this.z= value.x;this.x= value.y;this.y= value.z;}}
public half3 zxz {get => new half3(z,x,z);set{this.z= value.x;this.x= value.y;this.z= value.z;}}
public half3 zyx {get => new half3(z,y,x);set{this.z= value.x;this.y= value.y;this.x= value.z;}}
public half3 zyy {get => new half3(z,y,y);set{this.z= value.x;this.y= value.y;this.y= value.z;}}
public half3 zyz {get => new half3(z,y,z);set{this.z= value.x;this.y= value.y;this.z= value.z;}}
public half3 zzx {get => new half3(z,z,x);set{this.z= value.x;this.z= value.y;this.x= value.z;}}
public half3 zzy {get => new half3(z,z,y);set{this.z= value.x;this.z= value.y;this.y= value.z;}}
public half3 zzz {get => new half3(z,z,z);set{this.z= value.x;this.z= value.y;this.z= value.z;}}
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
public half2 rg;
[FieldOffset(0)]
public half3 xyz;
[FieldOffset(0)]
public half3 rgb;
public half4 xyzw { get => this; set{ }}
public half2 xx {get => new half2(x,x);set{this.x= value.x;this.x= value.y;}}
public half2 xz {get => new half2(x,z);set{this.x= value.x;this.z= value.y;}}
public half2 xw {get => new half2(x,w);set{this.x= value.x;this.w= value.y;}}
public half2 yx {get => new half2(y,x);set{this.y= value.x;this.x= value.y;}}
public half2 yy {get => new half2(y,y);set{this.y= value.x;this.y= value.y;}}
public half2 yz {get => new half2(y,z);set{this.y= value.x;this.z= value.y;}}
public half2 yw {get => new half2(y,w);set{this.y= value.x;this.w= value.y;}}
public half2 zx {get => new half2(z,x);set{this.z= value.x;this.x= value.y;}}
public half2 zy {get => new half2(z,y);set{this.z= value.x;this.y= value.y;}}
public half2 zz {get => new half2(z,z);set{this.z= value.x;this.z= value.y;}}
public half2 zw {get => new half2(z,w);set{this.z= value.x;this.w= value.y;}}
public half2 wx {get => new half2(w,x);set{this.w= value.x;this.x= value.y;}}
public half2 wy {get => new half2(w,y);set{this.w= value.x;this.y= value.y;}}
public half2 wz {get => new half2(w,z);set{this.w= value.x;this.z= value.y;}}
public half2 ww {get => new half2(w,w);set{this.w= value.x;this.w= value.y;}}
public half3 xxx {get => new half3(x,x,x);set{this.x= value.x;this.x= value.y;this.x= value.z;}}
public half3 xxy {get => new half3(x,x,y);set{this.x= value.x;this.x= value.y;this.y= value.z;}}
public half3 xxz {get => new half3(x,x,z);set{this.x= value.x;this.x= value.y;this.z= value.z;}}
public half3 xxw {get => new half3(x,x,w);set{this.x= value.x;this.x= value.y;this.w= value.z;}}
public half3 xyx {get => new half3(x,y,x);set{this.x= value.x;this.y= value.y;this.x= value.z;}}
public half3 xyy {get => new half3(x,y,y);set{this.x= value.x;this.y= value.y;this.y= value.z;}}
public half3 xyw {get => new half3(x,y,w);set{this.x= value.x;this.y= value.y;this.w= value.z;}}
public half3 xzx {get => new half3(x,z,x);set{this.x= value.x;this.z= value.y;this.x= value.z;}}
public half3 xzy {get => new half3(x,z,y);set{this.x= value.x;this.z= value.y;this.y= value.z;}}
public half3 xzz {get => new half3(x,z,z);set{this.x= value.x;this.z= value.y;this.z= value.z;}}
public half3 xzw {get => new half3(x,z,w);set{this.x= value.x;this.z= value.y;this.w= value.z;}}
public half3 xwx {get => new half3(x,w,x);set{this.x= value.x;this.w= value.y;this.x= value.z;}}
public half3 xwy {get => new half3(x,w,y);set{this.x= value.x;this.w= value.y;this.y= value.z;}}
public half3 xwz {get => new half3(x,w,z);set{this.x= value.x;this.w= value.y;this.z= value.z;}}
public half3 xww {get => new half3(x,w,w);set{this.x= value.x;this.w= value.y;this.w= value.z;}}
public half3 yxx {get => new half3(y,x,x);set{this.y= value.x;this.x= value.y;this.x= value.z;}}
public half3 yxy {get => new half3(y,x,y);set{this.y= value.x;this.x= value.y;this.y= value.z;}}
public half3 yxz {get => new half3(y,x,z);set{this.y= value.x;this.x= value.y;this.z= value.z;}}
public half3 yxw {get => new half3(y,x,w);set{this.y= value.x;this.x= value.y;this.w= value.z;}}
public half3 yyx {get => new half3(y,y,x);set{this.y= value.x;this.y= value.y;this.x= value.z;}}
public half3 yyy {get => new half3(y,y,y);set{this.y= value.x;this.y= value.y;this.y= value.z;}}
public half3 yyz {get => new half3(y,y,z);set{this.y= value.x;this.y= value.y;this.z= value.z;}}
public half3 yyw {get => new half3(y,y,w);set{this.y= value.x;this.y= value.y;this.w= value.z;}}
public half3 yzx {get => new half3(y,z,x);set{this.y= value.x;this.z= value.y;this.x= value.z;}}
public half3 yzy {get => new half3(y,z,y);set{this.y= value.x;this.z= value.y;this.y= value.z;}}
public half3 yzz {get => new half3(y,z,z);set{this.y= value.x;this.z= value.y;this.z= value.z;}}
public half3 yzw {get => new half3(y,z,w);set{this.y= value.x;this.z= value.y;this.w= value.z;}}
public half3 ywx {get => new half3(y,w,x);set{this.y= value.x;this.w= value.y;this.x= value.z;}}
public half3 ywy {get => new half3(y,w,y);set{this.y= value.x;this.w= value.y;this.y= value.z;}}
public half3 ywz {get => new half3(y,w,z);set{this.y= value.x;this.w= value.y;this.z= value.z;}}
public half3 yww {get => new half3(y,w,w);set{this.y= value.x;this.w= value.y;this.w= value.z;}}
public half3 zxx {get => new half3(z,x,x);set{this.z= value.x;this.x= value.y;this.x= value.z;}}
public half3 zxy {get => new half3(z,x,y);set{this.z= value.x;this.x= value.y;this.y= value.z;}}
public half3 zxz {get => new half3(z,x,z);set{this.z= value.x;this.x= value.y;this.z= value.z;}}
public half3 zxw {get => new half3(z,x,w);set{this.z= value.x;this.x= value.y;this.w= value.z;}}
public half3 zyx {get => new half3(z,y,x);set{this.z= value.x;this.y= value.y;this.x= value.z;}}
public half3 zyy {get => new half3(z,y,y);set{this.z= value.x;this.y= value.y;this.y= value.z;}}
public half3 zyz {get => new half3(z,y,z);set{this.z= value.x;this.y= value.y;this.z= value.z;}}
public half3 zyw {get => new half3(z,y,w);set{this.z= value.x;this.y= value.y;this.w= value.z;}}
public half3 zzx {get => new half3(z,z,x);set{this.z= value.x;this.z= value.y;this.x= value.z;}}
public half3 zzy {get => new half3(z,z,y);set{this.z= value.x;this.z= value.y;this.y= value.z;}}
public half3 zzz {get => new half3(z,z,z);set{this.z= value.x;this.z= value.y;this.z= value.z;}}
public half3 zzw {get => new half3(z,z,w);set{this.z= value.x;this.z= value.y;this.w= value.z;}}
public half3 zwx {get => new half3(z,w,x);set{this.z= value.x;this.w= value.y;this.x= value.z;}}
public half3 zwy {get => new half3(z,w,y);set{this.z= value.x;this.w= value.y;this.y= value.z;}}
public half3 zwz {get => new half3(z,w,z);set{this.z= value.x;this.w= value.y;this.z= value.z;}}
public half3 zww {get => new half3(z,w,w);set{this.z= value.x;this.w= value.y;this.w= value.z;}}
public half3 wxx {get => new half3(w,x,x);set{this.w= value.x;this.x= value.y;this.x= value.z;}}
public half3 wxy {get => new half3(w,x,y);set{this.w= value.x;this.x= value.y;this.y= value.z;}}
public half3 wxz {get => new half3(w,x,z);set{this.w= value.x;this.x= value.y;this.z= value.z;}}
public half3 wxw {get => new half3(w,x,w);set{this.w= value.x;this.x= value.y;this.w= value.z;}}
public half3 wyx {get => new half3(w,y,x);set{this.w= value.x;this.y= value.y;this.x= value.z;}}
public half3 wyy {get => new half3(w,y,y);set{this.w= value.x;this.y= value.y;this.y= value.z;}}
public half3 wyz {get => new half3(w,y,z);set{this.w= value.x;this.y= value.y;this.z= value.z;}}
public half3 wyw {get => new half3(w,y,w);set{this.w= value.x;this.y= value.y;this.w= value.z;}}
public half3 wzx {get => new half3(w,z,x);set{this.w= value.x;this.z= value.y;this.x= value.z;}}
public half3 wzy {get => new half3(w,z,y);set{this.w= value.x;this.z= value.y;this.y= value.z;}}
public half3 wzz {get => new half3(w,z,z);set{this.w= value.x;this.z= value.y;this.z= value.z;}}
public half3 wzw {get => new half3(w,z,w);set{this.w= value.x;this.z= value.y;this.w= value.z;}}
public half3 wwx {get => new half3(w,w,x);set{this.w= value.x;this.w= value.y;this.x= value.z;}}
public half3 wwy {get => new half3(w,w,y);set{this.w= value.x;this.w= value.y;this.y= value.z;}}
public half3 wwz {get => new half3(w,w,z);set{this.w= value.x;this.w= value.y;this.z= value.z;}}
public half3 www {get => new half3(w,w,w);set{this.w= value.x;this.w= value.y;this.w= value.z;}}
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
	