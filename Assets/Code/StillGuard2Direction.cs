using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class StillGuard2Direction : MonoBehaviour
    {
        [SerializeField] string direction1;
        [SerializeField] string direction2;
        [SerializeField] float turnTimer = 2.0f;
        StillGuard stillGuard;
        void Start()
        {
            stillGuard = GetComponent<StillGuard>();
            StartCoroutine("Turn");
        }
        IEnumerator Turn()
        {
            yield return new WaitForSeconds(turnTimer);
            stillGuard.NewDirection(direction2);
            StartCoroutine("Turn2");
        }

        IEnumerator Turn2()
        {
            yield return new WaitForSeconds(turnTimer);
            stillGuard.NewDirection(direction1);
            StartCoroutine("Turn");
        }
    }
}
