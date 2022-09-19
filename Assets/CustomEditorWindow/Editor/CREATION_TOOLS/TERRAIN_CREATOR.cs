using UnityEditor;
using UnityEngine;

namespace CREATION_TOOLS_CORE
{
    namespace TOOLS
    {
        public class TERRAIN_CREATOR : EditorWindow
        {
            public static TERRAIN_CONFIG mTerrainData = new TERRAIN_CONFIG();

            public static GameObject meshContainer;

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
                    MESH_GENERATION.CreateMeshFromData(mTerrainData);
                }
                GUILayout.EndVertical();
            }
        }
    }
}