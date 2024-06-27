using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Events;

public class SpawnConfig : MonoBehaviour
{
    public GameObject cube;
    public int amount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < amount; i++)
            {
                var pos = new Vector3(UnityEngine.Random.Range(-100f, 100f), UnityEngine.Random.Range(-2f, 5f), UnityEngine.Random.Range(-50f, 50f));
                Instantiate(cube, pos, Quaternion.identity);
            }
        }
    }

    public class Baker : Baker<SpawnConfig>
    {
        public override void Bake(SpawnConfig config)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new SpawnCubeConfig
            {
                cubePrefab = GetEntity(config.cube, TransformUsageFlags.Dynamic),
                amount = config.amount
            }); ;

            AddComponent(entity, new EnemyComponent
            {

            }); ;
        }
    }
}

public struct SpawnCubeConfig : IComponentData
{
    public Entity cubePrefab;
    public int amount;
    public NativeArray<Entity> entities;
    public DynamicBuffer<Entity> spawnnedEntities;
}
