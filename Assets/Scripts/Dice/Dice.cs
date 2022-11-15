using UnityEngine;

public class Dice : MonoBehaviour
{
    public GameObject Bowl;

    private void Update()
    {
        if (getDistance() && gameObject.layer != 10)
        {
            gameObject.layer = 10;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != Bowl) return;
        GameProcess.instance.IsDroping = false;
    }

    private bool getDistance()
    {
        Vector2 BowlPos = new Vector2(Bowl.transform.position.x, Bowl.transform.position.z);
        Vector2 DicePos = new Vector2(transform.position.x, transform.position.z);
        return 9.25 <= Vector2.Distance(BowlPos, DicePos);
    }
}
