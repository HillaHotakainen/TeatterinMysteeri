using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeatterinMysteeri.UI;

namespace TeatterinMysteeri.SelectionSystem
{
    public class Selection : MonoBehaviour
    {
        private static Selection current;
        public static Selection Current
        {
            get { return current; }
        }
        private SelectionUI ui;

        // Valittu olio.
        private Selectable selected;

        public Selectable SelectedObject
        {
            get { return selected; }
            set
            {
                selected = value;

                if (selected != null)
                {
                    Debug.Log(selected.Name + " Selected");
                }
                else
                {
                    Debug.Log("Selection cleared");
                }
                // Välitetään valinta UI:lle.
                ui.SetSelectedObject(selected);
            }
        }
        private void Awake()
        {
            current = this; // viittaa tähän olioon.
        }
        private void Start()
        {
            ui = FindObjectOfType<SelectionUI>();
        }
    }
}
