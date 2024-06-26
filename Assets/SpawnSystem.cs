using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public delegate void OnMove(float pow, bool hor);
public delegate void OnAction();
public partial class SpawnSystem : SystemBase
{
    public OnAction onAction;
    public event EventHandler onUnityEvent;
    public bool shouldSpawn = false;

    public NativeArray<Entity> entities;

    protected override void OnCreate()
    {
        RequireForUpdate<SpawnCubeConfig>();
    }

    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var item in SystemAPI.Query<RefRO<SpawnCubeConfig>>().WithDisabled<Stunned>())
            {
                shouldSpawn = true;
            }
        }

        if (shouldSpawn)
        {
            var spawnCubeCon = SystemAPI.GetSingleton<SpawnCubeConfig>();

            var buffer = new EntityCommandBuffer(WorldUpdateAllocator);

            for (int i = 0; i < spawnCubeCon.amount; i++)
            {
                var entity = buffer.Instantiate(spawnCubeCon.cubePrefab);
                var pos = new float3(UnityEngine.Random.Range(-100f, 100f), UnityEngine.Random.Range(-20f, 50f), UnityEngine.Random.Range(-50f, 50f));
                buffer.SetComponent(entity, new LocalTransform
                {
                    Position = pos,
                    Rotation = Quaternion.identity,
                    Scale = 1f
                });
                onUnityEvent?.Invoke(pos, EventArgs.Empty);
            }

            buffer.Playback(EntityManager);

            shouldSpawn = false;
        }
    }
}
