using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TeatterinMysteeri
{
    public class DeleteSave : MonoBehaviour
    {
        private bool saveDeleted = false;
        public bool SaveDeleted
        {
            get{return saveDeleted;}
            set{saveDeleted = value;}
        }
        public void DeletePrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            saveDeleted = true;
            LevelLoader.Current.CloseOptions();
        }
    }
}
