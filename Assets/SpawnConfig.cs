using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;

public class SpawnConfig : MonoBehaviour
{
    public GameObject cube;
    public int amount;

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
        }
    }
}

public struct SpawnCubeConfig : IComponentData
{
    public Entity cubePrefab;
    public int amount;
}
