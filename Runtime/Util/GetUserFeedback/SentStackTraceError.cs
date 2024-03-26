using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;
using System;

namespace Meangpu.Util
{
    // learn from [Collecting User Feedback in Unity (Google Forms & Email) - YouTube](https://www.youtube.com/watch?v=fPfH8ZLcrmY)
    public class SentStackTraceError : MonoBehaviour
    {
        [SerializeField] TMP_Text _textData;
        [SerializeField] Button _submitBtn;

        [SerializeField] string _emailToReceiveFeedback = "sermtat@gmail.com";

        private void Start()
        {
            Assert.IsNotNull(_textData);
            Assert.IsNotNull(_submitBtn);
        }

        public void OpenEmailClient(string feedbackWord)
        {
            string subject = "Feedback";
            string body = $"<{feedbackWord}>";
            OpenLink($"mailto:{_emailToReceiveFeedback}?subject={subject}&body={body}");
        }

        // prevent space in IOS device
        public static void OpenLink(string link)
        {
            bool googleSearch = link.Contains("google.com/search");
            string linkNoSpace = link.Replace(" ", googleSearch ? "+" : "%20");
            Application.OpenURL(linkNoSpace);
        }
    }
}