using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class UnlockAll : MonoBehaviour
    {
        public void UnlockLevels()
        {
            PlayerPrefs.SetInt("Level2", 1);
            PlayerPrefs.SetInt("Level3", 1);
            PlayerPrefs.SetInt("Level4", 1);
            PlayerPrefs.SetInt("Level5", 1);
            PlayerPrefs.SetInt("Level6", 1);
            PlayerPrefs.SetInt("Level7", 1);
            PlayerPrefs.SetInt("Level8", 1);
            PlayerPrefs.SetInt("Level9", 1);
            PlayerPrefs.SetInt("Level10", 1);
            LevelLoader.Current.LoadLevel("LevelSelection");
        }
    }
}