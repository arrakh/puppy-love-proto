using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizController : MonoBehaviour
{
    public float baseTimer = 20f;
    public int quizCount = 3;
    public int passingGrade = 2;
    public int otherAnswersCount = 4;
    public int fakeAnswersCount = 3;
    public int colorCount = 4;

    public BrainstormAnswer brainstormAnswer;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI resultText;
    public GameObject finalScreen;
    public GameObject quizHolder;
    public Image questionBg;
    public QuizEntries currentEntry;
    public Timer timer;
    public Color[] colors;
    public string[] stressDistractions;

    private Color currentColor;

    private Queue<(Quiz, Color)> quizQueue = new();
    private Quiz currentQuiz;
    private List<QuizGameAnswer> answerPool = new(); 

    private TaskCompletionSource<string> quizAnswerTsc = new();
    private int correctCount, wrongCount;

    public bool HasPassed => correctCount >= passingGrade;
    
    public string CurrentAnswer => currentQuiz.answer;

    public IEnumerator StartQuiz(QuizParameters parameters, QuizEntries entries)
    {
        quizHolder.gameObject.SetActive(true);

        ApplyParameters(parameters);
        brainstormAnswer.ApplyParameters(parameters);
        
        currentEntry = entries;

        correctCount = 0;
        wrongCount = 0;
        
        finalScreen.gameObject.SetActive(false);

        InitializeQuiz(currentEntry);

        UpdateStatus();
        
        while (quizQueue.Count > 0)
        {
            quizAnswerTsc = new();
            var (quiz, color) = quizQueue.Dequeue();
            currentQuiz = quiz;
            questionBg.color = color;
            questionText.text = currentQuiz.question;

            timer.Set(baseTimer);
            
            brainstormAnswer.ClearAnswers();
            
            yield return new WaitUntil(HasProgressedQuiz);

            if (timer.IsCompleted()) wrongCount++;
            else
            {
                var answer = quizAnswerTsc.Task.Result;
                var isCorrect = answer.Equals(currentQuiz.answer, StringComparison.InvariantCultureIgnoreCase);
                if (isCorrect) correctCount++;
                else wrongCount++;
            }

            UpdateStatus();
        }
        
        timer.Set(0f);

        brainstormAnswer.ClearAll();
        finalScreen.gameObject.SetActive(true);
        resultText.text = $"{correctCount} / {quizCount} correct, " + (HasPassed ? "<color=green>You Passed!" : "<color=red>You Failed!");
        
        yield return new WaitForSeconds(3f);
    }

    private void ApplyParameters(QuizParameters parameters)
    {
        baseTimer = parameters.baseTimer;
        quizCount = parameters.quizCount;
        passingGrade = parameters.passingGrade;
        otherAnswersCount = parameters.otherAnswersCount;
        fakeAnswersCount = parameters.fakeAnswersCount;
        colorCount = parameters.colorCount;
    }

    private void UpdateStatus()
    {
        var progress = wrongCount + correctCount + 1;
        statusText.text = $"Question #{progress} / {quizCount} " +
                          $"| Passing Grade: {passingGrade} correct" +
                          $"| <color=green>Correct: {correctCount} </color>" +
                          $"| <color=red>Wrong: {wrongCount} </color>";
    }

    private void InitializeQuiz(QuizEntries quizEntries)
    {
        answerPool.Clear();
        quizQueue.Clear();

        var shuffled = quizEntries.entries
            .OrderBy(_ => Random.value).ToList();

        var randomColors = colors.OrderBy(_ => Random.value).ToList();
        
        var length = Mathf.Min(shuffled.Count, quizCount + fakeAnswersCount);

        for (var i = 0; i < length; i++)
        {
            var entry = shuffled[i];

            var randomColor = randomColors[i % randomColors.Count];
            
            answerPool.Add(new (entry.answer, randomColor));

            if (i >= quizCount) continue;
            quizQueue.Enqueue((entry, randomColor));
        }
    }

    private bool HasProgressedQuiz() => timer.IsCompleted() || quizAnswerTsc.Task.IsCompleted;

    public void Answer(string answer) => quizAnswerTsc?.SetResult(answer);

    public List<QuizGameAnswer> GetAnswerPool() => answerPool;

    public Color GetCurrentColor() => currentColor;

    public void Clear()
    {
        finalScreen.gameObject.SetActive(false);
        questionText.text = String.Empty;
        quizHolder.gameObject.SetActive(false);
    }
}
