using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeatterinMysteeri
{
    public class MainMenu : MonoBehaviour
    {
        private bool fadeDone;

        private void Start()
        {
            StartCoroutine("MakeClickable");
        }

        IEnumerator MakeClickable()
        {
            yield return new WaitForSeconds(1.0f);
            fadeDone = true;
        }
        public void PlayGame()
        {
            if(fadeDone)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
