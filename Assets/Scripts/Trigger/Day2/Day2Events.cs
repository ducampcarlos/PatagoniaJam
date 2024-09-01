using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Events : MonoBehaviour
{
    public static Day2Events Instance;
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
}
