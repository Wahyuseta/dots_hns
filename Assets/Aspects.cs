using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct MovingCubeAspect : IAspect
{
    public readonly RefRW<LocalTransform> localTransform;
    public readonly RefRO<EnemyComponent> enemyComponent;
}
public readonly partial struct PlayerAspect : IAspect
{
    public readonly RefRW<LocalTransform> localTransform;
    public readonly RefRW<PhysicsVelocity> rigidbodyVelocity;
    public readonly RefRO<PlayerComponent> playerComponent;
    public readonly RefRO<Players> playerTagComponent;

    public void Move(float deltaTime, float2 dir)
    {
        localTransform.ValueRW.Position.xz += dir * deltaTime;
        //rigidbodyVelocity.ValueRW.Linear.xz += dir * deltaTime;

        if (math.lengthsq(dir) > float.Epsilon)
        {
            var forward = new float3(dir.x, 0f, dir.y);
            localTransform.ValueRW.Rotation = quaternion.LookRotation(forward, math.up());
        }
    }

    private float3 PosMultiplier()
    {
        float3 value = float3.zero;

        value.x = localTransform.ValueRO.Position.x > 0 ? 1 : -1;
        value.y = localTransform.ValueRO.Position.y > 0 ? 1 : -1;
        value.z = localTransform.ValueRO.Position.z > 0 ? 1 : -1;

        return value;
    }
}
