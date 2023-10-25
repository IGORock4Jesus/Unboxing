cbuffer MatrixBuffer : register(b0)
{
	matrix world;
	matrix view;
	matrix projection;
}

struct Input
{
	float4 position : POSITION;
};

struct Output
{
	float4 position : SV_POSITION;
};

Output main(Input input)
{
	Output output;
	input.position.w = 1.0f;
	
	output.position = mul(input.position, world);
	output.position = mul(output.position, view);
	output.position = mul(output.position, projection);
		
	return output;
}
