using System.Collections;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{
    [SerializeField] private GameObject uIElement;
    private Vector3 startPos;
    private Coroutine currentLerpCoroutine;

    public void ActivateUI()
    {
        uIElement.SetActive(true);
        startPos = uIElement.transform.localPosition;
        if (currentLerpCoroutine != null)
        {
            StopCoroutine(currentLerpCoroutine);
        }
        currentLerpCoroutine = StartCoroutine(LerpPosition(Vector3.zero, 0.2f, false)); // Lerp to (0,0,0) then deactivate after 2 seconds
    }

    public void DeactivateUI()
    {
        if (currentLerpCoroutine != null)
        {
            StopCoroutine(currentLerpCoroutine);
        }
        currentLerpCoroutine = StartCoroutine(LerpPosition(startPos, 0.2f, true, 0.3f)); // Lerp back to startPos without deactivation
    }

    private IEnumerator LerpPosition(Vector3 targetPos, float duration, bool deactivateAfterLerp = false, float deactivateDelay = 0f)
    {
        float time = 0;
        Vector3 initialPos = uIElement.transform.localPosition;

        while (time < duration)
        {
            uIElement.transform.localPosition = Vector3.Lerp(initialPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        uIElement.transform.localPosition = targetPos;

        if (deactivateAfterLerp)
        {
            yield return new WaitForSeconds(deactivateDelay);
            uIElement.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
