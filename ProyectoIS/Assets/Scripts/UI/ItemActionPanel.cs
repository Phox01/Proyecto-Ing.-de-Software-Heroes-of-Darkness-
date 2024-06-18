using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using TMPro;

public class ItemActionPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;

    // LocalizedString instead of LocalizedStringTable
    public LocalizedStringTable localizedStringTable;

    public void AddButon(string key, Action onClickAction)
    {
        GameObject button = Instantiate(buttonPrefab, transform);
        button.GetComponent<Button>().onClick.AddListener(() => onClickAction());

        // Create a LocalizedString and set the TableReference and EntryReference
        LocalizedString localizedString = new LocalizedString
        {
            TableReference = localizedStringTable.TableReference,
            TableEntryReference = key
        };

        // Asynchronously get the localized string and set the button text
        localizedString.StringChanged += (localizedText) =>
        {
            button.GetComponentInChildren<TMP_Text>().text = localizedText;
        };
    }

    public void Toggle(bool val)
    {
        if (val == true)
            RemoveOldButtons();
        gameObject.SetActive(val);
    }

    public void RemoveOldButtons()
    {
        foreach (Transform transformChildObjects in transform)
        {
            Destroy(transformChildObjects.gameObject);
        }
    }
}
