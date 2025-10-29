using System;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class CreateCuts : MonoBehaviour
{
    public GameObject cutInBombPrefab;
    public GameObject particlePrefab;
    public float durationTimeBananaFreeze, durationTimeBananaDouble, durationTimeBananaFrenzy, durationTimePomegranate;
    public SpawnObjects spawnfruit;
    public GameObject spawnfruitleft, spawnfruitright;
    public GameObject particleLeft, particleRight;
    public GameManager gameManager;
    private GameObject _particleInstance; 
    public bool _dragging, _freezing, _double, _pomegranate;
    public bool _frenzy, _frenzyreset;
    public GameObject doublepivotdown, frenzypivotdown, freezepivotdown, doublepivotup, frenzypivotup, freezepivotup;
    public GameObject doublePopup, freezePopup, frenzyPopup;
    public GameObject freezebackground, doublebackground;
    public GameObject doubleScorePopUp, doubleScorePivotUp, doubleScorePivotDown;
    public bool _pomegranatecutted;
    public float pomegranateTimeCoolDown;
    private float initialCameraOrthoSize;
    private FruitObject PomegranateFruitObj;
    public GameObject cuteffect, particlecuteffect;

    void Start()
    {
        //camera phong ban dau
        initialCameraOrthoSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        InputMouse();
        UpdateTimers();
        BananaBonus();
    }

    private void InputMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _dragging = true;
            _particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _dragging = false;
            if (_particleInstance != null)
            {
                Destroy(_particleInstance);
            }
        }
        if (_dragging && _particleInstance != null)
        {
            Vector3 tmpParticlePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tmpParticlePos.z = -3;
            _particleInstance.transform.position = tmpParticlePos;
            CreateCut();
        }
    }

    private void UpdateTimers()
    {
        durationTimeBananaFreeze -= Time.deltaTime * 2;
        durationTimeBananaDouble -= Time.deltaTime;
        durationTimeBananaFrenzy -= Time.deltaTime;
        durationTimePomegranate -= Time.deltaTime * 100;
        pomegranateTimeCoolDown -= Time.unscaledDeltaTime;
    }

    private void BananaBonus()
    {
        //---------------------------------------------double banana----------------------------------------------------
        if (durationTimeBananaDouble <= 0 && _double)
        {
            doublePopup.transform.DOMove(doublepivotup.transform.position, 0.5f);
            doublebackground.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
            spawnfruit.spawnRate = 6f;
        }

        if (durationTimeBananaDouble < -1 && _double)
        {
            gameManager.IncreaseDoubleScore(0);
            _double = false;
        }

        if (durationTimeBananaDouble < -2)
        {
            doubleScorePopUp.transform.DOMove(doubleScorePivotUp.transform.position, 0.2f);
        }

        //---------------------------------------------freeze banana----------------------------------------------------
        if (durationTimeBananaFreeze <= 0 && _freezing)
        {
            freezePopup.transform.DOMove(freezepivotup.transform.position, 0.5f);
            spawnfruit.spawnRate = 6f;
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            _freezing = false;
        }

        //----------------------------------------------frenzy banana---------------------------------------------------
        if (durationTimeBananaFrenzy <= 0 && _frenzy)
        {
            frenzyPopup.transform.DOMove(frenzypivotup.transform.position, 0.5f);
            particleRight.SetActive(false);
            particleLeft.SetActive(false);
            spawnfruitleft.SetActive(false);
            spawnfruitright.SetActive(false);
            _frenzy = false;
        }

        if (durationTimeBananaFrenzy < -3 && _frenzyreset)
        {
            _frenzyreset = false;
        }

        //-----------------------------------------------pomegranate----------------------------------------------------
        if (durationTimePomegranate > 0 && _pomegranate)
        {
            OnCutting();
        }

        if (durationTimePomegranate <= 0 && _pomegranate)
        {
            Camera.main.DOOrthoSize(initialCameraOrthoSize, 1.4f).SetUpdate(true);
            Camera.main.transform.DOMove(new Vector3(0, 0, -17), 1f).SetUpdate(true);
            Camera.main.transform.DORotateQuaternion(Quaternion.Euler(0,0,0),1f).SetUpdate(true);
        }
        
        if (durationTimePomegranate <= -1.5f && _pomegranate)
        {
            OnCuttingEnd();
        }
        
        //tinh diem cac qua con trong man hinh neu qua luu da bi cat 
        if (_pomegranatecutted && durationTimePomegranate > -5)
        {
            var fruits = FindObjectsOfType<FruitObject>();
            foreach (var fruit in fruits)
            {
                fruit.CutFruit();
                gameManager.IncreaseScore(1);
            }
        }
    }


    void OnBananaFreeze()
    {
        //Popup and background
        freezePopup.transform.DOMove(freezepivotdown.transform.position, 0.5f);
        freezebackground.GetComponent<SpriteRenderer>().DOFade(0.4f, 5f).SetUpdate(true).onComplete += () =>
        {
            freezebackground.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
        };
        
        //hieu ung freeze
        gameManager.IncreaseScore(1);
        spawnfruit.spawnRate = 3f;
        spawnfruit.OnFrenzy();
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        
        //freeze time
        _freezing = true;
        durationTimeBananaFreeze = 5;
    }

    void OnBananaFrenzy()
    {
        //pop up
        frenzyPopup.transform.DOMove(frenzypivotdown.transform.position, 0.5f);
        
        //hieu ung frenzy time
        gameManager.IncreaseScore(1);
        durationTimeBananaFrenzy = 5f;
        _frenzy = true;
        _frenzyreset = true;
        particleRight.SetActive(true);
        particleLeft.SetActive(true);
        spawnfruitleft.SetActive(true);
        spawnfruitright.SetActive(true);
    }

    void OnBananaDouble()
    {
        //pop up and background
        doublebackground.GetComponent<SpriteRenderer>().DOFade(0.8f, 0.5f);
        doublePopup.transform.DOMove(doublepivotdown.transform.position, 0.5f);
        doubleScorePopUp.transform.DOMove(doubleScorePivotDown.transform.position, 0.5f);

        //hieu ung double
        durationTimeBananaDouble = 5f;
        spawnfruit.spawnRate = 3f;
        spawnfruit.OnFrenzy();
        
        //double time
        _double = true;
        gameManager.DoubleScore();
    }

    void OnPomegranate()
    {
        if (pomegranateTimeCoolDown <= 0 && durationTimePomegranate >=0)
        {
            gameManager.IncreaseScore(1);
            //spawn vet chem
            pomegranateTimeCoolDown = 10 * Time.unscaledDeltaTime;
            cuteffect.transform.position = new Vector3(PomegranateFruitObj.transform.position.x,PomegranateFruitObj.transform.position.y,-1);
            cuteffect.transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
            GameObject cut = Instantiate(cuteffect, cuteffect.transform.position,
                cuteffect.transform.rotation);
            //spawn nuoc luu
            GameObject effectcut = Instantiate(particlecuteffect, cuteffect.transform.position,
                cuteffect.transform.rotation);
            
            //tat vet chem
            Destroy(cut,0.0003f); 
            Destroy(effectcut,1);
            //cam giat dung dung
            int i = Random.Range(1, 5); 
            Vector3 targetPosition = Camera.main.transform.position;
            switch (i)
            {
                case 1:
                    targetPosition += new Vector3(0,-0.3f,0);
                    break;
                case 2:
                    targetPosition += new Vector3(0,0.3f,0);
                    break;
                case 3:
                    targetPosition += new Vector3(0.3f,0,0);
                    break;
                case 4:
                    targetPosition += new Vector3(-0.3f,0,0);
                    break;
            }
            Camera.main.transform.DOMove(targetPosition, 0.1f).SetUpdate(true);
        }
        if (_pomegranate == false)
        {
            durationTimePomegranate = 5f;
            OnCut();
        }
    }
    
    void OnCut()
    {        
        Camera.main.DOOrthoSize(2.3f, 1.4f).SetUpdate(true);
        int k = Random.Range(1, 3);
        switch (k)
        {
            case 1:        
                Camera.main.transform.DORotateQuaternion(Quaternion.Euler(0,0,-7),1f).SetUpdate(true);
                break;
            case 2:
                Camera.main.transform.DORotateQuaternion(Quaternion.Euler(0,0,7),1f).SetUpdate(true);
                break;
        }
        
        _pomegranate = true;
        _pomegranatecutted = false;
        Time.timeScale = 0.01f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    void OnCutting()
    {
        if (PomegranateFruitObj != null)
        { 
            Vector3 pomegranatePosition = PomegranateFruitObj.transform.position;
            pomegranatePosition.z = -11;
            Camera.main.transform.DOMove(pomegranatePosition,1f).SetUpdate(true);
            
        }
    }

    void OnCuttingEnd()
    {  
        _pomegranate = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        _pomegranatecutted = true;
    }

    private void CreateCut()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("world pos of mouse" + worldPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector3.forward,Mathf.Infinity);
        Debug.DrawLine(worldPos, worldPos+Vector3.forward*100,Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Bomb"))
            {
                Destroy(hit.collider.gameObject);
                Instantiate(cutInBombPrefab, hit.transform.position, hit.transform.rotation);
                gameManager.DecreaseLive(-1);
            }
            else if (hit.collider.CompareTag("Fruit"))
            {
                FruitObject fruitObject = hit.collider.gameObject.GetComponent<FruitObject>();
                if (fruitObject != null)
                {
                    fruitObject.CutFruit();
                    GameObject cut = Instantiate(cuteffect, fruitObject.transform.position,
                        fruitObject.transform.rotation);
                    cut.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
                    Destroy(cut,1);
                }

                if (durationTimeBananaDouble > 0)
                {
                    gameManager.IncreaseDoubleScore(1);
                }
                else
                {
                    gameManager.IncreaseScore(1);
                }
            }
            else if (hit.collider.CompareTag("BananaFreeze"))
            {
                // Debug.Log("banana freeze hit");
                FruitObject fruitObject = hit.collider.gameObject.GetComponent<FruitObject>();
                if (fruitObject != null)
                {
                    fruitObject.CutFruit();
                }
                OnBananaFreeze();
            }
            else if (hit.collider.CompareTag("BananaFrenzy"))
            {
                // Debug.Log("banana frenzy hit");
                FruitObject fruitObject = hit.collider.gameObject.GetComponent<FruitObject>();
                if (fruitObject != null)
                {
                    fruitObject.CutFruit();
                }
                OnBananaFrenzy();
            }
            else if (hit.collider.CompareTag("BananaDouble"))
            {
                // Debug.Log("banana double hit");
                FruitObject fruitObject = hit.collider.gameObject.GetComponent<FruitObject>();
                if (fruitObject != null)
                {
                    fruitObject.CutFruit();
                }
                OnBananaDouble();
            }
            else if (hit.collider.CompareTag("Pomegranate"))
            {
                // Debug.Log("pomegranate hit");
                PomegranateFruitObj = hit.collider.gameObject.GetComponent<FruitObject>();
                OnPomegranate();
            }
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector3.zero);
    // }
}
