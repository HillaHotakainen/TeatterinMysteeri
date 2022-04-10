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

        private void Start()
        {
            musicControl.Setup(mixer, musicVolumeName);
            sfxControl.Setup(mixer, sfxVolumeName);
        }
        public void Open()
        {
            LevelLoader.Current.LoadOptions();
        }
        public void Close()
        {
            musicControl.Save();
            sfxControl.Save();
            LevelLoader.Current.CloseOptions();
        }
    }
}
