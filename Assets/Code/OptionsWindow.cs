using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    public class OptionsWindow : MonoBehaviour
    {
        [SerializeField]
        private AudioControl musicControl;

        [SerializeField]
        private AudioControl sfxControl;

        [SerializeField]
        private string musicVolumeName;

        [SerializeField]
        private string sfxVolumeName;

        [SerializeField]
        private AudioMixer mixer;
        [SerializeField] Image image;

        private void Start()
        {
            musicControl.Setup(mixer, musicVolumeName);
            sfxControl.Setup(mixer, sfxVolumeName);
            if(PlayerPrefs.HasKey("musicVolume"))
            {
                musicControl.SetSliderValue(PlayerPrefs.GetFloat("musicVolume"));
            }
            if(PlayerPrefs.HasKey("sfxVolume"))
            {
                sfxControl.SetSliderValue(PlayerPrefs.GetFloat("sfxVolume"));
            }
        }
        public void Open()
        {
            LevelLoader.Current.LoadOptions();
        }
        public void Close()
        {
            musicControl.Save();
            PlayerPrefs.SetFloat("musicVolume", musicControl.GetSliderValue());
            sfxControl.Save();
            PlayerPrefs.SetFloat("sfxVolume", sfxControl.GetSliderValue());
            LevelLoader.Current.CloseOptions();
        }
    }
}
