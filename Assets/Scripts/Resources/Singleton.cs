using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    public Resources Resources { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
            
        Instance = this;
        Resources = GetComponent<Resources>();
    }
}
