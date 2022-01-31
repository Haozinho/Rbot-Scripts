﻿//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Legion/CoreLegion.cs
using RBot;

public class FirstClassEntertainment
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new CoreLegion();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AddDrop("Legion Token");

        Legion.LTFirstClassEntertainment();

        Core.SetOptions(false);
    }
}