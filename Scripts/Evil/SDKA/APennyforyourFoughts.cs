//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
using RBot;

public class DSOPenny
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSDKA SDKA = new CoreSDKA();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AddDrop("DoomCoin", "Dark Spirit Orb", "Shadow Creeper Enchant");

        SDKA.Penny();

        Core.SetOptions(false);
    }
}
