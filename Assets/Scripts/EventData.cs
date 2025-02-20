using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class EventData : ScriptableObject
    {
        public string displayName;
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(displayName)) displayName = name;
        }
        #endif
    }
}