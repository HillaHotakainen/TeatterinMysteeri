using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class MuteAtStart : MonoBehaviour
    {
        [SerializeField] float untilUnmute;
        AudioSource audioSource;
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.mute = true;
            StartCoroutine("UnMute");
        }

        // Update is called once per frame
        IEnumerator UnMute()
        {
            yield return new WaitForSeconds(untilUnmute);
            audioSource.mute = false;
        }
    }
}
