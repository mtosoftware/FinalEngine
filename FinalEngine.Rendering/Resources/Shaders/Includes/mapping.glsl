#ifndef MAPPING_GLSL
#define MAPPING_GLSL

mat3 CreateTBN(mat4 transform, vec3 normal, vec3 tangent)
{
    vec3 n = normalize((transform * vec4(normal, 0.0)).xyz);
    vec3 t = normalize((transform * vec4(tangent, 0.0)).xyz); 
    t = normalize(t - dot(t, n) * n);

    vec3 b = cross(t, n);

    return mat3(t, b, n);
}

vec3 CalculateNormal(mat3 tbn, sampler2D sampler, vec2 texCoord)
{
    return normalize(tbn * (255.0 / 128.0 * texture2D(sampler, texCoord).xyz - 1));
}

#endif // MAPPING_GLSL
