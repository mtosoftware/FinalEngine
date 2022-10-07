#version 450

layout (location = 0) in vec4 in_color;
layout (location = 1) in vec2 in_texCoord;

struct Material
{
	sampler2D diffuseTexture;
};

out vec4 out_color;

uniform Material u_material;

void main()
{
	out_color = texture(u_material.diffuseTexture, in_texCoord);
}
