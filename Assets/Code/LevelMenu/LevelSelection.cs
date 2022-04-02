using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] private bool unlocked;
        public Image lockedImage;
        public Image star;

        private void update()
        {
            UpdateLevelImage();
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
    }
}
