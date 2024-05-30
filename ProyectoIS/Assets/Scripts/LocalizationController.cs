using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class LocalizationController : MonoBehaviour
{
    public LocalizeStringEvent localizeStringEvent;
    private string[][] keys;
    private string[][] lines;
    public event Action OnLocalizationReady;

    public void InitializeKeys(string[][] keys)
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
        lines = new string[keys.Length][];
        for (int i = 0; i < keys.Length; i++)
        {
            lines[i] = new string[keys[i].Length];
        }

        StartCoroutine(UpdateLocalizedStrings());
    }

    private IEnumerator UpdateLocalizedStrings()
    {
        int loadedCount = 0;

        for (int i = 0; i < keys.Length; i++)
        {
            for (int j = 0; j < keys[i].Length; j++)
            {
                int capturedDialogueIndex = i;
                int capturedLineIndex = j;

                bool isUpdated = false;
                localizeStringEvent.StringReference.SetReference("Tutorial", keys[i][j]);
                localizeStringEvent.OnUpdateString.AddListener((localizedString) =>
                {
                    lines[capturedDialogueIndex][capturedLineIndex] = localizedString;
                    Debug.Log($"Line {capturedDialogueIndex}-{capturedLineIndex} updated: {localizedString}");
                    isUpdated = true;
                });

                // Force the initial update
                localizeStringEvent.RefreshString();

                // Wait until the line is updated
                yield return new WaitUntil(() => isUpdated);
                localizeStringEvent.OnUpdateString.RemoveAllListeners();
            }

            loadedCount++;
            if (loadedCount == keys.Length)
            {
                OnLocalizationReady?.Invoke();
            }
        }
    }

    public string[][] GetLocalizedLines()
    {
        return lines;
    }
}
