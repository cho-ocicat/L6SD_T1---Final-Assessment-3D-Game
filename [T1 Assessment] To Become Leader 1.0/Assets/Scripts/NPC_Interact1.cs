using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPC_Interact1 : MonoBehaviour
{
    public Transform dialogueBox;
    public Transform characterNPC;

    //call the script
    DialogueSystem dialogueSystem;

    public string nameNPC;

    //area of the text, 5 lines down, 10 characters right across
    [TextArea(5,10)]
    public string[] sentences;

    void Start()
    {
        //find and call the script
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    void Update()
    {
        //select chat and put above the speaking character
        //make sure to use camera with MainCamera tag for this to work
        Vector3 Pos = Camera.main.WorldToScreenPoint(characterNPC.position);
        //change dialogue box position to above the character
        Pos.x += 700;
        dialogueBox.position = Pos;
    }

    //The function is on the physics timer so it won't necessarily run every frame. This message is sent to the trigger and the collider that touches the trigger (one must have RigidBody). Trigger events will be sent to disabled MonoBehaviours, to allow enabling Behaviours in response to collisions.
    private void OnTriggerStay(Collider other) 
    {
        //make sure the script on the NPC is off at first
        this.gameObject.GetComponent<NPC_Interact1>().enabled = true;

        FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();

        if((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.E))
        {
            this.gameObject.GetComponent<NPC_Interact1>().enabled = true;
            dialogueSystem.characterName = nameNPC;
            dialogueSystem.dialogueLines = sentences;
            FindObjectOfType<DialogueSystem>().NPCName();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        FindObjectOfType<DialogueSystem>().OutOfRange();
        this.gameObject.GetComponent<NPC_Interact1>().enabled = false;
    }
}
