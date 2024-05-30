using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class LocalizationController : MonoBehaviour
{
    public LocalizeStringEvent localizeStringEvent; // Solo un LocalizeStringEvent
    private string[] keys;
    private string[] lines;
    public event Action OnLocalizationReady;

    public string tablename;

    public void InitializeKeys(string[] keys)
    {
        if (localizeStringEvent == null)
        {
            Debug.LogError("LocalizeStringEvent is not assigned.");
            return;
        }

        if (keys == null || keys.Length == 0)
        {
            Debug.LogError("Keys array is not assigned or empty.");
            return;
        }

        this.keys = keys;
        lines = new string[keys.Length];

        StartCoroutine(UpdateLocalizedStrings());
    }

    private IEnumerator UpdateLocalizedStrings()
    {
        int loadedCount = 0;

        for (int i = 0; i < keys.Length; i++)
        {
            int capturedIndex = i; // Necesario para capturar el índice correctamente en el delegado

            bool isUpdated = false;
            localizeStringEvent.StringReference.SetReference(tablename, keys[i]);
            localizeStringEvent.OnUpdateString.AddListener((localizedString) =>
            {
                lines[capturedIndex] = localizedString;
                Debug.Log($"Line {capturedIndex} updated: {localizedString}");
                isUpdated = true;
            });

            // Forzar la actualización inicial
            localizeStringEvent.RefreshString();

            // Esperar hasta que la línea se haya actualizado
            yield return new WaitUntil(() => isUpdated);
            localizeStringEvent.OnUpdateString.RemoveAllListeners();

            loadedCount++;
            if (loadedCount == keys.Length)
            {
                OnLocalizationReady?.Invoke();
            }
        }
    }

    public string[] GetLocalizedLines()
    {
        return lines;
    }
}
