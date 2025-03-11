uniform mat4x4 ObjectToWorld;
uniform mat4x4 MATRIX_VP;

vec4 Global_ObjectToClipPos(vec4 inVertex)
{
	return MATRIX_VP * (ObjectToWorld * inVertex);

}
attribute vec4 vertex;
attribute vec4 color;
attribute vec2 texcoord;

varying vec4 v_color;
varying vec2 v_texcoord;
varying vec3 v_worldPos;

void main(){
	v_worldPos = ObjectToWorld * vertex;
	gl_Position = Global_ObjectToClipPos(vertex);
	v_texcoord = texcoord;
	v_color = color;
	return;//gl_Position is set

}
