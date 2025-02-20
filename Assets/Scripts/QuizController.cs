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
    public float baseTimer = 5f;
    public int quizCount = 10;
    public int otherAnswerCount = 7;
    public int fakeAnswerCount = 4;
    public int colorCount;
    
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI statusText;
    public Image questionBg;
    public QuizEntries currentEntry;
    public Timer timer;
    public Color[] colors;
    public string[] stressDistractions;
    
    private Color currentColor;

    private Queue<Quiz> quizQueue = new();
    private Quiz currentQuiz;
    private List<(string, bool)> answerPool = new(); 
    private List<Color> answerColorPool = new(); 

    private TaskCompletionSource<string> quizAnswerTsc = new();
    private int correctCount, wrongCount;

    public event Action onNewQuiz;
    
    public string CurrentAnswer => currentQuiz.answer;

    private IEnumerator StartQuiz()
    {
        QueueQuiz(currentEntry);
        
        UpdateStatus();
        
        while (quizQueue.Count > 0)
        {
            quizAnswerTsc = new();
            currentQuiz = quizQueue.Dequeue();
            questionText.text = currentQuiz.question;

            GenerateAnswerPool();
            DetermineColors();

            timer.Set(baseTimer);
            
            onNewQuiz?.Invoke();
            
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

        yield break;
    }

    private void DetermineColors()
    {
        currentColor = colors[Random.Range(0, colors.Length)];
        answerColorPool = colors.
            Where(x => !x.Equals(currentColor))
            .OrderBy(_ => Random.value).Take(colorCount).ToList();

        questionBg.color = currentColor;
    }

    private void GenerateAnswerPool()
    {
        answerPool.Clear();

        answerPool = currentEntry.entries
            .Where(x => !x.answer.Equals(currentQuiz.answer))
            .Select(x => (x.answer, false)).ToArray()
            .Take(otherAnswerCount).ToList();
        
        answerPool.Add((currentQuiz.answer, false));
        
        //distractions
        var fakeAnswers = currentEntry.fakeAnswers.Take(fakeAnswerCount).Select(x => (x, true));
        answerPool.AddRange(fakeAnswers);
        answerPool.AddRange(stressDistractions.Select(x => (x, true)));
    }

    private void UpdateStatus()
    {
        var progress = wrongCount + correctCount + 1;
        statusText.text = $"Question #{progress} / {quizQueue.Count} " +
                          $"| <color=green>Correct: {correctCount} </color>" +
                          $"| <color=red>Wrong: {wrongCount} </color>";
    }

    private void QueueQuiz(QuizEntries quizEntries)
    {
        var shuffled = quizEntries.entries.ToList()
            .OrderBy(_ => Random.value)
            .Take(Mathf.Min(quizEntries.entries.Length, quizCount));

        foreach (var entry in shuffled)
            quizQueue.Enqueue(entry);
    }

    private bool HasProgressedQuiz() => timer.IsCompleted() || quizAnswerTsc.Task.IsCompleted;

    public void Answer(string answer) => quizAnswerTsc?.SetResult(answer);

    public List<(string, bool)> GetAnswerPool() => answerPool;

    public Color GetRandomColor() => answerColorPool[Random.Range(0, answerColorPool.Count)];
    public Color GetCurrentColor() => currentColor;
}
