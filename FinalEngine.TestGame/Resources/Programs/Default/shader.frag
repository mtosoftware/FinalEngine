#version 450

layout (location = 0) in vec4 in_color;
layout (location = 1) in vec2 in_texCoord;
layout (location = 2) in vec3 in_normal;

out vec4 out_color;

struct Material
{
    sampler2D diffuseTexture;
};

uniform Material u_material;

void main()
{
	out_color = texture(u_material.diffuseTexture, in_texCoord);
}
