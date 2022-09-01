using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;
    Image buttonColor;
    Image buttonImage;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;
   
    [Header("Sounds")]
    private AudioSource audioSource;
    [SerializeField] AudioClip trueAnswer;
    [SerializeField] AudioClip wrongAnswer;
    [SerializeField] AudioClip newQuestion;

    [Header("50:50Button")]
    public int another5050Chance;
    [SerializeField] Button fiftyfiftyButton; 
    [SerializeField] TextMeshProUGUI icon5050;

    public bool isComplete;
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();  
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;    
        audioSource = GetComponent<AudioSource>(); 
    }
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
                
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
        if(another5050Chance==5)
        {
            fiftyfiftyButton.GetComponent<Button>().interactable = true;
        }
        if(fiftyfiftyButton.GetComponent<Button>().interactable == false)
        {
            icon5050.text = another5050Chance  + " /5";
        }
        else if(fiftyfiftyButton.GetComponent<Button>().interactable == true)
        {
            icon5050.text = "";
        }
    }
       
    public void OnAnswerSelected(int index)
    {  
        hasAnsweredEarly = true;
        DisplayAnswer(index);     
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
      
    }
    
    public void On5050ButtonSelected()
    {
        Display5050Answer();
    }
    void DisplayAnswer(int index)
    {
        if(index == currentQuestion.GetCorrectAnswerIndex()){
            questionText.text = "Correct";
            buttonImage = answerButtons[index].GetComponent<Image>(); 
            buttonImage.sprite = correctAnswerSprite;  
            scoreKeeper.IncrementCorrectAnswers();            
            audioSource.clip = trueAnswer;
            audioSource.Play(); 
            another5050Chance++;
            GetCategoryAttributes();
                     
        }
        else {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            if(hasAnsweredEarly){
                buttonImage = answerButtons[index].GetComponent<Image>(); 
                buttonImage.sprite = wrongAnswerSprite;
                audioSource.clip = wrongAnswer;
                audioSource.Play();
            }
            GetCategoryWrongAnswerSeen();
            scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
        }
    }
    void Another5050Chance()
    {
        another5050Chance=0;  
    }
    void Display5050Answer()
    {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        int index2 = 5;
        for(int i=0; i<2; )
        {
            int[] numbers = {0,1,2,3};     
            int index = Random.Range(0,numbers.Length);           
            if(index != correctAnswerIndex && index != index2)
            {
                Button button = answerButtons[index].GetComponent<Button>();
                buttonColor = answerButtons[index].GetComponent<Image>();
                button.interactable = false;
                buttonColor.color= Color.blue;
                index2 = index;
                i++;               
            }
        }
        fiftyfiftyButton.GetComponent<Button>().interactable = false;
        Another5050Chance();
    }
    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }    
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0,questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }  
    }
     
   void DisplayQuestion()
   {
        audioSource.clip = newQuestion;
        audioSource.Play();
        questionText.text = currentQuestion.GetQuestion();

        for(int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
   }
   void SetButtonState(bool state)
   {
    for (int i = 0; i<answerButtons.Length;i++)
    {
        Button button = answerButtons[i].GetComponent<Button>();
        button.interactable = state;
    }
   }
   void SetDefaultButtonSprites()
   {
        for(int i = 0; i<answerButtons.Length; i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>(); 
            buttonColor = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
            buttonColor.color= Color.white;
        }
   }

   void GetCategoryAttributes()
   {
        if(currentQuestion.GetCategory() == "Geography") 
        {
            scoreKeeper.IncrementGeographyQuestionSeen();
            scoreKeeper.IncrementGeographyCorrectAnswers();
        }
        else if(currentQuestion.GetCategory() == "Sports")
        {
            scoreKeeper.IncrementSportsQuestionSeen();
            scoreKeeper.IncrementSportsCorrectAnswers();
        }
        else if(currentQuestion.GetCategory() == "Gaming")
        {
            scoreKeeper.IncrementGamingQuestionSeen();
            scoreKeeper.IncrementGamingCorrectAnswers();
        }
        else
        {
            scoreKeeper.IncrementMoviesQuestionSeen();
            scoreKeeper.IncrementMoviesCorrectAnswers();
        }

   }

    void GetCategoryWrongAnswerSeen()
    {
        if(currentQuestion.GetCategory() == "Geography") 
        {
            scoreKeeper.IncrementGeographyQuestionSeen();
        }
        else if(currentQuestion.GetCategory() == "Sports")
        {
            scoreKeeper.IncrementSportsQuestionSeen();            
        }
        else if(currentQuestion.GetCategory() == "Gaming")
        {
            scoreKeeper.IncrementGamingQuestionSeen();        
        }
        else
        {
            scoreKeeper.IncrementMoviesQuestionSeen();            
        }
    }
  
}
