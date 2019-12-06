using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ScapeInternal
{
    static class Object
    {
        //Pos
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void setPos(IntPtr obj, float x, float y, float z);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static float[] pos(IntPtr obj);
        //Rot
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void setRot(IntPtr obj, float x, float y, float z);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static float[] rot(IntPtr obj);
        //Scl
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void setScl(IntPtr obj, float x, float y, float z);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static float[] scl(IntPtr obj);

        ////
    }
}

namespace Scape
{
    abstract class Object
    {
        public virtual void SetPos(float x, float y, float z)
        {
            ScapeInternal.Object.setPos(objectPtr, x, y, z);
        }
        public virtual void SetPos(Vector3 pos)
        {
            ScapeInternal.Object.setPos(objectPtr, pos.X, pos.Y, pos.Z);
        }

        public Vector3 Pos()
        {
            var pos = ScapeInternal.Object.pos(objectPtr);
            return new Vector3(pos[0], pos[1], pos[2]);
        }

        //

        public virtual void SetRot(float x, float y, float z)
        {
            ScapeInternal.Object.setRot(objectPtr, x, y, z);
        }
        public virtual void SetRot(Vector3 rot)
        {
            ScapeInternal.Object.setRot(objectPtr, rot.X, rot.Y, rot.Z);
        }

        public Vector3 Rot()
        {
            var rot = ScapeInternal.Object.rot(objectPtr);
            return new Vector3(rot[0], rot[1], rot[2]);
        }

        //

        public virtual void SetScl(float x, float y, float z)
        {
            ScapeInternal.Object.setScl(objectPtr, x, y, z);
        }
        public virtual void SetScl(Vector3 scl)
        {
            ScapeInternal.Object.setScl(objectPtr, scl.X, scl.Y, scl.Z);
        }

        public Vector3 Scl()
        {
            var scl = ScapeInternal.Object.scl(objectPtr);
            return new Vector3(scl[0], scl[1], scl[2]);
        }

        public Object()
        {
        }

        public Object(IntPtr existing)
        {
            objectPtr = existing;
        }

        public IntPtr objectPtr { protected set; get; } = IntPtr.Zero;
    }
}