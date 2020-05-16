#version 330 core
out vec4 FragColor;
in vec2 TexCoords;
in vec3 WorldPos;
in vec3 Normal;

// material parameters
uniform vec3 albedo;
uniform float metallic;
uniform float roughness;
uniform float ao;

// lights
uniform vec3 lightPosition;
uniform vec3 lightColor;

//uniform sampler2D diffuseMap;

uniform vec3 camPos;

const float PI = 3.14159265359;
void main()
{
   float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;

	vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPosition - WorldPos);
    float diff = max(dot(norm, lightDir), 0.0);

	if(diff < 1 && diff > 0.80)
	{
		diff = 0.80;
	}
	if(diff < 0.80 && diff > 0.60)
	{
		diff = 0.70;
	}
	if(diff < 0.60 && diff > 0.40)
	{
		diff = 0.50;
	}
	if(diff < 0.40 && diff > 0.30)
	{
		diff = 0.35;
	}
	if(diff < 0.30 && diff > 0.20)
	{
		diff= 0.25;
	}
	if(diff < 0.20 && diff > 0.0)
	{
		diff = 0.0;
	}


    vec3 diffuse = diff * lightColor;

	float specularStrength = 0.5;
    vec3 viewDir = normalize(camPos - WorldPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * lightColor;  

	vec3 result = (ambient + diffuse + specular) * vec3(1.0f, 0.5f, 0.31f);
    FragColor = vec4(result, 1.0);
}