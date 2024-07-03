using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class InventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private TMP_Text description;

    [SerializeField]
    private LocalizedStringTable localizedStringTable; // Referencia a la tabla de cadenas localizadas

    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        itemImage.gameObject.SetActive(false);
        title.text = "";
        description.text = "";
    }

    public void SetDescription(Sprite sprite, string itemNameKey, string itemDescriptionKey)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;

        // Create LocalizedStrings for the item name and description
        LocalizedString localizedItemName = new LocalizedString
        {
            TableReference = localizedStringTable.TableReference,
            TableEntryReference = itemNameKey
        };

        LocalizedString localizedItemDescription = new LocalizedString
        {
            TableReference = localizedStringTable.TableReference,
            TableEntryReference = itemDescriptionKey
        };

        // Asynchronously get the localized strings and set the text fields
        localizedItemName.StringChanged += (localizedText) =>
        {
            title.text = localizedText;
            Debug.Log(localizedText);
        };

        localizedItemDescription.StringChanged += (localizedText) =>
        {
            description.text = localizedText;
        };
    }
}
