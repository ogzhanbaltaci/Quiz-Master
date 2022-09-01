using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI geographyScoreText;
    [SerializeField] TextMeshProUGUI sportsScoreText;
    [SerializeField] TextMeshProUGUI gamingScoreText;
    [SerializeField] TextMeshProUGUI moviesScoreText;
    [SerializeField] Slider geographySlider;
    [SerializeField] Slider sportsSlider;
    [SerializeField] Slider gamingSlider;
    [SerializeField] Slider moviesSlider;
    ScoreKeeper scoreKeeper;
    
   
    void Awake()
    { 
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations!\nYou got a score of " + scoreKeeper.CalculateScore() + "%";
        geographyScoreText.text = "Geogpraphy Category Score " + scoreKeeper.CalculateGeographyScore() + "%";
        sportsScoreText.text = "Sports Category Score " + scoreKeeper.CalculateSportsScore() + "%";
        gamingScoreText.text = "Gaming Category Score " + scoreKeeper.CalculateGamingScore() + "%";
        moviesScoreText.text = "Movies Category Score " + scoreKeeper.CalculateMoviesScore() + "%";
        geographySlider.value = scoreKeeper.GetGeographyCorrectAnswers();
        sportsSlider.value = scoreKeeper.GetSportsCorrectAnswers();
        gamingSlider.value = scoreKeeper.GetGamimngCorrectAnswers();
        moviesSlider.value = scoreKeeper.GetMoviesCorrectAnswers();
    }


}
