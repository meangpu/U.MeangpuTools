using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Meangpu.Util
{
    public class DisplayRandomTip : BaseLoadingRandom
    {
        [SerializeField] List<string> _wordTipPool;
        [SerializeField] TMP_Text _targetText;
        protected override void SetToNewRandom() => _targetText.SetText(_wordTipPool[Random.Range(0, _wordTipPool.Count)]);
    }
}