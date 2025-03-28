precision mediump float;
uniform sampler2D _MainTex;
uniform sampler2D _PatternTex;
uniform vec4 _ScalePattern;
uniform float _AlphaAdd;

float average(vec4 color)
{
	return (((color.r + color.g) + color.b) + color.a) / 4;
}

float Global_Luminance(vec3 rgb)
{
	return 0.3333 * ((rgb.r + rgb.g) + rgb.b);
}


varying vec4 color;
varying vec2 texcoord;
varying vec3 worldPos;

void main(){
	vec2 uv = worldPos.xy * _ScalePattern.xy;
	vec4 pattern = texture2D(_PatternTex, uv);
	vec4 c = texture2D(_MainTex, texcoord);
	float l = Global_Luminance(c.rgb);
	l = average(c);
	l = l - _ScalePattern.z;
	l = l * _ScalePattern.w;
	c.rgb = clamp(l, 0, 1) + color;
	c.a = c.a * ((pattern.a + _AlphaAdd) + (color.a - 1));
	c = clamp(c, 0, 1);
	c.rgb = c.rgb * c.a;
	c.rgb = c.rgb + (1 - c.a);
	c = clamp(c, 0, 1);
	gl_FragColor = c; return;
}
