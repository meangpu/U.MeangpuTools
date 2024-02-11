using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using Meangpu;


namespace SpatialSys.UnitySDK.Editor
{
    [Overlay(typeof(SceneView), "Scene Vitals", true)]
    public class SceneVitalsOverlay : Overlay, ITransientOverlay
    {
        private const string BASE_BLOCK_CLASS = "InfoBlock";
        private const string BASE_SUB_BLOCK_CLASS = "SubBlock";
        private const string GREEN_BLOCK_CLASS = "InfoBlock_green";
        private const string YELLOW_BLOCK_CLASS = "InfoBlock_yellow";
        private const string RED_BLOCK_CLASS = "InfoBlock_red";

        public bool visible => true;

        private VisualElement _verticesBlock;
        private Label _verticesCount;
        private Label _verticesMax;

        private VisualElement _meshIcon;
        private VisualElement _textureIcon;
        private VisualElement _materialIcon;
        private VisualElement _audioIcon;

        private VisualElement _sharedTexturesBlock;
        private VisualElement _sharedTexturesSubBlock;
        private Label _sharedTexturesCount;
        private Label _sharedTexturesMax;

        private Label _materialTexturesCount;
        private VisualElement _lightmapTexturesBlock;
        private Label _lightmapTexturesCount;
        private VisualElement _graphicTexturesBlock;
        private Label _graphicTexturesCount;
        private VisualElement _reflectionProbeBlock;
        private Label _reflectionProbeCount;

        private VisualElement _materialsBlock;
        private Label _materialsCount;
        private Label _materialsMax;

        private VisualElement _audioBlock;
        private Label _audioCount;
        private Label _audioMax;

        private VisualElement _noLightmapsWarning;
        private VisualElement _noLightprobesWarning;
        private VisualElement _highCollisionMeshWarning;

        private double _lastUpdateTime = -1.0;
        private float _autoRefreshEvery = 30f;
        private bool _addedRefreshEvents = false;
        private bool _updateOnNextAutoRefresh = false;

        public override VisualElement CreatePanelContent()
        {
            var visualTree = EditorUtility.LoadAssetFromPackagePath<VisualTreeAsset>("Editor/NotMyCode/Spatial/SceneVitals/SceneVitals.uxml");

            if (visualTree == null) // this mean it in project file not as package
            {
                visualTree = AssetDatabase.LoadAssetAtPath("Assets/U.MeangpuTools/Editor/NotMyCode/Spatial/SceneVitals/SceneVitals.uxml", typeof(VisualTreeAsset)) as VisualTreeAsset;
            }

            VisualElement element = visualTree.Instantiate();
            var root = new VisualElement() { name = "Scene Vitals" };
            root.Add(element);
            InitializeElements(root);
            UpdatePerformanceStats();

            // This function can get called multiple times (e.g. closing and opening a docked panel). We only need to subscribe once.
            if (!_addedRefreshEvents)
            {
                _addedRefreshEvents = true;

                EditorApplication.update += PerformAutoRefresh;
                // Adding, deleting, toggling objects should trigger a refresh.
                EditorApplication.hierarchyChanged += () => _updateOnNextAutoRefresh = true;
                EditorSceneManager.activeSceneChanged += (prevActive, currActive) => UpdatePerformanceStats();
                EditorSceneManager.activeSceneChangedInEditMode += (prevActive, currActive) => UpdatePerformanceStats();
                EditorSceneManager.sceneSaved += (scene) =>
                {
                    if (scene == EditorSceneManager.GetActiveScene())
                        UpdatePerformanceStats();
                };
            }

            return root;
        }

        private void PerformAutoRefresh()
        {
            if (_updateOnNextAutoRefresh && EditorApplication.timeSinceStartup - _lastUpdateTime > _autoRefreshEvery)
            {
                _updateOnNextAutoRefresh = false;
                UpdatePerformanceStats();
            }
        }

        private void InitializeElements(VisualElement root)
        {
            _meshIcon = root.Q("MeshIcon");
            _textureIcon = root.Q("TextureIcon");
            _materialIcon = root.Q("MaterialIcon");
            _audioIcon = root.Q("AudioIcon");

            _verticesBlock = root.Q("Vertices");
            _verticesCount = root.Query<Label>("VerticesCount").First();
            _verticesMax = root.Query<Label>("VerticesMax").First();

            _sharedTexturesBlock = root.Q("SharedTextures");
            _sharedTexturesSubBlock = root.Q("SharedTexturesSubBlock");
            _sharedTexturesCount = root.Query<Label>("SharedTexturesCount").First();
            _sharedTexturesMax = root.Query<Label>("SharedTexturesMax").First();
            _materialTexturesCount = root.Query<Label>("MaterialTexturesCount").First();
            _lightmapTexturesBlock = root.Q("Lightmap");
            _lightmapTexturesCount = root.Query<Label>("LightmapTexturesCount").First();
            _graphicTexturesBlock = root.Q("GraphicTextures");
            _graphicTexturesCount = root.Query<Label>("GraphicTexturesCount").First();
            _reflectionProbeBlock = root.Q("ReflectionProbes");
            _reflectionProbeCount = root.Query<Label>("ReflectionProbesCount").First();

            _audioBlock = root.Q("Audio");
            _audioCount = root.Query<Label>("AudioCount").First();
            _audioMax = root.Query<Label>("AudioMax").First();

            _materialsBlock = root.Q("Materials");
            _materialsCount = root.Query<Label>("MaterialsCount").First();
            _materialsMax = root.Query<Label>("MaterialsMax").First();

            _noLightmapsWarning = root.Q("NoLightmapsWarning");
            _noLightprobesWarning = root.Q("NoLightprobesWarning");
            _highCollisionMeshWarning = root.Q("HighCollisionMeshWarning");
        }

        private void UpdatePerformanceStats()
        {
            if (!visible)
            {
                // Update the UI immediately when the panel becomes visible.
                _lastUpdateTime = -1.0;
                _updateOnNextAutoRefresh = true;
                return;
            }

            _lastUpdateTime = EditorApplication.timeSinceStartup;
            PerformanceResponse resp = PerformanceCalculator.GetActiveScenePerformanceResponse();

            // Change the refresh frequency based on how long the request takes, since it can affect performance of large scenes and slow computers.
            _autoRefreshEvery = Mathf.Clamp(resp.responseMilliseconds * 5f, 5f, 60f);

            SetBaseClass(_verticesBlock);
            SetBlockClassFromRatio(_verticesBlock, resp.vertPercent);
            _verticesCount.text = Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(resp.verts);
            _verticesMax.text = "/ " + Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(PerformanceResponse.MAX_SUGGESTED_VERTS);
            _meshIcon.ClearClassList();
            SetBlockClassFromRatio(_meshIcon, resp.vertPercent);

            SetBaseClass(_sharedTexturesBlock);
            SetBaseClass(_sharedTexturesSubBlock, true);
            SetBlockClassFromRatio(_sharedTexturesBlock, resp.sharedTexturePercent);
            SetBlockClassFromRatio(_sharedTexturesSubBlock, resp.sharedTexturePercent);
            _sharedTexturesCount.text = Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(resp.sharedTextureMB) + "mb";
            _sharedTexturesMax.text = "/ " + Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(PerformanceResponse.MAX_SUGGESTED_SHARED_TEXTURE_MB) + "mb";
            _materialTexturesCount.text = Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(resp.materialTextureMB) + "mb";
            _lightmapTexturesBlock.style.display = resp.hasLightmaps ? DisplayStyle.Flex : DisplayStyle.None;
            _lightmapTexturesCount.text = Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(resp.lightmapTextureMB) + "mb";
            _graphicTexturesBlock.style.display = resp.graphicTextureMB > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            _graphicTexturesCount.text = Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(resp.graphicTextureMB) + "mb";
            _reflectionProbeBlock.style.display = resp.reflectionProbeMB > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            _reflectionProbeCount.text = Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(resp.reflectionProbeMB) + "mb";
            _textureIcon.ClearClassList();
            SetBlockClassFromRatio(_textureIcon, resp.sharedTexturePercent);

            SetBaseClass(_materialsBlock);
            SetBlockClassFromRatio(_materialsBlock, resp.uniqueMaterialsPercent);
            _materialsCount.text = Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(resp.uniqueMaterials);
            _materialsMax.text = "/ " + Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(PerformanceResponse.MAX_SUGGESTED_UNIQUE_MATERIALS);
            _materialIcon.ClearClassList();
            SetBlockClassFromRatio(_materialIcon, resp.uniqueMaterialsPercent);

            SetBaseClass(_audioBlock);
            SetBlockClassFromRatio(_audioBlock, resp.audioPercent);
            _audioBlock.style.display = resp.audioMB > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            _audioCount.text = Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(resp.audioMB) + "mb";
            _audioMax.text = "/ " + Meangpu.Util.NumberUtil.ConvertToAbbreviateNumber(PerformanceResponse.MAX_SUGGESTED_AUDIO_MB) + "mb";
            _audioIcon.ClearClassList();
            SetBlockClassFromRatio(_audioIcon, resp.audioPercent);

            _noLightmapsWarning.style.display = resp.hasLightmaps ? DisplayStyle.None : DisplayStyle.Flex;
            //show if we have lightmaps but no light probes
            _noLightprobesWarning.style.display = resp.hasLightprobes || !resp.hasLightmaps ? DisplayStyle.None : DisplayStyle.Flex;
            _highCollisionMeshWarning.style.display = resp.meshColliderVertPercent < 1f ? DisplayStyle.None : DisplayStyle.Flex;
        }

        private void SetBaseClass(VisualElement element, bool isSubBlock = false)
        {
            element.ClearClassList();
            element.AddToClassList(isSubBlock ? BASE_SUB_BLOCK_CLASS : BASE_BLOCK_CLASS);
        }

        private void SetBlockClassFromRatio(VisualElement element, float ratio)
        {
            if (ratio > 1f)
            {
                element.AddToClassList(RED_BLOCK_CLASS);
            }
            else if (ratio > .6f)
            {
                element.AddToClassList(YELLOW_BLOCK_CLASS);
            }
            else
            {
                element.AddToClassList(GREEN_BLOCK_CLASS);
            }
        }
    }
}