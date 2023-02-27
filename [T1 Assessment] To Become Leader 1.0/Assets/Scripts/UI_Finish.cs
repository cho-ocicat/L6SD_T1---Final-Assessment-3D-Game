using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            GameObject.Find("TimerManager").SendMessage("Finish");
        }
    }
}
