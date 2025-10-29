using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public CreateCuts createCuts;
    public TextReadyGo textReady;

    public enum GameMode
    {
        Modebomb,
        Modetime
    };

    public GameMode gamemode;

    [FormerlySerializedAs("LiveParent")] public GameObject liveParent;
    
    // --------------------------------------Screen Home---------------------------------------------------
    
    public Button restartbutton;
    
     // ------------------------------------Screen Ingame--------------------------------------------------
                                            //gamestate

    public bool isGameOver;
    
                                     //camera (shake when lose live)
                                     
    public Camera mainCamera;
    
                                               //live (mode bomb)
                                               
    public GameObject live1, live2, live3, loselive1, loselive2, loselive3, fruitImageScore;
    
                                                 //timer (mode timer)

    public int timeRemaining;
    public GameObject timeRemainingText;
    [FormerlySerializedAs("Pomegranate")] public GameObject pomegranate;
    
                                                //score
                                                
    public Text scoreText, bestscoreText;
    [FormerlySerializedAs("Score")] public int score;
    [FormerlySerializedAs("Live")] public int live;
    [FormerlySerializedAs("BestScore")] public int bestScore;
    
                                              //spawner
                                              
    public SpawnBonusBanana spawnBonusBanana;
    [FormerlySerializedAs("SpawnPomegranate")] public GameObject spawnPomegranate;
    
                                            //double score
                                            
    public Text doubleScoreText;
    public int doubleScore;
    
                                                //color
    private Color _timeTextColor;
    private Color _scoreTextColor;

    // -------------------------------------Screen GameOver------------------------------------------------
    
    public GameObject screenHome, screenIngame, screenGameover;
    
    //-----------------------------------------------------------------------------------------------------
    private void Awake()
    {
        //Pause();
        isGameOver = true;
        if (gamemode == GameMode.Modetime)
        {
            timeRemaining = 60;
        }
    }

    IEnumerator ScreenHome()
    {
        yield return new WaitForSecondsRealtime(0);
        screenHome.SetActive(true);
        screenIngame.SetActive(false);
        screenGameover.SetActive(false);
    }
    // IEnumerator ScreenInGame()
    // {
    //     yield return new WaitForSecondsRealtime(7);
    //     screenHome.SetActive(false);
    //     screenIngame.SetActive(true);
    //     screenGameover.SetActive(false);
    // }
    IEnumerator ScreenGameOver()
    {
        yield return new WaitForSecondsRealtime(1);
        screenHome.SetActive(false);
        screenIngame.SetActive(true);
        screenGameover.SetActive(true);
    }
    private void Start()
    {
        StartCoroutine(ScreenHome());
        restartbutton.onClick.AddListener(Restart);
        bestScore = GetFruitScore();
        bestscoreText.text = "Best: " + bestScore;
        
        // //tap to play effect
        // CanvasGroup playButtonCanvasGroup = playbutton.GetComponent<CanvasGroup>();
        // if (playButtonCanvasGroup == null)
        // {
        //     playButtonCanvasGroup = playbutton.AddComponent<CanvasGroup>();
        // }
        // playButtonCanvasGroup.DOFade(0.2f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        
        //get normal color
        _timeTextColor = timeRemainingText.GetComponent<Text>().color;
        _scoreTextColor = scoreText.color;
    }
    
    private void Update()
    {
        //update bestscore
        if (score > bestScore)
        {
            bestScore = score;
            bestscoreText.text = "Best: " + bestScore;
            bestscoreText.color = Color.green;
            SetFruitScore(bestScore);
        }
        
        //mode timer
        if (gamemode==GameMode.Modetime)
        {
            if (createCuts._pomegranatecutted)
            {
                GameOver();
            }
            if (timeRemaining < 10 && timeRemaining >0)
            {
                timeRemainingText.GetComponent<Text>().DOColor(Color.red, 0.2f).onComplete += () =>
                {
                    timeRemainingText.GetComponent<Text>().DOColor(_timeTextColor, 0.2f);
                };
                timeRemainingText.GetComponent<Text>().text = "0:0" + timeRemaining;
            }

            if (timeRemaining >= 10 && timeRemaining <60)
            {
                timeRemainingText.GetComponent<Text>().text = "0:" + timeRemaining;
            }

            if (timeRemaining == 60)
            {
                timeRemainingText.GetComponent<Text>().text = "1:00";
            }
        }
    }
    
    int GetFruitScore()
    {
        return PlayerPrefs.GetInt("FruitScore", 1);
    }

    void SetFruitScore(int bestscore)
    {
        PlayerPrefs.SetInt("FruitScore", bestscore);
    }

    //scene Home
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SetTextModeBomb()
    {
        yield return new WaitForSecondsRealtime(1);
        textReady.SetTextReady("Ready");
        yield return new WaitForSecondsRealtime(1);
        textReady.SetTextReady("Go!!!");
        screenHome.SetActive(false);
        screenIngame.SetActive(true);
        screenGameover.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        isGameOver = false; // kich hoat ban qua 
    }

    IEnumerator SetTextModeTime()
    {
        yield return new WaitForSecondsRealtime(1);
        textReady.SetTextReady("60 seconds");
        yield return new WaitForSecondsRealtime(1);
        textReady.SetTextReady("Go!!!");
        screenHome.SetActive(false);
        screenIngame.SetActive(true);
        screenGameover.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        isGameOver = false; // kich hoat ban qua 
    }
    public void Play()
    {
        score = 0;

        switch (gamemode)
        {
            case GameMode.Modebomb:
                StartCoroutine(SetTextModeBomb());
                live = 3;
                loselive1.SetActive(false);
                loselive2.SetActive(false);
                loselive3.SetActive(false);
                liveParent.SetActive(true);
                break;
            case GameMode.Modetime:
                StartCoroutine(SetTextModeTime());
                timeRemaining = 62; //gom ca thoi gian hien thi text ready (~2s)
                Invoke ("Countdown", 1f);
                spawnPomegranate.SetActive(false);
                timeRemainingText.gameObject.SetActive(true);
                break;
        }
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        StartCoroutine(ScreenGameOver());
    }
    
    //reset doublescore
    public void DoubleScore()
    {
        doubleScore = 0;
        doubleScoreText.text = doubleScore.ToString();
    }
    
    //update doublescore
    public void IncreaseDoubleScore(int isDoubleScore)
    {
        if (isDoubleScore == 1)
        {
            FindObjectOfType<ComboManager>().AddScore();
            doubleScore++;
            doubleScoreText.text = doubleScore.ToString();
        }
        
        else if (isDoubleScore == 0)
        {
            doubleScore += doubleScore;
            doubleScoreText.text = doubleScore.ToString();
            doubleScoreText.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f, 0, 0);
            
            //hieu ung tang diem tu score ban dau thanh score duoc them diem
            int startScore = score;
            int endScore = score + doubleScore;
            DOVirtual.Int(startScore, endScore, 0.4f, newScore => 
            {
                score = newScore;  //doi tuong tang diem
                scoreText.text = score.ToString();
            });
            scoreText.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f, 0, 0);
        }
    }
    
    //update score
    public void IncreaseScore(int fruitscore)
    {
        //combo text
        if (fruitscore > 0)
        {
            FindObjectOfType<ComboManager>().AddScore();
        }
        //increase point
        score += fruitscore;
        score = Mathf.Clamp(score, 0, score);
        scoreText.text = score.ToString();

        fruitImageScore.transform.localScale = Vector3.one;
        fruitImageScore.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.1f, 0, 0).SetUpdate(true);
    }

    //mode time
    public void Countdown()
    {
        if (gamemode == GameMode.Modetime)
        {
            timeRemaining--;
            //freeze timer
            if (createCuts._freezing)
            {
                timeRemainingText.GetComponent<Text>().DOColor(Color.cyan, 0.2f).SetUpdate(true);
                StartCoroutine(FreezeTime());
            }
            else
            {
                if (timeRemaining > 0)
                {
                    Invoke("Countdown", 1f);
                }
            }

            // bi freeze lech 1 giay do phai goi den ham countdown
            if (timeRemaining == 0)
            {
                timeRemainingText.GetComponent<Text>().text = "0:00";
                //gamestate
                isGameOver = true;
                //dung ondisable thay vi setactive false de fruit ko tu nhien bien mat
                spawnBonusBanana.OnDisable();
                StartCoroutine(PomegranateIsComing());
            }
        }
    }

    IEnumerator PomegranateIsComing()
    {
        yield return new WaitForSeconds(3);
        GameObject newObject = Instantiate (pomegranate);
        newObject.transform.position = new Vector2 (0,-3f);
        newObject.transform.DORotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)), 2, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo);
    }
    IEnumerator FreezeTime()
    {
        yield return new WaitForSecondsRealtime(4);
        timeRemainingText.GetComponent<Text>().DOColor(_timeTextColor, 0.2f).SetUpdate(true);
        Invoke("Countdown",1f);
    }
    public void DecreaseLive(int decreaselive)
    {
        FindObjectOfType<ComboManager>().StopCombo();
        if (gamemode==GameMode.Modetime)
        {
            IncreaseScore(-5);
            scoreText.DOColor(Color.red, 0.2f).onComplete += () =>
            {
                scoreText.DOColor(_scoreTextColor, 0.2f);
            };
            mainCamera.transform.DOShakePosition(0.2f, 1f, 1, 10).onComplete += () =>
            {
                mainCamera.transform.DOMove(new Vector3(0, 0, -17), 0.001f);
            };
        }
        if (gamemode==GameMode.Modebomb)
        {
            live += decreaselive;
            mainCamera.transform.DOShakePosition(0.2f, 1f, 1, 10);

            if (live == 0)
            {
                live3.SetActive(false);
                loselive3.SetActive(true);
                loselive3.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f, 0, 0);
                // screen
                screenHome.SetActive(false);
                screenIngame.SetActive(true);
                screenGameover.SetActive(true);
                //gamestate
                isGameOver = true;
                //dung ondisable thay vi setactive false de fruit ko tu nhien bien mat
                spawnBonusBanana.OnDisable();
            }

            if (live == 2)
            {
                live1.SetActive(false);
                loselive1.SetActive(true);
                loselive1.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f, 0, 0);
            }

            if (live == 1)
            {
                live2.SetActive(false);
                loselive2.SetActive(true);
                loselive2.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f, 0, 0);
            }
        }
    }
}