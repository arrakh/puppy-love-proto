using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ActivitySlot : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI buttonText;
        public Button button;
        public int slotIndex;
        
        public ActivityData activityData = null;

        public bool IsEmpty => activityData == null;
        
        public void DisplayEmpty()
        {
            activityData = null;
            button.gameObject.SetActive(true);
            nameText.gameObject.SetActive(false);
            buttonText.text = "Pick Activity";
        }

        public void SetIsButton(bool isButton)
        {
            button.gameObject.SetActive(isButton);
            nameText.gameObject.SetActive(!isButton);
            buttonText.text = IsEmpty ? "Pick Activity" : activityData.displayName;
        }
        
        public void Display(ActivityData data)
        {
            activityData = data;
            buttonText.text = nameText.text = data.displayName;
        }
    }
}