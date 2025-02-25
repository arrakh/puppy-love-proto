using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class EventData : ScriptableObject
    {
        public string displayName;
        
        #if UNITY_EDITOR
        protected virtual void OnEventValidate() {}
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(displayName)) displayName = name;
            OnEventValidate();
        }
        #endif
    }
}