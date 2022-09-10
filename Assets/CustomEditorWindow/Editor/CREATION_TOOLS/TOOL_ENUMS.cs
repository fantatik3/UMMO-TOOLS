namespace CREATION_TOOLS
{
    public enum GAME_CONFIG { 
    MAX_AVATAR_LEVEL = 145,
    MAX_AVATAR_GOD_LEVEL = 12,
    MAX_AVATAR_REBIRTH_LEVEL = 12
    }

    public enum QUEST_SYSTEM_CONFIG
    {
        MAX_OBJETIVE_NUM = 5,
        MAX_LOOT_SLOT_NUM = 10,
        MAX_CURRENCY_REWARDS = 4,
        MAX_QUEST_NAME_LENGTH = 40
    }

    public enum TOOL_CONFIG
    {
        HEADER_PADDING = 15,
        ELEMENT_PADDING = 5,
        MAX_REWARD_ITEM_AMOUNT = 99
    }

    #region QUEST
    public enum QUEST_TYPE
    {
        Leveling = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3
    };

    public enum QUEST_NPC_TYPE
    {
        Quest_Manager = 0,
        Hideout_Quest_Manager = 1
    };

    public enum QUEST_OBJETIVES
    {
        None = 0,
        Labyrinth_Completed = 1,
        Labyrinth_Boss_Kill = 2,
        Labyrinth_Monster_Kill = 3,
        VOD_Completed = 4,
        VOD_Boss_Kill = 5,
        War_Wins = 6,
        War_Participation = 7,
        Mou_Participation = 8,
        Mou_Wins = 9,
        Taiyan_Monster_Kill = 10,
        Ruined_Monster_Kill = 11,
        Daily_Quest_Completed = 12,
        Hideout_Scavs_Sent = 13,
        Stats_Rerolled = 14,
        Options_Rerolled = 15,
        Successfull_Enchants_Over_87 = 16,
        Successfull_Enchants_Over_93 = 18,
        Enchants_Done = 19,
        Combines_Done = 20,
        Monster_Kill = 21,
        Premium_Monster_Kill = 22,
        PvP_Kills = 23,
        War_Rank = 24,
        Blood_Giant_Kills = 25,
        Y_Form_Capture = 27,
        Shield_Destroyed = 28,
        Spawn_Event_Monsters = 29
    };
    #endregion


    #region GAME
    public enum GAME_CURRENCY {
        None = 0,
        Silver = 1,
        Contribution_Points = 2,
        Contribution_Medals = 3,
        Explorers_Mark = 4,
        Shattered_Fragments = 5,
        Holy_Wood = 6
    }
    #endregion 
}