using System.Collections;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] TMP_Text _hourMin;
    [SerializeField] TMP_Text _sec;

    private void Start() => StartCoroutine(updateTime());

    IEnumerator updateTime()
    {
        var today = System.DateTime.Now;
        _hourMin.text = today.ToString("HH:mm");
        _sec.text = today.ToString("ss");
        yield return new WaitForSeconds(1f);
        StartCoroutine(updateTime());
    }
}
