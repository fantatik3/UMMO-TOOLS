using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System;

namespace CREATION_TOOLS
{
    public class TERRAIN_CREATOR : EditorWindow
    {

        [MenuItem("UMMORPG Tools/Terrain/Generate New...")]
        static void Init()
        {

        }
        public void OnGUI()
        {
            DrawTool();
        }
        void DrawTool() {
        
        }
    }
}