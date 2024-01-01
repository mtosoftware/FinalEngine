#ifndef MATERIAL_GLSL
#define MATERIAL_GLSL

struct Material
{
    sampler2D diffuseTexture;
    sampler2D specularTexture;
    sampler2D normalTexture;
    float shininess;
};

#endif // MATERIAL_GLSL
