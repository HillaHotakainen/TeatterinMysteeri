using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeatterinMysteeri
{
    public class MusicPlayer : MonoBehaviour
    {
        public AudioSource mainmenu;
        public AudioSource actortheme;
        public AudioSource actorthemedrums;
        public AudioSource ghosttheme;
        public AudioSource ghostthemedrums;
        public string newSong = "mainmenu";
        private string nowPlaying;
        public static MusicPlayer Current
        {
            get;
            private set;
        }
        private void Awake()
        {
            if(Current == null)
            {
                Current = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
        private void Update()
        {
            // jos musa vaihdetaan musiccontrollerista
            if (newSong != nowPlaying) {
                // stopataan edellinen biisi
                AudioSource[] songs = GetComponents<AudioSource>();
                for (int i = 0; i < songs.Length; i++) {
                    songs[i].Stop();
                }

                // aloitetaan uusi
                switch (newSong) {
                    case "mainmenu":
                        mainmenu.Play();
                        break;
                    case "actortheme":
                        actortheme.Play();
                        break;
                    case "actorthemedrums":
                        actorthemedrums.Play();
                        break;
                    case "ghosttheme":
                        ghosttheme.Play();
                        break;
                    case "ghostthemedrums":
                        ghostthemedrums.Play();
                        break;
                    default:
                        break;
                }
                nowPlaying = newSong;
            }
        }
    }
}
