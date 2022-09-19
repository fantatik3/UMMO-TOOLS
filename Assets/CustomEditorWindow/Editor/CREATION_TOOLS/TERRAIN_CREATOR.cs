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

            [MenuItem("UMMORPG Tools/Terrain/Generate New...")]
            static void Init()
            {
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
                DrawTool();
            }
            void DrawTool()
            {

            }

        }
    }
}