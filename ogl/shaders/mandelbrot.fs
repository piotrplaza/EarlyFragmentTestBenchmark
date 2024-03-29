#version 440

in vec3 vPos;

out vec3 fragColor;

uniform int fractalType = 0;
uniform int coloringType = 1;
uniform int iterations = 5000;
uniform float zoom = 0.5;
uniform vec2 translation = vec2(0.0, 0.0);
uniform vec2 juliaC = vec2(-0.1, 0.65);

vec2 complexAdd(vec2 c1, vec2 c2)
{
	return vec2(c1.x + c2.x, c1.y + c2.y);
}

vec2 complexMul(vec2 c1, vec2 c2)
{
	return vec2(c1.x * c2.x - c1.y * c2.y, c1.x * c2.y + c1.y * c2.x);
}

vec2 mandelbrot(vec2 p)
{
	vec2 result = vec2(0.0, 0.0);
	for (int i = 0; i < iterations; ++i)
	{
		result = complexAdd(complexMul(result, result), p);
	}
	return result;
}

vec2 julia(vec2 p)
{
	vec2 result = p;
	for (int i = 0; i < iterations; ++i)
	{
		result = complexAdd(complexMul(result, result), juliaC);
	}
	return result;
}

vec2 fractal(vec2 p)
{
	switch(fractalType)
	{
		case 0: return mandelbrot(p);
		case 1: return julia(p);
	}
}

vec3 blackWhiteColoring(vec2 fractalResult)
{
	return length(fractalResult) < 2.0
	? vec3(1.0, 1.0, 1.0)
	: vec3(0.0, 0.0, 0.0);
}

vec3 customColoring(vec2 fractalResult)
{
	fractalResult.x = abs(fractalResult.x);
	fractalResult.y = abs(fractalResult.y);
	return vec3(fractalResult.x, fractalResult.y, length(fractalResult));
}

vec3 coloring(vec2 fractalResult)
{
	switch(coloringType)
	{
		case 0: return blackWhiteColoring(fractalResult); break;
		case 1: return customColoring(fractalResult); break;
	}
}

void main()
{
	if (length(vPos.xy) < 0.1) discard;
	//if (abs(vPos.x) < 0.1 && abs(vPos.y) < 0.1) discard;
	fragColor = coloring(fractal(vPos.xy / zoom + translation));
}
