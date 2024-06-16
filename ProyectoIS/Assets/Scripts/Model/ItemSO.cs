using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    // Start is called before the first frame update
    //public abstract class ItemSOSO : ScriptableObject
    //{
        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        //[field: SerializeField]
        //public List<ItemSOParameter> DefaultParametersList { get; set; }

    //}

    //[Serializable]
    //public struct ItemSOParameter : IEquatable<ItemSOParameter>
    //{
    //    public ItemSOParameterSO ItemSOParameter;
    //    public float value;

    //    public bool Equals(ItemSOParameter other)
    //    {
    //        return other.ItemSOParameter == ItemSOParameter;
    //    }
    //}
}
