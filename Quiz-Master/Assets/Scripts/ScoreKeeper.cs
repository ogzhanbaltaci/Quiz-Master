using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    int geographyQuestionSeen = 0;
    int geographyCorrectAnswers = 0;
    int sportsQuestionSeen = 0;
    int sportsCorrectAnswers = 0;
    int gamingQuestionSeen = 0;
    int gamingCorrectAnswers = 0;
    int moviesQuestionSeen = 0;
    int moviesCorrectAnswers = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public void IncrementGeographyQuestionSeen()
    {
        geographyQuestionSeen++;
    }

    public void IncrementGeographyCorrectAnswers()
    {
        geographyCorrectAnswers++;
    }

    public int GetGeographyCorrectAnswers()
    {
        return geographyCorrectAnswers;
    }
    
    public void IncrementSportsQuestionSeen()
    {
        sportsQuestionSeen++;
    }

    public void IncrementSportsCorrectAnswers()
    {
        sportsCorrectAnswers++;
    }

    public int GetSportsCorrectAnswers()
    {
        return sportsCorrectAnswers;
    }
    public void IncrementGamingQuestionSeen()
    {
        gamingQuestionSeen++;
    }

    public void IncrementGamingCorrectAnswers()
    {
        gamingCorrectAnswers++;
    }

    public int GetGamimngCorrectAnswers()
    {
        return gamingCorrectAnswers;
    }
    public void IncrementMoviesQuestionSeen()
    {
        moviesQuestionSeen++;
    }

    public void IncrementMoviesCorrectAnswers()
    {
        moviesCorrectAnswers++;
    }

    public int GetMoviesCorrectAnswers()
    {
        return moviesCorrectAnswers;
    }
    
    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
    
    public int CalculateGeographyScore()
    {
        return Mathf.RoundToInt(geographyCorrectAnswers / (float)geographyQuestionSeen * 100);
    }
    public int CalculateSportsScore()
    {
        return Mathf.RoundToInt(sportsCorrectAnswers / (float)sportsQuestionSeen * 100);
    }
    public int CalculateGamingScore()
    {
        return Mathf.RoundToInt(gamingCorrectAnswers / (float)gamingQuestionSeen * 100);
    }
    public int CalculateMoviesScore()
    {
        return Mathf.RoundToInt(moviesCorrectAnswers / (float)moviesQuestionSeen * 100);
    }

    
    
}
