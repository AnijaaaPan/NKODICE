using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackToTitle : MonoBehaviour, IPointerClickHandler
{
    public RectTransform Left;
    public RectTransform Right;

    public RectTransform OptionTransform;
    public CanvasGroup OptionCanvas;

    private bool ShutOut = false;
    private float initLeftX = -2300;
    private float initRightX = 2300;

    readonly float Interval = 0.0001f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            StartCoroutine("BackScene");
        }
    }

    void Update()
    {
        if (ShutOut == true)
        {
            initLeftX += 5000 * Time.deltaTime;
            initRightX -= 5000 * Time.deltaTime;
            if (-700 <= initLeftX || initRightX <= 700) return;

                Left.localPosition = new Vector3(initLeftX, 100, 0);
            Right.localPosition = new Vector3(initRightX, -100, 0);
        };

        if (!Input.GetKeyDown(KeyCode.Escape)) return ;

        StartCoroutine("BackScene");
    }

    private IEnumerator BackScene()
    {
        ShutOut = true;

        for (int i = 1; i <= 10; i++)
        {
            OptionZoomOut(i * -3, 1 - i * 0.1f);
            yield return new WaitForSeconds(Interval);
        }

        while(true)
        {
            if (-700 <= initLeftX || initRightX <= 700)
            {
                yield return new WaitForSeconds(0.25f);
                ShutOut = false;
                SceneManager.LoadSceneAsync("Title");
            };

            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OptionZoomOut(int positionZ, float colorAlpha)
    {
        OptionTransform.localPosition = new Vector3(0, 0, positionZ);
        OptionCanvas.alpha = colorAlpha;
    }
}
