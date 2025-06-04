using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LogoSequence : MonoBehaviour
{
    public GameObject blackPanel;
    public Image unityLogo;
    public Image bulldozingStudiosLogo;

    [SerializeField] private float initialWaitTime = 2f;
    [SerializeField] private float logoDisplayTime = 1.5f;
    [SerializeField] private float finalWaitTime = 1f;
    [SerializeField] private float fadeSpeed = 1f;

    private CanvasGroup blackPanelCanvasGroup;
    private CanvasGroup unityLogoCanvasGroup;
    private CanvasGroup bulldozingStudiosCanvasGroup;

    void Start()
    {
        SetupCanvasGroups();
        StartCoroutine(PlayLogoSequence());
    }

    void SetupCanvasGroups()
    {
        blackPanelCanvasGroup = GetOrAddCanvasGroup(blackPanel);
        unityLogoCanvasGroup = GetOrAddCanvasGroup(unityLogo.gameObject);
        bulldozingStudiosCanvasGroup = GetOrAddCanvasGroup(bulldozingStudiosLogo.gameObject);

        blackPanelCanvasGroup.alpha = 1f;
        unityLogoCanvasGroup.alpha = 0f;
        bulldozingStudiosCanvasGroup.alpha = 0f;

        blackPanel.SetActive(true);
        unityLogo.gameObject.SetActive(true);
        bulldozingStudiosLogo.gameObject.SetActive(true);
    }

    CanvasGroup GetOrAddCanvasGroup(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }
        return canvasGroup;
    }

    IEnumerator PlayLogoSequence()
    {
        yield return new WaitForSeconds(initialWaitTime);
        yield return StartCoroutine(FadeIn(unityLogoCanvasGroup));
        yield return new WaitForSeconds(logoDisplayTime);
        yield return StartCoroutine(FadeOut(unityLogoCanvasGroup));
        yield return new WaitForSeconds(logoDisplayTime);
        yield return StartCoroutine(FadeIn(bulldozingStudiosCanvasGroup));
        yield return new WaitForSeconds(logoDisplayTime);
        yield return StartCoroutine(FadeOut(bulldozingStudiosCanvasGroup));
        yield return new WaitForSeconds(finalWaitTime);
        yield return StartCoroutine(FadeOut(blackPanelCanvasGroup));

        blackPanel.SetActive(false);
        unityLogo.gameObject.SetActive(false);
        bulldozingStudiosLogo.gameObject.SetActive(false);
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / fadeSpeed);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeSpeed);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}