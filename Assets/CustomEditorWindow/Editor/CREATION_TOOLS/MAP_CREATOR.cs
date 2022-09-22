using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System;

namespace CREATION_TOOLS_CORE
{
    namespace TOOLS
    {
        public class MAP_CREATOR : EditorWindow
        {

            struct MAP_DATA
            {
                public bool QuestUnfolded;
                public bool ShowCurrency;
                public bool ShowObjetives;
                public bool ShowLootTable;
            };

            #region GUILAYOUT
            static Vector2 _ViewerScrollPos = new Vector2();
            static Vector2 _CreatorScrollPos = new Vector2();
            #endregion

            #region SPLIT_VIEW
            EditorGUISplitView horizontalSplitView = new EditorGUISplitView(EditorGUISplitView.Direction.Horizontal);
            EditorGUISplitView verticalSplitView = new EditorGUISplitView(EditorGUISplitView.Direction.Vertical);
            #endregion

            const string QUEST_FILE_NAME = "/MAP_OUTPUT.csv";
            const string OUTPUT_FOLDER = "/OUTPUT";
            static string _OUTPUT_PATH = OUTPUT_FOLDER + QUEST_FILE_NAME;

            [MenuItem("UMMORPG Tools/Map/Create-Edit Data...")]

            static void Init()
            {
                MAP_CREATOR window = (MAP_CREATOR)GetWindow(typeof(MAP_CREATOR));
                window.Show();
                window.titleContent.text = "MAP DATA TOOL";
                _ViewerScrollPos.x = window.position.x / 2;
            }

            public void OnGUI()
            {
                horizontalSplitView.BeginSplitView();
               
                horizontalSplitView.Split();
                horizontalSplitView.EndSplitView();
                Repaint();
            }

        }
    }
}