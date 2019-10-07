using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ScapeInternal
{
    class Mesh
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static IntPtr create(string filename);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void destroy(IntPtr mesh);
    }
}

namespace Scape
{
    class Mesh
    {
        public Mesh(string filename)
        {
            objectPtr = ScapeInternal.Mesh.create(filename);
        }

        ~Mesh()
        {
            ScapeInternal.Mesh.destroy(objectPtr);
        }

        private IntPtr objectPtr = IntPtr.Zero;
    }
}