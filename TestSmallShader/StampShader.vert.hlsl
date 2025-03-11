float4x4 ObjectToWorld;
float4x4 MATRIX_VP;

float4 Global_ObjectToClipPos(float4 inVertex)
{
	return mul(MATRIX_VP, mul(ObjectToWorld, inVertex));
}

struct vertex_info
{
float4 vertex : POSITION;
float4 color : COLOR;
float2 texcoord : TEXCOORD0;
};

struct vertex_to_pixel
{
float4 vertex : POSITION;
float4 color : COLOR;
float2 texcoord : TEXCOORD0;
float3 worldPos : TEXCOORD1;
};

vertex_to_pixel main(in vertex_info IN)
{
	vertex_to_pixel OUT;
	OUT.worldPos = mul(ObjectToWorld, IN.vertex);
	OUT.vertex = Global_ObjectToClipPos(IN.vertex);
	OUT.texcoord = IN.texcoord;
	OUT.color = IN.color;
	return OUT;

}
