#version 460

layout (location = 0) in vec3 in_position;

uniform mat4 u_lightProjection;
uniform mat4 u_lightView;
uniform mat4 u_transform;

void main()
{
	gl_Position = u_lightProjection * u_lightView * u_transform * vec4(in_position, 1.0);
}
