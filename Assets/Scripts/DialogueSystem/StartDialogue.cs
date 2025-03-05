using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] DialogManager manager;
    [SerializeField] DialogueNode startNode;
     
    void Start()
    {
      Invoke("StartGame",1f);
    }

    void StartGame()
    {
        manager.StartDialogue(startNode); 
    }
   
}
