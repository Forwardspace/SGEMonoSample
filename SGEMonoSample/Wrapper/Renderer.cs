using System;
using System.Runtime.CompilerServices;

namespace ScapeInternal
{
    static class Renderer
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void init(int width, int height, string title, bool fullscreen);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void terminate();

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static double deltaTime();

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static IntPtr currentCamera();
    }
}

namespace Scape
{
    static class Renderer
    {
        public static void init(int width, int height, string title, bool fullscreen)
        {
            ScapeInternal.Renderer.init(width, height, title, fullscreen);
        }

        public static void terminate()
        {
            ScapeInternal.Renderer.terminate();
        }

        public static float DeltaTime()
        {
            //Cast to float for consistency
            return (float)ScapeInternal.Renderer.deltaTime();
        }

        public static Camera CurrentCamera()
        {
            return new Camera(ScapeInternal.Renderer.currentCamera());
        }
    }
}