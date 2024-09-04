using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorStates : MonoBehaviour
{
    public CursorLockMode clm;

    private void Start()
    {
        Cursor.lockState = clm;
    }
}
