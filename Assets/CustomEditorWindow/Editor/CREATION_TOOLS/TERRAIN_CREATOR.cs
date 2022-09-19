using UnityEditor;
using UnityEngine;

namespace CREATION_TOOLS_CORE
{
    namespace TOOLS
    {
        public class TERRAIN_GENERATOR : EditorWindow
        {
            public static GameObject meshContainer;

            static Vector2 _ViewerScrollPos = new Vector2();
            EditorGUISplitView verticalSplitView = new EditorGUISplitView(EditorGUISplitView.Direction.Vertical);

            [MenuItem("UMMORPG Tools/Terrain/New...")]
            static void Init()
            {
                TERRAIN_GENERATOR window = (TERRAIN_GENERATOR)GetWindow(typeof(TERRAIN_GENERATOR));
                window.Show();
                window.titleContent.text = "TERRAIN GENERATION (PERLIN NOISE)";
                _ViewerScrollPos.x = window.position.x / 2;
                MESH_GENERATION.Init();
            }
       
            public void OnGUI()
            {
                GUILayout.BeginScrollView(_ViewerScrollPos);
                GUILayout.BeginVertical();
                DrawTool();
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
            }

            void DrawTool()
            {
                GUILayout.BeginVertical();

                GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
                GUILayout.Label("Create Terrain Mesh:", EditorStyles.boldLabel);
                GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);

                //Values here
                GUILayout.Space((int)TOOL_CONFIG.HEADER_PADDING);
                MESH_GENERATION.mTerrainData.seed = EditorGUILayout.IntField("Seed:", MESH_GENERATION.mTerrainData.seed);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                MESH_GENERATION.mTerrainData.MeshScale = EditorGUILayout.Slider("Mesh Scale:", MESH_GENERATION.mTerrainData.MeshScale, 0.0f, 500.0f);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                MESH_GENERATION.mTerrainData.sizeX = EditorGUILayout.IntField("SizeX:", MESH_GENERATION.mTerrainData.sizeX);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                MESH_GENERATION.mTerrainData.sizeZ = EditorGUILayout.IntField("SizeZ:", MESH_GENERATION.mTerrainData.sizeZ);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                MESH_GENERATION.mTerrainData.scale = EditorGUILayout.Slider("Scale:", MESH_GENERATION.mTerrainData.scale, 0.0f, 500.0f);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                MESH_GENERATION.mTerrainData.heightCurve = EditorGUILayout.CurveField("Curve:", MESH_GENERATION.mTerrainData.heightCurve);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                MESH_GENERATION.mTerrainData.octaves = EditorGUILayout.IntField("Octaves:", MESH_GENERATION.mTerrainData.octaves);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                MESH_GENERATION.mTerrainData.lacunarity = EditorGUILayout.Slider("Lacunarity:", MESH_GENERATION.mTerrainData.lacunarity, 0.0f, 100.0f);
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);

                GUILayout.FlexibleSpace();
                GUILayout.Space((int)TOOL_CONFIG.ELEMENT_PADDING);
                if (GUILayout.Button("Generate"))
                {
                    MESH_GENERATION.CreateMeshFromData(meshContainer);
                }
                GUILayout.EndVertical();
            }

        }
    }
}