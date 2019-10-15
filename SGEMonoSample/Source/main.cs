using System;
using System.Collections.Generic;

static class Game
{
    static Scape.ScriptedObject palletObject;
    static Scape.ScriptedObject centralPallet;

    public static void startup()
    {
        Scape.Renderer.init(1024, 768, "Hello Mono World!", false);

        ScriptedPallet.InitMaterials();

        palletObject = new ScriptedPallet();
        palletObject.SetPos(0.5f, 0, 0);

        centralPallet = new ScriptedPallet();
    }

    public static void cleanup()
    {
    }

    //

    public static void preFrame()
    {
    }

    public static void postFrame()
    {
    }
}

class ScriptedPallet : Scape.ScriptedObject
{
    Scape.StaticObject pallet;
    float palletYrot = 0;

    public static void InitMaterials()
    {
        new Scape.Material(new Dictionary<Scape.Material.Type, Scape.Texture> {
            [Scape.Material.Type.diffuse] = new Scape.Texture("textures/woodtex.jpg"),
        }).Store("wood");
    }

    public override void startup()
    {
        pallet = new Scape.StaticObject("models/pallet.obj");
        AddChild(pallet);

        pallet.SetMaterial(Scape.Material.Get("wood"));
    }

    public override void update()
    {
        pallet.SetRot(0, palletYrot, 0);
        palletYrot += 0.01f;
    }
}