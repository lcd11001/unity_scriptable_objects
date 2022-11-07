using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public interface IDestroyableItem
    {

    }

    public interface IItemAction
    {
        public string ActionName {get; }
        public AudioClip ActionSFX {get; }
        bool PerformAction(GameObject character);
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float value;
    }

    [CreateAssetMenu]
    public class ConsumableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();

        public string ActionName => "Consume";

        public AudioClip ActionSFX {get; private set;}

        public bool PerformAction(GameObject character)
        {
            foreach (var data in modifierData)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }
            return true;
        }
    }
}
