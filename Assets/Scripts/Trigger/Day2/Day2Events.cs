using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Events : MonoBehaviour
{
    public static Day2Events Instance;
    public GameObject mannequinDoor;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartMannequinSounds()
    {
        //Sonidos de Maniqui
        mannequinDoor.GetComponent<Animator>().SetTrigger("Open");

        
    }

    IEnumerator ActivateMannequinCrawl()
    {
        yield return new WaitForSeconds(1);
        //Activar Trigger Maniqui corriendo
    }
}
