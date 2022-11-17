#ifndef ATTENUATION_GLSL
#define ATTENUATION_GLSL

struct Attenuation
{
    float constant;
    float linear;
    float quadratic;
};

float CalculateAttenuation(Attenuation attenuation, vec3 position, vec3 fragPosition)
{
    float dist = length(position - fragPosition);
    return 1.0 / (attenuation.constant + attenuation.linear * dist + attenuation.quadratic * (dist * dist));
}

#endif // ATTENUATION_GLSL
