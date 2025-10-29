using UnityEngine;
using System.Collections;

public class Coroutine : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Coroutine1());
    }

    IEnumerator Coroutine1()
    {
        for (int i = 1; i <= 10; i++)
        {
            Debug.Log("goi"+i);
            yield return new WaitForSeconds(1);
            Debug.Log("Ket thuc"+i);
            yield return new WaitForSeconds(1);
        }
        // Debug.Log("goi 1");
        // yield return new WaitForSeconds(1);
        // Debug.Log("Ket thuc 1");
        //StartCoroutine(Coroutine2());
    }

    // IEnumerator Coroutine2()
    // {
    //     Debug.Log("goi 2");
    //     yield return new WaitForSeconds(1);
    //     Debug.Log("Ket thuc 2");
    //     StartCoroutine(Coroutine3());
    // }
    //
    // IEnumerator Coroutine3()
    // {
    //     Debug.Log("goi 3");
    //     yield return new WaitForSeconds(1);
    //     Debug.Log("Ket thuc 3");
    //     StartCoroutine(Coroutine4());
    // }
    //
    // IEnumerator Coroutine4()
    // {
    //     Debug.Log("goi 4");
    //     yield return new WaitForSeconds(1);
    //     Debug.Log("Ket thuc 4");
    //     StartCoroutine(Coroutine5());
    // }
    //
    // IEnumerator Coroutine5()
    // {
    //     Debug.Log("goi 5");
    //     yield return new WaitForSeconds(1);
    //     Debug.Log("Ket thuc 5");
    //     StartCoroutine(Coroutine6());
    // }
    //
    // IEnumerator Coroutine6()
    // {
    //     Debug.Log("goi 6");
    //     yield return new WaitForSeconds(1);
    //     Debug.Log("Ket thuc 6");
    //     StartCoroutine(Coroutine7());
    // }
    //
    // IEnumerator Coroutine7()
    // {
    //     Debug.Log("goi 7");
    //     yield return new WaitForSeconds(1);
    //     Debug.Log("Ket thuc 7");
    //     StartCoroutine(Coroutine8());
    // }
    //
    // IEnumerator Coroutine8()
    // {
    //     Debug.Log("goi 8");
    //     yield return new WaitForSeconds(1);
    //     Debug.Log("Ket thuc 8");
    //     StartCoroutine(Coroutine9());
    // }
    //
    // IEnumerator Coroutine9()
    // {
    //     Debug.Log("goi 9");
    //     yield return new WaitForSeconds(1);
    //     Debug.Log("Ket thuc 9");
    //     StartCoroutine(Coroutine10());
    // }
    //
    // IEnumerator Coroutine10()
    // {
    //     Debug.Log("goi 10");
    //     yield return new WaitForSeconds(1);
    //     Debug.Log("Ket thuc 10");
    // }
}
