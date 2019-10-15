using System;
using System.Runtime.CompilerServices;

namespace ScapeInternal
{
    static class StaticObject
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static IntPtr create(string s);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void destroy(IntPtr p);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void setMaterial(IntPtr obj, IntPtr mat);
    }
}

namespace Scape
{
    class StaticObject : Object
    {
        public StaticObject(string filename)
        {
            objectPtr = ScapeInternal.StaticObject.create(filename);
        }

        ~StaticObject()
        {
            ScapeInternal.StaticObject.destroy(objectPtr);
        }

        public void SetMaterial(Material mat)
        {
            ScapeInternal.StaticObject.setMaterial(objectPtr, mat.objectPtr);
        }
    }
}