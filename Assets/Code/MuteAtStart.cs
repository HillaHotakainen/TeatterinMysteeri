using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class MuteAtStart : MonoBehaviour
    {
        [SerializeField] float untilUnmute;
        AudioSource[] audioSources;
        void Start()
        {
            audioSources = GetComponents<AudioSource>();
            for (int i = 0; i < audioSources.Length; i++) {
                audioSources[i].mute = true;
            }
            StartCoroutine("UnMute");
        }
        IEnumerator UnMute()
        {
            yield return new WaitForSeconds(untilUnmute);
            audioSources = GetComponents<AudioSource>();
            for (int i = 0; i < audioSources.Length; i++) {
                audioSources[i].mute = false;
            }
        }
    }
}
