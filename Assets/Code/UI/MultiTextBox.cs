using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    public class MultiTextBox : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmptext;
        [SerializeField] float delayAfter = 1f;
        [SerializeField] float timeBetweenCharacters = 0.1f;
        [SerializeField] float secondsBeforeDestroy = 2.0f;
        [SerializeField] string[] stringArray;

        private Image image;
        string writer;
        void Start()
        {
            image = GetComponent<Image>();
            writer = tmptext.text;
            StartCoroutine("TypeWriterTMP");
        }
        public void NextBox(string nextText)
        {
            tmptext.text = "";
            writer = nextText;
        }

        IEnumerator TypeWriterTMP()
        {
            foreach (string dialog in stringArray)
            {
                NextBox(dialog);
                foreach (char c in writer)
                {
                    tmptext.text += c;
                    yield return new WaitForSeconds(timeBetweenCharacters);
                }
                yield return new WaitForSeconds(delayAfter);  
            }
            yield return new WaitForSeconds(secondsBeforeDestroy);
            Destroy(gameObject);
        }
    }
}
