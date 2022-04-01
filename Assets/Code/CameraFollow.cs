using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        public Transform Target
        {
            get{return target;}
            set{target = value;}
        }
        [SerializeField]
        private float zOffset;

        private Vector3 velocity;

        [SerializeField]
        private float smoothTime;

        void LateUpdate()
        {
            Vector3 targetPosition = target.TransformPoint(new Vector3(0,0,zOffset));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}