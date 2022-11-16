using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour
{
    static public GameStart instance;

    public CameraMultiTarget cameraMultiTarget;
    public GameObject[] BowlObject;

    private CameraDice CameraDice;
    private CameraBowl CameraBowl;

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

    public void InitGame(int GameType)
    {
        GameProcess.instance.Type = GameType;
        StartCoroutine(FadeInOutImage.instance.FadeInOut(false, 0.0075f));

        CameraDice.enabled = false;
        CameraBowl.enabled = false;

        cameraMultiTarget.SetTargets(BowlObject);
        StartCoroutine(FadeInOutImage.instance.FadeInOut(true, 0.055f));
    }
}
