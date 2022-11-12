#ifndef LIGHTING_GLSL
#define LIGHTING_GLSL

#include "material"

struct LightBase
{
    vec3 ambientColor;
    vec3 diffuseColor;
    vec3 specularColor;
};

struct DirectionalLight
{
    LightBase base;
    vec3 direction;
};

float CalculateDiffuseShading(vec3 normal, vec3 lightDirection)
{
    return max(dot(normal, lightDirection), 0.0);
}

float CalculateSpecularShading(vec3 normal, vec3 lightDirection, vec3 viewDirection, float shininess)
{
    vec3 halfwayDirection = normalize(lightDirection + viewDirection);  
    return pow(max(dot(normal, halfwayDirection), 0.0), shininess);
}

vec3 CalculateLight(LightBase light, Material material, vec3 normal, vec3 viewDirection, vec3 lightDirection, vec2 texCoord)
{
    float diffuseShading = CalculateDiffuseShading(normal, lightDirection);
    float specularShading = CalculateSpecularShading(normal, lightDirection, viewDirection, material.shininess);

    vec3 ambient = light.ambientColor * vec3(texture(material.diffuseTexture, texCoord));
    vec3 diffuse = light.diffuseColor * diffuseShading * vec3(texture(material.diffuseTexture, texCoord));
    vec3 specular = light.specularColor * specularShading * vec3(texture(material.specularTexture, texCoord));

    return (ambient + diffuse + specular);
}

vec3 CalculateDirectionalLight(DirectionalLight light, Material material, vec3 normal, vec3 viewDirection, vec2 texCoord)
{
    return CalculateLight(light.base, material, normal, viewDirection, normalize(-light.direction), texCoord);
}

#endif // LIGHTING_GLSL
