using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class MusicController : MonoBehaviour
    {
        public string song = "mainmenu";
        void Start()
        {
            GameObject[] asdf = GameObject.FindGameObjectsWithTag("Music");
            if (asdf.Length > 0) {
                GameObject musicPlayer = asdf[0];
                MusicPlayer player = musicPlayer.GetComponent<MusicPlayer>();
                player.newSong = song;
            }
        }
    }
}
