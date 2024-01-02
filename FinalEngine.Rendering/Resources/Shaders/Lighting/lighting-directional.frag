#version 460

#include "lighting"

layout (location = 0) in vec2 in_texCoord;
layout (location = 1) in vec3 in_fragPos;
layout (location = 2) in mat3 in_tbnMatrix;
layout (location = 6) in vec4 in_fragPosLightSpace;

layout (location = 0) out vec4 out_color;

uniform vec3 u_viewPosition;
uniform DirectionalLight u_light;
uniform Material u_material;

uniform sampler2D shadowMap;
float ShadowCalculation(vec4 fragPosLightSpace)
{
    vec3 projCoords = fragPosLightSpace.xyz / fragPosLightSpace.w;
    projCoords = projCoords * 0.5 + 0.5;
    float closestDepth = texture(shadowMap, projCoords.xy).r; 
    float currentDepth = projCoords.z;
    float shadow = currentDepth > closestDepth  ? 1.0 : 0.0;
    return shadow;
}

void main()
{
    vec3 normal = CalculateNormal(in_tbnMatrix, u_material.normalTexture, in_texCoord);
    vec3 lightColor = CalculateDirectionalLight(u_light, u_material, normal, u_viewPosition, in_fragPos, in_texCoord);

    float shadow = ShadowCalculation(in_fragPosLightSpace);
    
    out_color = vec4((1.0 - shadow) * lightColor, 1.0);
}
