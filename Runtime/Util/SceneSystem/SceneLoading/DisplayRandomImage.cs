using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meangpu.Util
{
    public class DisplayRandomImage : BaseLoadingRandom
    {
        [SerializeField] Image _targetImage;
        [SerializeField] List<Sprite> _imagePool;
        protected override void SetToNewRandom() => _targetImage.sprite = _imagePool[Random.Range(0, _imagePool.Count)];
    }
}