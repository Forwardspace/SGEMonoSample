using System;
using System.Runtime.CompilerServices;

namespace ScapeInternal
{
    static class Texture
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static IntPtr createFromFilename(string fname);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void destroy(IntPtr tex);
    }
}

namespace Scape
{
    class Texture
    {
        public Texture(string filename)
        {
            objectPtr = ScapeInternal.Texture.createFromFilename(filename);
        }

        ~Texture()
        {
            ScapeInternal.Texture.destroy(objectPtr);
        }

        public IntPtr objectPtr { get; private set; }
    }
}