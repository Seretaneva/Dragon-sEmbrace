using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] DialogManager manager;
    [SerializeField] DialogueNode startNode;
    [SerializeField] float timeBeforeDialogStart = 1.0f;
     
    void Start()
    {
      
      Invoke("StartGame",timeBeforeDialogStart);
    }

    void StartGame()
    {
        manager.StartDialogue(startNode); 
    }
   
}
