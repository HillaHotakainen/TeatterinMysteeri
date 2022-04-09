using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class Perspective : MonoBehaviour
    {
        [SerializeField] int frontLayer = 6;
        [SerializeField] int behindLayer = 4;
        private SpriteRenderer spr;
        private GameObject hero;
        void Start()
        {
            spr = gameObject.GetComponent<SpriteRenderer>();
            hero = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        void Update()
        {
            if (hero.transform.position.y > transform.position.y) {
                spr.sortingOrder = frontLayer;
                Debug.Log("IN FRoNT");
            } else {
                spr.sortingOrder = behindLayer;
                Debug.Log("BEHIN");
            }
        }
    }
}
