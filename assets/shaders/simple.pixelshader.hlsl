struct Input
{
	float4 position : SV_Position;
	float2 texel : TEXCOORD;
};

struct Output
{
	float4 color : SV_TARGET;
};

SamplerState samplerState;
Texture2D spriteTexture;

Output main(Input input)
{
	Output output;
	
	output.color = spriteTexture.Sample(samplerState, input.texel);
	
	return output;
}
