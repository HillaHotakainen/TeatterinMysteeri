using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace TeatterinMysteeri
{
    public class SceneChanger : MonoBehaviour, IPointerDownHandler
    {

        [SerializeField]
        private string sceneName;
        private void OnTriggerEnter2D(Collider2D other)
        {
            LevelLoader.Current.LoadLevel(sceneName);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            LevelLoader.Current.LoadLevel(sceneName);
        }
    }
}
