#version 450

#include "material"

layout (location = 0) in vec4 in_color;
layout (location = 1) in vec2 in_texCoord;

out vec4 out_color;

uniform Material u_material;

void main()
{
    vec4 texColor = texture(u_material.diffuseTexture, in_texCoord) * in_color;

    if (texColor.a < 0.1)
    {
        discard;
    }

	out_color = texColor;
}
