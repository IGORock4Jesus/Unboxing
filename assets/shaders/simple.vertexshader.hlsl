cbuffer MatrixBuffer : register(b0)
{
	matrix world;
	matrix view;
	matrix projection;
}

struct Input
{
	float4 position : POSITION;
	float2 texel : TEXCOORD;
};

struct Output
{
	float4 position : SV_POSITION;
	float2 texel : TEXCOORD;
};

Output main(Input input)
{
	Output output;
	input.position.w = 1.0f;
	
	output.position = mul(input.position, world);
	output.position = mul(output.position, view);
	output.position = mul(output.position, projection);
	
	output.texel = input.texel;
	
	return output;
}
