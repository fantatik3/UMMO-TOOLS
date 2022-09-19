using UnityEngine;

namespace CREATION_TOOLS_CORE
{
    namespace TOOLS
    {
        public static class MESH_GENERATION
        {
            public static TERRAIN_CONFIG mTerrainData;

            public static void Init() {
                mTerrainData = new TERRAIN_CONFIG();
                mTerrainData.seed = 0;
                mTerrainData.MeshScale = 100;
                mTerrainData.sizeX = 50;
                mTerrainData.sizeZ = 50;
                mTerrainData.scale = 50;
                mTerrainData.octaves = 5;
                mTerrainData.lacunarity = 2;
                mTerrainData.heightCurve = AnimationCurve.Linear(0, 0, 10, 10);
            }
            public static void CreateMeshFromData(GameObject parent = null)
            {

                //Create mesh and grab height from perlin noise etc
                //https://docs.unity3d.com/ScriptReference/Mathf.PerlinNoise.html

                //Spawn the mesh in the parent
                Debug.Log("Mesh Creation Not Implemented...");
            }
        }
    }
}