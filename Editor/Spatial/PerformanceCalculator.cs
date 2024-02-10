using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine.Profiling;
using System.Linq;
using System;

// script from [spatialsys/spatial-unity-sdk: This package allows creators to build content in Unity and publish it to Spatial](https://github.com/spatialsys/spatial-unity-sdk)

namespace Meangpu
{
    public class PerformanceCalculator
    {
        //Takes usually 1ms or less on sample scene
        public static PerformanceResponse GetActiveScenePerformanceResponse()
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var scene = EditorSceneManager.GetActiveScene();

            List<Tuple<string, int>> meshVertCounts = new List<Tuple<string, int>>();
            List<Tuple<string, int>> meshColliderVertCounts = new List<Tuple<string, int>>();
            List<Tuple<string, float>> textureSizesMB = new List<Tuple<string, float>>();

            PerformanceResponse response = new PerformanceResponse
            {
                sceneName = scene.name,
                scenePath = scene.path,
                meshVertCounts = meshVertCounts,
                meshColliderVertCounts = meshColliderVertCounts,
                textureMemorySizesMB = textureSizesMB
            };

            // Count lightmaps size
            long bytes = 0;
            LightmapData[] lightmaps = LightmapSettings.lightmaps;
            foreach (LightmapData lightmap in lightmaps)
            {
                if (lightmap.lightmapColor != null)
                {
                    long sizeInBytes = Profiler.GetRuntimeMemorySizeLong(lightmap.lightmapColor);
                    bytes += sizeInBytes;
                    textureSizesMB.Add(new Tuple<string, float>(AssetDatabase.GetAssetPath(lightmap.lightmapColor), sizeInBytes / 1024f / 1024f));
                }
                if (lightmap.lightmapDir != null)
                {
                    long sizeInBytes = Profiler.GetRuntimeMemorySizeLong(lightmap.lightmapDir);
                    bytes += sizeInBytes;
                    textureSizesMB.Add(new Tuple<string, float>(AssetDatabase.GetAssetPath(lightmap.lightmapDir), sizeInBytes / 1024f / 1024f));
                }
                if (lightmap.shadowMask != null)
                {
                    long sizeInBytes = Profiler.GetRuntimeMemorySizeLong(lightmap.shadowMask);
                    bytes += sizeInBytes;
                    textureSizesMB.Add(new Tuple<string, float>(AssetDatabase.GetAssetPath(lightmap.shadowMask), sizeInBytes / 1024f / 1024f));
                }
            }
            response.lightmapTextureMB = (int)bytes / 1024 / 1024;

            // Count scene object sizes
            List<Texture> foundMaterialTextures = new List<Texture>();
            List<Texture> foundGraphicTextures = GameObject.FindObjectsOfType<Graphic>(true).Select(g => g.mainTexture).ToList();
            List<Material> materials = new List<Material>();
            List<Mesh> foundMeshes = new List<Mesh>();
            Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>(true);

            foreach (Renderer renderer in renderers)
            {
                //look for materials and textures
                materials.AddRange(renderer.sharedMaterials);
                foreach (var material in renderer.sharedMaterials)
                {
                    if (material == null)
                    {
                        continue;
                    }
                    foreach (var texName in material.GetTexturePropertyNames())
                    {
                        var tex = material.GetTexture(texName);
                        if (tex != null)
                        {
                            foundMaterialTextures.Add(tex);
                        }
                    }
                }

                // look for sprite textures
                if (renderer is SpriteRenderer)
                {
                    SpriteRenderer spriteRenderer = renderer as SpriteRenderer;
                    if (spriteRenderer.sprite != null && spriteRenderer.sprite.texture != null)
                    {
                        foundGraphicTextures.Add(spriteRenderer.sprite.texture);
                    }
                }

                // look for mesh
                if (renderer is MeshRenderer)
                {
                    MeshFilter filter = renderer.GetComponent<MeshFilter>();
                    if (filter != null && filter.sharedMesh != null)
                    {
                        foundMeshes.Add(filter.sharedMesh);
                        response.verts += filter.sharedMesh.vertexCount;
                    }
                }
                else if (renderer is SkinnedMeshRenderer)
                {
                    SkinnedMeshRenderer skinned = renderer as SkinnedMeshRenderer;
                    if (skinned.sharedMesh != null)
                    {
                        foundMeshes.Add(skinned.sharedMesh);
                        response.verts += skinned.sharedMesh.vertexCount;
                    }
                }
                else if (renderer is BillboardRenderer)
                {
                    response.verts += 4;
                }
            }
            IEnumerable<Mesh> uniqueMeshes = foundMeshes.Distinct();
            meshVertCounts.AddRange(uniqueMeshes.Select(m => new Tuple<string, int>(AssetDatabase.GetAssetPath(m), m.vertexCount)));
            response.uniqueVerts = uniqueMeshes.Distinct().Sum(m => m.vertexCount);
            response.uniqueMaterials = materials.FindAll(m => m != null).Select(m => m.name).Distinct().Count();

            // Count skybox material
            Material skyboxMaterial = RenderSettings.skybox;
            if (skyboxMaterial != null)
            {
                foreach (var texName in skyboxMaterial.GetTexturePropertyNames())
                {
                    var tex = skyboxMaterial.GetTexture(texName);
                    if (tex != null)
                    {
                        foundMaterialTextures.Add(tex);
                    }
                }
            }

            // Count material texture sizes
            bytes = 0;
            foreach (Texture texture in foundMaterialTextures.Distinct())
            {
                long sizeInBytes = Profiler.GetRuntimeMemorySizeLong(texture);
                bytes += sizeInBytes;
                textureSizesMB.Add(new Tuple<string, float>(AssetDatabase.GetAssetPath(texture), sizeInBytes / 1024f / 1024f));
            }
            response.materialTextureMB = (int)(bytes / 1024f / 1024f);

            // Count graphic texture sizes
            bytes = 0;
            foreach (Texture texture in foundGraphicTextures.Distinct())
            {
                if (texture == null)
                {
                    continue;
                }
                long sizeInBytes = Profiler.GetRuntimeMemorySizeLong(texture);
                bytes += sizeInBytes;

                string texturePath = AssetDatabase.GetAssetPath(texture);
                if (!string.IsNullOrEmpty(texturePath))
                {
                    textureSizesMB.Add(new Tuple<string, float>(texturePath, sizeInBytes / 1024f / 1024f));
                }
            }
            response.graphicTextureMB = (int)(bytes / 1024f / 1024f);

            // Count mesh collider vertices
            MeshCollider[] meshColliders = GameObject.FindObjectsOfType<MeshCollider>(true);
            foreach (MeshCollider meshCollider in meshColliders)
            {
                if (meshCollider.sharedMesh != null)
                {
                    response.meshColliderVerts += meshCollider.sharedMesh.vertexCount;
                    meshColliderVertCounts.Add(new Tuple<string, int>(GetGameObjectPath(meshCollider.gameObject), meshCollider.sharedMesh.vertexCount));
                }
            }

            // Look for light / reflection probes
            LightProbeGroup[] lightProbeGroups = GameObject.FindObjectsOfType<LightProbeGroup>(true);
            foreach (LightProbeGroup lightProbeGroup in lightProbeGroups)
            {
                if (lightProbeGroup.probePositions.Length > 0)
                {
                    response.hasLightprobes = true;
                    break;
                }
            }
            if (GameObject.FindObjectsOfType<ReflectionProbe>(true).Length > 0)
            {
                response.hasReflectionProbes = true;
            }

            // Look for lights
            Light[] lights = GameObject.FindObjectsOfType<Light>(true);
            response.realtimeLights = lights.Where(l => l.lightmapBakeType != LightmapBakeType.Baked).Count();

            bytes = 0;
            ReflectionProbe[] reflectionProbes = GameObject.FindObjectsOfType<ReflectionProbe>(true);
            foreach (ReflectionProbe probe in reflectionProbes)
            {
                if (probe.mode == UnityEngine.Rendering.ReflectionProbeMode.Baked && probe.texture != null)
                {
                    bytes += Profiler.GetRuntimeMemorySizeLong(probe.texture);
                }
                //realtime probes are currently disabled... but leaving this incase we enable them down the road.
                else if (probe.mode == UnityEngine.Rendering.ReflectionProbeMode.Realtime)
                {
                    bytes += probe.resolution * probe.resolution * 3;
                }
            }
            response.reflectionProbeMB = (int)(bytes / 1024 / 1024);

            // Look for audio
            bytes = 0;
            List<AudioClip> audioClips = new();
            foreach (string path in AssetDatabase.GetDependencies(scene.path, true))
            {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
                if (obj is AudioClip)
                {
                    AudioClip audioClip = obj as AudioClip;
                    audioClips.Add(audioClip);
                }
            }

            foreach (AudioClip audioClip in audioClips.Distinct())
            {
                long sizeInBytes = Profiler.GetRuntimeMemorySizeLong(audioClip);
                bytes += sizeInBytes;
            }

            response.audioMB = (int)(bytes / 1024 / 1024);

            // Sort by size descending
            meshVertCounts.Sort((a, b) => b.Item2.CompareTo(a.Item2));
            meshColliderVertCounts.Sort((a, b) => b.Item2.CompareTo(a.Item2));
            textureSizesMB.Sort((a, b) => b.Item2.CompareTo(a.Item2));

            timer.Stop();
            response.responseMilliseconds = timer.ElapsedMilliseconds;

            return response;
        }

        private static string GetGameObjectPath(GameObject obj)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            GetGameObjectRecursive(obj.transform, stringBuilder);
            return stringBuilder.ToString();
        }

        private static void GetGameObjectRecursive(Transform t, System.Text.StringBuilder stringBuilder)
        {
            if (t.parent != null)
                GetGameObjectRecursive(t.parent, stringBuilder);

            stringBuilder.AppendFormat("/{0}", t.gameObject.name);
        }
    }
}