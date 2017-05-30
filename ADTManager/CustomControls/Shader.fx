//--------------------------------------------------------------------------------------
// 
// WPF ShaderEffect HLSL -- SimpleMultiInputEffect
//
//--------------------------------------------------------------------------------------

//-----------------------------------------------------------------------------------------
// Shader constant register mappings (scalars - float, double, Point, Color, Point3D, etc.)
//-----------------------------------------------------------------------------------------

float4 Blend : register(C1);

//--------------------------------------------------------------------------------------
// Sampler Inputs (Brushes, including ImplicitInput)
//--------------------------------------------------------------------------------------

sampler2D input : register(S0);
//sampler2D binput : register(S1);

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------

float overlay(float a, float b, float ao)
{
	return (b <= 0.5f) ? a * b * 2 : ao - (ao - a) * (ao - b) * 2;
}

// a, ao - source color and opacity, b, bo - blend color and opacity
float blend(float a, float b, float ao, float bo)
{
    //return a + ((overlay(a, b, ao) - a) * bo);
	return (a + ((b - a) * bo)) * ao;//normal
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 sample = tex2D(input, uv);
	//Without saving them to variables, thing doesn't work
    float sa = sample.a;
    float sr = sample.r;
    float sg = sample.g;
    float sb = sample.b;

    //Same here, stripping values to different variables
    float ba = Blend.a;
    float br = Blend.r;
    float bg = Blend.g;
    float bb = Blend.b;

	//separate result variable
    float4 r = 0;
    r.r = blend(sr, br, sa, ba);
    r.g = blend(sg, bg, sa, ba);
    r.b = blend(sb, bb, sa, ba);
    r.a = sa;
    return r;
}
