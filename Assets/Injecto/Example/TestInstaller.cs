using Injecto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstaller : MonoBehaviour
{
    private void Awake()
    {
        Injector.BindingTo<UpdatableObject>();


        Injector.Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
