using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAppeared : MonoBehaviour
{
    [SerializeField] ObjectOnScreenDetector oosd;
    private void OnEnable()
    {
        StartCoroutine(EnableOOSD());
    }

    IEnumerator EnableOOSD()
    {
        yield return new WaitForSeconds(3);
        oosd.enabled = true;
        Destroy(this);
    }
}