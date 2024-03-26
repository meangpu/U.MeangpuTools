using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VInspector;

namespace Meangpu.Util
{
    // learn from [Collecting User Feedback in Unity (Google Forms & Email) - YouTube](https://www.youtube.com/watch?v=fPfH8ZLcrmY)
    public class GmailFeedback : MonoBehaviour
    {
        [SerializeField] TMP_Text _textData;
        [SerializeField] string _emailToReceiveFeedback = "sermtat@gmail.com";

        [Button] public void SentWordInTextToEmail() => OpenEmailClient(_textData.text);

        public void OpenEmailClient(string feedbackWord)
        {
            string subject = "GameFeedback";
            string body = $"<{feedbackWord}>";
            OpenLinkURL.OpenLink($"mailto:{_emailToReceiveFeedback}?subject={subject}&body={body}");
        }
    }
}