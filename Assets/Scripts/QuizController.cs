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
    public RectTransform runnerRect;
    public RectTransform progressRect;
    public Image progressFill;
    public QuizFeedbackObject feedbackObject;

    private Queue<Quiz> quizQueue = new();
    private Quiz currentQuiz;
    private List<QuizGameAnswer> answerPool = new();

    private AnswerButton lastAnswerButton = null;
    private TaskCompletionSource<string> quizAnswerTsc = new();
    private int correctCount, wrongCount;

    public bool HasPassed => correctCount >= passingGrade;
    
    public string CurrentAnswer => currentQuiz.answer;

    public IEnumerator StartQuiz(QuizParameters parameters, QuizEntries entries)
    {
        quizHolder.gameObject.SetActive(true);

        ApplyParameters(parameters);
        
        currentEntry = entries;

        correctCount = 0;
        wrongCount = 0;
        
        finalScreen.gameObject.SetActive(false);
        feedbackObject.gameObject.SetActive(false);

        InitializeQuiz(currentEntry);

        UpdateStatus();
        
        while (quizQueue.Count > 0)
        {
            quizAnswerTsc = new();
            currentQuiz = quizQueue.Dequeue();
            questionText.text = currentQuiz.question;
            UpdateRunner();

            GenerateAnswerPool();

            timer.Set(baseTimer);
            
            brainstormAnswer.ClearAnswers();
            
            yield return new WaitUntil(HasProgressedQuiz);

            if (timer.IsCompleted()) wrongCount++;
            else
            {
                var answer = quizAnswerTsc.Task.Result;
                var isCorrect = answer.Equals(currentQuiz.answer, StringComparison.InvariantCultureIgnoreCase);
                
                feedbackObject.gameObject.SetActive(true);
                feedbackObject.Display(isCorrect, lastAnswerButton);
                
                if (isCorrect) correctCount++;
                else wrongCount++;

                UpdateRunner();
            }

            UpdateStatus();
        }
        
        timer.Set(0f);

        brainstormAnswer.ClearAll();
        finalScreen.gameObject.SetActive(true);
        resultText.text = $"{correctCount} / {quizCount} correct, " + (HasPassed ? "<color=green>You Passed!" : "<color=red>You Failed!");

        yield return new WaitForSeconds(3f);
    }

    private void UpdateRunner()
    {
        var progress = wrongCount + correctCount;
        var alpha = (float) progress / quizCount;

        progressFill.fillAmount = alpha;

        var pixelProgress = progressRect.rect.width * alpha;

        var pos = runnerRect.anchoredPosition;
        pos.x = pixelProgress;
        runnerRect.anchoredPosition = pos;
    }

    private void GenerateAnswerPool()
    {
        answerPool.Clear();

        var colorToUse = colors.OrderBy(_ => Random.value).Take(1 + otherAnswersCount + fakeAnswersCount).ToList();
        var colorQueue = new Queue<Color>();
        foreach (var color in colorToUse) colorQueue.Enqueue(color);

        var currentColor = colorQueue.Dequeue();
        questionBg.color = currentColor;
        
        answerPool.Add(new (currentQuiz.answer, currentColor));

        //Other Answers
        var minOtherAnswerCount = Mathf.Min(currentEntry.entries.Length, otherAnswersCount);
        var otherAnswers = currentEntry.entries.Select(x => x.answer)
            .Where(x => !x.Equals(currentQuiz.answer))
            .OrderBy(_ => Random.value).Take(minOtherAnswerCount);

        foreach (var answer in otherAnswers) answerPool.Add(new (answer, colorQueue.Dequeue()));

        //Fake Answers
        var fakeAnswers = currentEntry.fakeAnswers.OrderBy(_ => Random.value)
            .Take(fakeAnswersCount).Select(x => new QuizGameAnswer(x, colorQueue.Dequeue()));
        answerPool.AddRange(fakeAnswers);
    }

    private void ApplyParameters(QuizParameters parameters)
    {
        baseTimer = parameters.baseTimer;
        quizCount = parameters.quizCount;
        passingGrade = parameters.passingGrade;
        otherAnswersCount = parameters.otherAnswersCount;
        fakeAnswersCount = parameters.fakeAnswersCount;
        
        brainstormAnswer.ApplyParameters(parameters);
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
        quizQueue.Clear();
        for (int i = 0; i < quizCount; i++)
        {
            var entry = quizEntries.entries[i];
            quizQueue.Enqueue(entry);
        }
    }

    private bool HasProgressedQuiz() => timer.IsCompleted() || quizAnswerTsc.Task.IsCompleted;

    public void Answer(AnswerButton button, string answer)
    {
        lastAnswerButton = button;
        quizAnswerTsc?.SetResult(answer);
    }

    public List<QuizGameAnswer> GetAnswerPool() => answerPool;

    public void Clear()
    {
        finalScreen.gameObject.SetActive(false);
        questionText.text = String.Empty;
        quizHolder.gameObject.SetActive(false);
    }

    [ContextMenu("Win Quiz")]
    public void WinQuiz()
    {
        quizAnswerTsc.SetResult(currentQuiz.answer);
        timer.Set(0f);
        quizQueue.Clear();
        correctCount = quizCount;
        wrongCount = 0;
    }

    [ContextMenu("Lose Quiz")]
    public void LoseQuiz()
    {
        quizAnswerTsc.SetResult("");
        timer.Set(0f);
        quizQueue.Clear();
        correctCount = 0;
        wrongCount = quizCount;
    }
}
