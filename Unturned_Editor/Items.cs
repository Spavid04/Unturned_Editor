using System.Collections.Generic;

public static class Items
{
    #region Gets the name from the specified ID

    public static string getItem(int ID)
    {
        switch (ID)
        {
            case 14000:
                return "Canned Beans";
            case 14001:
                return "Granola Bar";
            case 14002:
                return "Fresh Carrot";
            case 14003:
                return "Moldy Carrot";
            case 14004:
                return "Fresh Tomato";
            case 14005:
                return "Moldy Tomato";
            case 14006:
                return "Fresh Corn";
            case 14007:
                return "Moldy Corn";
            case 14008:
                return "Fresh Cabbage";
            case 14009:
                return "Moldy Cabbage";
            case 14010:
                return "Fresh Potato";
            case 14011:
                return "Moldy Potato";
            case 14012:
                return "Cooked Venison";
            case 14013:
                return "Raw Venison";
            case 14014:
                return "Canned Soup";
            case 14015:
                return "Canned Chili";
            case 14016:
                return "Canned Vegetables";
            case 14017:
                return "Canned Pasta";
            case 14018:
                return "Canned Ham";
            case 14019:
                return "Canned Tuna";
            case 14020:
                return "Candy Bar";
            case 14021:
                return "Chocolate Bar";
            case 14022:
                return "MRE";
            case 14023:
                return "Potato Chips";
            case 14024:
                return "Bran Cereal";
            case 14025:
                return "Red Berries";
            case 14026:
                return "Blue Berries";
            case 14027:
                return "Pink Berries";
            case 14028:
                return "Pale Berries";
            case 14029:
                return "Green Berries";
            case 14030:
                return "Purple Berries";
            case 14031:
                return "Coconut";
            case 14032:
                return "Cooked Bacon";
            case 14033:
                return "Raw Bacon";
            case 16000:
                return "Wooden Shield";
            case 16001:
                return "Metal Door";
            case 16002:
                return "Worklight";
            case 16003:
                return "Caltrop";
            case 16004:
                return "Barbed Wire";
            case 16005:
                return "Wooden Spike Trap";
            case 16006:
                return "Snare";
            case 16007:
                return "Generator";
            case 16008:
                return "Note";
            case 16009:
                return "Electric Trap";
            case 16010:
                return "Sandbags";
            case 16011:
                return "Sleeping Bag";
            case 16012:
                return "Cot";
            case 16013:
                return "Campfire";
            case 16014:
                return "Metal Shutter";
            case 16015:
                return "MOAB";
            case 16016:
                return "Tripmine";
            case 16017:
                return "Landmine";
            case 16018:
                return "Metal Shield";
            case 16019:
                return "Wooden Crate";
            case 16020:
                return "Barbed Fence";
            case 16021:
                return "Electric Fence";
            case 16022:
                return "Wooden Door";
            case 16023:
                return "Metal Locker";
            case 16024:
                return "Wooden Shutter";
            case 16025:
                return "Wooden Chest";
            case 16026:
                return "Brazier";
            case 16027:
                return "Wooden Gate";
            case 16028:
                return "Metal Gate";
            case 17000:
                return "Wooden Foundation";
            case 17001:
                return "Wooden Wall";
            case 17002:
                return "Wooden Doorway";
            case 17003:
                return "Wooden Pillar";
            case 17004:
                return "Wooden Platform";
            case 17005:
                return "Wooden Ramp";
            case 17006:
                return "Greenhouse Foundation";
            case 17007:
                return "Greenhouse Platform";
            case 17008:
                return "Wooden Hole";
            case 17009:
                return "Wooden Ladder";
            case 17010:
                return "Wooden Window";
            case 17011:
                return "Wooden Post";
            case 17012:
                return "Wooden Rampart";
            case 17013:
                return "Stone Rampart";
            case 17014:
                return "Stone Post";
            case 17015:
                return "Stone Wall";
            case 17016:
                return "Stone Doorway";
            case 17017:
                return "Stone Window";
            case 17018:
                return "Stone Pillar";
            case 17019:
                return "Dock Foundation";
            case 17020:
                return "Wooden Garage";
            case 17021:
                return "Stone Garage";
            case 4000:
                return "Fancy Suit";
            case 4001:
                return "RCMP Uniform1";
            case 4002:
                return "Police Uniform1";
            case 4003:
                return "Army Fatigues1";
            case 4004:
                return "Army Fatigues2";
            case 4005:
                return "Fireperson Top1";
            case 4006:
                return "Fireperson Top2";
            case 4007:
                return "Construction Vest";
            case 4008:
                return "Doctor Coat";
            case 4009:
                return "Orange Hoodie";
            case 4010:
                return "Pink Shirt";
            case 4011:
                return "Boring Suit";
            case 4012:
                return "Blue Sweatervest";
            case 4013:
                return "Jumper Top";
            case 4014:
                return "Grocer Top";
            case 4015:
                return "Sailor Fatigues1";
            case 4016:
                return "Animal Shirt";
            case 4017:
                return "Ghillie Top";
            case 4018:
                return "Ninja Top";
            case 4019:
                return "Plaid Shirt";
            case 4020:
                return "Chef Shirt";
            case 5000:
                return "Fancy Jeans";
            case 5001:
                return "RCMP Uniform2";
            case 5002:
                return "Police Uniform2";
            case 5003:
                return "Army Fatigues3";
            case 5004:
                return "Army Fatigues4";
            case 5005:
                return "Fireperson Pants1";
            case 5006:
                return "Fireperson Pants2";
            case 5007:
                return "Work Jeans";
            case 5008:
                return "Doctor Pants";
            case 5009:
                return "Grey Pants";
            case 5010:
                return "Khaki Pants";
            case 5011:
                return "Boring Pants";
            case 5012:
                return "Brown Pants";
            case 5013:
                return "Jumper Bottom";
            case 5014:
                return "Grocer Bottom";
            case 5015:
                return "Sailor Fatigues2";
            case 5016:
                return "Animal Pants";
            case 5017:
                return "Ghillie Bottom";
            case 5018:
                return "Ninja Bottom";
            case 5019:
                return "Lumberjack Pants";
            case 5020:
                return "Chef Pants";
            case 18000:
                return "Board";
            case 18001:
                return "Nails";
            case 18002:
                return "Bolts";
            case 18003:
                return "Log";
            case 18004:
                return "Stick";
            case 18005:
                return "Animal Pelt";
            case 18006:
                return "Rope";
            case 18007:
                return "Wire";
            case 18008:
                return "Scrap Metal";
            case 18009:
                return "Cloth";
            case 18010:
                return "Duct Tape";
            case 18011:
                return "Can";
            case 18012:
                return "Raw Explosives";
            case 18013:
                return "Civilian Bullets";
            case 18014:
                return "Military Bullets";
            case 18015:
                return "Stone";
            case 18016:
                return "Tracer Bullets";
            case 18017:
                return "Batteries";
            case 18018:
                return "Wooden Spike";
            case 18019:
                return "Rocks";
            case 18020:
                return "Shells";
            case 0:
                return "Fancy Shades";
            case 1:
                return "Civilian NVG";
            case 2:
                return "Stetson";
            case 3:
                return "Construction Helmet";
            case 4:
                return "Fire Helmet1";
            case 5:
                return "Police Cap";
            case 6:
                return "Fire Helmet2";
            case 7:
                return "Desert Helmet";
            case 8:
                return "Forest Helmet";
            case 9:
                return "Dixie Hat";
            case 10:
                return "Bandana";
            case 11:
                return "Ghillie Hood";
            case 12:
                return "Ninja Hood";
            case 13:
                return "Farmer Hat";
            case 14:
                return "Chef Hat";
            case 15:
                return "Beret";
            case 16:
                return "Ushanka";
            case 17:
                return "Military NVG";
            case 18:
                return "White Hat";
            case 19:
                return "Miner Helmet";
            case 8000:
                return "Axe";
            case 8001:
                return "Handlamp";
            case 8002:
                return "Pick";
            case 8003:
                return "Kitchen Knife";
            case 8004:
                return "Hammer";
            case 8005:
                return "Frying Pan";
            case 8006:
                return "Baseball Bat";
            case 8007:
                return "Police Baton";
            case 8008:
                return "Torch";
            case 8009:
                return "Fireaxe";
            case 8010:
                return "Crowbar";
            case 8011:
                return "Pocketknife";
            case 8012:
                return "Butcher Knife";
            case 8013:
                return "Sledgehammer";
            case 8014:
                return "Golf Club";
            case 8015:
                return "Katana";
            case 8016:
                return "Machete";
            case 8017:
                return "Spike";
            case 8018:
                return "Branch";
            case 8019:
                return "Handsaw";
            case 13000:
                return "Medkit";
            case 13001:
                return "Rag";
            case 13002:
                return "Splint";
            case 13003:
                return "Vitamins";
            case 13004:
                return "Antibiotics";
            case 13005:
                return "Painkillers";
            case 13006:
                return "Dressing";
            case 13007:
                return "Blood Bag";
            case 13008:
                return "Bandage";
            case 13009:
                return "Adrenaline";
            case 13010:
                return "Morphine";
            case 13011:
                return "Crushed Red Berries";
            case 13012:
                return "Crushed Blue Berries";
            case 13013:
                return "Crushed Pink Berries";
            case 13014:
                return "Crushed Pale Berries";
            case 13015:
                return "Crushed Green Berries";
            case 13016:
                return "Crushed Purple Berries";
            case 13017:
                return "Purification Tablets";
            case 13018:
                return "Vaccine";
            case 7000:
                return "Swissgewehr";
            case 7001:
                return "Colt";
            case 7002:
                return "Double Barrel";
            case 7003:
                return "Mosen";
            case 7004:
                return "Longbow";
            case 7005:
                return "Novuh";
            case 7006:
                return "Berette";
            case 7007:
                return "Crossbow";
            case 7008:
                return "Maplestrike";
            case 7009:
                return "Zubeknakov";
            case 7010:
                return "Magnum";
            case 7011:
                return "Timberwolf";
            case 7012:
                return "Uzy";
            case 7013:
                return "Matamorez";
            case 7014:
                return "Compound Bow";
            case 7015:
                return "Outfield";
            case 7016:
                return "Lever Action";
            case 7017:
                return "Proninety";
            case 7018:
                return "Desert Falcon";
            case 9000:
                return "Red Dot Sight";
            case 9001:
                return "Open Circle Rail";
            case 9002:
                return "12x Zoom Scope";
            case 9003:
                return "Holographic Sight";
            case 9004:
                return "6x Zoom Scope";
            case 9005:
                return "Half Circle Rail";
            case 9006:
                return "Full Circle Rail";
            case 9007:
                return "Dual Component Rail";
            case 9008:
                return "Planar Track Rail";
            case 9009:
                return "20x Zoom Scope";
            case 9010:
                return "Zoomomatic";
            case 9011:
                return "Point Circle Sight";
            case 9012:
                return "7x Zoom Scope";
            case 9013:
                return "Dual Point Rail";
            case 10000:
                return "NATO Magazine";
            case 10001:
                return "NATO Drum";
            case 10002:
                return "Swift Magazine";
            case 10003:
                return "Bonjour Clip";
            case 10004:
                return "Lebel Magazine";
            case 10005:
                return "NATO Tracer";
            case 10006:
                return "Savage Magazine";
            case 10007:
                return "Savage Drum";
            case 10008:
                return "Winchestre Clip";
            case 10009:
                return "Lapua Magazine";
            case 10010:
                return "Lapua Tracer";
            case 10011:
                return "Yuri Magazine";
            case 10012:
                return "Xtrmin Magazine";
            case 10013:
                return "PDW Magazine";
            case 15000:
                return "Bottled Water";
            case 15001:
                return "Canned Cola";
            case 15002:
                return "Apple Juice Box";
            case 15003:
                return "Large Bottled Water";
            case 15004:
                return "Energy Drink";
            case 15005:
                return "Moldy Milk";
            case 15006:
                return "Moldy Orange Juice";
            case 15007:
                return "Moldy Bottled Water";
            case 15008:
                return "Milk";
            case 15009:
                return "Orange Juice";
            case 23000:
                return "Road Flare";
            case 23001:
                return "Red Chemlight";
            case 23002:
                return "Blue Chemlight";
            case 23003:
                return "Yellow Chemlight";
            case 23004:
                return "Green Chemlight";
            case 23005:
                return "Orange Chemlight";
            case 23006:
                return "Purple Chemlight";
            case 23007:
                return "Frag Grenade";
            case 23008:
                return "Smoke Grenade";
            case 19000:
                return "Wooden Plate";
            case 19001:
                return "Wooden Support";
            case 19002:
                return "Wooden Frame";
            case 19003:
                return "Wooden Cross";
            case 19004:
                return "Stone Plate";
            case 19005:
                return "Stone Support";
            case 19006:
                return "Stone Frame";
            case 19007:
                return "Stone Cross";
            case 2000:
                return "Napsack";
            case 2001:
                return "Schoolbag";
            case 2002:
                return "Travelpack";
            case 2003:
                return "Coyotepack";
            case 2004:
                return "Rucksack";
            case 2005:
                return "Alicepack";
            case 2006:
                return "Animalpack";
            case 3000:
                return "Civilian Armor";
            case 3001:
                return "Desert Armor";
            case 3002:
                return "Forest Armor";
            case 3003:
                return "Police Armor";
            case 3004:
                return "Poncho";
            case 3005:
                return "Magick Cloak";
            case 11000:
                return "Vertical Grip";
            case 11001:
                return "Angled Grip";
            case 11002:
                return "Tactical Laser";
            case 11003:
                return "Tactical Light";
            case 11004:
                return "Bipod";
            case 11005:
                return "Bayonet";
            case 22000:
                return "Carrot Seed";
            case 22001:
                return "Tomato Seed";
            case 22002:
                return "Corn Seed";
            case 22003:
                return "Cabbage Seed";
            case 22004:
                return "Potato Seed";
            case 12000:
                return "Suppressor";
            case 12001:
                return "Flash Hider";
            case 12002:
                return "Muffler";
            case 25000:
                return "Buckshot";
            case 25001:
                return "Arrow";
            case 25002:
                return "Slug";
            case 20000:
                return "Gas Can";
            case 20001:
                return "Car Jack";
            case 21000:
                return "Blowtorch";
            case 21001:
                return "Chainsaw";
            case 24000:
                return "Binoculars";
            case 26000:
                return "Canteen";
            case 27000:
                return "Fertilizer";
            case 28000:
                return "PEI Map";
            case -1:
                return "<empty>";
            default:
                return "<unknown>";
        }
    }

    #endregion

    #region Gets the ID from the specified name

    public static int getID(string name)
    {
        name = name.ToLower();
        switch (name)
        {
            case "canned beans":
                return 14000;
            case "granola bar":
                return 14001;
            case "fresh carrot":
                return 14002;
            case "moldy carrot":
                return 14003;
            case "fresh tomato":
                return 14004;
            case "moldy tomato":
                return 14005;
            case "fresh corn":
                return 14006;
            case "moldy corn":
                return 14007;
            case "fresh cabbage":
                return 14008;
            case "moldy cabbage":
                return 14009;
            case "fresh potato":
                return 14010;
            case "moldy potato":
                return 14011;
            case "cooked venison":
                return 14012;
            case "raw venison":
                return 14013;
            case "canned soup":
                return 14014;
            case "canned chili":
                return 14015;
            case "canned vegetables":
                return 14016;
            case "canned pasta":
                return 14017;
            case "canned ham":
                return 14018;
            case "canned tuna":
                return 14019;
            case "candy bar":
                return 14020;
            case "chocolate bar":
                return 14021;
            case "mre":
                return 14022;
            case "potato chips":
                return 14023;
            case "bran cereal":
                return 14024;
            case "red berries":
                return 14025;
            case "blue berries":
                return 14026;
            case "pink berries":
                return 14027;
            case "pale berries":
                return 14028;
            case "green berries":
                return 14029;
            case "purple berries":
                return 14030;
            case "coconut":
                return 14031;
            case "cooked bacon":
                return 14032;
            case "raw bacon":
                return 14033;
            case "wooden shield":
                return 16000;
            case "metal door":
                return 16001;
            case "worklight":
                return 16002;
            case "caltrop":
                return 16003;
            case "barbed wire":
                return 16004;
            case "wooden spike trap":
                return 16005;
            case "snare":
                return 16006;
            case "generator":
                return 16007;
            case "note":
                return 16008;
            case "electric trap":
                return 16009;
            case "sandbags":
                return 16010;
            case "sleeping bag":
                return 16011;
            case "cot":
                return 16012;
            case "campfire":
                return 16013;
            case "metal shutter":
                return 16014;
            case "moab":
                return 16015;
            case "tripmine":
                return 16016;
            case "landmine":
                return 16017;
            case "metal shield":
                return 16018;
            case "wooden crate":
                return 16019;
            case "barbed fence":
                return 16020;
            case "electric fence":
                return 16021;
            case "wooden door":
                return 16022;
            case "metal locker":
                return 16023;
            case "wooden shutter":
                return 16024;
            case "wooden chest":
                return 16025;
            case "brazier":
                return 16026;
            case "wooden gate":
                return 16027;
            case "metal gate":
                return 16028;
            case "wooden foundation":
                return 17000;
            case "wooden wall":
                return 17001;
            case "wooden doorway":
                return 17002;
            case "wooden pillar":
                return 17003;
            case "wooden platform":
                return 17004;
            case "wooden ramp":
                return 17005;
            case "greenhouse foundation":
                return 17006;
            case "greenhouse platform":
                return 17007;
            case "wooden hole":
                return 17008;
            case "wooden ladder":
                return 17009;
            case "wooden window":
                return 17010;
            case "wooden post":
                return 17011;
            case "wooden rampart":
                return 17012;
            case "stone rampart":
                return 17013;
            case "stone post":
                return 17014;
            case "stone wall":
                return 17015;
            case "stone doorway":
                return 17016;
            case "stone window":
                return 17017;
            case "stone pillar":
                return 17018;
            case "dock foundation":
                return 17019;
            case "wooden garage":
                return 17020;
            case "stone garage":
                return 17021;
            case "fancy suit":
                return 4000;
            case "rcmp uniform1":
                return 4001;
            case "police uniform1":
                return 4002;
            case "army fatigues1":
                return 4003;
            case "army fatigues2":
                return 4004;
            case "fireperson top1":
                return 4005;
            case "fireperson top2":
                return 4006;
            case "construction vest":
                return 4007;
            case "doctor coat":
                return 4008;
            case "orange hoodie":
                return 4009;
            case "pink shirt":
                return 4010;
            case "boring suit":
                return 4011;
            case "blue sweatervest":
                return 4012;
            case "jumper top":
                return 4013;
            case "grocer top":
                return 4014;
            case "sailor fatigues1":
                return 4015;
            case "animal shirt":
                return 4016;
            case "ghillie top":
                return 4017;
            case "ninja top":
                return 4018;
            case "plaid shirt":
                return 4019;
            case "chef shirt":
                return 4020;
            case "fancy jeans":
                return 5000;
            case "rcmp uniform2":
                return 5001;
            case "police uniform2":
                return 5002;
            case "army fatigues3":
                return 5003;
            case "army fatigues4":
                return 5004;
            case "fireperson pants1":
                return 5005;
            case "fireperson pants2":
                return 5006;
            case "work jeans":
                return 5007;
            case "doctor pants":
                return 5008;
            case "grey pants":
                return 5009;
            case "khaki pants":
                return 5010;
            case "boring pants":
                return 5011;
            case "brown pants":
                return 5012;
            case "jumper bottom":
                return 5013;
            case "grocer bottom":
                return 5014;
            case "sailor fatigues2":
                return 5015;
            case "animal pants":
                return 5016;
            case "ghillie bottom":
                return 5017;
            case "ninja bottom":
                return 5018;
            case "lumberjack pants":
                return 5019;
            case "chef pants":
                return 5020;
            case "board":
                return 18000;
            case "nails":
                return 18001;
            case "bolts":
                return 18002;
            case "log":
                return 18003;
            case "stick":
                return 18004;
            case "animal pelt":
                return 18005;
            case "rope":
                return 18006;
            case "wire":
                return 18007;
            case "scrap metal":
                return 18008;
            case "cloth":
                return 18009;
            case "duct tape":
                return 18010;
            case "can":
                return 18011;
            case "raw explosives":
                return 18012;
            case "civilian bullets":
                return 18013;
            case "military bullets":
                return 18014;
            case "stone":
                return 18015;
            case "tracer bullets":
                return 18016;
            case "batteries":
                return 18017;
            case "wooden spike":
                return 18018;
            case "rocks":
                return 18019;
            case "shells":
                return 18020;
            case "fancy shades":
                return 0;
            case "civilian nvg":
                return 1;
            case "stetson":
                return 2;
            case "construction helmet":
                return 3;
            case "fire helmet1":
                return 4;
            case "police cap":
                return 5;
            case "fire helmet2":
                return 6;
            case "desert helmet":
                return 7;
            case "forest helmet":
                return 8;
            case "dixie hat":
                return 9;
            case "bandana":
                return 10;
            case "ghillie hood":
                return 11;
            case "ninja hood":
                return 12;
            case "farmer hat":
                return 13;
            case "chef hat":
                return 14;
            case "beret":
                return 15;
            case "ushanka":
                return 16;
            case "military nvg":
                return 17;
            case "white hat":
                return 18;
            case "miner helmet":
                return 19;
            case "axe":
                return 8000;
            case "handlamp":
                return 8001;
            case "pick":
                return 8002;
            case "kitchen knife":
                return 8003;
            case "hammer":
                return 8004;
            case "frying pan":
                return 8005;
            case "baseball bat":
                return 8006;
            case "police baton":
                return 8007;
            case "torch":
                return 8008;
            case "fireaxe":
                return 8009;
            case "crowbar":
                return 8010;
            case "pocketknife":
                return 8011;
            case "butcher knife":
                return 8012;
            case "sledgehammer":
                return 8013;
            case "golf club":
                return 8014;
            case "katana":
                return 8015;
            case "machete":
                return 8016;
            case "spike":
                return 8017;
            case "branch":
                return 8018;
            case "handsaw":
                return 8019;
            case "medkit":
                return 13000;
            case "rag":
                return 13001;
            case "splint":
                return 13002;
            case "vitamins":
                return 13003;
            case "antibiotics":
                return 13004;
            case "painkillers":
                return 13005;
            case "dressing":
                return 13006;
            case "blood bag":
                return 13007;
            case "bandage":
                return 13008;
            case "adrenaline":
                return 13009;
            case "morphine":
                return 13010;
            case "crushed red berries":
                return 13011;
            case "crushed blue berries":
                return 13012;
            case "crushed pink berries":
                return 13013;
            case "crushed pale berries":
                return 13014;
            case "crushed green berries":
                return 13015;
            case "crushed purple berries":
                return 13016;
            case "purification tablets":
                return 13017;
            case "vaccine":
                return 13018;
            case "swissgewehr":
                return 7000;
            case "colt":
                return 7001;
            case "double barrel":
                return 7002;
            case "mosen":
                return 7003;
            case "longbow":
                return 7004;
            case "novuh":
                return 7005;
            case "berette":
                return 7006;
            case "crossbow":
                return 7007;
            case "maplestrike":
                return 7008;
            case "zubeknakov":
                return 7009;
            case "magnum":
                return 7010;
            case "timberwolf":
                return 7011;
            case "uzy":
                return 7012;
            case "matamorez":
                return 7013;
            case "compound bow":
                return 7014;
            case "outfield":
                return 7015;
            case "lever action":
                return 7016;
            case "proninety":
                return 7017;
            case "desert falcon":
                return 7018;
            case "red dot sight":
                return 9000;
            case "open circle rail":
                return 9001;
            case "12x zoom scope":
                return 9002;
            case "holographic sight":
                return 9003;
            case "6x zoom scope":
                return 9004;
            case "half circle rail":
                return 9005;
            case "full circle rail":
                return 9006;
            case "dual component rail":
                return 9007;
            case "planar track rail":
                return 9008;
            case "20x zoom scope":
                return 9009;
            case "zoomomatic":
                return 9010;
            case "point circle sight":
                return 9011;
            case "7x zoom scope":
                return 9012;
            case "dual point rail":
                return 9013;
            case "nato magazine":
                return 10000;
            case "nato drum":
                return 10001;
            case "swift magazine":
                return 10002;
            case "bonjour clip":
                return 10003;
            case "lebel magazine":
                return 10004;
            case "nato tracer":
                return 10005;
            case "savage magazine":
                return 10006;
            case "savage drum":
                return 10007;
            case "winchestre clip":
                return 10008;
            case "lapua magazine":
                return 10009;
            case "lapua tracer":
                return 10010;
            case "yuri magazine":
                return 10011;
            case "xtrmin magazine":
                return 10012;
            case "pdw magazine":
                return 10013;
            case "bottled water":
                return 15000;
            case "canned cola":
                return 15001;
            case "apple juice box":
                return 15002;
            case "large bottled water":
                return 15003;
            case "energy drink":
                return 15004;
            case "moldy milk":
                return 15005;
            case "moldy orange juice":
                return 15006;
            case "moldy bottled water":
                return 15007;
            case "milk":
                return 15008;
            case "orange juice":
                return 15009;
            case "road flare":
                return 23000;
            case "red chemlight":
                return 23001;
            case "blue chemlight":
                return 23002;
            case "yellow chemlight":
                return 23003;
            case "green chemlight":
                return 23004;
            case "orange chemlight":
                return 23005;
            case "purple chemlight":
                return 23006;
            case "frag grenade":
                return 23007;
            case "smoke grenade":
                return 23008;
            case "wooden plate":
                return 19000;
            case "wooden support":
                return 19001;
            case "wooden frame":
                return 19002;
            case "wooden cross":
                return 19003;
            case "stone plate":
                return 19004;
            case "stone support":
                return 19005;
            case "stone frame":
                return 19006;
            case "stone cross":
                return 19007;
            case "napsack":
                return 2000;
            case "schoolbag":
                return 2001;
            case "travelpack":
                return 2002;
            case "coyotepack":
                return 2003;
            case "rucksack":
                return 2004;
            case "alicepack":
                return 2005;
            case "animalpack":
                return 2006;
            case "civilian armor":
                return 3000;
            case "desert armor":
                return 3001;
            case "forest armor":
                return 3002;
            case "police armor":
                return 3003;
            case "poncho":
                return 3004;
            case "magick cloak":
                return 3005;
            case "vertical grip":
                return 11000;
            case "angled grip":
                return 11001;
            case "tactical laser":
                return 11002;
            case "tactical light":
                return 11003;
            case "bipod":
                return 11004;
            case "bayonet":
                return 11005;
            case "carrot seed":
                return 22000;
            case "tomato seed":
                return 22001;
            case "corn seed":
                return 22002;
            case "cabbage seed":
                return 22003;
            case "potato seed":
                return 22004;
            case "suppressor":
                return 12000;
            case "flash hider":
                return 12001;
            case "muffler":
                return 12002;
            case "buckshot":
                return 25000;
            case "arrow":
                return 25001;
            case "slug":
                return 25002;
            case "gas can":
                return 20000;
            case "car jack":
                return 20001;
            case "blowtorch":
                return 21000;
            case "chainsaw":
                return 21001;
            case "binoculars":
                return 24000;
            case "canteen":
                return 26000;
            case "fertilizer":
                return 27000;
            case "pei map":
                return 28000;
            case "<empty>":
                return -1;
            default:
                return -2;
        }
    }

    #endregion

    #region Get weight of item based on ID

    public static float getWeight(int ID)
    {
        //woohoo! empty function!
        return -1;
    }

    #endregion

    public static bool isToggleable(string name)
    {
        name = name.ToLower();

        List<string> toggleables = new List<string>
        {
            "torch",
            "handlamp"
        };

        if (toggleables.Contains(name)) return true;
        else return false;
    }

    public static bool isFillable(string name)
    {
        name = name.ToLower();

        List<string> fillables = new List<string>
        {
            "canteen",
            "gas can"
        };

        if (fillables.Contains(name)) return true;
        else return false;
    }

    public static bool isWeapon(string name)
    {
        name = name.ToLower();

        List<string> weapons = new List<string>
        {
            "berette",
            "colt",
            "magnum",
            "desert falcon",
            "lever action",
            "double barrel",
            "novuh",
            "mosen",
            "outfield",
            "maplestrike",
            "swissgewehr",
            "zubeknakov",
            "uzy",
            "proninety",
            "matamorez",
            "timberwolf",
            "longbow",
            "compound bow",
            "crossbow"
        };

        if (weapons.Contains(name)) return true;
        else return false;
    }

    public static List<string> getPants()
    {
        List<string> toreturn = new List<string>()
        {
            "Fancy Jeans",
            "RCMP Uniform2",
            "Police Uniform2",
            "Army Fatigues3",
            "Army Fatigues4",
            "Fireperson Pants1",
            "Fireperson Pants2",
            "Work Jeans",
            "Doctor Pants",
            "Grey Pants",
            "Khaki Pants",
            "Boring Pants",
            "Brown Pants",
            "Jumper Bottom",
            "Grocer Bottom",
            "Sailor Fatigues2",
            "Animal Pants",
            "Ghillie Bottom",
            "Ninja Bottom",
            "Lumberjack Pants",
            "Chef Pants"
        };

        return toreturn;
    }

    public static List<string> getShirts()
    {
        List<string> toreturn = new List<string>()
        {
            "Fancy Suit",
            "RCMP Uniform1",
            "Police Uniform1",
            "Army Fatigues1",
            "Army Fatigues2",
            "Fireperson Top1",
            "Fireperson Top2",
            "Construction Vest",
            "Doctor Coat",
            "Orange Hoodie",
            "Pink Shirt",
            "Boring Suit",
            "Blue Sweatervest",
            "Jumper Top",
            "Grocer Top",
            "Sailor Fatigues1",
            "Animal Shirt",
            "Ghillie Top",
            "Ninja Top",
            "Plaid Shirt",
            "Chef Shirt"
        };

        return toreturn;
    }

    public static List<string> getHelmets()
    {
        List<string> toreturn = new List<string>()
        {
            "Fancy Shades",
            "Civilian NVG",
            "Stetson",
            "Construction Helmet",
            "Fire Helmet1",
            "Police Cap",
            "Fire Helmet2",
            "Desert Helmet",
            "Forest Helmet",
            "Dixie Hat",
            "Bandana",
            "Ghillie Hood",
            "Ninja Hood",
            "Farmer Hat",
            "Chef Hat",
            "Beret",
            "Ushanka",
            "Military NVG",
            "White Hat",
            "Miner Helmet"
        };

        return toreturn;
    }

    public static List<string> getArmors()
    {
        List<string> toreturn = new List<string>()
        {
            "Civilian Armor",
            "Desert Armor",
            "Forest Armor",
            "Police Armor",
            "Poncho",
            "Magick Cloak"
        };

        return toreturn;
    }

    public static List<string> getBackpacks()
    {
        List<string> toreturn = new List<string>()
        {
            "Napsack",
            "Schoolbag",
            "Travelpack",
            "Coyotepack",
            "Rucksack",
            "Alicepack",
            "Animalpack"
        };

        return toreturn;
    }

    public static List<string> getBarricades()
    {
        List<string> toreturn = new List<string>()
        {
            "Wooden Foundation",
            "Wooden Wall",
            "Wooden Doorway",
            "Wooden Pillar",
            "Wooden Platform",
            "Wooden Ramp",
            "Greenhouse Foundation",
            "Greenhouse Platform",
            "Wooden Hole",
            "Wooden Ladder",
            "Wooden Post",
            "Wooden Rampart",
            "Stone Rampart",
            "Stone Post",
            "Stone Wall",
            "Stone Pillar",
            "Dock Foundation",
            "Wooden Garage",
            "Stone Garage"
        };

        return toreturn;
    }
}