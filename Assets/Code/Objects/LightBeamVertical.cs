using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace TeatterinMysteeri
{
    public class LightBeamVertical : MonoBehaviour
    {
       private void OnTriggerEnter2D(Collider2D other) {
           if (other.name == "Ghost") {
               CharacterControl ctrl = other.GetComponent<CharacterControl>();
               ctrl.dontMove = true;
               LevelLoader.Current.LoadLevel(SceneManager.GetActiveScene().name);
           }
       }
    }
}
