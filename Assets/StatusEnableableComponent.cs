using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class StatusEnableableComponent : MonoBehaviour
{
    public class Baker : Baker<StatusEnableableComponent>
    {
        public override void Bake(StatusEnableableComponent authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Stunned());
            SetComponentEnabled<Stunned>(entity, false);
        }
    }
}

public struct Stunned : IComponentData, IEnableableComponent
{

}
