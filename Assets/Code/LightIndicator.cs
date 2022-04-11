using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class LightIndicator : MonoBehaviour
    {
        [SerializeField] LightReceiver receiver;
        Color onColor = Color.green;
        Color offColor = Color.red;
        SpriteRenderer sprite;

        void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            if(receiver.receivingLight)
            {
                sprite.color = onColor;
            }
            else
            {
                sprite.color = offColor;
            }
        }
    }
}
