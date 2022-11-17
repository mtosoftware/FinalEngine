#version 450

#include "lighting"

layout (location = 0) in vec4 in_color;
layout (location = 1) in vec2 in_texCoord;
layout (location = 2) in vec3 in_fragPos;
layout (location = 3) in mat3 in_tbn;

out vec4 out_color;

uniform vec3 u_viewPosition;
uniform Material u_material;
uniform DirectionalLight u_light;

void main()
{
    vec3 normal = normalize(in_tbn * (255.0 / 128.0 * texture2D(u_material.normalTexture, in_texCoord).xyz - 1));
    vec3 viewDirection = normalize(u_viewPosition - in_fragPos);

	out_color = vec4(CalculateDirectionalLight(u_light, u_material, normal, viewDirection, in_texCoord), 1.0);
}
