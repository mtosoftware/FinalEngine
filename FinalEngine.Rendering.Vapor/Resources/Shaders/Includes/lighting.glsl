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

vec3 CalculateDiffuseShading(vec3 direction, vec3 normal)
{
    // As the angle widens, diffuse lighting weakens.
    return max(0.0, dot(direction, normal));
}

vec3 CalculateLight(LightBase light, Material material, vec3 direction, vec3 normal)
{
    normal = normalize(normal);
    direction = normalize(direction);

    // Simulate global illumination
    // TODO: Perhaps this should be defined in some Scene struct, instead of LightBase.
    vec3 ambientStrength = light.ambientColor * 0.2;

    vec3 diffuseColor = CalculateDiffuseColor(direction, normal) * light.diffuseColor;

    return (ambientColor + diffuseColor);
}

#endif // LIGHTING_GLSL
