using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Events : MonoBehaviour
{
    public static Day2Events Instance;
    public GameObject mannequinDoor;
    [SerializeField] List<GameObject> mannequinTriggers = new List<GameObject>();

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
        mannequinDoor.GetComponent<Animator>().SetTrigger("QuickOpen");

        StartCoroutine(ActivateMannequinCrawl());
    }

    IEnumerator ActivateMannequinCrawl()
    {
        yield return new WaitForSeconds(1);
        foreach(GameObject obj in mannequinTriggers)
        {
            obj.SetActive(true);
        }
    }

    public void ShutMannequinDoor()
    {
        mannequinDoor.GetComponent<Animator>().SetTrigger("Shut");
        mannequinDoor.tag = "Door";
    }
}
