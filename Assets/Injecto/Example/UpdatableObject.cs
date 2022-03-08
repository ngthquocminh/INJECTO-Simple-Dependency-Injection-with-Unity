using Injecto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatableObject : IUpdatable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IUpdatable.Update()
    {
        Debug.Log("Hello! I am updating...");
    }
}
