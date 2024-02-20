using System.Linq;
using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class SetMaterialByStatus : MonoBehaviour
    {
        [Header("Renderer")]
        [SerializeField] MeshRenderer _meshRenderer;
        [SerializeField] SkinnedMeshRenderer _skinMeshRenderer;

        [Header("Material")]
        [SerializeField] Material _disableMaterial;

        [SerializeField] Material[] _originalMatArray;

        bool _isNowMatEnable = true;
        public bool IsEnableMat => _isNowMatEnable;

        private void Awake()
        {
            if (_meshRenderer != null) _originalMatArray = _meshRenderer.sharedMaterials;
            if (_skinMeshRenderer != null) _originalMatArray = _skinMeshRenderer.sharedMaterials;
        }

        public void SetMaterialToState(bool newState)
        {
            _isNowMatEnable = newState;
            if (_isNowMatEnable)
            {
                SetMatToEnable();
            }
            else
            {
                SetMatToDisable();
            }
        }

        [Button]
        public void ToggleMatState()
        {
            _isNowMatEnable = !_isNowMatEnable;
            SetMaterialToState(_isNowMatEnable);
        }

        [Button]
        public void SetMatToEnable()
        {
            _isNowMatEnable = true;
            if (_meshRenderer != null && _meshRenderer.sharedMaterials != null)
            {
                _meshRenderer.SetMaterials(_originalMatArray.ToList());
            }
            if (_skinMeshRenderer != null && _skinMeshRenderer.sharedMaterials != null)
            {
                _skinMeshRenderer.SetMaterials(_originalMatArray.ToList());
            }
        }

        [Button]
        public void SetMatToDisable()
        {
            _isNowMatEnable = false;
            if (_meshRenderer != null && _meshRenderer.sharedMaterials != null)
            {
                Material[] _disableMatArray = new Material[_meshRenderer.sharedMaterials.Length];
                for (int i = 0, length = _meshRenderer.sharedMaterials.Length; i < length; i++)
                {
                    _disableMatArray[i] = _disableMaterial;
                }
                _meshRenderer.sharedMaterials = _disableMatArray;
            }

            if (_skinMeshRenderer != null && _skinMeshRenderer.sharedMaterials != null)
            {
                Material[] _disableMatArray = new Material[_skinMeshRenderer.sharedMaterials.Length];
                for (int i = 0, length = _skinMeshRenderer.sharedMaterials.Length; i < length; i++)
                {
                    _disableMatArray[i] = _disableMaterial;
                }
                _skinMeshRenderer.sharedMaterials = _disableMatArray;
            }
        }
    }
}