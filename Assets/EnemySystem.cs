using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Burst;
using Unity.Physics;

public partial struct EnemySystem : ISystem
{
    void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemyComponent>();
    }


    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //foreach ((RefRO<EnemyComponent> enemy, RefRW<LocalTransform> transform) in SystemAPI.Query<RefRO<EnemyComponent>, RefRW<LocalTransform>>())
        //{
        //    transform.ValueRW = transform.ValueRO.RotateY(enemy.ValueRO.speed * SystemAPI.Time.DeltaTime);
        //}


        var trigger = SystemAPI.GetSingleton<SimulationSingleton>().AsSimulation().TriggerEvents;
        

        var rotateJobi = new RotateJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        rotateJobi.ScheduleParallel();
    }

    [BurstCompile]
    public partial struct RotateJob : IJobEntity
    {
        public float deltaTime;

        public void Execute(in EnemyComponent enemy, ref LocalTransform transform)
        {
            transform = transform.RotateY(enemy.speed * deltaTime);
        }
    }

    public partial struct DetectAttackARange : IJobEntity
    {
        public void Execute(in SimulationSingleton triggerEvent)
        {
            foreach (var item in triggerEvent.AsSimulation().TriggerEvents)
            {

            }
        }
    }
}
