using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;

#region custom editor value
public enum ObjectType
{
    None,
    RectTransform,
    GridLayout,
    HorizontalLayout
}

[System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
public sealed class ShowIfAttribute : PropertyAttribute
{
    public string ConditionalSourceField;
    public ObjectType ExpectedValue;

    public ShowIfAttribute(string ConditionalSourceField, ObjectType expectedValue)
    {
        this.ConditionalSourceField = ConditionalSourceField;
        this.ExpectedValue = expectedValue;
    }
}


[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
#if UNITY_EDITOR
        ShowIfAttribute condHAtt = (ShowIfAttribute)attribute;
        bool enabled = GetConditionalSourceField(property, condHAtt);
        GUI.enabled = enabled;

        if (enabled)
            EditorGUI.PropertyField(position, property, label, true);
#endif
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
#if UNITY_EDITOR
        ShowIfAttribute condHAtt = (ShowIfAttribute)attribute;
        bool enabled = GetConditionalSourceField(property, condHAtt);

        return enabled ? EditorGUI.GetPropertyHeight(property, label, true) : 0;
#else
        return 0f;
#endif
    }

    private bool GetConditionalSourceField(SerializedProperty property, ShowIfAttribute condHAtt)
    {
#if UNITY_EDITOR
        bool enabled = false;
        string propertyPath = property.propertyPath;
        string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField);
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

        if (sourcePropertyValue != null)
        {
            enabled = (int)condHAtt.ExpectedValue == sourcePropertyValue.enumValueIndex;
        }

        return enabled;
#else
        return false;
#endif
    }
}

[Serializable]
public class GridLayoutToAdjust
{
    public GridLayoutGroup layout;
    public int abc;
    public int aabc;
    public int abasc;
    public int abdasdc;
    public int aasdbc;
    public int abasdc;
    public int abasagsdc;
    public int abasfsc;
}

[Serializable]
public class HorizontalLaoutToAdjust
{
    public HorizontalLayoutGroup layout;
    public int abc;
    public int aabc;
    public int abasc;
    public int abdasdc;
    public int aasdbc;
    public int abasdc;
    public int abasagsdc;
    public int abasfsc;
}

[Serializable]
public class ElementToAdjust
{
    [SerializeField] ObjectType type;
    [ShowIf("type", ObjectType.HorizontalLayout)]
    [SerializeField]
    private HorizontalLaoutToAdjust verticalLayoutToAdjust;
    [ShowIf("type", ObjectType.GridLayout)]
    [SerializeField]
    private GridLayoutToAdjust gridLayoutToAdjust;
}

#endregion
public class Manager : MonoBehaviour
{
    public List<ElementToAdjust> elementToAdjusts = new List<ElementToAdjust>();


    //private NativeArray<Entity> entities;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    entities = new NativeArray<Entity>(1000, Allocator.Persistent);

    //    EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;

    //    EntityArchetype enemy = manager.CreateArchetype(typeof(EnemyComponent), typeof(LocalTransform), typeof(RenderMesh));

    //    manager.CreateEntity(enemy, entities);
        
    //}

    //private void OnDestroy()
    //{
    //    entities.Dispose();
    //}

    private class enemyBaker : Baker<Manager>
    {
        public override void Bake(Manager authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyComponent { speed = 10 });
        }
    }
}
