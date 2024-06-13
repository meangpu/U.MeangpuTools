using UnityEngine;
using UnityEngine.InputSystem;

namespace Meangpu.Util
{
    public class ResetAllBinding : MonoBehaviour
    {
        [SerializeField] InputActionAsset _inputAsset;

        public void ResetAllKeyBinding()
        {
            foreach (InputActionMap map in _inputAsset.actionMaps)
            {
                map.RemoveAllBindingOverrides();
            }
            PlayerPrefs.DeleteKey("rebinds");
        }
    }
}

