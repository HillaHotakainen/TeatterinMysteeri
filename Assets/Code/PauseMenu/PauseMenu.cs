using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace TeatterinMysteeri
{
    public class PauseMenu : MonoBehaviour, IPointerDownHandler
    {
        public GameObject pauseMenu;
        void Start()
        {
            pauseMenu.SetActive(false);
        }

        
        void Update()
        {
        
        }

        public void PauseGame()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        public void MainMenu()
        {
            Time.timeScale = 1f;
            LevelLoader.Current.LoadLevel("MainMenu");
        }

        public void LevelMenu()
        {
            Time.timeScale = 1f;
            LevelLoader.Current.LoadLevel("LevelSelection");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                
            }
        }
    }
}
