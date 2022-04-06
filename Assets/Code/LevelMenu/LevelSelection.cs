using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TeatterinMysteeri
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] private bool unlocked;
        public Image lockedImage;
        public Image star;
        public Image levelDone;
        private bool waitDone = false;
        private void Start()
        {
            StartCoroutine(Wait());
        }

        IEnumerator Wait()          //Odottaa sekunnin ennen kuin hyväksyy klikkauksen
        {
            yield return new WaitForSeconds(1f);
            waitDone = true;
        }

        private void UpdateLevelImage()
        {
            if(!unlocked)
            {
                lockedImage.gameObject.SetActive(true);
                star.gameObject.SetActive(false);
            }
            else
            {
                lockedImage.gameObject.SetActive(false);
                star.gameObject.SetActive(true);
            }
        }

        public void PressSelection(string levelName)
        {
            if(unlocked && waitDone)
            {
                LevelLoader.Current.LoadLevel(levelName);
            }
        }
        void Awake()
        {
            //PlayerPrefs.DeleteAll();
            //PlayerPrefs.Save();
            if(name == "Level1")
            {
                unlocked = true;
            }
            else
            {
                unlocked = PlayerPrefs.GetInt(name) == 1;
            }
            UpdateLevelImage();
        }
    }
}
