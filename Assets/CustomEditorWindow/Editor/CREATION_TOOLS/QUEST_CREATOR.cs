using UnityEditor;
using UnityEngine;

namespace CREATION_TOOLS
{
    public class QUEST_CREATOR : EditorWindow
    {
        #region GUILAYOUT_MEMBERS

        GUIStyle _GUIStyle;
       /* GUIContent _GUIContent;*/
        Vector2 scrollPos = new Vector2();
        #endregion

        static QUEST_DATA _QuestData = new QUEST_DATA();
        static int _QuestCount = 0;

        EditorGUISplitView horizontalSplitView = new EditorGUISplitView(EditorGUISplitView.Direction.Horizontal);
        EditorGUISplitView verticalSplitView = new EditorGUISplitView(EditorGUISplitView.Direction.Vertical);

        bool showCurrency = false;
        bool showObjetives = false;
        bool showLootTable = false;

        [MenuItem("MMORPG Tools/Quest/New...")]
        static void Init()
        {
            QUEST_CREATOR window = (QUEST_CREATOR)GetWindow(typeof(QUEST_CREATOR));
            window.Show();
            window.title = "QUEST CREATION TOOL";
        }

        public void OnGUI()
        {
            horizontalSplitView.BeginSplitView();
            DrawQuestViewer();
            horizontalSplitView.Split();
            DrawQuestEditor();
            horizontalSplitView.EndSplitView();
            Repaint();
        }


        void DrawQuestViewer()
        {
            GUILayout.Label("QUEST LIST VIEWER GOES HERE", EditorStyles.boldLabel);
        }

        void Awake()
        {
            _QuestData.name = new byte[(int)QUEST_SYSTEM_CONFIG.MAX_QUEST_NAME_LENGTH];
            _QuestData.name = System.Text.Encoding.ASCII.GetBytes("New Quest");
            _QuestData.currencyReward = new sCURRENCY[(int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS];
            _QuestData.objetiveData = new sOBJETIVES[(int)QUEST_SYSTEM_CONFIG.MAX_OBJETIVE_NUM];
            _QuestData.lootData = new sLOOT[(int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM];
        }

        void DrawQuestEditor() {
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            //Set Window Style
            _GUIStyle = new GUIStyle(GUI.skin.label);
            _GUIStyle.padding = new RectOffset(11, 22, 33, 44);

            //Quest Struct
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("Fill the quest data.", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            _QuestData.name = System.Text.Encoding.ASCII.GetBytes(EditorGUILayout.TextField("Quest Name:", System.Text.Encoding.ASCII.GetString(_QuestData.name)));

            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _QuestData.type = (QUEST_TYPE)EditorGUILayout.EnumPopup("Type:", _QuestData.type);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _QuestData.level = (int)EditorGUILayout.Slider("Level:", _QuestData.level, 0.0f, (int)GAME_CONFIG.MAX_AVATAR_LEVEL + (int)GAME_CONFIG.MAX_AVATAR_GOD_LEVEL + (int)GAME_CONFIG.MAX_AVATAR_REBIRTH_LEVEL);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _QuestData.NPCType = (QUEST_NPC_TYPE)EditorGUILayout.EnumPopup("NPCType:", _QuestData.NPCType);

            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("EXP REWARDS:", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _QuestData.avatarExperience = EditorGUILayout.Slider("Experience:", _QuestData.avatarExperience, 0.0f, 100.0f);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _QuestData.petExperience = EditorGUILayout.Slider("Pet Exp:", _QuestData.petExperience, 0.0f, 100.0f);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            showCurrency = EditorGUILayout.Foldout(showCurrency, "CURRENCY REWARDS");
            if (showCurrency)
            {
                for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS; i++)
                {
                    GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
                    GUILayout.Label("Currency " + (i + 1) + ":", EditorStyles.boldLabel);
                    _QuestData.currencyReward[i].currency = (GAME_CURRENCY)EditorGUILayout.EnumPopup("Type " + (i + 1) + ":", _QuestData.currencyReward[i].currency);
                    _QuestData.currencyReward[i].amount = EditorGUILayout.IntField("Amount" + (i + 1) + ":", _QuestData.currencyReward[i].amount);

                }
            }

            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            showObjetives = EditorGUILayout.Foldout(showObjetives, "OBJETIVES");
            if (showObjetives)
            {
                for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_OBJETIVE_NUM; i++)
                {
                    GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
                    GUILayout.Label("Objetive " + (i + 1) + ":", EditorStyles.boldLabel);
                    _QuestData.objetiveData[i].objetive = (QUEST_OBJETIVES)EditorGUILayout.EnumPopup("Type " + (i + 1) + ":", _QuestData.objetiveData[i].objetive);
                    _QuestData.objetiveData[i].amount = EditorGUILayout.IntField("Req. Amount:", _QuestData.objetiveData[i].amount);

                }
            }

            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            showLootTable = EditorGUILayout.Foldout(showLootTable, "LOOT TABLE");
            if (showLootTable)
            {
                for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM; i++)
                {
                    GUILayout.Label("Item Slot_" + (i + 1) + ":", EditorStyles.boldLabel);
                    GUILayout.Space(1);
                    _QuestData.lootData[i].itemID = EditorGUILayout.IntField("Item ID:", _QuestData.lootData[i].itemID);
                    _QuestData.lootData[i].amount = EditorGUILayout.IntField("Amount:", _QuestData.lootData[i].amount);
                    _QuestData.lootData[i].chance = EditorGUILayout.Slider("Chance:", _QuestData.lootData[i].chance, 0.0f, 100.0f);

                    GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
                }
            }

            _QuestData.index = _QuestCount;
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Insert Quest"))
            {
                SaveToFile();
            }
            GUILayout.EndScrollView();
        }

        // Start is called before the first frame update
        void SaveToFile() {

            if (_QuestData.index < 0) {
                Debug.LogError("[ERROR] Index cant be less than 0.");
                return;
            }
            if (_QuestData.name.Equals(""))
            {
                Debug.LogError("[ERROR] Quest name cant be empty.");
                return;
            }
            if (_QuestData.name.Length > (int)QUEST_SYSTEM_CONFIG.MAX_QUEST_NAME_LENGTH)
            {
                Debug.LogError("[ERROR] Quest name is too long.");
                return;
            }

            if (_QuestData.level < 0 || _QuestData.level > (int)GAME_CONFIG.MAX_AVATAR_LEVEL + (int)GAME_CONFIG.MAX_AVATAR_GOD_LEVEL + (int)GAME_CONFIG.MAX_AVATAR_REBIRTH_LEVEL)
            {
                Debug.LogError("[ERROR] Quest name cant be empty.");
                return;
            }

            for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS; i++) {
                if (_QuestData.currencyReward[i].amount < 0)
                {
                    Debug.LogError("[ERROR] Currency Reward Amount cant be less than 0.");
                    return;
                }
            }
            for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_OBJETIVE_NUM; i++)
            {
                if (_QuestData.objetiveData[i].amount < 0)
                {
                    Debug.LogError("[ERROR] Objetive Amount cant be less than 0.");
                    return;
                }
            }

            for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM; i++)
            {
                if (_QuestData.lootData[i].itemID < 0 || _QuestData.lootData[i].itemID > 99999)
                {
                    Debug.LogError("[ERROR] Wrong Item ID in Loot Slot: " + (i + 1));
                    return;
                }
                if (_QuestData.lootData[i].amount < 0 || _QuestData.lootData[i].amount > (int)TOOL_CONFIG.MAX_REWARD_ITEM_AMOUNT)
                {
                    Debug.LogError("[ERROR] Wrong Item Amount in Loot Slot: " + (i + 1));
                    return;
                }
            }
            _QuestCount++;
            ToolSaving.SaveQuest(_QuestCount, _QuestData);
            Debug.LogError("ALL WENT OK!");
        }

      
    }
}