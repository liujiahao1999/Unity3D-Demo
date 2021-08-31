    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private GAME_STATUS _status;

    public enum GAME_STATUS
    {
        Ready,
        InGame,
        GameOver
    }
    public GAME_STATUS Status
    {
        get { return _status; }
        set
        {
            _status = value;
            this.UIUpdate();
        }
    }

    public Text uiScore;
    public Text uiOverScore;
    public Text uiBestScore;

    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            this.uiScore.text = score.ToString();
        }
    }

    private int overScore;
    public int OverScore
    {
        get { return overScore; }
        set
        {
            overScore = value;
            this.uiOverScore.text = overScore.ToString();
        }
    }

    private int bestScore;
    public int BestScore
    {
        get { return bestScore; }
        set
        {
            bestScore = value;
            this.uiBestScore.text = bestScore.ToString();
        }
    }

    public GameObject panelReady;
    public GameObject panelInGame;
    public GameObject panelOver;

    public Slider slider;
    public PipeLineManager pipe;
    public Player player;

    private int maxScore;
    // Start is called before the first frame update
    void Start()
    {   
        UIUpdate();
    }

    private void Player_OnScore(int num)
    {
        this.Score += 10;
    }

    private void Player_OnDeath()
    {
        this.player.OnScore -= Player_OnScore;
        OverScore = Score;
        BestScore = GetMaxScore();
        this.Status = GAME_STATUS.GameOver;
        pipe.StopCreate();
    }

    public void OnClickStart()
    {
        Score = 0;
        this.player.OnDeath += Player_OnDeath;
        this.player.OnScore += Player_OnScore;
        this.Status =GAME_STATUS.InGame;
        pipe.StartRun();
        player.Fly();
    }
    public void OnClickReStart()
    {
        this.player.OnDeath -= Player_OnDeath;
        this.Status = GAME_STATUS.Ready;
        this.pipe.Init();
        player.Init();

    }
    public void UIUpdate()
    {
        panelReady.SetActive(this.Status==GAME_STATUS.Ready);
        panelInGame.SetActive(this.Status == GAME_STATUS.InGame);
        panelOver.SetActive(this.Status == GAME_STATUS.GameOver);
        
    }
    /// <summary>
    /// 分数计算
    /// </summary>
    /// <returns></returns>
    public int GetMaxScore()
    {
        if (!PlayerPrefs.HasKey("max"))
        {
            PlayerPrefs.SetInt("max", this.OverScore);
        }
        if (BestScore<this.OverScore)
        {
            PlayerPrefs.SetInt("max", this.OverScore);
        }
        return  PlayerPrefs.GetInt("max");
    }
    public void SetLevel()
    {
        pipe.speed = slider.value;
        Debug.Log("speed is "+pipe.speed);
    }
}
