using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Activity Database")]
    public class ActivityDatabase : ScriptableObject
    {
        public ActivityData[] allActivities;

        private Dictionary<string, ActivityData> dict = new();

        public void Initialize()
        {
            dict = new();
            foreach (var activity in allActivities)
                dict[activity.activityId] = activity;
        }

        public ActivityData Get(string id)
        {
            if (!dict.TryGetValue(id, out var activity))
                throw new Exception($"TRYING TO GET ACTIVITY WITH ID {id} BUT CANT FIND");

            return activity;
        }
    }
}