using System;
using System.Collections.Generic;

static class Game
{
    static Scape.ScriptedObject instancedPallet;

    public static void startup()
    {
        Scape.Renderer.init(1920, 1080, "Hello Mono World!", false);

        ScriptedPallet.InitMaterials();

        instancedPallet = new ScriptedPallet();
    }

    public static void cleanup()
    {
    }

    //

    public static void preFrame()
    {
        if (Scape.UserInputManager.IsPressed(Scape.Keys.KEY_W))
        {
            System.Console.WriteLine("W pressed");
        }
    }

    public static void postFrame()
    {
    }
}

class ScriptedPallet : Scape.ScriptedObject
{
    Scape.InstancedStaticObject pallet;
    float palletYrot = 0;

    public static void InitMaterials()
    {
        new Scape.Material(new Dictionary<Scape.Material.Type, Scape.Texture> {
            [Scape.Material.Type.diffuse] = new Scape.Texture("textures/woodtex.jpg"),
        }).Store("wood");
    }

    public override void startup()
    {
        float xPos = 0;
        float yPos = -50;

        bool hasMass = false;

        pallet = new Scape.InstancedStaticObject("models/pallet.obj", 20, (Scape.StaticObjectInstance inst) => {
            inst.SetPos(xPos, yPos, 0);
            xPos += 0.5f;
            yPos -= 10f;

            inst.SetScl(50, 50, 50);

            inst.SetRigidBody(0);
            hasMass = !hasMass;
        }, true);
        AddChild(pallet);

        pallet.ObjectMat = Scape.Material.Get("wood");
    }

    public override void update()
    {
        pallet.TransformInstances((Scape.StaticObjectInstance inst) => {
            inst.SetRot(0, palletYrot, 0);
        });

        //palletYrot += 0.05f;
    }
}