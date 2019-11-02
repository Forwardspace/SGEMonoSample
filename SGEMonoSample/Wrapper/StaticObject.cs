using System;
using System.Numerics;
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
        public extern static void setRigidBodyFromMass(IntPtr obj, float mass);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void setRigidBodyFromDetails(IntPtr obj, float mass, int type, float x, float y, float z);

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

        public void InitRigidBody(float mass)
        {
            ScapeInternal.StaticObject.setRigidBodyFromMass(objectPtr, mass);
        }

        public void InitRigidBody(float mass, RigidBodyType type, Vector3 dims)
        {
            ScapeInternal.StaticObject.setRigidBodyFromDetails(objectPtr, mass, (int)type, dims.X, dims.Y, dims.Z);
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