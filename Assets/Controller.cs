using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class Controller : MonoBehaviour
{
    public GameObject objectTospawn;

    private void Start()
    {
        var SpawnSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SpawnSystem>();

        SpawnSystem.onUnityEvent += OnActionCall;
    }

    private void OnActionCall(object enti, System.EventArgs e)
    {
        var entity = (float3)enti;
        Instantiate(objectTospawn, new Vector3(entity.x, entity.y, entity.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
