using System;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueLine", menuName ="Dialogue/Line")]
public class DialogueLine : ScriptableObject
{
public string speakerName;
[TextArea(3,5)]public string dialogText;

[Header("Left Character")]
public Sprite characterSprite;

[Header("Left Character Animation")]
public float animationDuration = 1;
public DialogueAnimation animationType;


[Header("Center Character")]
public Sprite secondCharacterSprite;


[Header("Center Character Animation ")]
public float secondAnimationDuration = 1;
public DialogueAnimation secondAnimationType;


[Header("Right Character")]
public Sprite thirdCharacterSprite;

[Header("Right Character Animation")]
public float thirdAnimationDuration = 1;
public DialogueAnimation thirdAnimationType;


[Header("Audio")]
public AudioClip spokenText;
public AudioClip soundEffect;
}

public enum DialogueAnimation{
    None,
    LeftEnteringScene,
    LeftExitingScene,
    CenterEnteringScene,
    RightEnteringScene,
    RightExitingScene,  
    Jumping,
    Rotating,
    Scaling,
    Shaking,
    Floating
}
