//--------------------------------------------------------------------------------------
// 
// WPF ShaderEffect HLSL -- zBlend
//
//--------------------------------------------------------------------------------------

#define DARKEN 10
#define MULTIPLY 11

#define LIGHTEN 20
#define SCREEN 21

#define OVERLAY 30
#define DIFFERENCE 40

//-----------------------------------------------------------------------------------------
// Shader constant register mappings (scalars - float, double, Point, Color, Point3D, etc.)
//-----------------------------------------------------------------------------------------

float4 blendColor : register(C0);
float blendMode : register(C1);

//--------------------------------------------------------------------------------------
// Sampler Inputs (Brushes, including ImplicitInput)
//--------------------------------------------------------------------------------------

sampler2D implicitInputSampler : register(S0);

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------

float overlay(float a, float b, float ao, float bo)
{
    return (b <= 0.5f) ? a * b * 2 : 1 - (1 - a) * (1 - b) * 2;
}

float normal(float a, float b, float ao, float bo, float fa)
{
    return (a + ((b - a) * bo)) * ao;
    //return (a * ao + b * bo * (1 - ao)) / fa;
}

float darken(float a, float b)
{
    return (b > a) ? a : b;
}

float multiply(float a, float b)
{
    return  a * b;
}

float lighten(float a, float b)
{
    return (b > a) ? b : a;
}

float screen(float a, float b)
{
    return 1 - (1 - a) * (1 - b);
}

float difference(float a, float b)
{
    float r = a - b;
    return (r > 0) ? r : -r;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
   // This particular shader just multiplies the color at 
   // a point by the colorFilter shader constant.
    float4 sourceColor = tex2D(implicitInputSampler, uv);
    float4 r = sourceColor;

	//Since it doesn't count as instruction will simplify this way
    float aa = sourceColor.a;
    float ar = sourceColor.r;
    float ag = sourceColor.g;
    float ab = sourceColor.b;
    float ba = blendColor.a;
    float br = blendColor.r;
    float bg = blendColor.g;
    float bb = blendColor.b;
	
    //r.a = aa * ba;
   // r.a = aa + ba * (1 - aa); //Nope 66 instructions out of 64 allowed... Sacrifycing this
    r.a = aa;//Now exactly 64 instructions
    if (blendMode == DARKEN)
    {
       // r.a = darken(aa, ba);
        r.r = darken(br, ar);
        r.g = darken(bg, ag);
        r.b = darken(bb, ab);
    }
    else if (blendMode == MULTIPLY)
    {
        r.r = multiply(br, ar);
        r.g = multiply(bg, ag);
        r.b = multiply(bb, ab);
    }
    else if (blendMode == LIGHTEN)
    {
        r.r = lighten(ar, br);
        r.g = lighten(ag, bg);
        r.b = lighten(ab, bb);
    }
    else if (blendMode == SCREEN)
    {
        r.r = screen(ar, br);
        r.g = screen(ag, bg);
        r.b = screen(ab, bb);
    }
    else if (blendMode == OVERLAY)
    {
        r.r = overlay(ar, br, aa, ba);
        r.g = overlay(ag, bg, aa, ba);
        r.b = overlay(ab, bb, aa, ba);
    }
    else if (blendMode == DIFFERENCE)
    {
        r.r = difference(br, ar);
        r.g = difference(bg, ag);
        r.b = difference(bb, ab);
    }
    else
    { //normal
        r.r = normal(ar, br, aa, ba, r.a);
        r.g = normal(ag, bg, aa, ba, r.a);
        r.b = normal(ab, bb, aa, ba, r.a);
    }
    return r;
}