using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ScapeInternal
{
    static class Material
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static IntPtr createEmpty();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static IntPtr createFromTextures(string[] types, IntPtr[] textures);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static void destroy(IntPtr mat);
    }
}

namespace Scape
{
    class Material
    {
        public enum Type
        {
            diffuse,
		    normal,
		    specular,
		    bump,
		    glow,
		    reflection,
		    opacity,
		    extra
        }
        
        //Get the material from the global storage
        public static Material Get(string globalName)
        {
            return GlobalStorage[globalName];
        }

        //Store this Material for use later in the global storage
        public void Store(string globalName)
        {
            GlobalStorage[globalName] = this;
        }

        public Material()
        {
            objectPtr = ScapeInternal.Material.createEmpty();
        }

        public Material(IntPtr mat)
        {
            objectPtr = mat;
        }

        public Material(Dictionary<Type, Texture> textures)
        {
            //Get the objectPtrs from Texture objects to pass to C++ directly
            //Additionally convert Type[] to string[]
            string[] strs = new string[textures.Count];
            IntPtr[] ptrs = new IntPtr[textures.Count];

            for (int i = 0; i < textures.Count; i++)
            {
                strs[i] = textures.Keys.ElementAt(i).ToString();
                ptrs[i] = textures.Values.ElementAt(i).objectPtr;
            }

            objectPtr = ScapeInternal.Material.createFromTextures(strs, ptrs);
        }

        ~Material()
        {
            ScapeInternal.Material.destroy(objectPtr);
        }

        public IntPtr objectPtr { get; private set; }

        private static Dictionary<string, Material> GlobalStorage = new Dictionary<string, Material>();
    }
}