using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Fall : MonoBehaviour
{
    public float fallDelay;
    public float destroyDelay;
    bool isFalling = false;
    float downSpeed = 0;
    [SerializeField] AudioSource crackSfx;
    public Material woodCrack;
    public GameObject platform;

    private void Update() {
        if(isFalling)
        {
            downSpeed += Time.deltaTime/20;
            transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            platform.GetComponent<MeshRenderer>().material = woodCrack;
            crackSfx.Play();
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall(){
        yield return new WaitForSeconds(fallDelay);
        isFalling = true;
        crackSfx.Play();
        Destroy(gameObject, destroyDelay);
    }
}
