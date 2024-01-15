#ifndef HDR_GLSL
#define HDR_GLSL

#define HDR_TYPE_REINHARD 0
#define HDR_TYPE_EXPOSURE 1

struct HDR
{
    bool enabled;
    float exposure;
    int type;
};

vec3 CalculateExposureToneMapping(float exposure, sampler2D screenTexture, vec2 texCoord)
{
    vec3 color = texture(screenTexture, texCoord).rgb;
    vec3 mapped = vec3(1.0) - exp(-color * exposure);

    return mapped;
}

vec3 CalculateReinhardToneMapping(sampler2D screenTexture, vec2 texCoord)
{
    vec3 color = texture(screenTexture, texCoord).rgb;
    vec3 mapped = color / (color + vec3(1.0));

    return mapped;
}

vec3 CalculateToneMapping(HDR hdr, sampler2D screenTexture, vec2 texCoord)
{
    vec3 result = vec3(0);

    if (!hdr.enabled)
    {
        result = texture(screenTexture, texCoord).rgb;
    }
    else
    {
        switch (hdr.type)
        {
            case HDR_TYPE_REINHARD:
                result = CalculateReinhardToneMapping(screenTexture, texCoord);
                break;

            case HDR_TYPE_EXPOSURE:
                result = CalculateExposureToneMapping(hdr.exposure, screenTexture, texCoord);
                break;
        }
    }

    return result;
}

#endif // HDR_GLSL
