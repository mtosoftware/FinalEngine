#version 460 core
out vec4 out_color;

layout (location = 0) in vec3 in_texCoords;

uniform samplerCube u_skybox;

void main()
{    
    out_color = texture(u_skybox, in_texCoords);
}
