// using System.Collections;
// using UnityEngine;
// using TMPro;

// public class DialogueManager : MonoBehaviour
// {
//     public TextMeshProUGUI textComponent;
//     public LocalizationController localizationController;
//     public GameObject dialoguePanel;
//     public float textSpeed;
//     public string[] dialogueKeys;

//     private string[] lines;
//     private int index;

//     void Start()
//     {
//         if (textComponent == null)
//         {
//             Debug.LogError("TextMeshProUGUI component is not assigned.");
//             return;
//         }

//         if (localizationController == null)
//         {
//             Debug.LogError("LocalizationController is not assigned.");
//             return;
//         }

//         textComponent.text = string.Empty;
//         localizationController.OnLocalizationReady += OnLocalizationReady;
//     }

//     void OnLocalizationReady()
//     {
//         lines = new string[dialogueKeys.Length];
//         for (int i = 0; i < dialogueKeys.Length; i++)
//         {
//             lines[i] = localizationController.GetLocalizedText(dialogueKeys[i]);
//         }
//         StartDialogue();
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.P))
//         {
//             if (textComponent.text == lines[index])
//             {
//                 NextLine();
//             }
//             else
//             {
//                 StopAllCoroutines();
//                 textComponent.text = lines[index];
//             }
//         }
//     }

//     void StartDialogue()
//     {
//         if (lines == null || lines.Length == 0)
//         {
//             Debug.LogError("Lines array is not initialized or empty.");
//             return;
//         }

//         dialoguePanel.SetActive(true);
//         index = 0;
//         Time.timeScale = 0f; // Pausar el tiempo del juego
//         StartCoroutine(TypeLine());
//     }

//     IEnumerator TypeLine()
//     {
//         if (lines == null || lines.Length == 0 || lines[index] == null)
//         {
//             Debug.LogError("Current line is null or lines array is not initialized properly.");
//             yield break;
//         }

//         float elapsed = 0f;
//         foreach (char c in lines[index].ToCharArray())
//         {
//             textComponent.text += c;
//             elapsed = 0f;
//             while (elapsed < textSpeed)
//             {
//                 elapsed += Time.unscaledDeltaTime; // Usar Time.unscaledDeltaTime para ignorar el timeScale
//                 yield return null;
//             }
//         }
//     }

//     void NextLine()
//     {
//         if (index < lines.Length - 1)
//         {
//             index++;
//             textComponent.text = string.Empty;
//             StartCoroutine(TypeLine());
//         }
//         else
//         {
//             dialoguePanel.SetActive(false);
//             Time.timeScale = 1f; // Reanudar el tiempo del juego
//         }
//     }
// }
