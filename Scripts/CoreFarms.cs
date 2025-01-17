﻿using RBot;
using RBot.Items;

public class CoreFarms
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    //CLASS Boost! (60 min) -- 27555
    //Doom CLASS Boost! (60 min) -- 19761 (can get this from /join Doom merge with Daily XP Boosts)
    //Daily Login Class Boost *20 min* -- 22447

    //REPUTATION Boost! (60 min) -- 27553
    //Doom REP Boost! (60 min) -- 19762 (can get this from /join Doom merge with Daily XP Boosts)
    //REPUTATION Boost! (20 min) -- 22449

    //GOLD Boost! (60 min) -- 27554
    //Doom GOLD Boost! (60 min) -- 19763 (can get this from /join Doom merge with Daily XP Boosts)
    //GOLD Boost! (20 min) -- 22450

    //XP Boost! (60 min) -- 27552
    //Daily XP Boost! (1 hr) -- 19189
    //XP Boost! (20 min) -- 22448

    /// <summary>
    /// Uses a boost with the given ID.
    /// </summary>
    /// <param name="boostID">ID of the Boost</param>
    /// <param name="type">Type of the Boost</param>
    /// <param name="useMultiple">Whether use more than one boost</param>
    public void UseBoost(int boostID, BoostType type, bool useMultiple = true)
    {
        if (useMultiple)
        {
            Bot.RegisterHandler(5, b =>
            {
                if (!b.Player.IsBoostActive(type))
                    b.Player.UseBoost(boostID);
            }); 
        }
        else
        {
            Bot.Player.UseBoost(boostID);
        }
    }

    /// <summary>
    /// Uses a boost with one of the IDs present in <see cref="BoostIDs"/>
    /// </summary>
    /// <param name="boost">Desired Boost</param>
    /// <param name="type">Type of the Boost</param>
    /// <param name="useMultiple">Whether use more than one boost</param>
    public void UseBoost(BoostIDs boost, BoostType type, bool useMultiple = true) => UseBoost((int)boost, type, useMultiple);

    #region Gold
    public void Gold(int quant = 100000000)
    {
        if(Bot.Player.Gold >= quant)
            return;
        BattleGroundE(quant);
        BerserkerBunny(quant);
    }

    /// <summary>
    /// Farms Gold in Battle Ground E with quests Level 46-60 and 61-75
    /// </summary>
    /// <param name="goldQuant">How much gold to farm</param>
    public void BattleGroundE(int goldQuant = 100000000)
    {
        if (Bot.Player.Gold >= goldQuant)
            return;
        if (Bot.Player.Level < 61)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {goldQuant} gold");
        int i = 1;
        while (Bot.Player.Gold < goldQuant && Bot.Player.Gold <= 100000000)
        {
            Core.EnsureAccept(3991, 3992);
            Core.KillMonster("battlegrounde", "r2", "Center", "*", "Battleground D Opponent Defeated", 10, log: false);
            Core.KillMonster("battlegrounde", "r2", "Center", "*", "Battleground E Opponent Defeated", 10, log: false);
            Core.EnsureComplete(3991, 3992, 0);
            Core.Logger($"Completed x{i++}");
        }
    }

    /// <summary>
    /// Farms Gold by selling Berserker Bunny
    /// </summary>
    /// <param name="goldQuant">How much gold to farm</param>
    public void BerserkerBunny(int goldQuant = 100000000)
    {
        if(Bot.Player.Gold >= goldQuant)
            return;
        Core.AddDrop("Berserker Bunny");
        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming {goldQuant} gold");
        int i = 1;
        while (Bot.Player.Gold < goldQuant && Bot.Player.Gold <= 100000000)
        {
            Core.EnsureAccept(236);
            Core.HuntMonster("greenguardwest", "Big Bad Boar", "Were Egg", log: false);
            Core.EnsureComplete(236);
            Bot.Player.Pickup("Berserker Bunny");
            Bot.Sleep(Core.ActionDelay);
            Bot.Shops.SellItem("Berserker Bunny");
            Core.Logger($"Completed x{i++}");
        }
    }
    #endregion

    #region Experience
    public void Experience(int level = 100)
    {
        if(Bot.Player.Level >= level)
            return;
        IcestormArena(level);
    }

    /// <summary>
    /// Farms level in Ice Storm Arena
    /// </summary>
    /// <param name="level">Desired level</param>
    public void IcestormArena(int level = 100, bool rankUpClass = false)
    {
        if(Bot.Player.Level >= level)
            return;
        if(!rankUpClass)
            Core.EquipClass(ClassType.Farm);

        while ((Bot.Player.Level < 5 && Bot.Player.Level < level) || (Bot.Player.Level < 5 && rankUpClass && Bot.Player.Rank != 10))
            Core.KillMonster("icestormarena", "r4", "Left", "*", log: false);

        while ((Bot.Player.Level < 10 && Bot.Player.Level < level) || (Bot.Player.Level < 10 && rankUpClass && Bot.Player.Rank != 10))
            Core.KillMonster("icestormarena", "r5", "Left", "*", log: false);
        
        while ((Bot.Player.Level < 20 && Bot.Player.Level < level) || (Bot.Player.Level < 20 && rankUpClass && Bot.Player.Rank != 10))
            Core.KillMonster("icestormarena", "r6", "Left", "*", log: false);

        while ((Bot.Player.Level < 25 && Bot.Player.Level < level) || (Bot.Player.Level < 25 && rankUpClass && Bot.Player.Rank != 10))
        {
            Core.EnsureAccept(6628);
            Core.KillMonster("icestormarena", "r7", "Left", "*", "Icewing Grunt Defeated", 3, log: false);
            Core.EnsureComplete(6628);
        }

        while ((Bot.Player.Level < 30 && Bot.Player.Level < level) || (Bot.Player.Level < 30 && rankUpClass && Bot.Player.Rank != 10))
            Core.KillMonster("icestormarena", "r10", "Left", "*", log: false);

        while ((Bot.Player.Level < 35 && Bot.Player.Level < level) || (Bot.Player.Level < 35 && rankUpClass && Bot.Player.Rank != 10))
        {
            Core.EnsureAccept(6629);
            Core.KillMonster("icestormarena", "r11", "Left", "*", "Icewing Warrior Defeated", 3, log: false);
            Core.EnsureComplete(6629);
        }

        while((Bot.Player.Level < 50 && Bot.Player.Level < level) || (Bot.Player.Level < 50 && rankUpClass && Bot.Player.Rank != 10))
            Core.KillMonster("icestormarena", "r14", "Left", "*", log: false);

        while((Bot.Player.Level < 75 && Bot.Player.Level < level) || (Bot.Player.Level < 75 && rankUpClass && Bot.Player.Rank != 10))
            Core.KillMonster("icestormarena", "r3b", "Top", "*", log: false);

        while((Bot.Player.Level < 100 && Bot.Player.Level < level) || (Bot.Player.Level < 100 && rankUpClass && Bot.Player.Rank != 10))
            Core.KillMonster("icestormarena", "r3c", "Top", "*", log: false);
    }

    /// <summary>
    /// Farms in Seven Circles War for level and items
    /// </summary>
    /// <param name="level">Desired level</param>
    /// <param name="wrathEssence">Desired quantity of "Essence of Wrath"</param>
    /// <param name="heresySouls">Desired quantity of "Souls of Heresy"</param>
    public void SevenCirclesWar(int level = 100, int wrathEssence = 0, int heresySouls = 0)
    {
        if(Bot.Player.Level >= level && wrathEssence == 0 && heresySouls == 0)
            return;
        
        if(!Bot.Quests.IsAvailable(7979))
            Core.Logger("Do the /Join SevenCircles history with the Farm/SevenCircles[History].cs", messageBox: true, stopBot: true);

        Core.AddDrop("Essence of Wrath", "Souls of Heresy");
        while (Bot.Player.Level < level || (!Core.CheckInventory("Essence of Wrath", wrathEssence) && !Core.CheckInventory("Souls of Heresy", heresySouls)))
        {
            Core.EnsureAccept(7979, 7980, 7981);
            Core.KillMonster("sevencircleswar", "Enter", "Spawn", "Wrath Guard", "Wrath Guards Defeated", 12);
            Core.EnsureComplete(7979);
            while (Bot.Inventory.ContainsTempItem("War Medal", 5))
                Core.ChainComplete(7980);
            while (Bot.Inventory.ContainsTempItem("Mega War Medal", 3))
                Core.ChainComplete(7981);
            Bot.Player.Pickup("Essence of Wrath", "Souls of Heresy");
        }
    }
    #endregion

    #region Misc
    /// <summary>
    /// Farms the Black Knight Orb
    /// </summary>
    public void BlackKnightOrb()
    {
        if (Core.CheckInventory("Black Knight Orb"))
            return;
        Core.AddDrop("Black Knight Orb");
        Core.EnsureAccept(318);
        Core.HuntMonster("well", "Gell Oh No", "Black Knight Leg Piece");
        Core.HuntMonster("greendragon", "Greenguard Dragon", "Black Knight Chest Piece");
        Core.HuntMonster("deathgazer", "DeathGazer", "Black Knight Shoulder Piece");
        Core.HuntMonster("trunk", "Greenguard Basilisk", "Black Knight Arm Piece");
        Core.EnsureComplete(318);
        Bot.Player.Pickup("Black Knight Orb");
    }

    /// <summary>
    /// Kills the Restorers from /BludrutBrawl for "The Secret 4" item
    /// </summary>
    public void TheSecret4()
    {
        if(Core.CheckInventory("The Secret 4"))
            return;
        Core.EquipClass(ClassType.Solo);
        while(!Core.CheckInventory("The Secret 4"))
        {
            Core.Join("bludrutbrawl-111111", "Enter0", "Spawn", true);
            Bot.Wait.ForMapLoad("bludrutbrawl");
            Core.BludrutMove(5, "Morale0C");
            Core.BludrutMove(4, "Morale0B");
            Core.BludrutMove(7, "Morale0A");
            Core.BludrutMove(9, "Crosslower");
            Core.BludrutMove(14, "Crossupper", 528, 255);
            Core.BludrutMove(18, "Resource1A");
            Bot.Player.Kill("Team B Restorer");
            Bot.Player.Pickup("The Secret 4");
            Bot.Player.Kill("Team B Restorer");
            Bot.Player.Pickup("The Secret 4");
            if(Core.CheckInventory("The Secret 4"))
                break;
            Core.BludrutMove(20, "Resource1B");
            Bot.Player.Kill("Team B Restorer");
            Bot.Player.Pickup("The Secret 4");
            Bot.Player.Kill("Team B Restorer");
            Bot.Player.Pickup("The Secret 4");
        }
    }

    /// <summary>
    /// Kills the Team B Captain in /BludrutBrawl for the desired item (Combat Trophy or Yoshino's Citrine)
    /// </summary>
    /// <param name="item">Name of the desired item</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="canSoloBoss">Whether you can solo the Boss without killing Restorers and Brawlers</param>
    public void BludrutBrawlBoss(string item = "Combat Trophy", int quant = 500, bool canSoloBoss = true)
    {
        if(Core.CheckInventory(item, quant))
            return;
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop(item);
        Core.Logger($"Farming {quant} {item}. SoloBoss = {canSoloBoss}");
        while (!Core.CheckInventory(item, quant))
        {
            Core.Join("bludrutbrawl-111111", "Enter0", "Spawn", true);
            Bot.Wait.ForMapLoad("bludrutbrawl");
            Core.BludrutMove(5, "Morale0C");
            Core.BludrutMove(4, "Morale0B");
            Core.BludrutMove(7, "Morale0A");
            Core.BludrutMove(9, "Crosslower");
            if(!canSoloBoss)
            {
                Core.BludrutMove(14, "Crossupper", 528, 255);
                Core.BludrutMove(18, "Resource1A");
                Bot.Player.Kill("Team B Restorer");
                Bot.Player.Kill("Team B Restorer");
                Core.BludrutMove(20, "Resource1B");
                Bot.Player.Kill("Team B Restorer");
                Bot.Player.Kill("Team B Restorer");
                Core.BludrutMove(21, "Resource1A", 124);
                Core.BludrutMove(19, "Crossupper", 124);
                Core.BludrutMove(17, "Crosslower", 488, 483);
            }
            Core.BludrutMove(15, "Morale1A");
            if(!canSoloBoss)
                Bot.Player.Kill("Team B Brawler");
            Core.BludrutMove(23, "Morale1B");
            if(!canSoloBoss)
                Bot.Player.Kill("Team B Brawler");
            Core.BludrutMove(25, "Morale1C");
            if(!canSoloBoss)
                Bot.Player.Kill("Team B Brawler");
            Core.BludrutMove(28, "Captain1", 528, 255);
            Bot.Player.Kill("Team B Captain");
            if(!Core.CheckInventory(item))
            {
                Bot.Wait.ForDrop(item, 30);
                Bot.Player.Pickup(item);
            }
            else
                Bot.Sleep(5000);
            Core.Rest();
        }
    }
    #endregion

    #region Reputation
    public void GetAllRanks()
        {
            AegisREP();
            AlchemyREP();
            ArcangroveREP();
            BaconCatREP();
            BeastMasterREP();
            BlacksmithingREP();
            BladeofAweREP();
            BrightoakREP();
            ChaosREP();
            DoomwoodREP();
            ElementalMasterREP();
            EmberseaREP();
            EternalREP();
            //EvilREP();
            GlaceraREP();
            //GoodREP();
            LycanREP();
    
    
            HollowbornREP();
            MysteriousDungeonREP();
            MythsongREP();
            RavenlossREP();
            SpellCraftingREP();
            VampireREP();
            YokaiREP();
        }
        
    public void AegisREP(int rank = 10)
    {
        if (FactionRank("Aegis") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Aegis") < rank)
        {
            Core.EnsureAccept(4900, 4910, 4914);

            Core.HuntMonster("skytower", "Seraphic Assassin", "Seraphic Assassin Dueled", 10);
            Core.HuntMonster("skytower", "Virtuous Warrior", "Warriors Dueled", 10);
            Core.HuntMonster("skytower", "Seraphic Assassin", "Assassins Handed To Them", 6);
            Core.HuntMonster("skytower", "Virtuous Warrior|Seraphic Assassin", "Warrior Butt Beaten", 6);

            Core.EnsureComplete(4900, 4910, 4914);
            Core.Logger($"Completed x{i++}");
        }

    }

    /// <summary>
    /// Uses the specified parameters to make an Alchemy misture
    /// </summary>
    /// <param name="reagent1">The first reagent.</param>
    /// <param name="reagent2">The second reagent</param>
    /// <param name="rune">The rune to be used (AlchemyRunes.Gebo by default).</param>
    /// <param name="rank">The minimum rank to make the misture, use 0 for any rank.</param>
    /// <param name="loop">Whether loop till you run out of reagents</param>
    /// <param name="modifier">Some mistures have specific packet modifiers, default is Moose but you can find Man, mRe and others.</param>
    public void AlchemyPacket(string reagent1, string reagent2, AlchemyRunes rune = AlchemyRunes.Gebo, int rank = 0, bool loop = true, string modifier = "Moose")
    {
        if (rank != 0 && FactionRank("Alchemy") < rank)
            AlchemyREP(rank);

        void Packet()
        {
            Bot.SendPacket($"%xt%zm%crafting%1%getAlchWait%11475%11478%false%Ready to Mix%{reagent1}%{reagent2}%{rune}%{modifier}%");
            Bot.Sleep(15000);
            Bot.SendPacket($"%xt%zm%crafting%1%checkAlchComplete%11475%11478%false%Mix Complete%{reagent1}%{reagent2}%{rune}%{modifier}%");
            Bot.Sleep(700);
        }

        Core.Join("alchemy");
        Core.Logger($"Reagents: [{reagent1}], [{reagent2}].");
        Core.Logger($"Rune: {rune}.");
        Core.Logger($"Modifier: {modifier}.");
        if(loop)
        {
            int i = 1;
            while (Core.CheckInventory(new[] { reagent1, reagent2}))
            {
                Packet();
                Core.Logger($"Completed alchemy x{i++}");
            }
        }
        else
            Packet();
    }
    
    public void AlchemyREP(int rank = 10, bool goldMethod = true)
    {
        if (FactionRank("Alchemy") >= rank)
            return;
        if (!Bot.Player.Factions.Exists(f => f.Name == "Alchemy"))
            Core.Logger("You need at least 1 point in Alchemy for the packets to work, make sure you do 1 potion first in /Join Alchemy. Bot Stopped", messageBox: true, stopBot: true);

        Core.AddDrop("Dragon Scale", "Ice Vapor");
        Core.Logger($"Farming rank {rank} Alchemy");
        int i = 1;
        while (FactionRank("Alchemy") < rank)
        {
            if (goldMethod)
            {
                if (!Core.CheckInventory(new[] { "Ice Vapor", "Dragon Scale" }))
                {
                    if(!Core.CheckInventory("Dragon Runestone", 10))
                    {
                        Gold(1000000);
                        Core.BuyItem("alchemyacademy", 395, "Gold Voucher 100k", 10);
                    }
                    Core.BuyItem("alchemyacademy", 395, 7132, 10);
                    Core.BuyItem("alchemyacademy", 397, 11475, 10, 2);
                    Core.BuyItem("alchemyacademy", 397, 11478, 10, 2);
                }
                AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Gebo);
            }
            else
            {
                Core.KillMonster("lair", "Enter", "Spawn", "*", "Dragon Scale", 10, false);
                Core.KillMonster("lair", "Enter", "Spawn", "*", "Ice Vapor", 10, false);
                AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Gebo);
            }
            Core.Logger($"Iteration {i++} completed");
        }
    }

    public void ArcangroveREP(int rank = 10)
    {
        if (FactionRank("Arcangrove") >= rank)
            return;
        
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Arcangrove") < rank)
        {
            Core.EnsureAccept(794, 795, 796, 797, 798, 799, 800, 801);

            Core.HuntMonster("arcangrove", "Seed Spitter", "Spool of Arcane Thread", 10);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Defeated Seed Spitter", 10);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Bundle of Thyme", 10);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Thistle", 5);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Pretzel Root", 4);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Lore-Strip Gorillaphant Steak", 8);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Slain Gorillaphant", 7);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Gorillaphant Tusk", 6);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Batch of Mustard Seeds", 3);

            Core.EnsureComplete(794, 795, 796, 797, 798, 799, 800, 801);
            Core.Logger($"Completed x{i++}");
        }

    }

    public void BaconCatREP(int rank = 10)
    {
        if (FactionRank("BaconCat") >= rank)
            return;
        if (Core.IsMember)
            Core.AddDrop("Wheel of Bacon Token");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("BaconCat") < rank)
        {
            if (!Core.IsMember)
            {
                Core.EnsureAccept(5111, 5112, 5119, 5120);

                Core.HuntMonster("baconcatlair", "Cloud Shark", "Cloudy Hide", 6);
                Core.HuntMonster("baconcatlair", "Ice Cream Shark", "Moglinberry Ice Cream", 5);
                Core.HuntMonster("baconcatlair", "Ice Cream Shark", "Shark Sprinkles", 3);
                Core.HuntMonster("baconcatlair", "Cloud Shark", "Cloud Shark Farts", 3);
                Core.HuntMonster("baconcatlair", "Sketchy Shark", "College-Ruled Paper", 3);
                Core.HuntMonster("baconcatlair", "8-Bit Shark", "Great White DLC", 3);
                Core.HuntMonster("baconcatlair", "Cat Clothed Shark", "Kittarian Costumes", 3);
                Core.HuntMonster("baconcatlair", "Cat Clothed Shark", "Shark Teeth", 10);

                Core.EnsureComplete(5111, 5112, 5119, 5120); 
            }
            else
            {
                Core.EnsureAccept(5121, 5123, 5124, 5131);

                Core.HuntMonster("baconcatlair", "Robo Shark", "Wheel of Bacon Token", 5, false);
                Core.HuntMonster("baconcatlair", "Robo Shark", "Shark Legs Smashed", 10);
                Core.HuntMonster("baconcatlair", "Robo Shark", "Shark Quarters", 7);
                Core.HuntMonster("baconcatlair", "Robo Shark", "Walking Shark 1 Destroyed", 4);
                Core.HuntMonster("baconcatlair", "Robo Shark", "Walking Shark 2 Destroyed", 4);

                Core.EnsureComplete(5121, 5123, 5124, 5131);
            }
            Core.Logger($"Completed x{i++}");
        }

    }

    public void BeastMasterREP(int rank = 10)
    {
        if (FactionRank("BeastMaster") >= rank || !Core.IsMember)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("BeastMaster") < rank)
        {
            Core.EnsureAccept(3757);

            Core.HuntMonster("pyramid", "Golden Scarab", "Gleaming Gems of Containment", 9);
            Core.HuntMonster("lair", "Golden Draconian", "Bright Binding of Submission", 8);

            Core.EnsureComplete(3757);
            Core.Logger($"Completed x{i++}");
        }

    }

    public void BlacksmithingREP(int rank = 4)
    {
        if (FactionRank("Blacksmithing") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Blacksmithing") < rank)
        {
            Core.EnsureAccept(2777);

            Core.HuntMonster("greenguardeast", "Wolf", "Furry Lost Sock", 2);
            Core.HuntMonster("greenguardwest", "Slime", "Slimy Lost Sock", 5);

            Core.EnsureComplete(2777);
            Core.EnsureAccept(2777);

            Core.HuntMonster("greenguardwest", "Slime", "Slimy Lost Sock", 5);
            Core.HuntMonster("greenguardeast", "Wolf", "Furry Lost Sock", 2);

            Core.EnsureComplete(2777);
            Core.Logger($"Completed x{i++*2}");
        }

    }

    public void BladeofAweREP(int rank = 10, bool farmBoA = true)
    {
        if (FactionRank("Blade of Awe") >= rank && !farmBoA)
            return;
        if (Core.CheckInventory("Blade of Awe", toInv: false) && farmBoA)
            farmBoA = false;
        if (farmBoA)
            Core.AddDrop("Legendary Stonewrit", "Legendary Handle", "Legendary Hilt", "Legendary Blade", "Legendary Runes");
        Core.AddDrop("Stonewrit Found!", "Handle Found!", "Hilt Found!", "Blade Found!", "Runes Found!");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        if(!Core.CheckInventory("Legendary Stonewrit", toInv: false) && (!Bot.Quests.IsUnlocked(2934) || farmBoA))
        {
            Core.EnsureAccept(2933);
            Core.HuntMonster("j6", "Sketchy Dragon", "Stonewrit Found!", 1, false);
            Core.EnsureComplete(2933);
            if(farmBoA)
                Bot.Player.Pickup("Legendary Stonewrit");
            Core.Logger("Find the Stonewrit! completed");
        }
        if (!Core.CheckInventory("Legendary Handle", toInv: false) && (!Bot.Quests.IsUnlocked(2935) || farmBoA))
        {
            Core.EnsureAccept(2934);
            Core.HuntMonster("gilead", "Fire Elemental|Water Elemental|Wind Elemental|Earth Elemental", "Handle Found!", 1, false);
            Core.EnsureComplete(2934);
            if(farmBoA)
                Bot.Player.Pickup("Legendary Handle");
            Core.Logger("Find the Handle! completed");
        }
        while (FactionRank("Blade of Awe") < rank)
        {
            Core.EnsureAccept(2935);
            Core.HuntMonster("castleundead", "Skeletal Viking|Skeletal Warrior", "Hilt Found!", 1, false);
            Core.EnsureComplete(2935);
            if (farmBoA)
            {
                Bot.Player.Pickup("Legendary Hilt");
                if(FactionRank("Blade of Awe") >= 6)
                    break;
            }
            Core.Logger($"Completed Find the Hilt! x{i++}");
        }
        if (farmBoA)
        {
            if (!Core.CheckInventory("Legendary Blade", toInv: false))
            {
                Core.EnsureAccept(2936);
                Core.HuntMonster("hydra", "Hydra Head", "Blade Found!", 1, false);
                Core.EnsureComplete(2936);
                Bot.Player.Pickup("Legendary Blade");
                Core.Logger("Find the Blade! completed");
            }
            if (!Core.CheckInventory("Legendary Runes", toInv: false))
            {
                Core.EnsureAccept(2937);
                Core.KillEscherion("Runes Found!");
                Core.EnsureComplete(2937);
                Bot.Player.Pickup("Legendary Runes");
                Core.Logger("Find the Runes! completed");
            }
            Core.Unbank("Legendary Stonewrit", "Legendary Handle", "Legendary Hilt", "Legendary Blade", "Legendary Runes");
            Core.Logger("You can now merge the Blade of Awe at /join museum", messageBox: true);
        }

    }

    public void BrightoakREP(int rank = 10)
    {
        if (FactionRank("Brightoak") >= rank)
            return;
        if (!Bot.Quests.IsAvailable(4667))
        {
            Core.Logger("Quest not avaible for farm, do Brightoak saga till Elfhame [Unlocking the Guardian's Mouth]");
            return;
        }
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        Core.Join("elfhame");
        while (FactionRank("Brightoak") < rank)
        {
            Core.EnsureAccept(4667);
            Bot.Map.GetMapItem(3984);
            Core.EnsureComplete(4667);
            Core.Logger($"Completed x{i++}");
        }
    }

    public void ChaosREP(int rank = 10)
    {
        if (FactionRank("Chaos") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Chaos") < rank)
        {
            Core.EnsureAccept(3594);
            Core.KillMonster("mountdoomskull", "b1", "Left", "*", "Chaos Power Increased", 6);
            Core.EnsureComplete(3594);
            Core.Logger($"Completed x{i++}");
        }
    }
    
    public void DoomwoodREP(int rank = 10)
    {
        if (FactionRank("Doomwood") >= rank)
            return;
        if (Core.IsMember)
            Core.AddDrop("Dark Tower Sword", "Light Tower Sword");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Doomwood") < rank)
        {
            if (!Core.IsMember)
            {
                Core.EnsureAccept(1151, 1152, 1153);
                Core.HuntMonster("shadowfallwar", "*", "To Do List of Doom");
                Core.HuntMonster("shadowfallwar", "*", "Skeleton Key");
                if (Bot.Inventory.ContainsTempItem("Un-Dead Tag", 15))
                    Core.EnsureComplete(1151);
                Core.EnsureComplete(1152, 1153, 0);
            }
            else
            {
                if(!Core.CheckInventory("Light Tower Sword"))
                {
                    Core.EnsureAccept(2100);
                    Core.HuntMonster("battleunderb", "Skeleton Warrior", "Battered Dark Tower Sword");
                    Core.EnsureComplete(2100);
                    Bot.Player.Pickup("Dark Tower Sword");
                    Core.EnsureAccept(2101);
                    Core.HuntMonster("doomwar", "Bronze DracoZombie", "Dracozombies' Spirits", 13);
                    Core.EnsureComplete(2101);
                    Bot.Player.Pickup("Dark Tower Sword");
                }
                Core.EnsureAccept(2102);
                Core.HuntMonster("doomwar", "Dark DracoZombie", "Bones of the Dracozombie");
                Core.EnsureComplete(2102);
            }
            Core.Logger($"Completed x{i++}");
        }
        if (Core.IsMember)
            Bot.Shops.SellItem("Light Tower Sword");
    }

    public void ElementalMasterREP(int rank = 10)
    {
        if (FactionRank("Elemental Master") >= rank)
            return;
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Elemental Master") < rank)
        {
            Core.EnsureAccept(3050, 3298);
            Core.HuntMonster("gilead", "Water Elemental", "Water Core");
            Core.HuntMonster("gilead", "Fire Elemental", "Fire Core");
            Core.HuntMonster("gilead", "Wind Elemental", "Air Core");
            Core.HuntMonster("gilead", "Earth Elemental", "Earth Core");
            Core.HuntMonster("gilead", "Mana Elemental", "Mana Core");
            Core.EnsureComplete(3050);
            if(Bot.Quests.CanComplete(3298))
                Core.EnsureComplete(3298);
            Core.Logger($"Completed x{i++}");
        }
    }

    public void EmberseaREP(int rank = 10)
    {
        if (FactionRank("Embersea") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Embersea") < rank)
        {
            Core.EnsureAccept(4228);
            //Core.EnsureAccept(4224);
            Core.HuntMonster("fireforge", "Blazebinder", "Defeated Blazebinder", 5);
            //Core.HuntMonster("fireforge", "Blazebinder", "Blazebinder defeated", 2);
            //Core.EnsureComplete(4224);
            Core.EnsureComplete(4228);
            Core.Logger($"Completed x{i++}");
        }
    }

    public void EternalREP(int rank = 10)
    {
        if (FactionRank("Eternal") >= rank)
            return;
        if (!Bot.Quests.IsAvailable(5198))
        {
            Core.Logger("Can't do farming quest [Sphynxes are Riddled with Gems] (/fourdpyramid)", messageBox: true);
            return;
        }
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Eternal") < rank)
        {
            Core.EnsureAccept(5198);
            Core.KillMonster("fourdpyramid", "r11", "Right", 2908, "White Gem", 2);
            Core.KillMonster("fourdpyramid", "r11", "Right", 2909, "Black Gem", 2);
            Core.EnsureComplete(5198);
            Core.Logger($"Completed x{i++}");
        }
    }
    
    public void EvilREP(int rank = 10)
    {
        if (FactionRank("Evil") >= rank)
            return;
        Core.Logger("This needs the player to be aligned to evil", messageBox: true);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while(FactionRank("Evil") < 4)
        {
            Core.EnsureAccept(364);
            Core.HuntMonster("newbie", "Slime", "Youthanize");
            Core.EnsureComplete(364);
            Core.Logger($"Completed x{i++}");
        }
        while (FactionRank("Evil") < rank)
        {
            if (!Core.IsMember)
            {
                Core.EnsureAccept(367);
                Core.HuntMonster("castleundead", "*", "Replacement Tibia", 6);
                Core.HuntMonster("castleundead", "*", "Phalanges", 3);
                Core.EnsureComplete(367); 
            }
            else
            {
                Core.EnsureAccept(366);
                Core.HuntMonster("sleuthhound", "Chair", "Chair", 4);
                Core.HuntMonster("sleuthhound", "Table", "Table", 2);
                Core.HuntMonster("sleuthhound", "Bookcase", "Bookcase");
                Core.EnsureComplete(366);
            }
            Core.Logger($"Completed x{i++}");
        }
    }

    public void GlaceraREP(int rank = 10)
    {
        if (FactionRank("Glacera") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Glacera") < rank)
        {
            Core.EnsureAccept(5597, 5598, 5599, 5600);
            Core.KillMonster("icewindwar", "r2", "Left", "*", "World Ender Medal", 10);
            Core.EnsureComplete(5599);
            if (Bot.Quests.CanComplete(5600))
                Core.EnsureComplete(5600);
            if (Bot.Quests.CanComplete(5598))
                Core.EnsureComplete(5598);
            if(Bot.Quests.CanComplete(5597))
                Core.EnsureComplete(5597);
            Core.Logger($"Completed x{i++}");
        }
    }
    
    public void GoodREP(int rank = 10)
    {
        if (FactionRank("Good") >= rank)
            return;
        Core.Logger("This needs the player to be aligned to good", messageBox: true);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while(FactionRank("Good") < 4)
        {
            Core.EnsureAccept(369);
            Core.HuntMonster("swordhavenbridge", "Slime", "Slime in a Jar", 6);
            Core.EnsureComplete(369);
            Core.Logger($"Completed x{i++}");
        }
        while (FactionRank("Good") < rank)
        {
            if (!Core.IsMember)
            {
                Core.EnsureAccept(372);
                Core.HuntMonster("castleundead", "*", "Chaorrupted Skull", 5);
                Core.EnsureComplete(372); 
            }
            else
            {
                Core.EnsureAccept(371);
                Core.HuntMonster("sewer", "Grumble", "Grumble's Fang");
                Core.EnsureComplete(371);
            }
            Core.Logger($"Completed x{i++}");
        }
    }

    public void LycanREP(int rank = 10)
    {
        if (FactionRank("Lycan") >= rank)
            return;
        if (!Bot.Quests.IsAvailable(537))
        {
            Core.Logger("Can't do farming quest [Sanguine] (/lycan)", messageBox: true);
            return;
        }
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Lycan") < rank)
        {
            Core.EnsureAccept(537);
            Core.HuntMonster("sanguine", "Sanguine", "Sanguine Mask");
            Core.EnsureComplete(537);
            Core.Logger($"Completed x{i++}");
        }
    }
    
    public void HollowbornREP(int rank = 10)
    {
        if (FactionRank("Hollowborn") >= rank)
            return;
        Core.AddDrop("Hollow Soul");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Hollowborn") < rank)
        {
            Core.EnsureAccept(7553, 7555);
            Core.KillMonster("shadowrealm", "r2", "Down", "*", "Darkseed", 8);
            Core.KillMonster("shadowrealm", "r2", "Down", "*", "Shadow Medallion", 5);
            Core.EnsureComplete(7553, 7555, 0);
            Core.Logger($"Completed x{i++}");
        }
    }

    public void MysteriousDungeonREP(int rank = 10)
    {
        if (FactionRank("Mysterious Dungeon") >= rank)
            return;
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        if (!Bot.Quests.IsAvailable(5429))
        {
            Core.Join("cursedshop");
            Core.EnsureAccept(5428);
            Bot.Map.GetMapItem(4803);
            Bot.Sleep(2500);
            if(Bot.Quests.CanComplete(5428))
                Core.EnsureComplete(5428);
            Bot.Player.Jump("Enter", "Spawn");
        }
        while (FactionRank("Mysterious Dungeon") < rank)
        {
            Core.EnsureAccept(5429);
            Core.HuntMonster("cursedshop", "Antique Chair", "Antique Chair Defeated");
            Core.EnsureComplete(5429);
            Core.Logger($"Completed x{i++}");
        }
    }

    public void MythsongREP(int rank = 10)
    {
        if (FactionRank("Mythsong") >= rank)
            return;
        if (!Bot.Quests.IsAvailable(710))
        {
            Core.Logger("Can't do farming quest [Kimberly] (/palooza)", messageBox: true);
            return;
        }
        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Mythsong") < rank)
        {
            Core.EnsureAccept(710);
            Core.HuntMonster("palooza", "Kimberly", "Kimberly Defeated");
            Core.EnsureComplete(710);
            Core.Logger($"Completed x{i++}");
        }
    }

    public void RavenlossREP(int rank = 10)
    {
        if (FactionRank("Ravenloss") >= rank)
            return;
        if (!Bot.Quests.IsAvailable(3445))
        {
            Core.Logger("Can't do farming quest [Slay the Spiderkin] (/twilightedge)", messageBox: true);
            return;
        }
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Ravenloss") < rank)
        {
            Core.EnsureAccept(3445);
            Core.HuntMonster("twilightedge", "ChaosWeaver Mage", "ChaosWeaver Slain", 10);
            Core.EnsureComplete(3445);
            Core.Logger($"Completed x{i++}");
        }
    }
    
    public void SpellCraftingREP(int rank = 10)
    {
        if (FactionRank("SpellCrafting") >= rank)
            return;
        Core.AddDrop("Mystic Quills", "Mystic Parchment");
        Core.Logger($"Farming rank {rank}");

        RBot.Factions.Faction spellcrafting = Bot.Player.Factions.Find(f => f.ID == 23);
        if (spellcrafting == null)
        {
            Core.EnsureAccept(2260);
            Core.Join("dragonrune");
            Bot.Map.GetMapItem(1920);
            Core.HuntMonster("castleundead", "Skeletal Warrior", "Arcane Parchment", 13);
            Core.JumpWait();
            Bot.Quests.EnsureComplete(2260, tries: 1);
        }

        int i = 1;
        if(FactionRank("SpellCrafting") < 4)
        {
            Core.HuntMonster("mobius", "Slugfit", "Mystic Quills", 10, false);
            Core.BuyItem("dragonrune", 549, "Ember Ink", 50, 5);
            Core.Join("spellcraft");
            while (FactionRank("SpellCrafting") < 4)
            {
                Bot.SendPacket("%xt%zm%crafting%1%spellOnStart%1%1555%Spell%");
                Bot.Sleep(3000);
                Bot.SendPacket("%xt%zm%crafting%1%spellComplete%1%2299%Ssikari's Breath%");
                Bot.Sleep(3000);
                Core.Logger($"Completed x{i++}");
            }
        }
        while (FactionRank("SpellCrafting") < rank)
        {
            Core.HuntMonster("underworld", "Skull Warrior", "Mystic Parchment", 10, false);
            Core.BuyItem("dragonrune", 549, "Hallow Ink", 50, 5);
            Core.Join("spellcraft");
            while(Core.CheckInventory("Hallow Ink") && FactionRank("SpellCrafting") < rank)
            {
                Bot.SendPacket("%xt%zm%crafting%1%spellOnStart%6%1555%Spell%");
                Bot.Sleep(3000);
                Bot.SendPacket("%xt%zm%crafting%1%spellComplete%6%2322%Plague Flare%");
                Bot.Sleep(3000);
                Core.Logger($"Completed x{i++}");
            }
        }
        Core.SellItem("Ember Ink", all: true);
        Core.SellItem("Hallow Ink", all: true);
    }

    public void VampireREP(int rank = 10)
    {
        if (FactionRank("Vampire") >= rank)
            return;
        if (!Bot.Quests.IsAvailable(522))
        {
            Core.Logger("Can't do farming quest [Twisted Paw] (/safiria)", messageBox: true);
            return;
        }
        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Vampire") < rank)
        {
            Core.EnsureAccept(522);
            Core.HuntMonster("safiria", "Twisted Paw", "Twisted Paw's Head");
            Core.EnsureComplete(522);
            Core.Logger($"Completed x{i++}");
        }
    }
    
    public void YokaiREP(int rank = 10)
    {
        if (FactionRank("Yokai") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int i = 1;
        while (FactionRank("Yokai") < rank)
        {
            Core.EnsureAccept(383);
            Core.KillMonster("dragonkoiz", "t1", "Left", "Pockey Chew", "Piece of Pockey", 3);
            Core.EnsureComplete(383);
            Bot.Player.Jump("Enter", "Spawn");
            Core.Logger($"Completed x{i++}");
        }
    }
    #endregion

    public int FactionRank(string faction) => Bot.Player.GetFactionRank(faction);
}

public enum BoostIDs
{
    DailyXP60 = 19189,
    XP20 = 22448,
    XP60 = 27552,
    DoomClass60 = 19761,
    Class20 = 22447,
    Class60 = 27555,
    DoomREP60 = 19762,
    REP20 = 22449,
    REP60 = 27553,
    DoomGold60 = 19763,
    Gold20 = 22450,
    Gold60 = 27554
}

public enum AlchemyRunes
{
    Dragon,
    Jera,
    Uruz,
    Fehu,
    Gebo
}