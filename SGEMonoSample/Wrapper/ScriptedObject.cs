using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ScapeInternal
{
    static class ScriptedObject
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static IntPtr createEmpty();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static IntPtr createFromObject(Scape.ScriptedObject scr);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void destroy(IntPtr scr);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void attachScript(IntPtr scr, string classname);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void initAttachedObjectInstance(IntPtr scr);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void addChild(IntPtr obj, IntPtr child);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void removeChild(IntPtr obj, IntPtr child);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void setPos(IntPtr scr, float x, float y, float z);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void setRot(IntPtr scr, float x, float y, float z);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void setScl(IntPtr scr, float x, float y, float z);
    }
}

namespace Scape
{
    class ScriptedObject : Object
    {
        virtual public void startup() { }
        virtual public void update() { }
        virtual public void cleanup() { }

        public ScriptedObject()
        {
            objectPtr = ScapeInternal.ScriptedObject.createFromObject(this);
            startup();
        }

        public ScriptedObject(IntPtr existing) : base(existing)
        {
            startup();
        }

        ~ScriptedObject()
        {
            cleanup();
            ScapeInternal.ScriptedObject.destroy(objectPtr);
        }

        public void AttachScript(string classname)
        {
            ScapeInternal.ScriptedObject.attachScript(objectPtr, classname);
        }

        public void AddChild(Object child)
        {
            ScapeInternal.ScriptedObject.addChild(objectPtr, child.objectPtr);
        }

        public void RemoveChild(Object child)
        {
            ScapeInternal.ScriptedObject.removeChild(objectPtr, child.objectPtr);
        }

        public override void SetPos(float x, float y, float z)
        {
            ScapeInternal.ScriptedObject.setPos(objectPtr, x, y, z);
        }
        public override void SetPos(Vector3 pos)
        {
            ScapeInternal.ScriptedObject.setPos(objectPtr, pos.X, pos.Y, pos.Z);
        }

        public override void SetRot(float x, float y, float z)
        {
            ScapeInternal.ScriptedObject.setRot(objectPtr, x, y, z);
        }
        public override void SetRot(Vector3 rot)
        {
            ScapeInternal.ScriptedObject.setRot(objectPtr, rot.X, rot.Y, rot.Z);
        }

        public override void SetScl(float x, float y, float z)
        {
            ScapeInternal.ScriptedObject.setScl(objectPtr, x, y, z);
        }
        public override void SetScl(Vector3 scl)
        {
            ScapeInternal.ScriptedObject.setScl(objectPtr, scl.X, scl.Y, scl.Z);
        }
    }
}
