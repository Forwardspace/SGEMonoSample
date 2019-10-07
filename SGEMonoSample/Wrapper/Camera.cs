using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ScapeInternal
{
    static class Camera
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static IntPtr create();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void destroy(IntPtr obj);

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
    }
}

namespace Scape
{
    class Camera
    {
        //From pos
        public Camera(float x, float y, float z)
        {
            objectPtr = ScapeInternal.Camera.create();

            ScapeInternal.Camera.setPos(objectPtr, x, y, z);
        }

        //From pos and rot
        public Camera(Vector3 pos, Vector3 rot)
        {
            objectPtr = ScapeInternal.Camera.create();

            ScapeInternal.Camera.setPos(objectPtr, pos.X, pos.Y, pos.Z);
            ScapeInternal.Camera.setRot(objectPtr, rot.X, rot.Y, rot.Z);
        }

        //(Internal) from other Camera*
        public Camera(IntPtr internalCamera)
        {
            objectPtr = internalCamera;
        }

        ~Camera()
        {
            ScapeInternal.Camera.destroy(objectPtr);
        }

        public void SetPos(float x, float y, float z)
        {
            ScapeInternal.Camera.setPos(objectPtr, x, y, z);
        }
        public void SetPos(Vector3 pos)
        {
            ScapeInternal.Camera.setPos(objectPtr, pos.X, pos.Y, pos.Z);
        }

        public Vector3 Pos()
        {
            var pos = ScapeInternal.Camera.pos(objectPtr);
            return new Vector3(pos[0], pos[1], pos[2]);
        }

        //

        public void SetRot(float x, float y, float z)
        {
            ScapeInternal.Camera.setRot(objectPtr, x, y, z);
        }
        public void SetRot(Vector3 rot)
        {
            ScapeInternal.Camera.setRot(objectPtr, rot.X, rot.Y, rot.Z);
        }

        public Vector3 Rot()
        {
            var rot = ScapeInternal.Camera.rot(objectPtr);
            return new Vector3(rot[0], rot[1], rot[2]);
        }

        private IntPtr objectPtr;
    }
}
