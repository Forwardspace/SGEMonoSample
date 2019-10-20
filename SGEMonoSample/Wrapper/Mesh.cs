using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ScapeInternal
{
    static class MeshInVBOs
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void destroy(IntPtr mesh);
    }

    static class Mesh
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static IntPtr create(string filename);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void destroy(IntPtr mesh);
    }
}

namespace Scape
{
    class MeshInVBOs
    {
        public MeshInVBOs(IntPtr meshPtr)
        {
            objectPtr = meshPtr;
        }

        ~MeshInVBOs()
        {
            ScapeInternal.MeshInVBOs.destroy(objectPtr);
        }

        public IntPtr objectPtr { get; private set; } = IntPtr.Zero;
    }

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

        public IntPtr objectPtr { get; private set; } = IntPtr.Zero;
    }
}