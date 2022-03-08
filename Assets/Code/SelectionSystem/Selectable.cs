using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TeatterinMysteeri.SelectionSystem
{
    public class Selectable : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private string objectName;
        
        [SerializeField]
        private Sprite objectIcon;

        // Property jossa vaan get on määritetty joten käyttäytyy kuin olis Read-Only muuttuja.
        public string Name
        {
            get { return objectName; }
        }
        public Sprite Icon
        {
            get 
            {
                // Jos objektin iconi on määritetty palautetaan se.
                if (objectIcon != null)
                {
                    return objectIcon;
                }

                // Jos iconia ei oo määritetty niin käytetään spriteRenderin spriteä.
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    return spriteRenderer.sprite;
                }

                // jos koko spriteRenderiä ei löydy
                return null;
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Selection.Current.SelectedObject = this;
        }
    }
}
