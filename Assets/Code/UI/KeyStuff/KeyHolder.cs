using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    public class KeyHolder : MonoBehaviour
    {
        private bool gotKey = false;

        [SerializeField]
        private Key key;

        private Image keyImage;


        public bool GotKey
        {
            get {return gotKey;}
        }
        private void Awake()
        {
            keyImage = GetComponent<Image>();
        }
        public void GetKey()
        {
            keyImage.sprite = key.Icon;
            keyImage.color = new Color(1, 1, 1, 1);
            gotKey = true;
        }
    }
}
