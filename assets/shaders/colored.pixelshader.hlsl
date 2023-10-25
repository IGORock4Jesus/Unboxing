cbuffer ColorBuffer : register(b0)
{
	float4 color;
}

struct Input
{
	float4 position : SV_Position;
};

struct Output
{
	float4 color : SV_TARGET;
};

Output main(Input input)
{
	Output output;
	
	output.color = color;
	
	return output;
}
