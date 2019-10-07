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
    }
}