#version 460

layout (location = 0) in vec4 in_color;
layout (location = 1) in vec2 in_texCoord;
layout (location = 2) in float in_textureID;

out vec4 out_color;

uniform sampler2D u_textures[32];

void main()
{
    out_color = texture(u_textures[int(in_textureID)], in_texCoord) * in_color;
}