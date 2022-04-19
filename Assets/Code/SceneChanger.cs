using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace TeatterinMysteeri
{
    public class SceneChanger : MonoBehaviour, IPointerDownHandler
    {

        [SerializeField] private string sceneName;
        [SerializeField] private string thisSceneName;
        [SerializeField] private string nextSceneName;
        public bool cantClick;
        private bool waitDone = false;
        private bool fadingOut = false;
        public bool dontFadeout = false;
        private AudioSource music;
        private void Start()
        {
            StartCoroutine(Wait());
        }
        private void FixedUpdate() {
            if (fadingOut) {
                if (music.volume > 0) {
                    music.volume -= 0.03f;
                } else {
                    music.Stop();
                    music.volume = 1;
                    fadingOut = false;
                }
            }
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);
            GameObject[] asdf = GameObject.FindGameObjectsWithTag("Music");
            if (asdf.Length > 0) {
                GameObject musicPlayer = asdf[0];
                AudioSource[] songs = musicPlayer.GetComponents<AudioSource>();
                for (int i = 0; i < songs.Length; i++) {
                    if (songs[i].isPlaying) {
                        music = songs[i];
                    }
                }
            }
            waitDone = true;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            LevelLoader.Current.LoadLevel(sceneName);
            SaveProgress();
            if (music != null && !dontFadeout) {
                fadingOut = true;
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!cantClick && waitDone) {
                LevelLoader.Current.LoadLevel(sceneName);
                SaveProgress();
                if (music != null && !dontFadeout) {
                    fadingOut = true;
                }
            }
        }
        private void SaveProgress()
        {
            PlayerPrefs.SetInt(thisSceneName + "Completed", 1);
            PlayerPrefs.SetInt(nextSceneName, 1);
            PlayerPrefs.Save();
        }
    }
}
