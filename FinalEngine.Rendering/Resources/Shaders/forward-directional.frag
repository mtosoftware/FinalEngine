#version 450

#include "lighting"
#include "mapping"

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
    vec4 texColor = texture(u_material.diffuseTexture, in_texCoord);

    if (texColor.a < 0.1)
    {
        discard;
    }

    vec3 normal = CalculateNormal(in_tbn, u_material.normalTexture, in_texCoord);
    vec3 viewDirection = normalize(u_viewPosition - in_fragPos);

	out_color = vec4(CalculateDirectionalLight(u_light, u_material, normal, viewDirection, in_texCoord), 1.0);
}
