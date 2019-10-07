class MainInternal
{
    public static void Main()
    {
        //Call the game's startup function (public static void startup() in class Game)
        Game.startup();
    }

    public static void cleanup()
    {
        //Call the game's cleanup function (public static void cleanup() in class Game)
        Game.cleanup();
    }

    //Specific timed functions

    public static void preFrame()
    {
        Game.preFrame();
    }

    public static void postFrame()
    {
        Game.postFrame();
    }
}