struct VOut
{
	float4 position : SV_POSITION;
	float3 color : COLOR;
};

VOut VShader(float4 position : POSITION, float3 color : COLOR)
{
	VOut output;

	output.position = position;
	output.color = color;

	return output;
}