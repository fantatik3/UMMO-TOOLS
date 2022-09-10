using UnityEditor;
using UnityEngine;
using System.Collections;

namespace CREATION_TOOLS
{
    public class QUEST_CREATOR : EditorWindow
    {
        static EditorWindow _ThisWindow;

        #region GUILAYOUT_MEMBERS

        GUIStyle _GUIStyle;
        GUIContent _GUIContent;
        Vector2 scrollPos = new Vector2();
        #endregion

        #region MEMBERS

        int _Index = 0;
        string _Name = "New Quest";
        QUEST_TYPE _Type = new QUEST_TYPE();
        int _Level = 0;
        QUEST_NPC_TYPE _NPCType = new QUEST_NPC_TYPE();
        float _AvatarExperience = 0;
        float _PetExperience = 0;

        sCURRENCY[] _CurrencyReward = new sCURRENCY[(int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS];
        sOBJETIVES[] _ObjetiveData = new sOBJETIVES[(int)QUEST_SYSTEM_CONFIG.MAX_OBJETIVE_NUM];
        sLOOT[] _LootData = new sLOOT[(int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM];

        #endregion


        [MenuItem("MMORPG Tools/Quest/New...")]
        public static void ShowWindow()
        {
            _ThisWindow = GetWindow(typeof(QUEST_CREATOR));
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

            _Index = EditorGUILayout.IntField("Index:", _Index);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
            _Name = EditorGUILayout.TextField("Quest Name:", _Name);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _Type = (QUEST_TYPE)EditorGUILayout.EnumPopup("Type:", _Type);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _Level = EditorGUILayout.IntField("Level:", _Level);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _NPCType = (QUEST_NPC_TYPE)EditorGUILayout.EnumPopup("NPCType:", _NPCType);

            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("EXP REWARDS:", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            _AvatarExperience = EditorGUILayout.Slider("Experience:", _AvatarExperience, 0.0f, 100.0f);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            _PetExperience = EditorGUILayout.Slider("Pet Exp:", _PetExperience, 0.0f, 100.0f);
            GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("CURRENCY REWARDS:", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS; i++)
            {
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
                GUILayout.Label("Currency " + (i + 1) + ":", EditorStyles.boldLabel);
                _CurrencyReward[i].currency = (GAME_CURRENCY)EditorGUILayout.EnumPopup("Type " + (i + 1) + ":", _CurrencyReward[i].currency);
                _CurrencyReward[i].amount = EditorGUILayout.IntField("Amount:" + (i + 1), _CurrencyReward[i].amount);
    
            }

            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("OBJETIVES:", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_OBJETIVE_NUM; i++)
            {
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
                GUILayout.Label("Objetive " + (i + 1) + ":", EditorStyles.boldLabel);
                _ObjetiveData[i].objetive = (QUEST_OBJETIVES)EditorGUILayout.EnumPopup("Type " + (i + 1) + ":", _ObjetiveData[i].objetive);
                _ObjetiveData[i].amount = EditorGUILayout.IntField("Req. Amount:", _ObjetiveData[i].amount);

            }

            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            GUILayout.Label("LOOT TABLE:", EditorStyles.boldLabel);
            GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

            for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM; i++)
            {
                GUILayout.Label("Item Slot_" + (i + 1) + ":", EditorStyles.boldLabel);
                GUILayout.Space(1);
                _LootData[i].itemID = EditorGUILayout.IntField("Item ID:", _LootData[i].itemID);
                _LootData[i].amount = EditorGUILayout.IntField("Amount:", _LootData[i].amount);
                _LootData[i].chance = EditorGUILayout.Slider("Chance:", _LootData[i].chance, 0.0f, 100.0f);

                GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
            }

            GUILayout.EndScrollView();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("SAVE")) {
                SaveFile();
            }

        }

        // Start is called before the first frame update
        void SaveFile() {

            if (_Index < 0) {
                Debug.LogError("[ERROR] Index cant be less than 0.");
                return;
            }
            if (_Name.Equals(""))
            {
                Debug.LogError("[ERROR] Quest name cant be empty.");
                return;
            }

            if (_Level < 0 || _Level > (int)GAME_CONFIG.MAX_AVATAR_LEVEL + (int)GAME_CONFIG.MAX_AVATAR_GOD_LEVEL + (int)GAME_CONFIG.MAX_AVATAR_REBIRTH_LEVEL)
            {
                Debug.LogError("[ERROR] Quest name cant be empty.");
                return;
            }

            for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_CURRENCY_REWARDS; i++) {
                if (_CurrencyReward[i].amount < 0)
                {
                    Debug.LogError("[ERROR] Currency Reward Amount cant be less than 0.");
                    return;
                }
            }
            for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_OBJETIVE_NUM; i++)
            {
                if (_ObjetiveData[i].amount < 0)
                {
                    Debug.LogError("[ERROR] Objetive Amount cant be less than 0.");
                    return;
                }
            }

            for (int i = 0; i < (int)QUEST_SYSTEM_CONFIG.MAX_LOOT_SLOT_NUM; i++)
            {
                if (_LootData[i].itemID < 0 || _LootData[i].itemID > 99999)
                {
                    Debug.LogError("[ERROR] Wrong Item ID in Loot Slot: " + (i + 1));
                    return;
                }
                if (_LootData[i].amount < 0 || _LootData[i].amount > (int)TOOL_CONFIG.MAX_REWARD_ITEM_AMOUNT)
                {
                    Debug.LogError("[ERROR] Wrong Item Amount in Loot Slot: " + (i + 1));
                    return;
                }
            }



            Debug.LogError("ALL WENT OK!");
        }

    }
}