using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateH2Sounds : MonoBehaviour
{
    public void Activate() 
    {
        StartCoroutine(DelayActivation());
    }

    IEnumerator DelayActivation()
    {
        yield return new WaitForSeconds(10);
        Day1Events.Instance.ActivateH2Sound();
    }
}
