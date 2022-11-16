using UnityEngine;
using System.Threading.Tasks;

public class CameraDice : MonoBehaviour
{
    static public CameraDice instance;

    public CameraMultiTarget cameraMultiTarget;

    private int MovePitch = 0; // 0: 何もなし, 1: 30度に近づく, 2: 0度に近づく
    private int MoveRoll = 0; // 0: 何もなし, 1: 時計回り, 2: 反時計回り
    private int MovePadding = 0; // 0: 何もなし, 1: 拡大, 2: 縮小

    private readonly float PitchMin = 10f;
    private readonly float PitchMax = 30f;
    private readonly float RollMin = 0f;
    private readonly float RollMax = 360f;
    private readonly float PaddingMin = 1f;
    private readonly float PaddingMax = 2f;

    private float InitPitch = 0;
    private float InitRoll = 0;
    private float InitPadding = 0;

    private float DiffPitch = 0;
    private float DiffRoll = 0;
    private float DiffPadding = 0;

    private readonly int IntervalTime = 50;

    private void Awake()
    {
        instance = this;
    }

    async void Start()
    {
        InitCamera();
        InitCameraDice();

        while (true)
        {
            UpdatePitch();
            UpdateRoll();
            UpdatePadding();
            await Task.Delay(IntervalTime);
        }
    }
    
    public void InitCamera()
    {
        cameraMultiTarget.Pitch = 0;
        cameraMultiTarget.Roll = 0;
        cameraMultiTarget.Yaw = 0;
        cameraMultiTarget.PaddingDown = 0;
        cameraMultiTarget.PaddingLeft = 0;
        cameraMultiTarget.PaddingRight = 0;
        cameraMultiTarget.PaddingUp = 0;
    }

    public void InitCameraDice()
    {
        MovePitch = RandomChoiceType();
        MoveRoll = RandomChoiceType();
        MovePadding = RandomChoiceType();

        InitPitch = Random.Range(PitchMin, PitchMax);
        InitRoll = Random.Range(RollMin, RollMax);
        InitPadding = Random.Range(PaddingMin, PaddingMax);

        cameraMultiTarget.Pitch = InitPitch;
        cameraMultiTarget.Roll = InitRoll;
        cameraMultiTarget.PaddingDown = InitPadding;
        cameraMultiTarget.PaddingLeft = InitPadding;
        cameraMultiTarget.PaddingRight = InitPadding;
        cameraMultiTarget.PaddingUp = InitPadding;

        DiffPitch = Random.Range(0f, 0.06f);
        DiffRoll = Random.Range(0f, 0.05f);
        DiffPadding = Random.Range(0f, 0.025f);
    }

    private int RandomChoiceType()
    {
        int RandomRange = Random.Range(1, 101);
        if (RandomRange <= 20) return 0;
        if (RandomRange <= 60) return 1;
        return 2;
    }

    private void UpdatePitch()
    {
        if (MovePitch == 0) return;

        float UpdateValue = MovePitch == 1 ? DiffPitch : -DiffPitch;
        cameraMultiTarget.Pitch += UpdateValue;

        if (cameraMultiTarget.Pitch >= PitchMax) MovePitch = 2;
        else if (cameraMultiTarget.Pitch <= PitchMin) MovePitch = 1;
    }

    private void UpdateRoll()
    {
        if (MoveRoll == 0) return;

        float UpdateValue = MoveRoll == 1 ? DiffRoll : -DiffRoll;
        cameraMultiTarget.Roll += UpdateValue;

        if (cameraMultiTarget.Roll >= RollMax) MoveRoll = 2;
        else if (cameraMultiTarget.Roll <= RollMin) MoveRoll = 1;
    }

    private void UpdatePadding()
    {
        if (MovePadding == 0) return;

        float UpdateValue = MovePadding == 1 ? DiffPadding : -DiffPadding;
        cameraMultiTarget.PaddingDown += UpdateValue;
        cameraMultiTarget.PaddingLeft += UpdateValue;
        cameraMultiTarget.PaddingRight += UpdateValue;
        cameraMultiTarget.PaddingUp += UpdateValue;

        if (cameraMultiTarget.PaddingDown >= PaddingMax) MovePadding = 2;
        else if (cameraMultiTarget.PaddingDown <= PaddingMin) MovePadding = 1;
    }
}
