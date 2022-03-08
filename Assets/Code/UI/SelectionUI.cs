using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TeatterinMysteeri.SelectionSystem;
// Kahdessa namespacessa on saman niminen luokka, niin pitää meidän kertoa
// Kumpaa tässä koodissa käytetään.
using Selectable = TeatterinMysteeri.SelectionSystem.Selectable;

namespace TeatterinMysteeri.UI
{
    public class SelectionUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text nameText;

        [SerializeField]
        private Image icon;
        public void SetSelectedObject(Selectable selected)
        {
            if (selected != null)
            {
                nameText.text = selected.Name;
                icon.sprite = selected.Icon;
                icon.color = new Color(1, 1, 1, 1);
            }
            else
            {
                nameText.text = "";
                icon.sprite = null;
                icon.color = new Color(1, 1, 1, 0);
            }
        }
        public void ClearSelection()
        {
            Selection.Current.SelectedObject = null;
        }
    }
}
