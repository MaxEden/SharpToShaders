


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
public float3 xxx {get => new float3(x,x,x);set{x= value.x;x= value.y;x= value.z;}}
public float3 xxy {get => new float3(x,x,y);set{x= value.x;x= value.y;y= value.z;}}
public float3 xyx {get => new float3(x,y,x);set{x= value.x;y= value.y;x= value.z;}}
public float3 xyy {get => new float3(x,y,y);set{x= value.x;y= value.y;y= value.z;}}
public float3 yxx {get => new float3(y,x,x);set{y= value.x;x= value.y;x= value.z;}}
public float3 yxy {get => new float3(y,x,y);set{y= value.x;x= value.y;y= value.z;}}
public float3 yyx {get => new float3(y,y,x);set{y= value.x;y= value.y;x= value.z;}}
public float3 yyy {get => new float3(y,y,y);set{y= value.x;y= value.y;y= value.z;}}
public float4 xxxx {get => new float4(x,x,x,x);set{x= value.x;x= value.y;x= value.z;x= value.w;}}
public float4 xxxy {get => new float4(x,x,x,y);set{x= value.x;x= value.y;x= value.z;y= value.w;}}
public float4 xxyx {get => new float4(x,x,y,x);set{x= value.x;x= value.y;y= value.z;x= value.w;}}
public float4 xxyy {get => new float4(x,x,y,y);set{x= value.x;x= value.y;y= value.z;y= value.w;}}
public float4 xyxx {get => new float4(x,y,x,x);set{x= value.x;y= value.y;x= value.z;x= value.w;}}
public float4 xyxy {get => new float4(x,y,x,y);set{x= value.x;y= value.y;x= value.z;y= value.w;}}
public float4 xyyx {get => new float4(x,y,y,x);set{x= value.x;y= value.y;y= value.z;x= value.w;}}
public float4 xyyy {get => new float4(x,y,y,y);set{x= value.x;y= value.y;y= value.z;y= value.w;}}
public float4 yxxx {get => new float4(y,x,x,x);set{y= value.x;x= value.y;x= value.z;x= value.w;}}
public float4 yxxy {get => new float4(y,x,x,y);set{y= value.x;x= value.y;x= value.z;y= value.w;}}
public float4 yxyx {get => new float4(y,x,y,x);set{y= value.x;x= value.y;y= value.z;x= value.w;}}
public float4 yxyy {get => new float4(y,x,y,y);set{y= value.x;x= value.y;y= value.z;y= value.w;}}
public float4 yyxx {get => new float4(y,y,x,x);set{y= value.x;y= value.y;x= value.z;x= value.w;}}
public float4 yyxy {get => new float4(y,y,x,y);set{y= value.x;y= value.y;x= value.z;y= value.w;}}
public float4 yyyx {get => new float4(y,y,y,x);set{y= value.x;y= value.y;y= value.z;x= value.w;}}
public float4 yyyy {get => new float4(y,y,y,y);set{y= value.x;y= value.y;y= value.z;y= value.w;}}
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
public static float2 operator -(float2 vector)
{
return new float2(
-vector.x,
-vector.y
);
}
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
public float3 xxx {get => new float3(x,x,x);set{x= value.x;x= value.y;x= value.z;}}
public float3 xxy {get => new float3(x,x,y);set{x= value.x;x= value.y;y= value.z;}}
public float3 xxz {get => new float3(x,x,z);set{x= value.x;x= value.y;z= value.z;}}
public float3 xyx {get => new float3(x,y,x);set{x= value.x;y= value.y;x= value.z;}}
public float3 xyy {get => new float3(x,y,y);set{x= value.x;y= value.y;y= value.z;}}
public float3 xzx {get => new float3(x,z,x);set{x= value.x;z= value.y;x= value.z;}}
public float3 xzy {get => new float3(x,z,y);set{x= value.x;z= value.y;y= value.z;}}
public float3 xzz {get => new float3(x,z,z);set{x= value.x;z= value.y;z= value.z;}}
public float3 yxx {get => new float3(y,x,x);set{y= value.x;x= value.y;x= value.z;}}
public float3 yxy {get => new float3(y,x,y);set{y= value.x;x= value.y;y= value.z;}}
public float3 yxz {get => new float3(y,x,z);set{y= value.x;x= value.y;z= value.z;}}
public float3 yyx {get => new float3(y,y,x);set{y= value.x;y= value.y;x= value.z;}}
public float3 yyy {get => new float3(y,y,y);set{y= value.x;y= value.y;y= value.z;}}
public float3 yyz {get => new float3(y,y,z);set{y= value.x;y= value.y;z= value.z;}}
public float3 yzx {get => new float3(y,z,x);set{y= value.x;z= value.y;x= value.z;}}
public float3 yzy {get => new float3(y,z,y);set{y= value.x;z= value.y;y= value.z;}}
public float3 yzz {get => new float3(y,z,z);set{y= value.x;z= value.y;z= value.z;}}
public float3 zxx {get => new float3(z,x,x);set{z= value.x;x= value.y;x= value.z;}}
public float3 zxy {get => new float3(z,x,y);set{z= value.x;x= value.y;y= value.z;}}
public float3 zxz {get => new float3(z,x,z);set{z= value.x;x= value.y;z= value.z;}}
public float3 zyx {get => new float3(z,y,x);set{z= value.x;y= value.y;x= value.z;}}
public float3 zyy {get => new float3(z,y,y);set{z= value.x;y= value.y;y= value.z;}}
public float3 zyz {get => new float3(z,y,z);set{z= value.x;y= value.y;z= value.z;}}
public float3 zzx {get => new float3(z,z,x);set{z= value.x;z= value.y;x= value.z;}}
public float3 zzy {get => new float3(z,z,y);set{z= value.x;z= value.y;y= value.z;}}
public float3 zzz {get => new float3(z,z,z);set{z= value.x;z= value.y;z= value.z;}}
public float4 xxxx {get => new float4(x,x,x,x);set{x= value.x;x= value.y;x= value.z;x= value.w;}}
public float4 xxxy {get => new float4(x,x,x,y);set{x= value.x;x= value.y;x= value.z;y= value.w;}}
public float4 xxxz {get => new float4(x,x,x,z);set{x= value.x;x= value.y;x= value.z;z= value.w;}}
public float4 xxyx {get => new float4(x,x,y,x);set{x= value.x;x= value.y;y= value.z;x= value.w;}}
public float4 xxyy {get => new float4(x,x,y,y);set{x= value.x;x= value.y;y= value.z;y= value.w;}}
public float4 xxyz {get => new float4(x,x,y,z);set{x= value.x;x= value.y;y= value.z;z= value.w;}}
public float4 xxzx {get => new float4(x,x,z,x);set{x= value.x;x= value.y;z= value.z;x= value.w;}}
public float4 xxzy {get => new float4(x,x,z,y);set{x= value.x;x= value.y;z= value.z;y= value.w;}}
public float4 xxzz {get => new float4(x,x,z,z);set{x= value.x;x= value.y;z= value.z;z= value.w;}}
public float4 xyxx {get => new float4(x,y,x,x);set{x= value.x;y= value.y;x= value.z;x= value.w;}}
public float4 xyxy {get => new float4(x,y,x,y);set{x= value.x;y= value.y;x= value.z;y= value.w;}}
public float4 xyxz {get => new float4(x,y,x,z);set{x= value.x;y= value.y;x= value.z;z= value.w;}}
public float4 xyyx {get => new float4(x,y,y,x);set{x= value.x;y= value.y;y= value.z;x= value.w;}}
public float4 xyyy {get => new float4(x,y,y,y);set{x= value.x;y= value.y;y= value.z;y= value.w;}}
public float4 xyyz {get => new float4(x,y,y,z);set{x= value.x;y= value.y;y= value.z;z= value.w;}}
public float4 xyzx {get => new float4(x,y,z,x);set{x= value.x;y= value.y;z= value.z;x= value.w;}}
public float4 xyzy {get => new float4(x,y,z,y);set{x= value.x;y= value.y;z= value.z;y= value.w;}}
public float4 xyzz {get => new float4(x,y,z,z);set{x= value.x;y= value.y;z= value.z;z= value.w;}}
public float4 xzxx {get => new float4(x,z,x,x);set{x= value.x;z= value.y;x= value.z;x= value.w;}}
public float4 xzxy {get => new float4(x,z,x,y);set{x= value.x;z= value.y;x= value.z;y= value.w;}}
public float4 xzxz {get => new float4(x,z,x,z);set{x= value.x;z= value.y;x= value.z;z= value.w;}}
public float4 xzyx {get => new float4(x,z,y,x);set{x= value.x;z= value.y;y= value.z;x= value.w;}}
public float4 xzyy {get => new float4(x,z,y,y);set{x= value.x;z= value.y;y= value.z;y= value.w;}}
public float4 xzyz {get => new float4(x,z,y,z);set{x= value.x;z= value.y;y= value.z;z= value.w;}}
public float4 xzzx {get => new float4(x,z,z,x);set{x= value.x;z= value.y;z= value.z;x= value.w;}}
public float4 xzzy {get => new float4(x,z,z,y);set{x= value.x;z= value.y;z= value.z;y= value.w;}}
public float4 xzzz {get => new float4(x,z,z,z);set{x= value.x;z= value.y;z= value.z;z= value.w;}}
public float4 yxxx {get => new float4(y,x,x,x);set{y= value.x;x= value.y;x= value.z;x= value.w;}}
public float4 yxxy {get => new float4(y,x,x,y);set{y= value.x;x= value.y;x= value.z;y= value.w;}}
public float4 yxxz {get => new float4(y,x,x,z);set{y= value.x;x= value.y;x= value.z;z= value.w;}}
public float4 yxyx {get => new float4(y,x,y,x);set{y= value.x;x= value.y;y= value.z;x= value.w;}}
public float4 yxyy {get => new float4(y,x,y,y);set{y= value.x;x= value.y;y= value.z;y= value.w;}}
public float4 yxyz {get => new float4(y,x,y,z);set{y= value.x;x= value.y;y= value.z;z= value.w;}}
public float4 yxzx {get => new float4(y,x,z,x);set{y= value.x;x= value.y;z= value.z;x= value.w;}}
public float4 yxzy {get => new float4(y,x,z,y);set{y= value.x;x= value.y;z= value.z;y= value.w;}}
public float4 yxzz {get => new float4(y,x,z,z);set{y= value.x;x= value.y;z= value.z;z= value.w;}}
public float4 yyxx {get => new float4(y,y,x,x);set{y= value.x;y= value.y;x= value.z;x= value.w;}}
public float4 yyxy {get => new float4(y,y,x,y);set{y= value.x;y= value.y;x= value.z;y= value.w;}}
public float4 yyxz {get => new float4(y,y,x,z);set{y= value.x;y= value.y;x= value.z;z= value.w;}}
public float4 yyyx {get => new float4(y,y,y,x);set{y= value.x;y= value.y;y= value.z;x= value.w;}}
public float4 yyyy {get => new float4(y,y,y,y);set{y= value.x;y= value.y;y= value.z;y= value.w;}}
public float4 yyyz {get => new float4(y,y,y,z);set{y= value.x;y= value.y;y= value.z;z= value.w;}}
public float4 yyzx {get => new float4(y,y,z,x);set{y= value.x;y= value.y;z= value.z;x= value.w;}}
public float4 yyzy {get => new float4(y,y,z,y);set{y= value.x;y= value.y;z= value.z;y= value.w;}}
public float4 yyzz {get => new float4(y,y,z,z);set{y= value.x;y= value.y;z= value.z;z= value.w;}}
public float4 yzxx {get => new float4(y,z,x,x);set{y= value.x;z= value.y;x= value.z;x= value.w;}}
public float4 yzxy {get => new float4(y,z,x,y);set{y= value.x;z= value.y;x= value.z;y= value.w;}}
public float4 yzxz {get => new float4(y,z,x,z);set{y= value.x;z= value.y;x= value.z;z= value.w;}}
public float4 yzyx {get => new float4(y,z,y,x);set{y= value.x;z= value.y;y= value.z;x= value.w;}}
public float4 yzyy {get => new float4(y,z,y,y);set{y= value.x;z= value.y;y= value.z;y= value.w;}}
public float4 yzyz {get => new float4(y,z,y,z);set{y= value.x;z= value.y;y= value.z;z= value.w;}}
public float4 yzzx {get => new float4(y,z,z,x);set{y= value.x;z= value.y;z= value.z;x= value.w;}}
public float4 yzzy {get => new float4(y,z,z,y);set{y= value.x;z= value.y;z= value.z;y= value.w;}}
public float4 yzzz {get => new float4(y,z,z,z);set{y= value.x;z= value.y;z= value.z;z= value.w;}}
public float4 zxxx {get => new float4(z,x,x,x);set{z= value.x;x= value.y;x= value.z;x= value.w;}}
public float4 zxxy {get => new float4(z,x,x,y);set{z= value.x;x= value.y;x= value.z;y= value.w;}}
public float4 zxxz {get => new float4(z,x,x,z);set{z= value.x;x= value.y;x= value.z;z= value.w;}}
public float4 zxyx {get => new float4(z,x,y,x);set{z= value.x;x= value.y;y= value.z;x= value.w;}}
public float4 zxyy {get => new float4(z,x,y,y);set{z= value.x;x= value.y;y= value.z;y= value.w;}}
public float4 zxyz {get => new float4(z,x,y,z);set{z= value.x;x= value.y;y= value.z;z= value.w;}}
public float4 zxzx {get => new float4(z,x,z,x);set{z= value.x;x= value.y;z= value.z;x= value.w;}}
public float4 zxzy {get => new float4(z,x,z,y);set{z= value.x;x= value.y;z= value.z;y= value.w;}}
public float4 zxzz {get => new float4(z,x,z,z);set{z= value.x;x= value.y;z= value.z;z= value.w;}}
public float4 zyxx {get => new float4(z,y,x,x);set{z= value.x;y= value.y;x= value.z;x= value.w;}}
public float4 zyxy {get => new float4(z,y,x,y);set{z= value.x;y= value.y;x= value.z;y= value.w;}}
public float4 zyxz {get => new float4(z,y,x,z);set{z= value.x;y= value.y;x= value.z;z= value.w;}}
public float4 zyyx {get => new float4(z,y,y,x);set{z= value.x;y= value.y;y= value.z;x= value.w;}}
public float4 zyyy {get => new float4(z,y,y,y);set{z= value.x;y= value.y;y= value.z;y= value.w;}}
public float4 zyyz {get => new float4(z,y,y,z);set{z= value.x;y= value.y;y= value.z;z= value.w;}}
public float4 zyzx {get => new float4(z,y,z,x);set{z= value.x;y= value.y;z= value.z;x= value.w;}}
public float4 zyzy {get => new float4(z,y,z,y);set{z= value.x;y= value.y;z= value.z;y= value.w;}}
public float4 zyzz {get => new float4(z,y,z,z);set{z= value.x;y= value.y;z= value.z;z= value.w;}}
public float4 zzxx {get => new float4(z,z,x,x);set{z= value.x;z= value.y;x= value.z;x= value.w;}}
public float4 zzxy {get => new float4(z,z,x,y);set{z= value.x;z= value.y;x= value.z;y= value.w;}}
public float4 zzxz {get => new float4(z,z,x,z);set{z= value.x;z= value.y;x= value.z;z= value.w;}}
public float4 zzyx {get => new float4(z,z,y,x);set{z= value.x;z= value.y;y= value.z;x= value.w;}}
public float4 zzyy {get => new float4(z,z,y,y);set{z= value.x;z= value.y;y= value.z;y= value.w;}}
public float4 zzyz {get => new float4(z,z,y,z);set{z= value.x;z= value.y;y= value.z;z= value.w;}}
public float4 zzzx {get => new float4(z,z,z,x);set{z= value.x;z= value.y;z= value.z;x= value.w;}}
public float4 zzzy {get => new float4(z,z,z,y);set{z= value.x;z= value.y;z= value.z;y= value.w;}}
public float4 zzzz {get => new float4(z,z,z,z);set{z= value.x;z= value.y;z= value.z;z= value.w;}}
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
public static float3 operator -(float3 vector)
{
return new float3(
-vector.x,
-vector.y,
-vector.z
);
}
public static implicit operator float2(float3 v) { throw new NotImplementedException(); }
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
public float3 xxx {get => new float3(x,x,x);set{x= value.x;x= value.y;x= value.z;}}
public float3 xxy {get => new float3(x,x,y);set{x= value.x;x= value.y;y= value.z;}}
public float3 xxz {get => new float3(x,x,z);set{x= value.x;x= value.y;z= value.z;}}
public float3 xxw {get => new float3(x,x,w);set{x= value.x;x= value.y;w= value.z;}}
public float3 xyx {get => new float3(x,y,x);set{x= value.x;y= value.y;x= value.z;}}
public float3 xyy {get => new float3(x,y,y);set{x= value.x;y= value.y;y= value.z;}}
public float3 xyw {get => new float3(x,y,w);set{x= value.x;y= value.y;w= value.z;}}
public float3 xzx {get => new float3(x,z,x);set{x= value.x;z= value.y;x= value.z;}}
public float3 xzy {get => new float3(x,z,y);set{x= value.x;z= value.y;y= value.z;}}
public float3 xzz {get => new float3(x,z,z);set{x= value.x;z= value.y;z= value.z;}}
public float3 xzw {get => new float3(x,z,w);set{x= value.x;z= value.y;w= value.z;}}
public float3 xwx {get => new float3(x,w,x);set{x= value.x;w= value.y;x= value.z;}}
public float3 xwy {get => new float3(x,w,y);set{x= value.x;w= value.y;y= value.z;}}
public float3 xwz {get => new float3(x,w,z);set{x= value.x;w= value.y;z= value.z;}}
public float3 xww {get => new float3(x,w,w);set{x= value.x;w= value.y;w= value.z;}}
public float3 yxx {get => new float3(y,x,x);set{y= value.x;x= value.y;x= value.z;}}
public float3 yxy {get => new float3(y,x,y);set{y= value.x;x= value.y;y= value.z;}}
public float3 yxz {get => new float3(y,x,z);set{y= value.x;x= value.y;z= value.z;}}
public float3 yxw {get => new float3(y,x,w);set{y= value.x;x= value.y;w= value.z;}}
public float3 yyx {get => new float3(y,y,x);set{y= value.x;y= value.y;x= value.z;}}
public float3 yyy {get => new float3(y,y,y);set{y= value.x;y= value.y;y= value.z;}}
public float3 yyz {get => new float3(y,y,z);set{y= value.x;y= value.y;z= value.z;}}
public float3 yyw {get => new float3(y,y,w);set{y= value.x;y= value.y;w= value.z;}}
public float3 yzx {get => new float3(y,z,x);set{y= value.x;z= value.y;x= value.z;}}
public float3 yzy {get => new float3(y,z,y);set{y= value.x;z= value.y;y= value.z;}}
public float3 yzz {get => new float3(y,z,z);set{y= value.x;z= value.y;z= value.z;}}
public float3 yzw {get => new float3(y,z,w);set{y= value.x;z= value.y;w= value.z;}}
public float3 ywx {get => new float3(y,w,x);set{y= value.x;w= value.y;x= value.z;}}
public float3 ywy {get => new float3(y,w,y);set{y= value.x;w= value.y;y= value.z;}}
public float3 ywz {get => new float3(y,w,z);set{y= value.x;w= value.y;z= value.z;}}
public float3 yww {get => new float3(y,w,w);set{y= value.x;w= value.y;w= value.z;}}
public float3 zxx {get => new float3(z,x,x);set{z= value.x;x= value.y;x= value.z;}}
public float3 zxy {get => new float3(z,x,y);set{z= value.x;x= value.y;y= value.z;}}
public float3 zxz {get => new float3(z,x,z);set{z= value.x;x= value.y;z= value.z;}}
public float3 zxw {get => new float3(z,x,w);set{z= value.x;x= value.y;w= value.z;}}
public float3 zyx {get => new float3(z,y,x);set{z= value.x;y= value.y;x= value.z;}}
public float3 zyy {get => new float3(z,y,y);set{z= value.x;y= value.y;y= value.z;}}
public float3 zyz {get => new float3(z,y,z);set{z= value.x;y= value.y;z= value.z;}}
public float3 zyw {get => new float3(z,y,w);set{z= value.x;y= value.y;w= value.z;}}
public float3 zzx {get => new float3(z,z,x);set{z= value.x;z= value.y;x= value.z;}}
public float3 zzy {get => new float3(z,z,y);set{z= value.x;z= value.y;y= value.z;}}
public float3 zzz {get => new float3(z,z,z);set{z= value.x;z= value.y;z= value.z;}}
public float3 zzw {get => new float3(z,z,w);set{z= value.x;z= value.y;w= value.z;}}
public float3 zwx {get => new float3(z,w,x);set{z= value.x;w= value.y;x= value.z;}}
public float3 zwy {get => new float3(z,w,y);set{z= value.x;w= value.y;y= value.z;}}
public float3 zwz {get => new float3(z,w,z);set{z= value.x;w= value.y;z= value.z;}}
public float3 zww {get => new float3(z,w,w);set{z= value.x;w= value.y;w= value.z;}}
public float3 wxx {get => new float3(w,x,x);set{w= value.x;x= value.y;x= value.z;}}
public float3 wxy {get => new float3(w,x,y);set{w= value.x;x= value.y;y= value.z;}}
public float3 wxz {get => new float3(w,x,z);set{w= value.x;x= value.y;z= value.z;}}
public float3 wxw {get => new float3(w,x,w);set{w= value.x;x= value.y;w= value.z;}}
public float3 wyx {get => new float3(w,y,x);set{w= value.x;y= value.y;x= value.z;}}
public float3 wyy {get => new float3(w,y,y);set{w= value.x;y= value.y;y= value.z;}}
public float3 wyz {get => new float3(w,y,z);set{w= value.x;y= value.y;z= value.z;}}
public float3 wyw {get => new float3(w,y,w);set{w= value.x;y= value.y;w= value.z;}}
public float3 wzx {get => new float3(w,z,x);set{w= value.x;z= value.y;x= value.z;}}
public float3 wzy {get => new float3(w,z,y);set{w= value.x;z= value.y;y= value.z;}}
public float3 wzz {get => new float3(w,z,z);set{w= value.x;z= value.y;z= value.z;}}
public float3 wzw {get => new float3(w,z,w);set{w= value.x;z= value.y;w= value.z;}}
public float3 wwx {get => new float3(w,w,x);set{w= value.x;w= value.y;x= value.z;}}
public float3 wwy {get => new float3(w,w,y);set{w= value.x;w= value.y;y= value.z;}}
public float3 wwz {get => new float3(w,w,z);set{w= value.x;w= value.y;z= value.z;}}
public float3 www {get => new float3(w,w,w);set{w= value.x;w= value.y;w= value.z;}}
public float4 xxxx {get => new float4(x,x,x,x);set{x= value.x;x= value.y;x= value.z;x= value.w;}}
public float4 xxxy {get => new float4(x,x,x,y);set{x= value.x;x= value.y;x= value.z;y= value.w;}}
public float4 xxxz {get => new float4(x,x,x,z);set{x= value.x;x= value.y;x= value.z;z= value.w;}}
public float4 xxxw {get => new float4(x,x,x,w);set{x= value.x;x= value.y;x= value.z;w= value.w;}}
public float4 xxyx {get => new float4(x,x,y,x);set{x= value.x;x= value.y;y= value.z;x= value.w;}}
public float4 xxyy {get => new float4(x,x,y,y);set{x= value.x;x= value.y;y= value.z;y= value.w;}}
public float4 xxyz {get => new float4(x,x,y,z);set{x= value.x;x= value.y;y= value.z;z= value.w;}}
public float4 xxyw {get => new float4(x,x,y,w);set{x= value.x;x= value.y;y= value.z;w= value.w;}}
public float4 xxzx {get => new float4(x,x,z,x);set{x= value.x;x= value.y;z= value.z;x= value.w;}}
public float4 xxzy {get => new float4(x,x,z,y);set{x= value.x;x= value.y;z= value.z;y= value.w;}}
public float4 xxzz {get => new float4(x,x,z,z);set{x= value.x;x= value.y;z= value.z;z= value.w;}}
public float4 xxzw {get => new float4(x,x,z,w);set{x= value.x;x= value.y;z= value.z;w= value.w;}}
public float4 xxwx {get => new float4(x,x,w,x);set{x= value.x;x= value.y;w= value.z;x= value.w;}}
public float4 xxwy {get => new float4(x,x,w,y);set{x= value.x;x= value.y;w= value.z;y= value.w;}}
public float4 xxwz {get => new float4(x,x,w,z);set{x= value.x;x= value.y;w= value.z;z= value.w;}}
public float4 xxww {get => new float4(x,x,w,w);set{x= value.x;x= value.y;w= value.z;w= value.w;}}
public float4 xyxx {get => new float4(x,y,x,x);set{x= value.x;y= value.y;x= value.z;x= value.w;}}
public float4 xyxy {get => new float4(x,y,x,y);set{x= value.x;y= value.y;x= value.z;y= value.w;}}
public float4 xyxz {get => new float4(x,y,x,z);set{x= value.x;y= value.y;x= value.z;z= value.w;}}
public float4 xyxw {get => new float4(x,y,x,w);set{x= value.x;y= value.y;x= value.z;w= value.w;}}
public float4 xyyx {get => new float4(x,y,y,x);set{x= value.x;y= value.y;y= value.z;x= value.w;}}
public float4 xyyy {get => new float4(x,y,y,y);set{x= value.x;y= value.y;y= value.z;y= value.w;}}
public float4 xyyz {get => new float4(x,y,y,z);set{x= value.x;y= value.y;y= value.z;z= value.w;}}
public float4 xyyw {get => new float4(x,y,y,w);set{x= value.x;y= value.y;y= value.z;w= value.w;}}
public float4 xyzx {get => new float4(x,y,z,x);set{x= value.x;y= value.y;z= value.z;x= value.w;}}
public float4 xyzy {get => new float4(x,y,z,y);set{x= value.x;y= value.y;z= value.z;y= value.w;}}
public float4 xyzz {get => new float4(x,y,z,z);set{x= value.x;y= value.y;z= value.z;z= value.w;}}
public float4 xywx {get => new float4(x,y,w,x);set{x= value.x;y= value.y;w= value.z;x= value.w;}}
public float4 xywy {get => new float4(x,y,w,y);set{x= value.x;y= value.y;w= value.z;y= value.w;}}
public float4 xywz {get => new float4(x,y,w,z);set{x= value.x;y= value.y;w= value.z;z= value.w;}}
public float4 xyww {get => new float4(x,y,w,w);set{x= value.x;y= value.y;w= value.z;w= value.w;}}
public float4 xzxx {get => new float4(x,z,x,x);set{x= value.x;z= value.y;x= value.z;x= value.w;}}
public float4 xzxy {get => new float4(x,z,x,y);set{x= value.x;z= value.y;x= value.z;y= value.w;}}
public float4 xzxz {get => new float4(x,z,x,z);set{x= value.x;z= value.y;x= value.z;z= value.w;}}
public float4 xzxw {get => new float4(x,z,x,w);set{x= value.x;z= value.y;x= value.z;w= value.w;}}
public float4 xzyx {get => new float4(x,z,y,x);set{x= value.x;z= value.y;y= value.z;x= value.w;}}
public float4 xzyy {get => new float4(x,z,y,y);set{x= value.x;z= value.y;y= value.z;y= value.w;}}
public float4 xzyz {get => new float4(x,z,y,z);set{x= value.x;z= value.y;y= value.z;z= value.w;}}
public float4 xzyw {get => new float4(x,z,y,w);set{x= value.x;z= value.y;y= value.z;w= value.w;}}
public float4 xzzx {get => new float4(x,z,z,x);set{x= value.x;z= value.y;z= value.z;x= value.w;}}
public float4 xzzy {get => new float4(x,z,z,y);set{x= value.x;z= value.y;z= value.z;y= value.w;}}
public float4 xzzz {get => new float4(x,z,z,z);set{x= value.x;z= value.y;z= value.z;z= value.w;}}
public float4 xzzw {get => new float4(x,z,z,w);set{x= value.x;z= value.y;z= value.z;w= value.w;}}
public float4 xzwx {get => new float4(x,z,w,x);set{x= value.x;z= value.y;w= value.z;x= value.w;}}
public float4 xzwy {get => new float4(x,z,w,y);set{x= value.x;z= value.y;w= value.z;y= value.w;}}
public float4 xzwz {get => new float4(x,z,w,z);set{x= value.x;z= value.y;w= value.z;z= value.w;}}
public float4 xzww {get => new float4(x,z,w,w);set{x= value.x;z= value.y;w= value.z;w= value.w;}}
public float4 xwxx {get => new float4(x,w,x,x);set{x= value.x;w= value.y;x= value.z;x= value.w;}}
public float4 xwxy {get => new float4(x,w,x,y);set{x= value.x;w= value.y;x= value.z;y= value.w;}}
public float4 xwxz {get => new float4(x,w,x,z);set{x= value.x;w= value.y;x= value.z;z= value.w;}}
public float4 xwxw {get => new float4(x,w,x,w);set{x= value.x;w= value.y;x= value.z;w= value.w;}}
public float4 xwyx {get => new float4(x,w,y,x);set{x= value.x;w= value.y;y= value.z;x= value.w;}}
public float4 xwyy {get => new float4(x,w,y,y);set{x= value.x;w= value.y;y= value.z;y= value.w;}}
public float4 xwyz {get => new float4(x,w,y,z);set{x= value.x;w= value.y;y= value.z;z= value.w;}}
public float4 xwyw {get => new float4(x,w,y,w);set{x= value.x;w= value.y;y= value.z;w= value.w;}}
public float4 xwzx {get => new float4(x,w,z,x);set{x= value.x;w= value.y;z= value.z;x= value.w;}}
public float4 xwzy {get => new float4(x,w,z,y);set{x= value.x;w= value.y;z= value.z;y= value.w;}}
public float4 xwzz {get => new float4(x,w,z,z);set{x= value.x;w= value.y;z= value.z;z= value.w;}}
public float4 xwzw {get => new float4(x,w,z,w);set{x= value.x;w= value.y;z= value.z;w= value.w;}}
public float4 xwwx {get => new float4(x,w,w,x);set{x= value.x;w= value.y;w= value.z;x= value.w;}}
public float4 xwwy {get => new float4(x,w,w,y);set{x= value.x;w= value.y;w= value.z;y= value.w;}}
public float4 xwwz {get => new float4(x,w,w,z);set{x= value.x;w= value.y;w= value.z;z= value.w;}}
public float4 xwww {get => new float4(x,w,w,w);set{x= value.x;w= value.y;w= value.z;w= value.w;}}
public float4 yxxx {get => new float4(y,x,x,x);set{y= value.x;x= value.y;x= value.z;x= value.w;}}
public float4 yxxy {get => new float4(y,x,x,y);set{y= value.x;x= value.y;x= value.z;y= value.w;}}
public float4 yxxz {get => new float4(y,x,x,z);set{y= value.x;x= value.y;x= value.z;z= value.w;}}
public float4 yxxw {get => new float4(y,x,x,w);set{y= value.x;x= value.y;x= value.z;w= value.w;}}
public float4 yxyx {get => new float4(y,x,y,x);set{y= value.x;x= value.y;y= value.z;x= value.w;}}
public float4 yxyy {get => new float4(y,x,y,y);set{y= value.x;x= value.y;y= value.z;y= value.w;}}
public float4 yxyz {get => new float4(y,x,y,z);set{y= value.x;x= value.y;y= value.z;z= value.w;}}
public float4 yxyw {get => new float4(y,x,y,w);set{y= value.x;x= value.y;y= value.z;w= value.w;}}
public float4 yxzx {get => new float4(y,x,z,x);set{y= value.x;x= value.y;z= value.z;x= value.w;}}
public float4 yxzy {get => new float4(y,x,z,y);set{y= value.x;x= value.y;z= value.z;y= value.w;}}
public float4 yxzz {get => new float4(y,x,z,z);set{y= value.x;x= value.y;z= value.z;z= value.w;}}
public float4 yxzw {get => new float4(y,x,z,w);set{y= value.x;x= value.y;z= value.z;w= value.w;}}
public float4 yxwx {get => new float4(y,x,w,x);set{y= value.x;x= value.y;w= value.z;x= value.w;}}
public float4 yxwy {get => new float4(y,x,w,y);set{y= value.x;x= value.y;w= value.z;y= value.w;}}
public float4 yxwz {get => new float4(y,x,w,z);set{y= value.x;x= value.y;w= value.z;z= value.w;}}
public float4 yxww {get => new float4(y,x,w,w);set{y= value.x;x= value.y;w= value.z;w= value.w;}}
public float4 yyxx {get => new float4(y,y,x,x);set{y= value.x;y= value.y;x= value.z;x= value.w;}}
public float4 yyxy {get => new float4(y,y,x,y);set{y= value.x;y= value.y;x= value.z;y= value.w;}}
public float4 yyxz {get => new float4(y,y,x,z);set{y= value.x;y= value.y;x= value.z;z= value.w;}}
public float4 yyxw {get => new float4(y,y,x,w);set{y= value.x;y= value.y;x= value.z;w= value.w;}}
public float4 yyyx {get => new float4(y,y,y,x);set{y= value.x;y= value.y;y= value.z;x= value.w;}}
public float4 yyyy {get => new float4(y,y,y,y);set{y= value.x;y= value.y;y= value.z;y= value.w;}}
public float4 yyyz {get => new float4(y,y,y,z);set{y= value.x;y= value.y;y= value.z;z= value.w;}}
public float4 yyyw {get => new float4(y,y,y,w);set{y= value.x;y= value.y;y= value.z;w= value.w;}}
public float4 yyzx {get => new float4(y,y,z,x);set{y= value.x;y= value.y;z= value.z;x= value.w;}}
public float4 yyzy {get => new float4(y,y,z,y);set{y= value.x;y= value.y;z= value.z;y= value.w;}}
public float4 yyzz {get => new float4(y,y,z,z);set{y= value.x;y= value.y;z= value.z;z= value.w;}}
public float4 yyzw {get => new float4(y,y,z,w);set{y= value.x;y= value.y;z= value.z;w= value.w;}}
public float4 yywx {get => new float4(y,y,w,x);set{y= value.x;y= value.y;w= value.z;x= value.w;}}
public float4 yywy {get => new float4(y,y,w,y);set{y= value.x;y= value.y;w= value.z;y= value.w;}}
public float4 yywz {get => new float4(y,y,w,z);set{y= value.x;y= value.y;w= value.z;z= value.w;}}
public float4 yyww {get => new float4(y,y,w,w);set{y= value.x;y= value.y;w= value.z;w= value.w;}}
public float4 yzxx {get => new float4(y,z,x,x);set{y= value.x;z= value.y;x= value.z;x= value.w;}}
public float4 yzxy {get => new float4(y,z,x,y);set{y= value.x;z= value.y;x= value.z;y= value.w;}}
public float4 yzxz {get => new float4(y,z,x,z);set{y= value.x;z= value.y;x= value.z;z= value.w;}}
public float4 yzxw {get => new float4(y,z,x,w);set{y= value.x;z= value.y;x= value.z;w= value.w;}}
public float4 yzyx {get => new float4(y,z,y,x);set{y= value.x;z= value.y;y= value.z;x= value.w;}}
public float4 yzyy {get => new float4(y,z,y,y);set{y= value.x;z= value.y;y= value.z;y= value.w;}}
public float4 yzyz {get => new float4(y,z,y,z);set{y= value.x;z= value.y;y= value.z;z= value.w;}}
public float4 yzyw {get => new float4(y,z,y,w);set{y= value.x;z= value.y;y= value.z;w= value.w;}}
public float4 yzzx {get => new float4(y,z,z,x);set{y= value.x;z= value.y;z= value.z;x= value.w;}}
public float4 yzzy {get => new float4(y,z,z,y);set{y= value.x;z= value.y;z= value.z;y= value.w;}}
public float4 yzzz {get => new float4(y,z,z,z);set{y= value.x;z= value.y;z= value.z;z= value.w;}}
public float4 yzzw {get => new float4(y,z,z,w);set{y= value.x;z= value.y;z= value.z;w= value.w;}}
public float4 yzwx {get => new float4(y,z,w,x);set{y= value.x;z= value.y;w= value.z;x= value.w;}}
public float4 yzwy {get => new float4(y,z,w,y);set{y= value.x;z= value.y;w= value.z;y= value.w;}}
public float4 yzwz {get => new float4(y,z,w,z);set{y= value.x;z= value.y;w= value.z;z= value.w;}}
public float4 yzww {get => new float4(y,z,w,w);set{y= value.x;z= value.y;w= value.z;w= value.w;}}
public float4 ywxx {get => new float4(y,w,x,x);set{y= value.x;w= value.y;x= value.z;x= value.w;}}
public float4 ywxy {get => new float4(y,w,x,y);set{y= value.x;w= value.y;x= value.z;y= value.w;}}
public float4 ywxz {get => new float4(y,w,x,z);set{y= value.x;w= value.y;x= value.z;z= value.w;}}
public float4 ywxw {get => new float4(y,w,x,w);set{y= value.x;w= value.y;x= value.z;w= value.w;}}
public float4 ywyx {get => new float4(y,w,y,x);set{y= value.x;w= value.y;y= value.z;x= value.w;}}
public float4 ywyy {get => new float4(y,w,y,y);set{y= value.x;w= value.y;y= value.z;y= value.w;}}
public float4 ywyz {get => new float4(y,w,y,z);set{y= value.x;w= value.y;y= value.z;z= value.w;}}
public float4 ywyw {get => new float4(y,w,y,w);set{y= value.x;w= value.y;y= value.z;w= value.w;}}
public float4 ywzx {get => new float4(y,w,z,x);set{y= value.x;w= value.y;z= value.z;x= value.w;}}
public float4 ywzy {get => new float4(y,w,z,y);set{y= value.x;w= value.y;z= value.z;y= value.w;}}
public float4 ywzz {get => new float4(y,w,z,z);set{y= value.x;w= value.y;z= value.z;z= value.w;}}
public float4 ywzw {get => new float4(y,w,z,w);set{y= value.x;w= value.y;z= value.z;w= value.w;}}
public float4 ywwx {get => new float4(y,w,w,x);set{y= value.x;w= value.y;w= value.z;x= value.w;}}
public float4 ywwy {get => new float4(y,w,w,y);set{y= value.x;w= value.y;w= value.z;y= value.w;}}
public float4 ywwz {get => new float4(y,w,w,z);set{y= value.x;w= value.y;w= value.z;z= value.w;}}
public float4 ywww {get => new float4(y,w,w,w);set{y= value.x;w= value.y;w= value.z;w= value.w;}}
public float4 zxxx {get => new float4(z,x,x,x);set{z= value.x;x= value.y;x= value.z;x= value.w;}}
public float4 zxxy {get => new float4(z,x,x,y);set{z= value.x;x= value.y;x= value.z;y= value.w;}}
public float4 zxxz {get => new float4(z,x,x,z);set{z= value.x;x= value.y;x= value.z;z= value.w;}}
public float4 zxxw {get => new float4(z,x,x,w);set{z= value.x;x= value.y;x= value.z;w= value.w;}}
public float4 zxyx {get => new float4(z,x,y,x);set{z= value.x;x= value.y;y= value.z;x= value.w;}}
public float4 zxyy {get => new float4(z,x,y,y);set{z= value.x;x= value.y;y= value.z;y= value.w;}}
public float4 zxyz {get => new float4(z,x,y,z);set{z= value.x;x= value.y;y= value.z;z= value.w;}}
public float4 zxyw {get => new float4(z,x,y,w);set{z= value.x;x= value.y;y= value.z;w= value.w;}}
public float4 zxzx {get => new float4(z,x,z,x);set{z= value.x;x= value.y;z= value.z;x= value.w;}}
public float4 zxzy {get => new float4(z,x,z,y);set{z= value.x;x= value.y;z= value.z;y= value.w;}}
public float4 zxzz {get => new float4(z,x,z,z);set{z= value.x;x= value.y;z= value.z;z= value.w;}}
public float4 zxzw {get => new float4(z,x,z,w);set{z= value.x;x= value.y;z= value.z;w= value.w;}}
public float4 zxwx {get => new float4(z,x,w,x);set{z= value.x;x= value.y;w= value.z;x= value.w;}}
public float4 zxwy {get => new float4(z,x,w,y);set{z= value.x;x= value.y;w= value.z;y= value.w;}}
public float4 zxwz {get => new float4(z,x,w,z);set{z= value.x;x= value.y;w= value.z;z= value.w;}}
public float4 zxww {get => new float4(z,x,w,w);set{z= value.x;x= value.y;w= value.z;w= value.w;}}
public float4 zyxx {get => new float4(z,y,x,x);set{z= value.x;y= value.y;x= value.z;x= value.w;}}
public float4 zyxy {get => new float4(z,y,x,y);set{z= value.x;y= value.y;x= value.z;y= value.w;}}
public float4 zyxz {get => new float4(z,y,x,z);set{z= value.x;y= value.y;x= value.z;z= value.w;}}
public float4 zyxw {get => new float4(z,y,x,w);set{z= value.x;y= value.y;x= value.z;w= value.w;}}
public float4 zyyx {get => new float4(z,y,y,x);set{z= value.x;y= value.y;y= value.z;x= value.w;}}
public float4 zyyy {get => new float4(z,y,y,y);set{z= value.x;y= value.y;y= value.z;y= value.w;}}
public float4 zyyz {get => new float4(z,y,y,z);set{z= value.x;y= value.y;y= value.z;z= value.w;}}
public float4 zyyw {get => new float4(z,y,y,w);set{z= value.x;y= value.y;y= value.z;w= value.w;}}
public float4 zyzx {get => new float4(z,y,z,x);set{z= value.x;y= value.y;z= value.z;x= value.w;}}
public float4 zyzy {get => new float4(z,y,z,y);set{z= value.x;y= value.y;z= value.z;y= value.w;}}
public float4 zyzz {get => new float4(z,y,z,z);set{z= value.x;y= value.y;z= value.z;z= value.w;}}
public float4 zyzw {get => new float4(z,y,z,w);set{z= value.x;y= value.y;z= value.z;w= value.w;}}
public float4 zywx {get => new float4(z,y,w,x);set{z= value.x;y= value.y;w= value.z;x= value.w;}}
public float4 zywy {get => new float4(z,y,w,y);set{z= value.x;y= value.y;w= value.z;y= value.w;}}
public float4 zywz {get => new float4(z,y,w,z);set{z= value.x;y= value.y;w= value.z;z= value.w;}}
public float4 zyww {get => new float4(z,y,w,w);set{z= value.x;y= value.y;w= value.z;w= value.w;}}
public float4 zzxx {get => new float4(z,z,x,x);set{z= value.x;z= value.y;x= value.z;x= value.w;}}
public float4 zzxy {get => new float4(z,z,x,y);set{z= value.x;z= value.y;x= value.z;y= value.w;}}
public float4 zzxz {get => new float4(z,z,x,z);set{z= value.x;z= value.y;x= value.z;z= value.w;}}
public float4 zzxw {get => new float4(z,z,x,w);set{z= value.x;z= value.y;x= value.z;w= value.w;}}
public float4 zzyx {get => new float4(z,z,y,x);set{z= value.x;z= value.y;y= value.z;x= value.w;}}
public float4 zzyy {get => new float4(z,z,y,y);set{z= value.x;z= value.y;y= value.z;y= value.w;}}
public float4 zzyz {get => new float4(z,z,y,z);set{z= value.x;z= value.y;y= value.z;z= value.w;}}
public float4 zzyw {get => new float4(z,z,y,w);set{z= value.x;z= value.y;y= value.z;w= value.w;}}
public float4 zzzx {get => new float4(z,z,z,x);set{z= value.x;z= value.y;z= value.z;x= value.w;}}
public float4 zzzy {get => new float4(z,z,z,y);set{z= value.x;z= value.y;z= value.z;y= value.w;}}
public float4 zzzz {get => new float4(z,z,z,z);set{z= value.x;z= value.y;z= value.z;z= value.w;}}
public float4 zzzw {get => new float4(z,z,z,w);set{z= value.x;z= value.y;z= value.z;w= value.w;}}
public float4 zzwx {get => new float4(z,z,w,x);set{z= value.x;z= value.y;w= value.z;x= value.w;}}
public float4 zzwy {get => new float4(z,z,w,y);set{z= value.x;z= value.y;w= value.z;y= value.w;}}
public float4 zzwz {get => new float4(z,z,w,z);set{z= value.x;z= value.y;w= value.z;z= value.w;}}
public float4 zzww {get => new float4(z,z,w,w);set{z= value.x;z= value.y;w= value.z;w= value.w;}}
public float4 zwxx {get => new float4(z,w,x,x);set{z= value.x;w= value.y;x= value.z;x= value.w;}}
public float4 zwxy {get => new float4(z,w,x,y);set{z= value.x;w= value.y;x= value.z;y= value.w;}}
public float4 zwxz {get => new float4(z,w,x,z);set{z= value.x;w= value.y;x= value.z;z= value.w;}}
public float4 zwxw {get => new float4(z,w,x,w);set{z= value.x;w= value.y;x= value.z;w= value.w;}}
public float4 zwyx {get => new float4(z,w,y,x);set{z= value.x;w= value.y;y= value.z;x= value.w;}}
public float4 zwyy {get => new float4(z,w,y,y);set{z= value.x;w= value.y;y= value.z;y= value.w;}}
public float4 zwyz {get => new float4(z,w,y,z);set{z= value.x;w= value.y;y= value.z;z= value.w;}}
public float4 zwyw {get => new float4(z,w,y,w);set{z= value.x;w= value.y;y= value.z;w= value.w;}}
public float4 zwzx {get => new float4(z,w,z,x);set{z= value.x;w= value.y;z= value.z;x= value.w;}}
public float4 zwzy {get => new float4(z,w,z,y);set{z= value.x;w= value.y;z= value.z;y= value.w;}}
public float4 zwzz {get => new float4(z,w,z,z);set{z= value.x;w= value.y;z= value.z;z= value.w;}}
public float4 zwzw {get => new float4(z,w,z,w);set{z= value.x;w= value.y;z= value.z;w= value.w;}}
public float4 zwwx {get => new float4(z,w,w,x);set{z= value.x;w= value.y;w= value.z;x= value.w;}}
public float4 zwwy {get => new float4(z,w,w,y);set{z= value.x;w= value.y;w= value.z;y= value.w;}}
public float4 zwwz {get => new float4(z,w,w,z);set{z= value.x;w= value.y;w= value.z;z= value.w;}}
public float4 zwww {get => new float4(z,w,w,w);set{z= value.x;w= value.y;w= value.z;w= value.w;}}
public float4 wxxx {get => new float4(w,x,x,x);set{w= value.x;x= value.y;x= value.z;x= value.w;}}
public float4 wxxy {get => new float4(w,x,x,y);set{w= value.x;x= value.y;x= value.z;y= value.w;}}
public float4 wxxz {get => new float4(w,x,x,z);set{w= value.x;x= value.y;x= value.z;z= value.w;}}
public float4 wxxw {get => new float4(w,x,x,w);set{w= value.x;x= value.y;x= value.z;w= value.w;}}
public float4 wxyx {get => new float4(w,x,y,x);set{w= value.x;x= value.y;y= value.z;x= value.w;}}
public float4 wxyy {get => new float4(w,x,y,y);set{w= value.x;x= value.y;y= value.z;y= value.w;}}
public float4 wxyz {get => new float4(w,x,y,z);set{w= value.x;x= value.y;y= value.z;z= value.w;}}
public float4 wxyw {get => new float4(w,x,y,w);set{w= value.x;x= value.y;y= value.z;w= value.w;}}
public float4 wxzx {get => new float4(w,x,z,x);set{w= value.x;x= value.y;z= value.z;x= value.w;}}
public float4 wxzy {get => new float4(w,x,z,y);set{w= value.x;x= value.y;z= value.z;y= value.w;}}
public float4 wxzz {get => new float4(w,x,z,z);set{w= value.x;x= value.y;z= value.z;z= value.w;}}
public float4 wxzw {get => new float4(w,x,z,w);set{w= value.x;x= value.y;z= value.z;w= value.w;}}
public float4 wxwx {get => new float4(w,x,w,x);set{w= value.x;x= value.y;w= value.z;x= value.w;}}
public float4 wxwy {get => new float4(w,x,w,y);set{w= value.x;x= value.y;w= value.z;y= value.w;}}
public float4 wxwz {get => new float4(w,x,w,z);set{w= value.x;x= value.y;w= value.z;z= value.w;}}
public float4 wxww {get => new float4(w,x,w,w);set{w= value.x;x= value.y;w= value.z;w= value.w;}}
public float4 wyxx {get => new float4(w,y,x,x);set{w= value.x;y= value.y;x= value.z;x= value.w;}}
public float4 wyxy {get => new float4(w,y,x,y);set{w= value.x;y= value.y;x= value.z;y= value.w;}}
public float4 wyxz {get => new float4(w,y,x,z);set{w= value.x;y= value.y;x= value.z;z= value.w;}}
public float4 wyxw {get => new float4(w,y,x,w);set{w= value.x;y= value.y;x= value.z;w= value.w;}}
public float4 wyyx {get => new float4(w,y,y,x);set{w= value.x;y= value.y;y= value.z;x= value.w;}}
public float4 wyyy {get => new float4(w,y,y,y);set{w= value.x;y= value.y;y= value.z;y= value.w;}}
public float4 wyyz {get => new float4(w,y,y,z);set{w= value.x;y= value.y;y= value.z;z= value.w;}}
public float4 wyyw {get => new float4(w,y,y,w);set{w= value.x;y= value.y;y= value.z;w= value.w;}}
public float4 wyzx {get => new float4(w,y,z,x);set{w= value.x;y= value.y;z= value.z;x= value.w;}}
public float4 wyzy {get => new float4(w,y,z,y);set{w= value.x;y= value.y;z= value.z;y= value.w;}}
public float4 wyzz {get => new float4(w,y,z,z);set{w= value.x;y= value.y;z= value.z;z= value.w;}}
public float4 wyzw {get => new float4(w,y,z,w);set{w= value.x;y= value.y;z= value.z;w= value.w;}}
public float4 wywx {get => new float4(w,y,w,x);set{w= value.x;y= value.y;w= value.z;x= value.w;}}
public float4 wywy {get => new float4(w,y,w,y);set{w= value.x;y= value.y;w= value.z;y= value.w;}}
public float4 wywz {get => new float4(w,y,w,z);set{w= value.x;y= value.y;w= value.z;z= value.w;}}
public float4 wyww {get => new float4(w,y,w,w);set{w= value.x;y= value.y;w= value.z;w= value.w;}}
public float4 wzxx {get => new float4(w,z,x,x);set{w= value.x;z= value.y;x= value.z;x= value.w;}}
public float4 wzxy {get => new float4(w,z,x,y);set{w= value.x;z= value.y;x= value.z;y= value.w;}}
public float4 wzxz {get => new float4(w,z,x,z);set{w= value.x;z= value.y;x= value.z;z= value.w;}}
public float4 wzxw {get => new float4(w,z,x,w);set{w= value.x;z= value.y;x= value.z;w= value.w;}}
public float4 wzyx {get => new float4(w,z,y,x);set{w= value.x;z= value.y;y= value.z;x= value.w;}}
public float4 wzyy {get => new float4(w,z,y,y);set{w= value.x;z= value.y;y= value.z;y= value.w;}}
public float4 wzyz {get => new float4(w,z,y,z);set{w= value.x;z= value.y;y= value.z;z= value.w;}}
public float4 wzyw {get => new float4(w,z,y,w);set{w= value.x;z= value.y;y= value.z;w= value.w;}}
public float4 wzzx {get => new float4(w,z,z,x);set{w= value.x;z= value.y;z= value.z;x= value.w;}}
public float4 wzzy {get => new float4(w,z,z,y);set{w= value.x;z= value.y;z= value.z;y= value.w;}}
public float4 wzzz {get => new float4(w,z,z,z);set{w= value.x;z= value.y;z= value.z;z= value.w;}}
public float4 wzzw {get => new float4(w,z,z,w);set{w= value.x;z= value.y;z= value.z;w= value.w;}}
public float4 wzwx {get => new float4(w,z,w,x);set{w= value.x;z= value.y;w= value.z;x= value.w;}}
public float4 wzwy {get => new float4(w,z,w,y);set{w= value.x;z= value.y;w= value.z;y= value.w;}}
public float4 wzwz {get => new float4(w,z,w,z);set{w= value.x;z= value.y;w= value.z;z= value.w;}}
public float4 wzww {get => new float4(w,z,w,w);set{w= value.x;z= value.y;w= value.z;w= value.w;}}
public float4 wwxx {get => new float4(w,w,x,x);set{w= value.x;w= value.y;x= value.z;x= value.w;}}
public float4 wwxy {get => new float4(w,w,x,y);set{w= value.x;w= value.y;x= value.z;y= value.w;}}
public float4 wwxz {get => new float4(w,w,x,z);set{w= value.x;w= value.y;x= value.z;z= value.w;}}
public float4 wwxw {get => new float4(w,w,x,w);set{w= value.x;w= value.y;x= value.z;w= value.w;}}
public float4 wwyx {get => new float4(w,w,y,x);set{w= value.x;w= value.y;y= value.z;x= value.w;}}
public float4 wwyy {get => new float4(w,w,y,y);set{w= value.x;w= value.y;y= value.z;y= value.w;}}
public float4 wwyz {get => new float4(w,w,y,z);set{w= value.x;w= value.y;y= value.z;z= value.w;}}
public float4 wwyw {get => new float4(w,w,y,w);set{w= value.x;w= value.y;y= value.z;w= value.w;}}
public float4 wwzx {get => new float4(w,w,z,x);set{w= value.x;w= value.y;z= value.z;x= value.w;}}
public float4 wwzy {get => new float4(w,w,z,y);set{w= value.x;w= value.y;z= value.z;y= value.w;}}
public float4 wwzz {get => new float4(w,w,z,z);set{w= value.x;w= value.y;z= value.z;z= value.w;}}
public float4 wwzw {get => new float4(w,w,z,w);set{w= value.x;w= value.y;z= value.z;w= value.w;}}
public float4 wwwx {get => new float4(w,w,w,x);set{w= value.x;w= value.y;w= value.z;x= value.w;}}
public float4 wwwy {get => new float4(w,w,w,y);set{w= value.x;w= value.y;w= value.z;y= value.w;}}
public float4 wwwz {get => new float4(w,w,w,z);set{w= value.x;w= value.y;w= value.z;z= value.w;}}
public float4 wwww {get => new float4(w,w,w,w);set{w= value.x;w= value.y;w= value.z;w= value.w;}}
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
public static implicit operator float4(float v) { throw new NotImplementedException(); }
}
}
	