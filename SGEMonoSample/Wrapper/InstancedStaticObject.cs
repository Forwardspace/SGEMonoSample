using System;
using System.Numerics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ScapeInternal
{
    static class InstancedStaticObject
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static IntPtr create(string filename, uint numInstances, bool initPhysics);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static IntPtr createFromMesh(IntPtr m, uint numInstances, bool initPhysics);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void destroy(IntPtr obj);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void attachInstance(IntPtr obj, IntPtr inst);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void removeInstance(IntPtr obj, IntPtr inst);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void removeInstanceAt(IntPtr obj, uint index);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static IntPtr[] getInstances(IntPtr obj);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static IntPtr getMaterial(IntPtr obj);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void setMaterial(IntPtr obj, IntPtr mat);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static IntPtr getMeshInVBOs(IntPtr obj);

        //
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void setInstanceRigidBodyFromMass(IntPtr obj, float mass);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void setInstanceRigidBodyFromDetails(IntPtr obj, float mass, int type, float x, float y, float z);

    }

    static class StaticObjectInstance
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void destroy(IntPtr obj);
    }
}

namespace Scape
{
    class InstancedStaticObject : Object
    {
        public InstancedStaticObject(string filename, uint numInstances, Action<StaticObjectInstance> transformer, bool initPhys = false)
        {
            objectPtr = ScapeInternal.InstancedStaticObject.create(filename, numInstances, initPhys);
            var instancePtrs = ScapeInternal.InstancedStaticObject.getInstances(objectPtr);

            foreach (var ptr in instancePtrs)
            {
                instances.Add(new StaticObjectInstance(ptr));

                transformer(instances[instances.Count - 1]);
            }
        }

        ~InstancedStaticObject()
        {
            ScapeInternal.InstancedStaticObject.destroy(objectPtr);
        }

        public void AttachInstance(StaticObjectInstance instance)
        {
            instances.Add(instance);
            ScapeInternal.InstancedStaticObject.attachInstance(objectPtr, instance.objectPtr);
        }

        public void RemoveInstance(StaticObjectInstance instance)
        {
            instances.Remove(instance);
            ScapeInternal.InstancedStaticObject.removeInstance(objectPtr, instance.objectPtr);
        }
        public void RemoveInstance(uint index)
        {
            if (index >= instances.Count)
            {
                return;
            }

            instances.RemoveAt((int)index);
            ScapeInternal.InstancedStaticObject.removeInstanceAt(objectPtr, index);
        }

        public void TransformInstances(Action<StaticObjectInstance> transformer)
        {
            foreach (var inst in instances)
            {
                transformer(inst);
            }
        }

        //Range includes start and end
        public void TransformInstancesRange(uint start, uint end, Action<StaticObjectInstance> transformer)
        {
            for (uint i = start; i <= end; i++)
            {
                transformer(instances[(int)i]);
            }
        }

        public Material ObjectMat
        {
            get
            {
                return new Material(ScapeInternal.InstancedStaticObject.getMaterial(objectPtr));
            }
            set
            {
                ScapeInternal.InstancedStaticObject.setMaterial(objectPtr, value.objectPtr);
            }
        }

        public MeshInVBOs ObjectMesh
        {
            get
            {
                return new MeshInVBOs(ScapeInternal.InstancedStaticObject.getMeshInVBOs(objectPtr));
            }
            private set { }
        }

        public List<StaticObjectInstance> instances { get; private set; } = new List<StaticObjectInstance>();
    }

    class StaticObjectInstance : Object
    {
        public StaticObjectInstance(IntPtr instance)
        {
            objectPtr = instance;
        }

        ~StaticObjectInstance()
        {
            ScapeInternal.StaticObjectInstance.destroy(objectPtr);
        }

        public void SetRigidBody(float mass)
        {
            ScapeInternal.InstancedStaticObject.setInstanceRigidBodyFromMass(objectPtr, mass);
        }

        public void SetRigidBody(float mass, RigidBodyType type, Vector3 dims)
        {
            ScapeInternal.InstancedStaticObject.setInstanceRigidBodyFromDetails(objectPtr, mass, (int)type, dims.X, dims.Y, dims.Z);
        }
    }
}