using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PosDice
{
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float MinZ;
    public float MaxZ;
}

public class GameProcess : MonoBehaviour
{
    static public GameProcess instance;

    public GameObject InitDice;
    public MeshCollider DiceMeshCollider;
    public int DiceCount = 5;

    public bool IsDroping = true;
    private readonly List<GameObject> Dices = new List<GameObject>();
    private readonly List<PosDice> PosDices = new List<PosDice>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitSetDice();
    }

    void Update()
    {
        if (IsDroping == false) return;
        transform.Rotate(new Vector3(0, 30 * Time.deltaTime, 0));
    }

    void InitSetDice()
    {
        DeleteDices();
        SetDices();
    }

    private void DeleteDices()
    {
        for (int i = 0; i < Dices.Count; i++)
        {
            GameObject DiceObject = Dices[i];
            Destroy(DiceObject);
        }
    }

    private void SetDices()
    {
        for (int i = 0; i < DiceCount; i++)
        {
            Vector3 InitPos = InitRandomPos();
            Quaternion InitQ = Quaternion.Euler(0, 0, Random.Range(-180f, 180f));

            GameObject DiceObject = Instantiate(InitDice, InitPos, InitQ);
            Rigidbody Rigidbody = DiceObject.GetComponent<Rigidbody>();
            Rigidbody.angularVelocity = new Vector3(getRandomPower(), getRandomPower(), getRandomPower());
             
            DiceObject.SetActive(true);
            DiceObject.transform.SetParent(transform);
            Dices.Add(DiceObject);
        }
    }

    private float getRandomPower()
    {
        return Random.Range(-5f, 5f);
    }

    Vector3 InitRandomPos()
    {
        float DiceSize = 2;

        while (true)
        {
            var spawnPos = 3f * Random.insideUnitSphere + InitDice.transform.position;

            float RandomX = Random.Range(spawnPos.x, spawnPos.x);
            float RandomY = Random.Range(spawnPos.y, spawnPos.y);
            float RandomZ = Random.Range(spawnPos.z, spawnPos.z);
            if (CheckDistinctDice(RandomX, RandomY, RandomZ)) continue;

            PosDice PosDice = new PosDice
            {
                MinX = RandomX - DiceSize,
                MaxX = RandomX + DiceSize,
                MinY = RandomY - DiceSize,
                MaxY = RandomY + DiceSize,
                MinZ = RandomZ - DiceSize,
                MaxZ = RandomZ + DiceSize
            };
            PosDices.Add(PosDice);
            return new Vector3(RandomX, RandomY, RandomZ);
        }
    }

    private bool CheckDistinctDice(float RandomX, float RandomY, float RandomZ)
    {
        if (PosDices.Count == 0) return false;

        for (int i = 0; i < PosDices.Count; i++)
        {
            PosDice PosDice = PosDices[i];
            if (PosDice.MinX <= RandomX && RandomX <= PosDice.MaxX &&
                PosDice.MinY <= RandomY && RandomY <= PosDice.MaxY &&
                PosDice.MinZ <= RandomZ && RandomZ <= PosDice.MaxZ)
                return true;
        }
        return false;
    }
}
