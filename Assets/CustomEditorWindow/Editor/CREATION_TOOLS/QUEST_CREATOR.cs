using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System; //This allows the IComparable Interface

namespace CREATION_TOOLS
{
    public class QUEST_CREATOR : EditorWindow
    {

        struct QUEST_FOLDING
        {
            public bool QuestUnfolded;
            public bool ShowCurrency;
            public bool ShowObjetives;
            public bool ShowLootTable;
        };

        #region GUILAYOUT
        GUIStyle _GUIStyle;
        Vector2 scrollPos = new Vector2();
        #endregion

        #region SPLIT_VIEW
        EditorGUISplitView horizontalSplitView = new EditorGUISplitView(EditorGUISplitView.Direction.Horizontal);
        EditorGUISplitView verticalSplitView = new EditorGUISplitView(EditorGUISplitView.Direction.Vertical);
        #endregion

        const string QUEST_FILE_NAME = "\\QUEST_OUTPUT.hex";
        const string OUTPUT_FOLDER = "\\OUTPUT";
        static string _OUTPUT_PATH = OUTPUT_FOLDER + QUEST_FILE_NAME;

        static QUEST_DATA _NewQuestData = new QUEST_DATA();

        static List<QUEST_DATA> _QuestData = new List<QUEST_DATA>();

        QUEST_FOLDING _EditorFold = new QUEST_FOLDING();
        static List<QUEST_FOLDING> _ViewerFold = new List<QUEST_FOLDING>();

        [MenuItem("UMMORPG Tools/Quest/New...")]

        static void Init()
        {
            _QuestData.Clear();
            QUEST_CREATOR window = (QUEST_CREATOR)GetWindow(typeof(QUEST_CREATOR));
            window.Show();
            window.title = "QUEST CREATION TOOL";

            Load();
            InitNewQuest();
        }

        static void InitNewQuest()
        {
            _NewQuestData.name = new byte[(int)QUEST_SYSTEM_CONFIG.MAX_QUEST_NAME_LENGTH];
            _NewQuestData.name = Encoding.ASCII.GetBytes("New Quest " + (_QuestData.Count + 1) + char.MinValue);
            _NewQuestData.currencyReward = new sCURRENCY[(int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS];
            _NewQuestData.objetiveData = new sOBJETIVES[(int)QUEST_SYSTEM_CONFIG.MAX_OBJETIVE_NUM];
            _NewQuestData.lootData = new sLOOT[(int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM];
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
            QUEST_FOLDING tFold = new QUEST_FOLDING();
            QUEST_DATA tQuestData = new QUEST_DATA();
            if (_QuestData.Count < 1)
            {
                //Centered
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                GUILayout.Label("NO QUESTS FOUND, PLEASE CREATE SOME", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                return;
            }
            GUILayout.BeginVertical();

            for (int i = 0; i < _QuestData.Count; i++)
            {
                tFold = _ViewerFold[i];
                tQuestData = _QuestData[i];

                tFold.QuestUnfolded = EditorGUILayout.Foldout(tFold.QuestUnfolded, "Quest: " + Encoding.ASCII.GetString(_QuestData[i].name));
                if (tFold.QuestUnfolded)
                {
                    //Quest Struct
                    GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
                    GUILayout.Label("Fill the quest data.", EditorStyles.boldLabel);
                    GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

                    tQuestData.name = Encoding.ASCII.GetBytes(EditorGUILayout.TextField("Quest Name:", Encoding.ASCII.GetString(tQuestData.name) + "\0"));

                    GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                    tQuestData.type = (QUEST_TYPE)EditorGUILayout.EnumPopup("Type:", tQuestData.type);
                    GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                    tQuestData.level = (int)EditorGUILayout.Slider("Level:", tQuestData.level, 0.0f, (int)GAME_CONFIG.MAX_AVATAR_LEVEL + (int)GAME_CONFIG.MAX_AVATAR_GOD_LEVEL + (int)GAME_CONFIG.MAX_AVATAR_REBIRTH_LEVEL);
                    GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                    tQuestData.NPCType = (QUEST_NPC_TYPE)EditorGUILayout.EnumPopup("NPCType:", tQuestData.NPCType);

                    GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
                    GUILayout.Label("EXP REWARDS:", EditorStyles.boldLabel);
                    GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                    tQuestData.avatarExperience = EditorGUILayout.Slider("Experience:", tQuestData.avatarExperience, 0.0f, 100.0f);
                    GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                    tQuestData.petExperience = EditorGUILayout.Slider("Pet Exp:", tQuestData.petExperience, 0.0f, 100.0f);
                    GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

                    tFold.ShowCurrency = EditorGUILayout.Foldout(tFold.ShowCurrency, "CURRENCY REWARDS");
                    if (tFold.ShowCurrency)
                    {
                        for (int jj = 0; jj < (int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS; jj++)
                        {
                            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
                            GUILayout.Label("Currency " + (jj + 1) + ":", EditorStyles.boldLabel);
                            tQuestData.currencyReward[jj].currency = (GAME_CURRENCY)EditorGUILayout.EnumPopup("Type " + (jj + 1) + ":", tQuestData.currencyReward[jj].currency);
                            tQuestData.currencyReward[jj].amount = EditorGUILayout.IntField("Amount" + (jj + 1) + ":", tQuestData.currencyReward[jj].amount);

                        }
                    }

                    GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

                    tFold.ShowObjetives = EditorGUILayout.Foldout(tFold.ShowObjetives, "OBJETIVES");
                    if (tFold.ShowObjetives)
                    {
                        for (int jj = 0; jj < (int)QUEST_SYSTEM_CONFIG.MAX_OBJETIVE_NUM; jj++)
                        {
                            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
                            GUILayout.Label("Objetive " + (jj + 1) + ":", EditorStyles.boldLabel);
                            tQuestData.objetiveData[jj].objetive = (QUEST_OBJETIVES)EditorGUILayout.EnumPopup("Type " + (jj + 1) + ":", tQuestData.objetiveData[jj].objetive);
                            tQuestData.objetiveData[jj].amount = EditorGUILayout.IntField("Req. Amount:", tQuestData.objetiveData[jj].amount);

                        }
                    }

                    GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

                    tFold.ShowLootTable = EditorGUILayout.Foldout(tFold.ShowLootTable, "LOOT TABLE");
                    if (tFold.ShowLootTable)
                    {
                        for (int jj = 0; jj < (int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM; jj++)
                        {
                            GUILayout.Label("Item Slot_" + (jj + 1) + ":", EditorStyles.boldLabel);
                            GUILayout.Space(1);
                            tQuestData.lootData[jj].itemID = EditorGUILayout.IntField("Item ID:", tQuestData.lootData[jj].itemID);
                            tQuestData.lootData[jj].amount = EditorGUILayout.IntField("Amount:", tQuestData.lootData[jj].amount);
                            tQuestData.lootData[jj].chance = EditorGUILayout.Slider("Chance:", tQuestData.lootData[jj].chance, 0.0f, 100.0f);

                            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
                        }
                    }
                 
                    GUILayout.Label("=================================================================================================================", EditorStyles.boldLabel);
                    GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

                    tQuestData.index = _QuestData.Count;

                }

                _ViewerFold[i] = tFold;
                _QuestData[i] = tQuestData;
            }

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save Work"))
            {
                Save();
            }
            GUILayout.EndVertical();
        }

        void DrawQuestEditor()
        {

            scrollPos = GUILayout.BeginScrollView(scrollPos);

            //Set Window Style
            _GUIStyle = new GUIStyle(GUI.skin.label);
            _GUIStyle.padding = new RectOffset(11, 22, 33, 44);

            //Quest Struct
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("Fill the quest data.", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            _NewQuestData.name = Encoding.ASCII.GetBytes(EditorGUILayout.TextField("Quest Name:", Encoding.ASCII.GetString(_NewQuestData.name) + "\0"));

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

            _EditorFold.ShowCurrency = EditorGUILayout.Foldout(_EditorFold.ShowCurrency, "CURRENCY REWARDS");
            if (_EditorFold.ShowCurrency)
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

            _EditorFold.ShowObjetives = EditorGUILayout.Foldout(_EditorFold.ShowObjetives, "OBJETIVES");
            if (_EditorFold.ShowObjetives)
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

            _EditorFold.ShowLootTable = EditorGUILayout.Foldout(_EditorFold.ShowLootTable, "LOOT TABLE");
            if (_EditorFold.ShowLootTable)
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

            _NewQuestData.index = _QuestData.Count;
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Insert Quest"))
            {
                InsertQuest();
                InitNewQuest();
            }
            GUILayout.EndScrollView();
        }

        void InsertQuest()
        {
            _QuestData.Add(_NewQuestData);
            _ViewerFold.Add(new QUEST_FOLDING());
        }

        // Start is called before the first frame update
        public static void Save()
        {
            if (!File.Exists(Application.dataPath + _OUTPUT_PATH))
            {
                File.Create(Application.dataPath + _OUTPUT_PATH);
            }

            using FileStream fileStream = File.OpenWrite(Application.dataPath + _OUTPUT_PATH);
            using BinaryWriter bW = new(fileStream);

            bW.Write(_QuestData.Count);

            for (int i = 0; i < _QuestData.Count; i++)
            {
                bW.Write(Serialization.ToByteArray(_QuestData[i]));
            }
            bW.Close();
            fileStream.Close();
        }

        public static void Load()
        {
            int tElemetsToRead = 0;
            if (CheckForCreateFile())
            {
                return;
            }
            using (var stream = File.Open(Application.dataPath + _OUTPUT_PATH, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    tElemetsToRead = reader.ReadInt32();
                    for (int i = 0; i < tElemetsToRead; i++)
                    {
                        _ViewerFold.Add(new QUEST_FOLDING());
                        _QuestData.Add((QUEST_DATA)Serialization.ToStructure<QUEST_DATA>(reader.ReadBytes(Serialization.SizeOf<QUEST_DATA>())));
                    }
                }
            }
        }

        public static bool CheckForCreateFile()
        {
            if (!File.Exists(Application.dataPath + _OUTPUT_PATH))
            {
                using (var stream = File.Open(Application.dataPath + _OUTPUT_PATH, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        writer.Write(0);
                    }
                }
                Debug.Log("Quest file created in: " + Application.dataPath + _OUTPUT_PATH);
                return true;
            }
            return false;
        }

    }
}