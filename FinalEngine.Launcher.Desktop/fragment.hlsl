float4 PShader(float4 position : SV_POSITION, float3 color : COLOR) : SV_TARGET
{
	return float4(color, 1.0f);
}