using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager
{
    
[Header("UI Elements")]
[SerializeField] TMP_Text speakerNameText;
[SerializeField] TMP_Text dialogText;
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



}
