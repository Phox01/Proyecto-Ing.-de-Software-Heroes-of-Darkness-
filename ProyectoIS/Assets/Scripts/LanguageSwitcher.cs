using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using TMPro;

public class LanguageSwitcher : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;

    void Start()
    {
        languageDropdown.onValueChanged.AddListener(delegate {
            ChangeLanguage(languageDropdown.value);
        });

        InitializeDropdown();
    }

    void InitializeDropdown()
    {
        int currentLanguageIndex = LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0] ? 0 : 1;
        languageDropdown.value = currentLanguageIndex;
    }

    void ChangeLanguage(int index)
    {
        if (index == 0)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0]; 
        }
        else if (index == 1)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1]; 
        }
    }
}
