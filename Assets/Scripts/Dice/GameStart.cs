using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameStart : MonoBehaviour
{
    static public GameStart instance;

    public CameraFilter MonoFilter;
    public CameraMultiTarget cameraMultiTarget;
    public GameObject[] BowlObject;
    public GameObject[] TitleObjects;
    public GameObject InGameUIObject;
    public Rigidbody BowlRigidbody;

    public CanvasGroup BowlCanvasGroup;
    public TextMeshProUGUI RollText;
    public TextMeshProUGUI DiceCountText;
    public TextMeshProUGUI RemainRollText;
    public TextMeshProUGUI RemainNudgeText;
    public Cinemachine.CinemachineImpulseSource CinemachineImpulseSource;

    public CameraDice CameraDice;
    public CameraBowl CameraBowl;

    public int Round = 1;
    public int WaitType = 0; // 0: 最初のアニメーション中, 1: クリック待機中, 2: サイコロ落下中, 3: サイコロ落下後のNUDGE受付時間, 4: 役などを確認, 4: ゲームを次に進める
    public int RemainNudge = 5;
    public bool IsClick = false;

    private int RemainRound = 3;
    private float time;
    private bool IsFirst = false;
    private bool IsNudgeClick = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (WaitType == 0 || IsClick == true) return;

        if (WaitType == 1 && Input.GetMouseButton(0)) {
            Sound.instance.UpdateBGM(true);
            UpdateRemainRollCount(0);
            WaitType = 2;
            IsClick = true;

            GameProcess.instance.InitSetDice();
            CameraDice.enabled = true;
            CameraBowl.enabled = false;
            CameraDice.instance.InitCameraDice();
            RandomTimeScale();
        }

        if (WaitType == 3 && RemainNudge != 0 && IsNudgeClick == false && Input.GetMouseButton(1))
        {
            IsClick = true;
            IsFirst = true;
            IsNudgeClick = true;
            GameProcess.instance.IsSleepingDices = new List<GameObject>();
            GameProcess.instance.AllDiceSleep = 0;
            UpdateRemainNudgeCount(0);
        }

        if (WaitType == 3 && IsNudgeClick == true && Input.GetMouseButtonUp(1))
        {
            IsNudgeClick = false;
        }
    }

    void FixedUpdate()
    {
        if (IsFirst)
        {
            IsFirst = false;
            Vector3 InitPos = BowlObject[0].transform.position;
            Vector3 ImpulseVec = new Vector3(Random.Range(0, 0.05f), Random.Range(0.075f, 0.125f), Random.Range(0, 0.05f));
            BowlRigidbody.MovePosition(InitPos + ImpulseVec);
            IsClick = false;
        }
    }

    public async void InitGame(int GameType)
    {
        Sound.instance.UpdateBGM(false);
        TitleObjects[GameType - 1].SetActive(true);
        await FadeInOutImage.instance.FadeInOut(false, 0.005f, 25);
        GameProcess.instance.Type = GameType;
        GameProcess.instance.DeleteDices();

        CameraDice.enabled = false;
        MonoFilter.enabled = false;
        CameraBowl.enabled = true;
        CameraBowl.instance.InitCameraBowl();
        GameProcess.instance.InitSetCameraObject(BowlObject);

        RollText.text = $"ROLL : {Round}";
        await FadeInOutImage.instance.FadeInOut(true, 0.055f, 20);
        UpdateBowlCanvas();
    }

    private async void UpdateBowlCanvas()
    {
        await Task.Delay(1000);

        string DiceText = $"{GameProcess.instance.DiceCount} DICE";
        for (int i = 1; i <= 10; i++)
        {
            BowlCanvasGroup.alpha = i * 0.1f;

            string randomText = RandomPassword.Generate(DiceText.Length);
            DiceCountText.text = randomText;
            await Task.Delay(30);
        }

        DiceCountText.text = DiceText;

        await Task.Delay(1000);

        for (int i = 0; i < 20; i++)
        {
            BowlCanvasGroup.alpha = GetAlphaColor();
            await Task.Delay(6);
        }
        BowlCanvasGroup.alpha = 0;

        await Task.Delay(750);
        InGameUIObject.SetActive(true);
        WaitType = 1;

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

    private async void UpdateRemainRollCount(int UpdateType=0)
    {
        if (UpdateType == 0)
        {
            RemainRound -= 1;
            RemainRollText.text = RemainRound.ToString();
            for (int i = 1; i <= 10; i++)
            {
                RemainRollText.alpha = i % 2 == 0 ? 1 : 0;
                await Task.Delay(25);
            }
            RemainRollText.alpha = 1;

        } else if (UpdateType == 1) {
            RemainRound += 1;
            RemainRollText.text = "+1";

        } else
        {
            RemainRollText.text = RemainRound.ToString();
        }
    }

    private void UpdateRemainNudgeCount(int UpdateType = 0)
    {
        if (UpdateType == 0)
        {
            RemainNudge -= 1;
            RemainNudgeText.text = $"NUDGE   {RemainNudge}";
        }
        else if (UpdateType == 1)
        {
            RemainNudge += 1;
            RemainNudgeText.text = "NUDGE   +1";
        }
        else
        {
            RemainNudgeText.text = $"NUDGE   {RemainNudge}";
        }

        float ColorValue = RemainNudge == 0 ? 0.25f : 1;
        RemainNudgeText.color = new Color(ColorValue, ColorValue, ColorValue);
    }

    private async void RandomTimeScale()
    {
        PlayerData PlayerData = Json.instance.Load();
        await Task.Delay(Random.Range(200, 400));

        float timeScale = Random.Range(0.1f, 0.5f);
        Time.timeScale *= timeScale;

        Sound.instance.SoundFaa();
        await Task.Delay(Random.Range(1500, 2000));
        Sound.instance.SoundEffect.Stop();

        Json.instance.UpdateTimeScale(PlayerData.Speed);
    }
}
