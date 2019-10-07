using System;

static class Game
{

    static Scape.ScriptedObject palletObject;
    static Scape.ScriptedObject centralPallet;

    public static void startup()
    {
        Scape.Renderer.init(1024, 768, "Hello Mono World!", false);

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

    public override void startup()
    {
        pallet = new Scape.StaticObject(".//models//pallet.obj");
        AddChild(pallet);
    }

    public override void update()
    {
        pallet.SetRot(0, palletYrot, 0);
        palletYrot += 0.01f;
    }
}