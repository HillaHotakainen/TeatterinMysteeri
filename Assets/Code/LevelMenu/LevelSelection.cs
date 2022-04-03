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
            if(unlocked)
            {
                LevelLoader.Current.LoadLevel(levelName);
            }
        }
        void Awake()
        {
            //PlayerPrefs.DeleteAll();
            //PlayerPrefs.Save();
            if(name == "Testaus")
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
