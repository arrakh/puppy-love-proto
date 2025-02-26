using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ActivitySlot : MonoBehaviour
    {
        public TextMeshProUGUI buttonText;
        public Button button;
        public int slotIndex;
        
        public ActivityData activityData = null;

        public bool IsEmpty => activityData == null;
        
        public void DisplayEmpty()
        {
            activityData = null;
            button.gameObject.SetActive(true);
            buttonText.text = "Pick Activity";
        }
        
        public void Display(ActivityData data)
        {
            activityData = data;
            buttonText.text = data.displayName;
        }
    }
}