#version 460

struct Material
{
    sampler2D diffuseTexture;
    sampler2D specularTexture;
    float shininess;
};

struct LightBase
{
    vec3 diffuseColor;
    vec3 specularColor;
};

struct DirectionalLight
{
    LightBase base;
    vec3 direction;
};

vec3 CalculateLight(LightBase light, Material material, vec3 direction, vec3 normal, vec3 viewPosition, vec3 fragPosition, vec2 texCoord)
{
    normal = normalize(normal);
    direction = normalize(direction);

    // As the angle widens, diffuse shading decreases.
    float diffuseShading = max(dot(direction, normal), 0.0);
    vec3 diffuseColor = diffuseShading * light.diffuseColor * texture(material.diffuseTexture, texCoord).rgb;

    // Calculate the view direction and reflect the light direction around the normal.
    vec3 viewDirection = normalize(viewPosition - fragPosition);

    // Reverse light direction: light source to fragment.
    // Then get the reflection vector.
    vec3 inverseDirection = -direction;
    vec3 reflectDirection = reflect(inverseDirection, normal);

    // As the angle widens, specular shading decreases.
    // This is why we raise to the power of shininess.
    float specularShading = pow(max(dot(viewDirection, reflectDirection), 0.0), material.shininess);
    vec3 specularColor = specularShading * light.specularColor * texture(material.specularTexture, texCoord).rgb;

    return diffuseColor + specularColor;
}

layout (location = 0) in vec4 in_color;
layout (location = 1) in vec2 in_texCoord;
layout (location = 2) in vec3 in_normal;
layout (location = 3) in vec3 in_fragPos;

layout (location = 0) out vec4 out_color;

uniform vec3 u_viewPosition;
uniform DirectionalLight u_light;
uniform Material u_material;

void main()
{
    vec3 lightColor = CalculateLight(
        u_light.base,
        u_material,
        -u_light.direction,
        in_normal,
        u_viewPosition,
        in_fragPos,
        in_texCoord);

    out_color = vec4(lightColor, 1.0);
}
