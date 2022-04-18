using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization;

namespace TeatterinMysteeri
{
    public class MultiTextBox : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmptext;
        [SerializeField] float delayAfter = 1f;
        [SerializeField] float timeBetweenCharacters = 0.1f;
        [SerializeField] float secondsBeforeDestroy = 2.0f;
        [SerializeField] string[] stringArray;
        [SerializeField] LocalizedString[] localizedStrings;
        private bool textDone = false;
        private Image image;
        string writer;

        public bool TextDone
        {
            get {return textDone;}
        }

        public float SecondsBfrDestroy
        {
            get {return secondsBeforeDestroy;}
        }
        void Start()
        {
            image = GetComponent<Image>();
            writer = tmptext.text;
        }

        public void BeginText()
        {
            image.enabled = true;
            tmptext.enabled = true;
            StartCoroutine("TypeWriterTMP");
        }
        public void NextBox(string nextText)
        {
            tmptext.text = "";
            writer = nextText;
        }

        IEnumerator TypeWriterTMP()
        {
            foreach (LocalizedString dialog in localizedStrings)
            {
                NextBox(dialog.GetLocalizedString());
                foreach (char c in writer)
                {
                    tmptext.text += c;
                    yield return new WaitForSeconds(timeBetweenCharacters);
                }
                yield return new WaitForSeconds(delayAfter);  
            }
            textDone = true;
            yield return new WaitForSeconds(secondsBeforeDestroy);
            Destroy(gameObject);
        }
    }
}
