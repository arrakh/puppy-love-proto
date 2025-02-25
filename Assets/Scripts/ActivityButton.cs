using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ActivityButton : MonoBehaviour
    {
        public ActivityData data;
        public Button button;

        public string Id => data.activityId;
    }
}