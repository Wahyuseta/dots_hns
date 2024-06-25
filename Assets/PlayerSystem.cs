using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

[BurstCompile]
public partial struct PlayerSystem : ISystem
{
    void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerComponent>();
    }

    void OnDestroy(ref SystemState state)
    {
    }

    void OnUpdate(ref SystemState state)
    {
        var moveJob = new MoveJob
        {
            deltaTime = SystemAPI.Time.DeltaTime,
            dir = InputManager.moveInputData
        };

        moveJob.ScheduleParallel();
    }

    [BurstCompile]
    public partial struct MoveJob : IJobEntity
    {
        public float deltaTime;
        public float2 dir;

        public void Execute(PlayerAspect playerAspect)
        {
            deltaTime *= 20f;
            playerAspect.Move(deltaTime, dir);
        }
    }
}
