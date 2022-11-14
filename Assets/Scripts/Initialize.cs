using UnityEngine;

public class Initialize : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void RuntimeInitializeApplication()
    {
        Debug.Log(Application.targetFrameRate);
        Application.targetFrameRate = 60;
        Debug.Log(Application.targetFrameRate);
    }
}