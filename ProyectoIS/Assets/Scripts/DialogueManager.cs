using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class LocalizationController : MonoBehaviour
{
    public LocalizeStringEvent[] localizedStrings;

    private string[] lines;
    public event Action OnLocalizationReady;

    void Awake()
    {
        if (localizedStrings == null || localizedStrings.Length == 0)
        {
            Debug.LogError("LocalizedStringEvent array is not assigned or empty.");
            return;
        }

        lines = new string[localizedStrings.Length];
        int loadedCount = 0;

        for (int i = 0; i < localizedStrings.Length; i++)
        {
            int capturedIndex = i; // Necesario para capturar el índice correctamente en el delegado
            localizedStrings[i].OnUpdateString.AddListener((localizedString) =>
            {
                lines[capturedIndex] = localizedString;
                Debug.Log($"Line {capturedIndex} updated: {localizedString}");

                // Verificar si todas las líneas están cargadas
                loadedCount++;
                if (loadedCount == localizedStrings.Length)
                {
                    OnLocalizationReady?.Invoke();
                }
            });
            // Forzar la actualización inicial
            localizedStrings[i].RefreshString();
        }
    }

    public string[] GetLocalizedLines()
    {
        return lines;
    }
}
