using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueNode", menuName ="Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    public DialogueLine[] dialogLines;
    public Choice[] choices;
    public string nextScene;// name of the next scene to be loaded
}

[System.Serializable]
public class Choice
{
 public string choiceText; // text for choices displayed on the button
 public DialogueNode nextNode;
}
