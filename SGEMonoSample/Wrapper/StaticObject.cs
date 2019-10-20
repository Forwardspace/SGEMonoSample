using System;
using System.Runtime.CompilerServices;

namespace ScapeInternal
{
    static class StaticObject
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static IntPtr create(string s);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static IntPtr createFromMesh(IntPtr m);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void destroy(IntPtr p);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static IntPtr getMaterial(IntPtr obj);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void setMaterial(IntPtr obj, IntPtr mat);


        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static IntPtr getMeshInVBOs(IntPtr obj);
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

        public StaticObject(Mesh mesh)
        {
            objectPtr = ScapeInternal.StaticObject.createFromMesh(mesh.objectPtr);
        }

        ~StaticObject()
        {
            ScapeInternal.StaticObject.destroy(objectPtr);
        }

        public Material ObjectMat
        {
            get
            {
                return new Material(ScapeInternal.StaticObject.getMaterial(objectPtr));
            }
            set
            {
                ScapeInternal.StaticObject.setMaterial(objectPtr, value.objectPtr);
            }
        }

        public MeshInVBOs ObjectMesh {
            get
            {
                return new MeshInVBOs(ScapeInternal.StaticObject.getMeshInVBOs(objectPtr));
            }
            private set { }
        }
    }
}