sampler2D _MainTex;
sampler2D _PatternTex;
float4 _ScalePattern;
float _AlphaAdd;

float average(float4 color)
{
	return (((color.r + color.g) + color.b) + color.a) / 4;
}
float Global_Luminance(float3 rgb)
{
	return 0.3333 * ((rgb.r + rgb.g) + rgb.b);
}

struct input_from_vertex
{
float4 color : COLOR;
float2 texcoord : TEXCOORD0;
float3 worldPos : TEXCOORD1;
};

float4 main(in input_from_vertex IN)
{
	float2 uv = IN.worldPos.xy * _ScalePattern.xy;
	float4 pattern = tex2D(_PatternTex, uv);
	float4 c = tex2D(_MainTex, IN.texcoord);
	float l = Global_Luminance(c.rgb);
	l = average(c);
	l = l - _ScalePattern.z;
	l = l * _ScalePattern.w;
	c.rgb = saturate(l) + IN.color;
	c.a = c.a * ((pattern.a + _AlphaAdd) + (IN.color.a - 1));
	c = saturate(c);
	c.rgb = c.rgb * c.a;
	c.rgb = c.rgb + (1 - c.a);
	c = saturate(c);
	return c;
}
