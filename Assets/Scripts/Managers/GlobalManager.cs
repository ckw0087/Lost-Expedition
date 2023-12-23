using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }

    [field:SerializeField] public NetworkRunnerController NetworkRunnerController { get; private set; }

    [SerializeField] private GameObject parentObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(parentObject);
        }
    }
}
