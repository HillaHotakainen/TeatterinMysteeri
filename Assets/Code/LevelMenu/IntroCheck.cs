using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class IntroCheck : MonoBehaviour
    {
        [SerializeField] SceneChanger sceneChanger;
        void Start()
        {
            if(PlayerPrefs.GetInt("introCS")==0 || PlayerPrefs.HasKey("introCS") == false)
            {
                sceneChanger.SceneName = "PrologueCS";
            }
            else
            {
                sceneChanger.SceneName = "LevelSelection";
            }
        }
    }
}
