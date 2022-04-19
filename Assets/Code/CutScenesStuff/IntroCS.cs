using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class IntroCS : MonoBehaviour
    {
        [SerializeField] MultiTextBox firstDialogue;
        void Start()
        {
            StartCoroutine("StartCutscene");
        }

        IEnumerator StartCutscene()
        {
            yield return new WaitForSeconds(0.5f);
            firstDialogue.BeginText();
            yield return new WaitUntil(() => firstDialogue.TextDone);
            yield return new WaitForSeconds(1.5f);
            LevelLoader.Current.LoadLevel("LevelSelection");
        }
    }
}
