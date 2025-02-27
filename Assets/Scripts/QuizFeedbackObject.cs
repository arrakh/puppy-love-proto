using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class QuizFeedbackObject : MonoBehaviour
    {
        public Animation anim;
        public Image icon;
        public AnswerButton answerButton;
        public Sprite correct, incorrect;

        public void Display(bool isCorrect, AnswerButton toCopy)
        {
            transform.position = toCopy.transform.position;
            icon.sprite = isCorrect ? correct : incorrect;
            answerButton.CopyVisual(toCopy);
            
            anim.Stop();
            anim.Play();
        }
    }
}