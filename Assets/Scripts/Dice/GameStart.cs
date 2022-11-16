using TMPro;
using UnityEngine;
using System.Threading.Tasks;

public class GameStart : MonoBehaviour
{
    static public GameStart instance;

    public CameraFilter MonoFilter;
    public CameraMultiTarget cameraMultiTarget;
    public GameObject[] BowlObject;
    public GameObject[] TitleObjects;
    public GameObject InGameUIObject;

    public CanvasGroup BowlCanvasGroup;
    public TextMeshProUGUI RollText;
    public TextMeshProUGUI DiceCountText;

    private CameraDice CameraDice;
    private CameraBowl CameraBowl;

    private int Roll = 1;
    private float time;

    void Start()
    {
        CameraDice = GetComponent<CameraDice>();
        CameraBowl = GetComponent<CameraBowl>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public async void InitGame(int GameType)
    {
        TitleObjects[GameType - 1].SetActive(true);
        GameProcess.instance.Type = GameType;
        GameProcess.instance.DeleteDices();
        CameraDice.instance.InitCamera();
        await FadeInOutImage.instance.FadeInOut(false, 0.005f, 20);

        CameraDice.enabled = false;
        MonoFilter.enabled = false;
        CameraBowl.enabled = true;

        cameraMultiTarget.SetTargets(BowlObject);

        RollText.text = $"ROLL : {Roll}";
        await FadeInOutImage.instance.FadeInOut(true, 0.055f, 10);
        UpdateBowlCanvas();
    }

    private async void UpdateBowlCanvas()
    {
        await Task.Delay(500);

        string DiceText = $"{GameProcess.instance.DiceCount} DICE";
        for (int i = 1; i <= 10; i++)
        {
            BowlCanvasGroup.alpha = i * 0.1f;
            await Task.Delay(75);
        }

        for (int i = 0; i < 10; i++)
        {
            string randomText = RandomPassword.Generate(DiceText.Length);
            DiceCountText.text = randomText;
            await Task.Delay(10);
        }
        DiceCountText.text = DiceText;

        await Task.Delay(1000);

        for (int i = 0; i < 20; i++)
        {
            BowlCanvasGroup.alpha = GetAlphaColor();
            await Task.Delay(6);
        }
        BowlCanvasGroup.alpha = 0;
        InGameUIObject.SetActive(true);

        CanvasGroup UICanvasGroup = InGameUIObject.GetComponent<CanvasGroup>();
        for (int i = 1; i <= 10; i++)
        {
            UICanvasGroup.alpha = i * 0.1f;
            await Task.Delay(50);
        }
    }

    private float GetAlphaColor()
    {
        time += Time.unscaledDeltaTime * 20;
        return Mathf.Sin(time) * 0.5f + 0.5f;
    }
}
