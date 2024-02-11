using System;
using System.Collections.Generic;

// script from [spatialsys/spatial-unity-sdk: This package allows creators to build content in Unity and publish it to Spatial](https://github.com/spatialsys/spatial-unity-sdk)

namespace Meangpu
{
    public class PerformanceResponse
    {
        private const int m = 1000000;
        private const int k = 1000;

        public static readonly int MAX_SUGGESTED_VERTS = 500 * k;
        public static readonly int MAX_SUGGESTED_UNIQUE_MATERIALS = 75;
        public static readonly int MAX_SUGGESTED_SHARED_TEXTURE_MB = 256;
        public static readonly int MAX_SUGGESTED_COLLIDER_VERTS = 75 * k;
        public static readonly int MAX_SUGGESTED_AUDIO_MB = 16;

        public string sceneName;
        public string scenePath;

        public bool hasLightmaps => lightmapTextureMB > 0;
        public bool hasLightprobes;
        public bool hasReflectionProbes;

        public int lightmapTextureMB;
        public int verts;
        public int uniqueVerts;
        public int uniqueMaterials;
        public int materialTextureMB;
        public int meshColliderVerts;
        public int realtimeLights;
        public int reflectionProbeMB;//textures
        public int graphicTextureMB;
        public int audioMB;

        // Per asset data about their size
        public IReadOnlyList<Tuple<string, int>> meshVertCounts;
        public IReadOnlyList<Tuple<string, int>> meshColliderVertCounts;
        public IReadOnlyList<Tuple<string, float>> textureMemorySizesMB;

        public int sharedTextureMB => materialTextureMB + lightmapTextureMB + graphicTextureMB + reflectionProbeMB;

        //how long it took to analyze the scene.
        //used in sceneVitals to auto adjust the refresh rate.
        public long responseMilliseconds;

        public float vertPercent => (float)verts / MAX_SUGGESTED_VERTS;
        public float uniqueMaterialsPercent => (float)uniqueMaterials / MAX_SUGGESTED_UNIQUE_MATERIALS;
        public float sharedTexturePercent => (float)sharedTextureMB / MAX_SUGGESTED_SHARED_TEXTURE_MB;
        public float meshColliderVertPercent => (float)meshColliderVerts / MAX_SUGGESTED_COLLIDER_VERTS;
        public float audioPercent => (float)audioMB / MAX_SUGGESTED_AUDIO_MB;
    }
}