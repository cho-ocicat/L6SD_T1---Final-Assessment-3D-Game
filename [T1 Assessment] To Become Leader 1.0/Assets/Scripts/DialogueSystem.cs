using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    //before start, disable DialogueGUI (tick off)
    
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    //the pop-up 'E'
    public GameObject dialogueGUI;
    //what sets the whole system as active or not
    public Transform dialogueBoxGUI;

    public float letterDelay = 1f;
    public float letterMultiplier = 3f;

    //get E key as input
    public KeyCode dialogueInput = KeyCode.E;

    //tied to NPC name in NPC_Interact1
    public string characterName;
    public string[] dialogueLines;

    public bool letterIsMultiplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    //no dialogue unless the player is in range
    public bool outOfRange = true;

    [SerializeField] AudioClip audioClip;
    AudioSource chatSfx;
    
    void Start()
    {
        chatSfx = GetComponent<AudioSource>();
        dialogueText.text = "";
    }

    void Update()
    {
        
    }

    public void EnterRangeOfNPC()
    {
        outOfRange = false;
        dialogueGUI.SetActive(true);

        if (dialogueActive == true)
        {
            //E will pop up until dialogueActive is true
            dialogueGUI.SetActive(false);
        }
    }

    public void NPCName()
    {
        outOfRange = false;
        //pop up the dialogue box
        dialogueBoxGUI.gameObject.SetActive(true);
        nameText.text = characterName;

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(StartDialogue());
            }
        }

        StartDialogue();
    }

    IEnumerator StartDialogue()
    {
        if(outOfRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            int dialogueIndex = 0;

            while(dialogueIndex < dialogueLength || !letterIsMultiplied)
            {
                if(!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialogueLines[dialogueIndex++]));

                    if(dialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                    }
                }
               yield return 0;
            }
            

            while (true)
            {
                if(Input.GetKeyDown(dialogueInput) && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }

            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
        }
    }

    IEnumerator DisplayString(string stringToDisplay)
    {
        if(outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int charIndex = 0;
            dialogueText.text = "";

            while (charIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[charIndex];
                charIndex++;

                if(charIndex < stringLength)
                {
                    if(Input.GetKey(dialogueInput))
                    {
                        //for every key press, letter display gets faster
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);
                        if (audioClip) chatSfx.PlayOneShot(audioClip, 0.5f);
                    }
                    else{
                        yield return new WaitForSeconds(letterDelay);
                        if(audioClip) chatSfx.PlayOneShot(audioClip, 0.5f);
                    }
                }
                else{
                    dialogueEnded = false;
                    break;
                }
            }

            while (true)
            {
                if(Input.GetKey(dialogueInput))
                {
                    break;
                }
                yield return 0;
            }
            

            dialogueEnded = false;
            letterIsMultiplied = false;
            dialogueText.text = "";
        }
    }

    public void DropDialogue()
    {
        dialogueGUI.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true) {
            {
                letterIsMultiplied = false;
                dialogueActive = false;
                StopAllCoroutines();
                dialogueGUI.SetActive(false);
                dialogueBoxGUI.gameObject.SetActive(false);
            }
        }
    }
}
