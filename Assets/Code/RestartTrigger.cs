using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace TeatterinMysteeri
{
    public class RestartTrigger : MonoBehaviour
    {
        [SerializeField] bool onlyGhost = false;
        WaypointMover mover;
        private void OnTriggerEnter2D(Collider2D other) {
            if (onlyGhost) {
                if (other.name == "Ghost") {
                    CharacterControl ctrl = other.GetComponent<CharacterControl>();
                    ctrl.dontMove = true;
                    RestartLevel();
                }
            } else if (other.name == "Ghost" | other.name == "Hero") {
                CharacterControl ctrl = other.GetComponent<CharacterControl>();
                ctrl.dontMove = true;
                RestartLevel();
            }
        }
        private void RestartLevel() 
        {
            if (gameObject.name == "Flashlight(Clone)") {
                StopNearestGuard();
            }
            LevelLoader.Current.LoadLevel(SceneManager.GetActiveScene().name);
        }
        private void StopNearestGuard()
        {
            GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            foreach (GameObject guard in guards) {
                Vector3 dist = guard.transform.position - transform.position;
                float curDist = dist.sqrMagnitude;
                if (curDist < distance) {
                    closest = guard;
                    distance = curDist;
                }
            }
            mover = closest.GetComponent<WaypointMover>();
            mover.caught = true;
        }
    }
}
