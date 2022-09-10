using UnityEditor;
using UnityEngine;

public enum GAME_CURRENCY { 
    SILVER = 0,
    CONTRIBUTION_POINTS = 1,
    CONTRIBUTION_MEDAL = 2,
    EXPLORERS_MARK = 3,
    SHATTERED_FRAGMENTS = 4,
    HOLY_WOOD = 5
}

public enum QUEST_SYSTEM_CONFIG { 
    MAX_OBJETIVE_NUM = 5,
    MAX_LOOT_SLOT_NUM = 10,
    MAX_CURRENCY_REWARDS = 4
}

public enum TOOL_CONFIG
{
    HEADER_PADDING = 15,
    ELEMENT_PADDING = 5
}

namespace CREATION_TOOLS
{
    public class QUEST_CREATOR : EditorWindow
    {
        public struct sOBJETIVES
        {
            public int type;
            public int amount;
        }

        public struct sLOOT
        {
            public int itemID;
            public int amount;
            public int chance;
        }

        int _Index = 0;
        string _Name = "";
        int _Type = 0;
        int _Level = 0;
        int _NPCType = 0;
        float _AvatarExperience = 0;
        float _PetExperience = 0;
        int[] _CurrencyReward = new int[(int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS];

        sOBJETIVES[] _ObjetiveData = new sOBJETIVES[(int)QUEST_SYSTEM_CONFIG.MAX_OBJETIVE_NUM];
        sLOOT[] _LootData = new sLOOT[(int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM];

        GUIStyle _GUIStyle;
        GUIContent _GUIContent;
        static EditorWindow _ThisWindow;

        Vector2 scrollPos = new Vector2();
        [MenuItem("MMORPG Tools/Quest/New...")]
        public static void ShowWindow()
        {
            _ThisWindow = GetWindow(typeof(QUEST_CREATOR));
        }

        private void Awake()
        {

        }

        private void OnGUI()
        {
            //Set Window name
            _ThisWindow.title = "QUEST CREATION TOOL";
     
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            //Set Window Style
            _GUIStyle = new GUIStyle(GUI.skin.label);
            _GUIStyle.padding = new RectOffset(11, 22, 33, 44);

            //Quest Struct
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("Fill the quest data.", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            _Index = EditorGUILayout.IntField("Index", _Index);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
            _Name = EditorGUILayout.TextField("Quest Name", _Name);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _Type = EditorGUILayout.IntField("Type", _Type);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _Level = EditorGUILayout.IntField("Level", _Level);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _NPCType = EditorGUILayout.IntField("NPCType", _NPCType);

            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("EXP REWARDS:", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            _AvatarExperience = EditorGUILayout.Slider("Experience", _AvatarExperience, 0.0f, 100.0f);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _PetExperience = EditorGUILayout.Slider("Pet Exp", _PetExperience, 0.0f, 100.0f);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _PetExperience = EditorGUILayout.Slider("Pet Exp", _PetExperience, 0.0f, 100.0f);

            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("CURRENCY REWARDS:", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS; i++)
            {
                _CurrencyReward[i] = EditorGUILayout.IntField("Currency_" + (i + 1), _CurrencyReward[i]);
            }

            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("LOOT TABLE:", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM; i++)
            {
                GUILayout.Label("Item Slot_" + (i + 1), EditorStyles.boldLabel);
                GUILayout.Space(1);
                _LootData[i].itemID = EditorGUILayout.IntField("Item ID", _LootData[i].itemID);
                _LootData[i].amount = EditorGUILayout.IntField("Amount", _LootData[i].amount);
                _LootData[i].chance = EditorGUILayout.IntField("Chance", _LootData[i].chance);
                GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            }
            GUILayout.EndScrollView();
            GUILayout.FlexibleSpace();
        }

        // Start is called before the first frame update

    }
}