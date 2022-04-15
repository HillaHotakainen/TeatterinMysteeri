using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TeatterinMysteeri
{
    public class DeleteSave : MonoBehaviour
    {
        public void DeletePrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            LevelLoader.Current.LoadLevel("LevelSelection");
        }
    }
}
