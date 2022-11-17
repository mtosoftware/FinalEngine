#version 450

#include "lighting"

layout (location = 0) in vec4 in_color;
layout (location = 1) in vec2 in_texCoord;
layout (location = 2) in vec3 in_normal;
layout (location = 3) in vec3 in_fragPos;

out vec4 out_color;

uniform vec3 u_viewPosition;
uniform Material u_material;
uniform PointLight u_light;

void main()
{
    vec3 normal = normalize(in_normal);
    vec3 viewDirection = normalize(u_viewPosition - in_fragPos);

	out_color = vec4(CalculatePointLight(u_light, u_material, normal, viewDirection, in_fragPos, in_texCoord), 1.0);
}
