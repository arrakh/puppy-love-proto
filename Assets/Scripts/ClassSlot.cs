using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ClassSlot : MonoBehaviour
    {
        public TextMeshProUGUI classText;
        public SchoolClassData classData;

        public void Display(SchoolClassData data)
        {
            classData = data;
            classText.text = data.displayName;
        }
    }
}