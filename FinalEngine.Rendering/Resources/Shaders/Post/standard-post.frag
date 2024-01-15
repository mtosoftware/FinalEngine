#version 460

#include "hdr"
  
layout (location = 0) in vec2 in_texCoord;

layout (location = 0) out vec4 out_color;

uniform sampler2D u_screenTexture;
uniform HDR u_hdr;

void main()
{
    out_color = vec4(CalculateToneMapping(u_hdr, u_screenTexture, in_texCoord), 1.0);
}
