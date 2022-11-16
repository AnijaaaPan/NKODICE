using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class BackToTitle : MonoBehaviour, IPointerClickHandler
{
    public RectTransform Left;
    public RectTransform Right;

    public RectTransform OptionTransform;
    public CanvasGroup OptionCanvas;

    private bool ShutOut = false;
    private float initLeftX = -2300;
    private float initRightX = 2300;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            BackScene();
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

        BackScene();
    }

    private async void BackScene()
    {
        ShutOut = true;

        for (int i = 1; i <= 10; i++)
        {
            OptionZoomOut(i * -3, 1 - i * 0.1f);
            await Task.Delay(20);
        }

        while(true)
        {
            if (-725 <= initLeftX || initRightX <= 725)
            {
                await Task.Delay(250);
                ShutOut = false;
                SceneManager.LoadSceneAsync("Title");
                break;
            };

            await Task.Delay(10);
        }
    }

    private void OptionZoomOut(int positionZ, float colorAlpha)
    {
        OptionTransform.localPosition = new Vector3(0, 0, positionZ);
        OptionCanvas.alpha = colorAlpha;
    }
}
