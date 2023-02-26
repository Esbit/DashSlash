using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingSoon : MonoBehaviour
{
    public GameObject Text;
    bool isFlashing = false;

    private void Start()
    {
        StartCoroutine(FlashingTextCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashingTextCoroutine());
        }
    }

    private IEnumerator FlashingTextCoroutine()
    {
        isFlashing = true;
        const float waitTime = 0.5f;
        yield return new WaitForSeconds(waitTime);
        Text.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        Text.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Text.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        Text.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Text.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        Text.SetActive(true);
        isFlashing = false;
    }
}
