using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

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

[SerializeField] bool deactivateLeftImage;//FOR DEBUGGING
[SerializeField] bool deactivateRightImage;
[SerializeField] bool deactivateCenterImage;

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
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
        //ADD LISTENER TO PROGRESS BUTTON
        progressButton.onClick.AddListener(OnClickEvent);
        //HIDE THE IMAGES
        if(leftImage!= null && deactivateLeftImage)
        {
            leftImage.color = new Color32(255,255,255,0);
        }
        if(rightImage!= null && deactivateRightImage)
        {
            rightImage.color = new Color32(255,255,255,0);
        }
        if(centerImage!= null && deactivateCenterImage)
        {
            centerImage.color = new Color32(255,255,255,0);
        }
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
    if(currentDialogNode == null || currentDialogNode.dialogLines.Length == 0)
    {
        //END DIALOGUE
        return;
    }
        //CHECK IF THERE ARE MORE LINES TO DISPLAY

    if(currentLineIndex < currentDialogNode.dialogLines.Length)
    {
        DialogueLine line = currentDialogNode.dialogLines[currentLineIndex];
        speakerNameText.text = line.speakerName;
        dialogueText.text = line.dialogText;

      
        Image targetImage = GetTargetImage(line.targetImage);

        if(targetImage != null && line.characterSprite != null)
        {
            targetImage.sprite = line.characterSprite;
            targetImage.color = Color.white;
        }
            //PLAY AUDIO CLIPS
         
            PlayAudio(line);

            // ADD COROUTINE ANIMATE CHARACTER AND TYPE TEXT AFTERWARDS 
            StartCoroutine(AnimateAndType(line,targetImage));
    }   
        else
        {
            //END OF LINE, DISPLAY CHOICES
        }
    
}


  Image GetTargetImage(DialogueTarget targetImage)
    {
       switch(targetImage)
       {
            case DialogueTarget.LeftImage: return leftImage;
           
            case DialogueTarget.CenterImage: return centerImage;
           
            case DialogueTarget.RightImage: return rightImage;

            
            
            default:return null;
       }
    }
    IEnumerator AnimateAndType(DialogueLine line, Image targetImage)
    {
        //PERFORM AN ANIMATION BASEN ON LINE SETTINGS
        if(line.animationType != DialogueAnimation.None)
        {
        //PLAY ANIMATION
        yield return new WaitForSeconds(line.animationDuration);
        }
        //START WRITING TEXT

        yield return StartCoroutine(TypeText(line.dialogText));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text  = "";//CLEAR OLD TEXT
        int visibleCharacterCount = 0; // TRACK VISIBLE CHARACTERS FOR TEXT MESH PRO
        for(int i = 0; i < text.Length;i++)
        {
            //FILTER RICH TEXT
            if(text[i] == '<')
            {
                int closingTagIndex = text.IndexOf('>',i);
                if(closingTagIndex != -1)
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

        if(voiceAudioSource.isPlaying)
        {
            voiceAudioSource.Stop();
        }
     
        //PLAY VOICE TEXT
        if(line.spokenText != null)
        { 
            voiceAudioSource.clip = line.spokenText;
            voiceAudioSource.Play();
        }
   
        //PLAY EFFECT AUDIO
        if(line.soundEffect != null)
        {
            effectAudioSource.clip = line.soundEffect;
            effectAudioSource.Play();
        }
    }

    public void OnClickEvent()
    {
        if(isTyping)
        {
            //SKIP TYPING
            StopAllCoroutines();
            //SHOW ALL TEXT LINE
            DialogueLine currentLine = currentDialogNode.dialogLines[currentLineIndex];
            dialogueText.text = currentLine.dialogText;//ASSIGN THE FULL DIALOGUE TEXT LINE
            dialogueText.maxVisibleCharacters = currentLine.dialogText.Length;
            isTyping = false;
            //STOP VOICE AUDIO
           
            if(voiceAudioSource.isPlaying)
            {
                 voiceAudioSource.Stop();
            }
        }
        
            currentLineIndex++;
            DisplayCurrentLine();
        
    }

    void DisplayChoices()
    {
        //DELETE ALL CHOICE BUTTONS

    }

}
