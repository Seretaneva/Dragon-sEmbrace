using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueLine", menuName ="Dialogue/Line")]
public class DialogueLine : ScriptableObject
{
public string speakerName;
[TextArea(3,5)]public string dialogText;
public Sprite characterSprite;

public DialogueTarget dialogueTarget;

[Header("Audio")]
public AudioClip spokenText;
public AudioClip soundEffect;

[Header("Animation")]
public float animationDuration = 1;

public DialogueAnimation animationType;

}

public enum DialogueTarget
{
LeftImage,
RightImage,
CenterImage
}

public enum DialogueAnimation{
    
    None,
    EnteringScene,
    ExitingScene,
    Jumping,
    Rotating,
    Scaling,
    Shaking


}