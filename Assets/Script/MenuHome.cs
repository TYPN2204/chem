using System.Collections;
using UnityEngine;
using DG.Tweening;
public class MenuHome : MonoBehaviour
{
    private bool _dragging;
    public GameManager gamemanager;

    public GameObject fruitModeBomb;
    public GameObject fruitModeTime;
    public GameObject logo, logoPivotUp;
    public GameObject circleModeBomb, circleModeTime;
    void Update()
    {
        InputMouse();
    }

    private void Start()
    {
        logo.transform.DOScale(Vector3.one, 4f).SetEase(Ease.InOutExpo).SetUpdate(true);
    }

    void InputMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _dragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _dragging = false;
        }

        if (_dragging)
        {
            Cut();
        }
    }

    void Cut()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            logo.transform.DOMove(logoPivotUp.transform.position, 0.5f).SetEase(Ease.InBack).SetUpdate(true);
            if (hit.collider.CompareTag("Mode Bomb"))
            {
                FruitObject fruitObject = hit.collider.gameObject.GetComponent<FruitObject>();
                if (fruitObject != null)
                {
                    fruitObject.CutFruit();
                    gamemanager.gamemode = GameManager.GameMode.Modebomb;
                    gamemanager.Play();
                }
                fruitModeTime.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InOutExpo).SetUpdate(true);
                StartCoroutine(HideFruitMode(fruitModeTime));
                this.gameObject.SetActive(false);
            }
            else if (hit.collider.CompareTag("Mode Time"))
            {
                FruitObject fruitObject = hit.collider.gameObject.GetComponent<FruitObject>();
                if (fruitObject != null)
                {
                    fruitObject.CutFruit();
                    gamemanager.gamemode = GameManager.GameMode.Modetime;
                    gamemanager.Play();
                }
                fruitModeBomb.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InOutExpo).SetUpdate(true);
                StartCoroutine(HideFruitMode(fruitModeBomb));
                this.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator HideFruitMode(GameObject fruitmode)
    {
        fruitmode.SetActive(false);
        circleModeBomb.GetComponent<SpriteRenderer>().DOFade(0,0.1f).SetEase(Ease.InOutExpo).SetUpdate(true);
        circleModeTime.GetComponent<SpriteRenderer>().DOFade(0,0.1f).SetEase(Ease.InOutExpo).SetUpdate(true);
        yield return new WaitForSecondsRealtime(2);
        circleModeTime.SetActive(false);
        circleModeBomb.SetActive(false);
    }
}