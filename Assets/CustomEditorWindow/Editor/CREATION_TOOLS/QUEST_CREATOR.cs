using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System; //This allows the IComparable Interface

namespace CREATION_TOOLS
{
    public class QUEST_CREATOR : EditorWindow
    {
        #region GUILAYOUT_MEMBERS

        GUIStyle _GUIStyle;
       /* GUIContent _GUIContent;*/
        Vector2 scrollPos = new Vector2();
        #endregion
        static int _QuestCount = 0;

        static QUEST_DATA _NewQuestData = new QUEST_DATA();

        static List<QUEST_DATA> _QuestData;

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
            if (_QuestCount < 1)
            {
                GUILayout.Label("NO QUESTS FOUND, PLEASE CREATE SOME", EditorStyles.boldLabel);
            }            
        }

        void Awake()
        {
            Load();
            _NewQuestData.name = new byte[(int)QUEST_SYSTEM_CONFIG.MAX_QUEST_NAME_LENGTH];
            _NewQuestData.name = System.Text.Encoding.ASCII.GetBytes("New Quest");
            _NewQuestData.currencyReward = new sCURRENCY[(int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS];
            _NewQuestData.objetiveData = new sOBJETIVES[(int)QUEST_SYSTEM_CONFIG.MAX_OBJETIVE_NUM];
            _NewQuestData.lootData = new sLOOT[(int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM];
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

            _NewQuestData.name = System.Text.Encoding.ASCII.GetBytes(EditorGUILayout.TextField("Quest Name:", System.Text.Encoding.ASCII.GetString(_NewQuestData.name)));

            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _NewQuestData.type = (QUEST_TYPE)EditorGUILayout.EnumPopup("Type:", _NewQuestData.type);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _NewQuestData.level = (int)EditorGUILayout.Slider("Level:", _NewQuestData.level, 0.0f, (int)GAME_CONFIG.MAX_AVATAR_LEVEL + (int)GAME_CONFIG.MAX_AVATAR_GOD_LEVEL + (int)GAME_CONFIG.MAX_AVATAR_REBIRTH_LEVEL);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _NewQuestData.NPCType = (QUEST_NPC_TYPE)EditorGUILayout.EnumPopup("NPCType:", _NewQuestData.NPCType);

            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("EXP REWARDS:", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _NewQuestData.avatarExperience = EditorGUILayout.Slider("Experience:", _NewQuestData.avatarExperience, 0.0f, 100.0f);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _NewQuestData.petExperience = EditorGUILayout.Slider("Pet Exp:", _NewQuestData.petExperience, 0.0f, 100.0f);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            showCurrency = EditorGUILayout.Foldout(showCurrency, "CURRENCY REWARDS");
            if (showCurrency)
            {
                for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS; i++)
                {
                    GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
                    GUILayout.Label("Currency " + (i + 1) + ":", EditorStyles.boldLabel);
                    _NewQuestData.currencyReward[i].currency = (GAME_CURRENCY)EditorGUILayout.EnumPopup("Type " + (i + 1) + ":", _NewQuestData.currencyReward[i].currency);
                    _NewQuestData.currencyReward[i].amount = EditorGUILayout.IntField("Amount" + (i + 1) + ":", _NewQuestData.currencyReward[i].amount);

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
                    _NewQuestData.objetiveData[i].objetive = (QUEST_OBJETIVES)EditorGUILayout.EnumPopup("Type " + (i + 1) + ":", _NewQuestData.objetiveData[i].objetive);
                    _NewQuestData.objetiveData[i].amount = EditorGUILayout.IntField("Req. Amount:", _NewQuestData.objetiveData[i].amount);

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
                    _NewQuestData.lootData[i].itemID = EditorGUILayout.IntField("Item ID:", _NewQuestData.lootData[i].itemID);
                    _NewQuestData.lootData[i].amount = EditorGUILayout.IntField("Amount:", _NewQuestData.lootData[i].amount);
                    _NewQuestData.lootData[i].chance = EditorGUILayout.Slider("Chance:", _NewQuestData.lootData[i].chance, 0.0f, 100.0f);

                    GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
                }
            }

            _NewQuestData.index = _QuestCount;
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Insert Quest"))
            {
                InsertQuest();
            }
            GUILayout.EndScrollView();
        }

        void InsertQuest() {
            Debug.LogError("Insert Quest Not Implemented!");
        }
        // Start is called before the first frame update
        public static void Save(int tQuestCount, QUEST_DATA[] tQuest)
        {
            string filename = Application.dataPath + "\\OUTPUT\\QUEST_OUTPUT.hex";
            if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            using FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Write);
            using BinaryWriter bW = new(fileStream);

            bW.Write(tQuestCount);

            for (int i = 0; i < tQuestCount; i++)
            {
                bW.Write(Serialization.ToByteArray((QUEST_DATA)tQuest[i]));
            }
            bW.Close();
            fileStream.Close();
        }

        public static void Load()
        {
            string filename = Application.dataPath + "\\OUTPUT\\QUEST_OUTPUT.hex";
            if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            using FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read);
            using BinaryReader bR = new(fileStream);

            _QuestCount = bR.ReadInt32();

            int structSize = Serialization.SizeOf<QUEST_DATA>();
            for (int i = 0; i < _QuestCount; i++)
            {
                QUEST_DATA tQuestData = new QUEST_DATA();
                tQuestData = (QUEST_DATA)Serialization.ToStructure<QUEST_DATA>(bR.ReadBytes(structSize));
                _QuestData.Add(tQuestData);      
            }
            bR.Close();
            fileStream.Close();
        }

    }
}