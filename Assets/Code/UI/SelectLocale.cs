using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace TeatterinMysteeri.UI
{
    public class SelectLocale : MonoBehaviour
    {
        [SerializeField]
        private Locale locale;

        public void SetLocale()
        {
            LocalizationSettings.SelectedLocale = locale;
        }
    }
}
