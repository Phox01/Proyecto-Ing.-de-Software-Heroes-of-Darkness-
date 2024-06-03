using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

[Serializable]
public class DialogueKeys
{
    public List<string> keys; 
}

public class LocalizationController : MonoBehaviour
{
    public LocalizeStringEvent localizeStringEvent;
    public List<DialogueKeys> dialoguesKeysList; 
    private List<string[]> dialogues;
    public event Action OnLocalizationReady;

    public string tablename;

    void Start()
    {
        dialogues = new List<string[]>();
        InitializeKeys();
    }

    public void InitializeKeys()
    {
        if (localizeStringEvent == null)
        {
            Debug.LogError("LocalizeStringEvent is not assigned.");
            return;
        }

        if (dialoguesKeysList == null || dialoguesKeysList.Count == 0)
        {
            Debug.LogError("Dialogues keys list is not assigned or empty.");
            return;
        }

        StartCoroutine(UpdateLocalizedStrings());
    }

    private IEnumerator UpdateLocalizedStrings()
    {
        foreach (var dialogueKeys in dialoguesKeysList)
        {
            string[] lines = new string[dialogueKeys.keys.Count];
            int loadedCount = 0;

            for (int i = 0; i < dialogueKeys.keys.Count; i++)
            {
                int capturedIndex = i; 

                bool isUpdated = false;
                localizeStringEvent.StringReference.SetReference(tablename, dialogueKeys.keys[i]);
                localizeStringEvent.OnUpdateString.AddListener((localizedString) =>
                {
                    lines[capturedIndex] = localizedString;
                    isUpdated = true;
                });

                localizeStringEvent.RefreshString();

                yield return new WaitUntil(() => isUpdated);
                localizeStringEvent.OnUpdateString.RemoveAllListeners();

                loadedCount++;
                if (loadedCount == dialogueKeys.keys.Count)
                {
                    dialogues.Add(lines);
                }
            }
        }
        OnLocalizationReady?.Invoke();
    }

    public List<string[]> GetAllLocalizedLines()
    {
        return dialogues;
    }
}
