using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class LightBeamVertical : MonoBehaviour
    {
        public LightBeamVertical pysty;
        private Vector3 direction = new Vector3(1,0,0);
        private Vector3 targetPos = Vector3.zero;
        void Start()
        {
            targetPos = transform.position + direction;
            if (Physics2D.OverlapPoint(targetPos) == null) {
                Instantiate(gameObject, targetPos, transform.rotation);
            }
            Debug.Log(Physics2D.OverlapPoint(transform.position + new Vector3(-1,0,0)));
        }
        void Update()
        {
            
        }
    }
}
