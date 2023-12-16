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

struct Attenuation
{
    float constant;
    float linear;
    float quadratic;
};

struct PointLight
{
    LightBase base;
    Attenuation attenuation;
    vec3 position;
};

struct SpotLight
{
    LightBase base;
    Attenuation attenuation;
    vec3 direction;
    vec3 position;
    float radius;
    float outerRadius;
};

vec3 CalculateLight(LightBase light, Material material, vec3 direction, vec3 normal, vec3 viewPosition, vec3 fragPosition, vec2 texCoord)
{
    normal = normalize(normal);
    direction = normalize(direction);

    // As the angle widens, diffuse shading decreases.
    float diffuseShading = max(dot(direction, normal), 0.0);
    vec3 diffuseColor = diffuseShading * light.diffuseColor * texture(material.diffuseTexture, texCoord).rgb;

    // Now let's measure the angle between the normal and halfway point of the light and view direction.
    vec3 viewDirection = normalize(viewPosition - fragPosition);
    vec3 halfWayDirection = normalize(direction + viewDirection);

    // As the angle widens, specular shading decreases.
    // This is why we raise to the power of shininess.
    float specularShading = pow(max(dot(normal, halfWayDirection), 0.0), material.shininess);
    vec3 specularColor = specularShading * light.specularColor * texture(material.specularTexture, texCoord).rgb;

    return diffuseColor + specularColor;
}

vec3 CalculateDirectionalLight(DirectionalLight light, Material material, vec3 normal, vec3 viewPosition, vec3 fragPosition, vec2 texCoord)
{
    return CalculateLight(light.base, material, -light.direction, normal, viewPosition, fragPosition, texCoord);
}

float CalculateAttenuation(Attenuation attenuation, vec3 position, vec3 fragPosition)
{
    // The distance between the light and the fragment position.
    float dist = length(position - fragPosition);

    // Use the quadratic equation to determine the lights attenuation over the distance.
    return 1.0 / (attenuation.constant + attenuation.linear * dist + attenuation.quadratic * (dist * dist));
}

vec3 CalculatePointLight(PointLight light, Material material, vec3 normal, vec3 viewPosition, vec3 fragPosition, vec2 texCoord)
{
    float attenuation = CalculateAttenuation(light.attenuation, light.position, fragPosition);
    vec3 lightColor = CalculateLight(light.base, material, light.position - fragPosition, normal, viewPosition, fragPosition, texCoord);

    return lightColor * attenuation;
}

vec3 CalculateSpotLight(SpotLight light, Material material, vec3 normal, vec3 viewPosition, vec3 fragPosition, vec2 texCoord)
{
    vec3 lightDirection = normalize(light.position - fragPosition);

    // Calculate the cut off radius
    float theta = dot(lightDirection, normalize(-light.direction));
    float epsilon = light.radius - light.outerRadius;
    float intensity = clamp((theta - light.outerRadius) / epsilon, 0.0, 1.0);

    float attenuation = CalculateAttenuation(light.attenuation, light.position, fragPosition);
    vec3 lightColor = CalculateLight(light.base, material, lightDirection, normal, viewPosition, fragPosition, texCoord);

    return lightColor * attenuation * intensity;
}

layout (location = 0) in vec4 in_color;
layout (location = 1) in vec2 in_texCoord;
layout (location = 2) in vec3 in_normal;
layout (location = 3) in vec3 in_fragPos;

layout (location = 0) out vec4 out_color;

uniform vec3 u_viewPosition;
uniform DirectionalLight u_light;
uniform Material u_material;
uniform PointLight u_plight;
uniform SpotLight u_slight;

void main()
{
    vec3 ambient = 0.05 * texture(u_material.diffuseTexture, in_texCoord).rgb;

    vec3 lightColor = CalculateDirectionalLight(
        u_light,
        u_material,
        in_normal,
        u_viewPosition,
        in_fragPos,
        in_texCoord);

    vec3 pColor = CalculatePointLight(
        u_plight,
        u_material,
        in_normal,
        u_viewPosition,
        in_fragPos,
        in_texCoord);

    vec3 sLight = CalculateSpotLight(
        u_slight,
        u_material,
        in_normal,
        u_viewPosition,
        in_fragPos,
        in_texCoord);

    out_color = vec4(ambient + pColor, 1.0);
}
