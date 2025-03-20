using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static TMPro.TextMeshProUGUI;

public class DialogManager : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] TMP_Text speakerNameText;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject choicePanel;
    [SerializeField] Button choiceButtonPrefab;
    [SerializeField] Button progressButton;

    [Header("Protagonists")]
    [SerializeField] Image leftImage;
    [SerializeField] Image rightImage;
    [SerializeField] Image centerImage;


    [Header("Audio")]
    [SerializeField] AudioSource voiceAudioSource;
    [SerializeField] AudioSource effectAudioSource;

    [Header("Settings")]
    [SerializeField] float textSpeed = 0.05f;
    DialogueNode currentDialogNode;
    int currentLineIndex = 0;
    bool isTyping = false;

    void Start()

    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
        //ADD LISTENER TO PROGRESS BUTTON
        progressButton.onClick.AddListener(OnClickEvent);

    }

    public void StartDialogue(DialogueNode startNode)
    {
        dialoguePanel.SetActive(true);
        currentDialogNode = startNode;
        currentLineIndex = 0;
        DisplayCurrentLine();

    }

    void DisplayCurrentLine()
    {
        if (currentDialogNode == null || currentDialogNode.dialogLines.Length == 0)
        {
            //END DIALOGUE
            EndDialogue();
            return;
        }
        //CHECK IF THERE ARE MORE LINES TO DISPLAY

        if (currentLineIndex < currentDialogNode.dialogLines.Length)
        {
            DialogueLine line = currentDialogNode.dialogLines[currentLineIndex];
            speakerNameText.text = line.speakerName;
            dialogueText.text = line.dialogText;

            // GESTIONAREA PERSONAJELOR
            UpdateCharacter(leftImage, line.characterSprite, line.animationType);
            UpdateCharacter(centerImage, line.secondCharacterSprite, line.secondAnimationType);
            UpdateCharacter(rightImage, line.thirdCharacterSprite, line.thirdAnimationType);

            //PLAY AUDIO CLIPS

            PlayAudio(line);

            // ADD COROUTINE ANIMATE CHARACTER AND TYPE TEXT AFTERWARDS 
            StartCoroutine(AnimateAndType(line));
        }
        else
        {
            //END OF LINE, DISPLAY CHOICES
            if (currentDialogNode.choices.Length > 0)
            {
                DisplayChoices();
            }
            else
            {
                EndDialogue();
            }

        }

    }

    void UpdateCharacter(Image characterImage, Sprite newSprite, DialogueAnimation animationType)
    {
        if (characterImage == null) return;

        Animator animator = characterImage.GetComponent<Animator>();

        if (newSprite != null)
        {
            characterImage.sprite = newSprite;
            characterImage.color = Color.white;
            characterImage.gameObject.SetActive(true);
        }

        if (animationType != DialogueAnimation.None && animator != null)
        {
            ApplyAnimation(animator, animationType);
        }
    }
    void ApplyAnimation(Animator animator, DialogueAnimation animationType)
    {
        if (animator == null) return;

        switch (animationType)
        {
            case DialogueAnimation.LeftEnteringScene:
                animator.SetTrigger("leftEnter");
                break;
            case DialogueAnimation.LeftExitingScene:
                animator.SetTrigger("leftExit");
                break;
            case DialogueAnimation.RightEnteringScene:
                animator.SetTrigger("rightEnter");
                break;
            case DialogueAnimation.RightExitingScene:
                animator.SetTrigger("rightExit");
                break;
            case DialogueAnimation.CenterEnteringScene:
                animator.SetTrigger("centerEnter");
                break;
            case DialogueAnimation.Jumping:
                animator.SetTrigger("jump");
                break;
            case DialogueAnimation.Rotating:
                animator.SetTrigger("rotate");
                break;
            case DialogueAnimation.Scaling:
                animator.SetTrigger("scale");
                break;
            case DialogueAnimation.Shaking:
                animator.SetTrigger("shake");
                break;
            case DialogueAnimation.Floating:
                animator.SetTrigger("float");
                break;
            default:
                Debug.LogWarning($"Unhandled animation type {animationType}");
                break;
        }
    }


    IEnumerator AnimateAndType(DialogueLine line)
    {
        // AȘTEAPTĂ DURATA ANIMAȚIEI DACĂ EXISTĂ
        if (line.animationDuration > 0)
        {
             yield return StartCoroutine(TypeText(line.dialogText));
            yield return new WaitForSeconds(line.animationDuration);
        }

        // START WRITING TEXT
       

    }
    IEnumerator TypeText(string text)
    {
        if (isTyping) yield break;
        isTyping = true;
        dialogueText.text = "";//CLEAR OLD TEXT
        int visibleCharacterCount = 0; // TRACK VISIBLE CHARACTERS FOR TEXT MESH PRO
        for (int i = 0; i < text.Length; i++)
        {
            //FILTER RICH TEXT
            if (text[i] == '<')
            {
                int closingTagIndex = text.IndexOf('>', i);
                if (closingTagIndex != -1)
                {
                    //ADD THE ENTIRE TAG TO DIALOGUE TEXT INSTANTLY
                    dialogueText.text += text.Substring(i, closingTagIndex - i + 1);
                    i = closingTagIndex;//SKIP TO THE END OF THE TAG
                    continue;
                }
            }
            //ADD THE NEXT VISIBLE CHARACTERS
            dialogueText.text += text[i];
            visibleCharacterCount++;
            //ENSURE TEXT MESH PRO UPDATES PPROPERLY
            dialogueText.maxVisibleCharacters = visibleCharacterCount;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;//MARK TYPING COMPLETE
    }
    void PlayAudio(DialogueLine line)
    {

        //IF THERE IS SOMETHING PLAYING, STOP IT

        if (voiceAudioSource.isPlaying)
        {
            voiceAudioSource.Stop();
        }

        //PLAY VOICE TEXT
        if (line.spokenText != null)
        {
            voiceAudioSource.clip = line.spokenText;
            voiceAudioSource.Play();
        }

        //PLAY EFFECT AUDIO
        if (line.soundEffect != null)
        {
            effectAudioSource.clip = line.soundEffect;
            effectAudioSource.Play();
        }
    }

    public void OnClickEvent()
    {
        if (isTyping)
        {
            //SKIP TYPING
            StopAllCoroutines();
            //SHOW ALL TEXT LINE
            DialogueLine currentLine = currentDialogNode.dialogLines[currentLineIndex];
            dialogueText.text = currentLine.dialogText;//ASSIGN THE FULL DIALOGUE TEXT LINE
            dialogueText.maxVisibleCharacters = currentLine.dialogText.Length;
            isTyping = false;
            //STOP VOICE AUDIO

            if (voiceAudioSource.isPlaying)
            {
                voiceAudioSource.Stop();
            }
        }
        else
        {
            currentLineIndex++;
            DisplayCurrentLine();
        }


    }

    void DisplayChoices()
    {
        //DELETE ALL CHOICE BUTTONS
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.transform);
        }
        choicePanel.SetActive(true);
        //CREATE ALL BUTTONS FROM CHOICES
        foreach (Choice choice in currentDialogNode.choices)
        {
            Button choiceButton = Instantiate(choiceButtonPrefab, choicePanel.transform);
            choiceButton.GetComponentInChildren<TMP_Text>().text = choice.choiceText;
            choiceButton.onClick.AddListener(() => SelectChoice(choice));
        }
    }

    void SelectChoice(Choice choice)
    {
        if (choice.nextNode != null)
        {
            choicePanel.SetActive(false);
            StartDialogue(choice.nextNode);
        }
        else
        {
            //END THE DIALOGUE NODE 
            EndDialogue();

        }

    }

    void EndDialogue()
    {
        dialogueText.text = "";
        speakerNameText.text = "";
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
        //STOP ALL AUDIO
        if (effectAudioSource.isPlaying)
        {
            effectAudioSource.Stop();
        }

        if (voiceAudioSource.isPlaying)
        {
            voiceAudioSource.Stop();
        }
        Debug.Log("Dialogue has ended");
        //TRANSITION TO ANOTHER SCENE IF NEEDED
        if (currentDialogNode.IsUnityNull())
        {
            Debug.Log("nu sa gasit node");
        }
        Debug.Log(currentDialogNode.name);
        if (!string.IsNullOrEmpty(currentDialogNode.nextScene))
        {
            SceneTransitionManager.Instance.LoadSceneWithFade(currentDialogNode.nextScene);
        }

    }

   
}
