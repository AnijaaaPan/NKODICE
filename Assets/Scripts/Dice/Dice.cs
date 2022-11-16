using UnityEngine;
using System.Threading.Tasks;

public class Dice : MonoBehaviour
{
    public GameObject Bowl;

    void Start()
    {
        ChangeDiceLayer();
    }

    private async void ChangeDiceLayer()
    {
        while (true)
        {
            if (IsGetDistance()) {
                gameObject.layer = 10;
                GameProcess.instance.RemoveTargetFromCamera(gameObject);
                break;
            }

            await Task.Delay(100);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != Bowl) return;

        if (GameProcess.instance.Type == 0)
        {
            GameProcess.instance.InitSetDice();
            CameraDice.instance.InitCameraDice();
        } else {
            GameProcess.instance.IsDroping = false;
        }
    }

    private bool IsGetDistance()
    {
        Vector2 BowlPos = new Vector2(Bowl.transform.position.x, Bowl.transform.position.z);
        Vector2 DicePos = new Vector2(transform.position.x, transform.position.z);
        return 9.25 <= Vector2.Distance(BowlPos, DicePos);
    }
}
