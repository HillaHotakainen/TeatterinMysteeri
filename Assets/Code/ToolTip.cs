using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    public class ToolTip : MonoBehaviour
    {
        [SerializeField] TextBox textBox;
        Collider2D myCollider;

        private void Start()
        {
            myCollider = GetComponent<Collider2D>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(textBox != null)
            {
                textBox.StartFade();
            }
        }
    }
}
