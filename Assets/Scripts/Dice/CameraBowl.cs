using System.Collections;
using UnityEngine;

public class CameraBowl : MonoBehaviour
{
    static public CameraBowl instance;

    public CameraMultiTarget cameraMultiTarget;

    private int MovePitch = 0; // 0: 何もなし, 1: 90度に近づく, 2: 0度に近づく
    private int MoveYaw = 0; // 0: 何もなし, 1: 時計回り, 2: 反時計回り
    private int MovePadding = 0; // 0: 何もなし, 1: 拡大, 2: 縮小

    private readonly float PitchMin = 0f;
    private readonly float PitchMax = 90f;
    private readonly float YawMin = 0f;
    private readonly float YawMax = 360f;
    private readonly float PaddingMin = 1f;
    private readonly float PaddingMax = 5f;

    private float InitPitch = 0;
    private float InitYaw = 0;
    private float InitPadding = 0;

    private float DiffPitch = 0;
    private float DiffYaw = 0;
    private float DiffPadding = 0;

    private float IntervalTime = 0.01f;

    private void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {
        InitCameraBowl();

        while (true)
        {
            UpdatePitch();
            UpdateYaw();
            UpdatePadding();
            yield return new WaitForSeconds(IntervalTime);
        }
    }

    public void InitCameraBowl()
    {
        MovePitch = RandomChoiceType();
        MoveYaw = RandomChoiceType();
        MovePadding = RandomChoiceType();

        InitPitch = Random.Range(PitchMin, PitchMax);
        InitYaw = Random.Range(YawMin, YawMax);
        InitPadding = Random.Range(PaddingMin, PaddingMax);

        cameraMultiTarget.Pitch = InitPitch;
        cameraMultiTarget.Yaw = InitYaw;
        cameraMultiTarget.PaddingDown = InitPadding;
        cameraMultiTarget.PaddingLeft = InitPadding;
        cameraMultiTarget.PaddingRight = InitPadding;
        cameraMultiTarget.PaddingUp = InitPadding;

        DiffPitch = Random.Range(0f, 0.12f);
        DiffYaw = Random.Range(0f, 0.1f);
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

    private void UpdateYaw()
    {
        if (MoveYaw == 0) return;

        float UpdateValue = MoveYaw == 1 ? DiffYaw : -DiffYaw;
        cameraMultiTarget.Yaw += UpdateValue;
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
