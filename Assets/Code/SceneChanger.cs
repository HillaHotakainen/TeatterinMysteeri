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
        private void Start()
        {
            StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);
            waitDone = true;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            LevelLoader.Current.LoadLevel(sceneName);
            SaveProgress();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!cantClick && waitDone) {
                LevelLoader.Current.LoadLevel(sceneName);
                SaveProgress();
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
