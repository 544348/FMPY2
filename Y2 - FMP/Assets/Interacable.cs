using UnityEngine;

public class Interacable : MonoBehaviour
{
    public enum theType{button, computer, interactable}
    public theType interactableType = theType.interactable;


    void Start()
    {
        interactableType = theType.computer;
        if(interactableType == theType.computer)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
