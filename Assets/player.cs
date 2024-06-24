using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;

public class player : MonoBehaviour
{
    private class playerbaker : Baker<player>
    {
        public override void Bake(player authoring)
        {
            var entity = GetEntity(TransformUsageFlags.ManualOverride);
            AddComponent(entity, new PlayerComponent { MovSpeed = 2 });
            AddComponent(entity, new Players());

        }
    }
}

public struct Players : IComponentData { }
