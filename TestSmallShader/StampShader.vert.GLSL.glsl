#version 330
uniform mat4x4 ObjectToWorld;
uniform mat4x4 MATRIX_VP;

vec4 Global_ObjectToClipPos(vec4 inVertex)
{
	return MATRIX_VP * (ObjectToWorld * inVertex);
}

layout(location = 0) in vec4 vertex;
layout(location = 1) in vec4 color;
layout(location = 2) in vec2 texcoord;

out vec4 v_color;
out vec2 v_texcoord;
out vec3 v_worldPos;

void main(){
	v_worldPos = (ObjectToWorld * vertex).xyz;
	gl_Position = Global_ObjectToClipPos(vertex);
	v_texcoord = texcoord;
	v_color = color;
	return;//gl_Position is set
}
