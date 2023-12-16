#version 460

#include "lighting"

layout (location = 0) in vec4 in_color;
layout (location = 1) in vec2 in_texCoord;
layout (location = 2) in vec3 in_fragPos;
layout (location = 3) in mat3 in_tbnMatrix;

layout (location = 0) out vec4 out_color;

uniform vec3 u_viewPosition;
uniform DirectionalLight u_light;
uniform Material u_material;

void main()
{
    vec3 normal = CalculateNormal(in_tbnMatrix, u_material.normalTexture, in_texCoord);
    vec3 lightColor = CalculateDirectionalLight(u_light, u_material, normal, u_viewPosition, in_fragPos, in_texCoord);

    out_color = vec4(lightColor, 1.0);
}
