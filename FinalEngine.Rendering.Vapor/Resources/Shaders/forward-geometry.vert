#version 460

layout (location = 0) in vec3 in_position;
layout (location = 1) in vec4 in_color;
layout (location = 2) in vec2 in_texCoord;

layout (location = 0) out vec4 out_color;
layout (location = 1) out vec2 out_texCoord;

uniform mat4 u_projection;
uniform mat4 u_view;
uniform mat4 u_transform;

void main()
{
	out_color = in_color;
	out_texCoord = in_texCoord;

	gl_Position = u_projection * u_view * u_transform * vec4(in_position, 1.0);
}
