using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Unified.UniversalBlur.Runtime
{
    public class UniversalBlurFeature : ScriptableRendererFeature
    {
        private enum InjectionPoint
        {
            BeforeRenderingTransparents = RenderPassEvent.BeforeRenderingTransparents,
            BeforeRenderingPostProcessing = RenderPassEvent.BeforeRenderingPostProcessing,
            AfterRenderingPostProcessing = RenderPassEvent.AfterRenderingPostProcessing
        }

        [field: SerializeField, HideInInspector] public int PassIndex { get; set; } = 0;

        [field: Header("Blur Settings")]
        [field: SerializeField] private InjectionPoint injectionPoint = InjectionPoint.AfterRenderingPostProcessing;

        [field: Space]
        [field: Range(0f, 1f)][field: SerializeField] public float Intensity { get; set; } = 1.0f;

        [Range(1f, 10f)][SerializeField] private float downsample = 2.0f;
        [Range(1, 20)][SerializeField] private int iterations = 6;
        [Range(0f, 5f)][SerializeField] private float scale = .5f;
        [SerializeField] private ScaleBlurWith scaleBlurWith;
        [SerializeField] private float scaleReferenceSize = 1080f;

        [SerializeField]
        [HideInInspector]
        [Reload("Shaders/KawaseBlur.shader")]
        private Shader shader;

        private readonly ScriptableRenderPassInput _requirements = ScriptableRenderPassInput.Color;

        public Material PassMaterial => _material;

        private Material _material;
        private UniversalBlurPass _fullScreenPass;
        private bool _injectedBeforeTransparents;

        /// <inheritdoc/>
        public override void Create()
        {
            _fullScreenPass = new UniversalBlurPass();
            _fullScreenPass.renderPassEvent = (RenderPassEvent)injectionPoint;

            ScriptableRenderPassInput modifiedRequirements = _requirements;

            var requiresColor = (_requirements & ScriptableRenderPassInput.Color) != 0;
            _injectedBeforeTransparents = injectionPoint <= InjectionPoint.BeforeRenderingTransparents;

            if (requiresColor && !_injectedBeforeTransparents)
            {
                modifiedRequirements ^= ScriptableRenderPassInput.Color;
            }

            _fullScreenPass.ConfigureInput(modifiedRequirements);
        }

        /// <inheritdoc/>
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (!TrySetShadersAndMaterials())
            {
                Debug.LogErrorFormat("{0}.AddRenderPasses(): Missing material. {1} render pass will not be added.", GetType().Name, name);
                return;
            }

            var passData = GetBlurPassData();

            _fullScreenPass.Setup(passData, renderingData);

            renderer.EnqueuePass(_fullScreenPass);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            _fullScreenPass?.Dispose();
            CoreUtils.Destroy(_material);
        }

        private bool TrySetShadersAndMaterials()
        {
            if (shader == null)
            {
                shader = Shader.Find("Unified/KawaseBlur");
            }

            if (_material == null && shader != null)
                _material = CoreUtils.CreateEngineMaterial(shader);
            return _material != null;
        }

        private float CalculateScale() => scaleBlurWith switch
        {
            ScaleBlurWith.ScreenHeight => scale * (Screen.height / scaleReferenceSize),
            ScaleBlurWith.ScreenWidth => scale * (Screen.width / scaleReferenceSize),
            _ => scale
        };

        private BlurPassData GetBlurPassData()
        {
            return new BlurPassData()
            {
                EffectMaterial = _material,
                Downsample = downsample,
                Intensity = Intensity,
                PassIndex = PassIndex,
                Scale = CalculateScale(),
                Iterations = iterations,
            };
        }
    }
}