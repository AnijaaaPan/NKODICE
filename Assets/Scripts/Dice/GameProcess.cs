using UnityEngine;
using System.Collections;
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

    public CameraMultiTarget cameraMultiTarget;
    public GameObject InitDice;
    public MeshCollider DiceMeshCollider;
    public int DiceCount = 5;

    public bool IsDroping = true;
    private List<GameObject> CameraDices = new List<GameObject>();

    private readonly List<GameObject> Dices = new List<GameObject>();
    private readonly List<PosDice> PosDices = new List<PosDice>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Time.timeScale = 0.25f;
        InitSetDice();
        StartCoroutine("RotateYDice");
    }

    private IEnumerator RotateYDice()
    {
        while (true)
        {
            if (IsDroping == false) yield break;

            transform.Rotate(new Vector3(0, 30 * Time.deltaTime, 0));
            yield return new WaitForSeconds(0.1f);
        }
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
        CameraDices = Dices;
        cameraMultiTarget.SetTargets(CameraDices.ToArray());
    }

    private float getRandomPower()
    {
        return Random.Range(-2.5f, 2.5f);
    }

    Vector3 InitRandomPos()
    {
        float DiceSize = 2.5f;

        while (true)
        {
            Vector3 SpawnPos = 3f * Random.insideUnitSphere + InitDice.transform.position;

            float RandomX = Random.Range(SpawnPos.x, SpawnPos.x);
            float RandomY = Random.Range(SpawnPos.y, SpawnPos.y);
            float RandomZ = Random.Range(SpawnPos.z, SpawnPos.z);
            if (CheckDistinctDice(RandomX, RandomY, RandomZ)) continue;

            PosDice PosDice = new()
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

    public void RemoveTargetFromCamera(GameObject TargetObject)
    {
        CameraDices.Remove(TargetObject);
        cameraMultiTarget.SetTargets(CameraDices.ToArray());
    }
}
