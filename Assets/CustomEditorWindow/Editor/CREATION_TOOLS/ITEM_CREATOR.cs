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
        public class ITEM_CREATOR : EditorWindow
        {

            #region GUILAYOUT
            static Vector2 _ViewerScrollPos = new Vector2();
            static Vector2 _CreatorScrollPos = new Vector2();
            #endregion

            #region SPLIT_VIEW
            EditorGUISplitView horizontalSplitView = new EditorGUISplitView(EditorGUISplitView.Direction.Horizontal);
            EditorGUISplitView verticalSplitView = new EditorGUISplitView(EditorGUISplitView.Direction.Vertical);
            #endregion

            const string QUEST_FILE_NAME = "/ITEM_OUTPUT.csv";
            const string OUTPUT_FOLDER = "/OUTPUT";
            static string _OUTPUT_PATH = OUTPUT_FOLDER + QUEST_FILE_NAME;

            [MenuItem("UMMORPG Tools/Items/Create-Edit...")]

            static void Init()
            {
                ITEM_CREATOR window = (ITEM_CREATOR)GetWindow(typeof(ITEM_CREATOR));
                window.Show();
                window.titleContent.text = "ITEM DATA TOOL";
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