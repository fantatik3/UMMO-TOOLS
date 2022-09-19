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
                public float MeshScale;
                public int sizeX;
                public int sizeZ;
                public float scale;
                public int octaves;
                public float lacunarity;
            }
            static TERRAIN_CONFIG mTerrainData = new TERRAIN_CONFIG();

            static GameObject meshContainer;

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

                if (GameObject.Find("--- Tool Generated Terrains ---") == null)
                {
                    meshContainer = new GameObject("--- Tool Generated Terrains ---");
                }
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

                //Values here
                GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
                mTerrainData.seed = EditorGUILayout.IntField("Seed:", mTerrainData.seed);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                mTerrainData.MeshScale = EditorGUILayout.Slider("Mesh Scale:", mTerrainData.MeshScale, 0.0f, 100.0f);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                mTerrainData.sizeX = EditorGUILayout.IntField("SizeX:", mTerrainData.sizeX);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                mTerrainData.sizeZ = EditorGUILayout.IntField("SizeZ:", mTerrainData.sizeZ);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                mTerrainData.scale = EditorGUILayout.Slider("Scale:", mTerrainData.scale, 0.0f, 100.0f);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                mTerrainData.octaves = EditorGUILayout.IntField("Octaves:", mTerrainData.octaves);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                mTerrainData.lacunarity = EditorGUILayout.Slider("Lacunarity:", mTerrainData.lacunarity, 0.0f, 100.0f);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                GUILayout.FlexibleSpace();
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
                if (GUILayout.Button("Generate"))
                {
                    CreateMeshFromParams();
                }
                GUILayout.EndVertical();
            }

            void CreateMeshFromParams() {

                //Locate/Create an empty object called Generated terrains or something to set as parent

                //Create mesh and grab height from perlin noise etc
                //https://docs.unity3d.com/ScriptReference/Mathf.PerlinNoise.html

                //Spawn the mesh in the parent
                Debug.Log("Mesh Creation Not Implemented...");
            }
        }
    }
}