//--------------------------------------------------------------------------------------
// 
// WPF ShaderEffect HLSL -- SimpleMultiInputEffect
//
//--------------------------------------------------------------------------------------

//-----------------------------------------------------------------------------------------
// Shader constant register mappings (scalars - float, double, Point, Color, Point3D, etc.)
//-----------------------------------------------------------------------------------------

float factor : register(c0);

//--------------------------------------------------------------------------------------
// Sampler Inputs (Brushes, including ImplicitInput)
//--------------------------------------------------------------------------------------

sampler2D input : register(S0);
//sampler2D binput : register(s1);

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);
	float gray = color.r * 0.3 + color.g * 0.59 + color.b * 0.11;

	float4 result;
	result.r = (color.r - gray) * factor + gray;
	result.g = (color.g - gray) * factor + gray;
	result.b = (color.b - gray) * factor + gray;
	result.a = color.a;

	return result;
}
