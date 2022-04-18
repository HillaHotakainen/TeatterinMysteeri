using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace TeatterinMysteeri
{
    public class AudioControl : MonoBehaviour
    {
        private AudioMixer mixer;
        private Slider slider;
        private string volumeName;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
        }

        public void Setup(AudioMixer mixer, string volumeName)
        {
            this.mixer = mixer;
            this.volumeName = volumeName;

            if (mixer.GetFloat(volumeName, out float volume))
            {
                //volume saatiin luettua, asetetaan se slideriin.
                slider.value = ToLinear(volume);
            }
        }

        private float ToDB(float linear)
        {
            return linear <= 0 ? -80f : Mathf.Log10(linear) * 20f;
        }

        private float ToLinear(float db)
        {
            return Mathf.Clamp01(Mathf.Pow(10.0f, db / 20.0f));
        }

        //void Update()
        //{
        //    Save();
        //}

        public void SetSliderValue(float volume)
        {
            slider.value = volume;
        }

        public float GetSliderValue()
        {
            float volume = slider.value;
            return volume;
        }
        public void Save()
        {
            mixer.SetFloat(volumeName, ToDB(slider.value));
        }

    }
}
