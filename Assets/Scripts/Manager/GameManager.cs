using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int score = 0;

    [SerializeField]
    private int goalCoin = 15;

    private enum OBJ { COIN, GHOST };
    private Dictionary<OBJ, int> maxSize = new Dictionary<OBJ, int>();
    private Dictionary<OBJ, float> createTime = new Dictionary<OBJ, float>();
    private Dictionary<OBJ, GameObject> Prefabs = new Dictionary<OBJ, GameObject>();
    private Dictionary<OBJ, GameObject> Parents = new Dictionary<OBJ, GameObject>();
    private Dictionary<OBJ, List<GameObject>> Pools = new Dictionary<OBJ, List<GameObject>>();

    public static GameManager gameManager = null;
    
    private void Awake()
    {
        gameManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePool(OBJ.COIN, "Coin", 10, 1.0f);
        CreatePool(OBJ.GHOST, "Ghost", 5, 2.0f);

        StartCoroutine(ActivePool(OBJ.COIN));
        StartCoroutine(ActivePool(OBJ.GHOST));
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void PlaceObject(GameObject obj)
    {
        Transform tr = obj.GetComponent<Transform>();
        tr.position = new Vector3(Random.Range(-10, 10), 0.6f, Random.Range(-10, 10));
    }

    private bool CheckClear()
    {
        if (score < goalCoin) return false;
        Debug.LogFormat("Clear! (score : {0})", score);
        return true;
    }

    public void RaiseScore(int number) { score += number; }

    IEnumerator<object> ActivePool(OBJ OBJtype)
    {
        while (!CheckClear())
        {
            yield return new WaitForSeconds(createTime[OBJtype]);

            foreach (GameObject obj in Pools[OBJtype]) {
                if (obj.activeSelf) continue;

                PlaceObject(obj);
                obj.SetActive(true);
                break;
            }
        }
    }
}
