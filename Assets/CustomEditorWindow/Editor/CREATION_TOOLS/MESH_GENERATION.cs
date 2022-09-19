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
        public static class MESH_GENERATION
        {
            public static void CreateMeshFromData(TERRAIN_CONFIG terrainData)
            {

                //Locate/Create an empty object called Generated terrains or something to set as parent

                //Create mesh and grab height from perlin noise etc
                //https://docs.unity3d.com/ScriptReference/Mathf.PerlinNoise.html

                //Spawn the mesh in the parent
                Debug.Log("Mesh Creation Not Implemented...");
            }
        }
    }
}