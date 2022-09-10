using System;
using System.Runtime.InteropServices;

namespace CREATION_TOOLS
{

    #region QUEST

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    [Serializable]
    public struct QUEST_DATA
    {
        public int index;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)QUEST_SYSTEM_CONFIG.MAX_QUEST_NAME_LENGTH)]
        public string name;
        public QUEST_TYPE type;
        public int level;
        public QUEST_NPC_TYPE NPCType;
        public int avatarExperience;
        public int petExperience;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = (int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS)]
        public sCURRENCY[] currencyReward;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = (int)QUEST_SYSTEM_CONFIG.MAX_OBJETIVE_NUM)]
        public sOBJETIVES[] objetiveData;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = (int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM)]
        public sLOOT[] lootData;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    [Serializable]
    public struct sOBJETIVES
    {
        public QUEST_OBJETIVES objetive;
        public int amount;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    [Serializable]
    public struct sCURRENCY
    {
        public GAME_CURRENCY currency;
        public int amount;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    [Serializable]
    public struct sLOOT
    {
        public int itemID;
        public int amount;
        public float chance;
    }
    #endregion
}