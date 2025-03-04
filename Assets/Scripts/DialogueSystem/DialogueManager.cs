using System;
using TMPro;
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
[SerializeField] AudioSource voiceSound;
[SerializeField] AudioSource effectSound;

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

    //CHECK IF THERE ARE MORE LINES TO DISPLAY
    if(currentLineIndex < currentDialogNode.dialogLines.Length)
    {
        DialogueLine line = currentDialogNode.dialogLines[currentLineIndex];
        speakerNameText.text = line.speakerName;
        dialogueText.text = line.dialogText;

        //TARGET IMAGE TO BE PLACED FROM LINE
        Image targetImage = GetTargetImage(line.targetImage);
        if(targetImage != null)
        {
            targetImage.sprite = line.characterSprite;
            targetImage.color = Color.white;

            //PLAY AUDIO CLIPS

            // ADD CORROUTINE
        }else
        {
            //END OF LINE, DISPLAY CHOICES
        }
    }

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
}
