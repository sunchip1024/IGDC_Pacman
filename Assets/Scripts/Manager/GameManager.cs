using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver;

    [SerializeField]
    private static int score = 0;

    [SerializeField]
    private static int goalCoin = 15;

    private enum OBJ { COIN, GHOST_RED, GHOST_BLUE };
    private Dictionary<OBJ, int> maxSize = new Dictionary<OBJ, int>();
    private Dictionary<OBJ, float> createTime = new Dictionary<OBJ, float>();
    private Dictionary<OBJ, GameObject> Prefabs = new Dictionary<OBJ, GameObject>();
    private Dictionary<OBJ, GameObject> Parents = new Dictionary<OBJ, GameObject>();
    private Dictionary<OBJ, List<GameObject>> Pools = new Dictionary<OBJ, List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        CreatePool(OBJ.COIN, "Coin", 10, 1.0f);
        CreatePool(OBJ.GHOST_RED, "Ghost_RED", 5, 2.0f);
        CreatePool(OBJ.GHOST_BLUE, "Ghost_BLUE", 3, 3.0f);

        StartCoroutine(ActivePool(OBJ.COIN));
        StartCoroutine(ActivePool(OBJ.GHOST_RED));
        StartCoroutine(ActivePool(OBJ.GHOST_BLUE));
    }

    private void CreatePool(OBJ OBJtype, string path, int size, float time)
    {
        Prefabs.Add(OBJtype, Resources.Load<GameObject>("Prefabs/" + path));
        Pools.Add(OBJtype, new List<GameObject>());
        maxSize.Add(OBJtype, size);
        createTime.Add(OBJtype, time);

        GameObject prefabParent = new GameObject(Prefabs[OBJtype].name + "s");
        prefabParent.transform.position = Vector3.zero;
        Parents.Add(OBJtype, prefabParent);

        for (int i = 0; i < maxSize[OBJtype]; ++i)
        {
            GameObject obj = Instantiate<GameObject>(Prefabs[OBJtype]);
            obj.name = Prefabs[OBJtype].name + i.ToString();
            
            if(prefabParent != null)    obj.transform.parent = prefabParent.transform;

            PlaceObject(obj);
            obj.SetActive(false);
            Pools[OBJtype].Add(obj);
        }
    }

    public static void PlaceObject(GameObject obj)
    {
        Transform tr = obj.GetComponent<Transform>();
        tr.position = new Vector3(Random.Range(-10, 10), 0.6f, Random.Range(-10, 10));
    }

    private bool isClear()
    {
        if (score < goalCoin) return false;
        return true;
    }

    public static void RaiseScore(int number) { score += number; }

    public static void setGameOver() { isGameOver = true;  }

    System.Collections.IEnumerator ActivePool(OBJ OBJtype)
    {
        while (!isClear() && !isGameOver)
        {
            yield return new WaitForSeconds(createTime[OBJtype]);

            foreach (GameObject obj in Pools[OBJtype]) {
                if (obj.activeSelf) continue;

                PlaceObject(obj);
                obj.SetActive(true);
                break;
            }
        }

        yield return null;
    }
}
