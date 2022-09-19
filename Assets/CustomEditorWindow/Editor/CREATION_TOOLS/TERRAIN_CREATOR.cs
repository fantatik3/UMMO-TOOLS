using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System;

namespace CREATION_TOOLS_CORE
{
    namespace TOOLS
    {
        public class TERRAIN_CREATOR : EditorWindow
        {
            //members for tool
            public struct TERRAIN_CONFIG
            {
                public int seed;
                public int MeshScale;
                public int sizeX;
                public int sizeZ;
                public float scale;
                public int octaves;
                public float lacunarity;
            }
            static TERRAIN_CONFIG mTerrainData = new TERRAIN_CONFIG();

            EditorGUISplitView verticalSplitView = new EditorGUISplitView(EditorGUISplitView.Direction.Vertical);

            [MenuItem("UMMORPG Tools/Terrain/Generate New...")]
            static void Init()
            {

                TERRAIN_CREATOR window = (TERRAIN_CREATOR)GetWindow(typeof(TERRAIN_CREATOR));
                window.Show();
                window.titleContent.text = "MAP GENERATION TOOL";

                mTerrainData.seed = 0;
                mTerrainData.MeshScale = 0;
                mTerrainData.sizeX = 0;
                mTerrainData.sizeZ = 0;
                mTerrainData.scale = 0;
                mTerrainData.octaves = 0;
                mTerrainData.lacunarity = 0;
            }
            public void OnGUI()
            {
                verticalSplitView.BeginSplitView();
                DrawTool();
                verticalSplitView.Split();
                verticalSplitView.EndSplitView();
                Repaint();

            }
            void DrawTool()
            {
                GUILayout.BeginVertical();

                GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
                GUILayout.Label("Create Terrain Mesh:", EditorStyles.boldLabel);
                GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
        
                GUILayout.FlexibleSpace();
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
                if (GUILayout.Button("Create"))
                {
                    CreateMeshFromParams();
                }
                GUILayout.EndVertical();
            }

            void CreateMeshFromParams() {
                Debug.Log("Mesh Creation Not Implemented...");
            }
        }
    }
}