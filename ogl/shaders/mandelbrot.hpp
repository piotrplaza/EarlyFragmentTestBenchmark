#pragma once

#include "programBase.hpp"

namespace Shaders
{
	namespace Programs
	{
		struct MandelbrotAccessor : ProgramBase
		{
			using ProgramBase::ProgramBase;

			MandelbrotAccessor(Shaders::ProgramId program):
				ProgramBase(program),
				mvp(program, "mvp")
			{
			}

			Uniforms::UniformControllerMat4f mvp;
		};

		struct Mandelbrot : MandelbrotAccessor
		{
			Mandelbrot() :
				MandelbrotAccessor(Shaders::LinkProgram(Shaders::CompileShaders("ogl/shaders/mandelbrot.vs",
					"ogl/shaders/mandelbrot.fs"), { {0, "bPos"} }))
			{
			}

			Mandelbrot(const Mandelbrot&) = delete;

			~Mandelbrot()
			{
				glDeleteProgram(getProgramId());
			}
		};
	}
}
