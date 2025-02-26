using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ClassSlot : MonoBehaviour
    {
        public TextMeshProUGUI classText;
        public Slider slider;
        public SchoolClassData classData;

        public void Display(SchoolClassData data)
        {
            classData = data;
            classText.text = data.displayName;
            slider.value = data.parameters.classLogicToMoodRatio;
        }
    }
}