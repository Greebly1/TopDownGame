using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] GameObject prefabTemplate;
    List<GameObject> objectPool = new List<GameObject>();
    public int maxObjects = 10;

    public static Dictionary<GameObject, GameObjectPool> pools = new Dictionary<GameObject, GameObjectPool>();

    bool isLoadingObjects = false;
    Coroutine loadingCoroutine = null;

    #region Pool functions
    public GameObject firstAvailableObject { get => objectPool.First(obj => !obj.activeInHierarchy); } 
    bool poolEmpty
    {
        get
        {
            foreach (GameObject obj in objectPool)
            {
                if (obj.activeInHierarchy) { return false; }
            }
            return true;
        }
    }
    public GameObject PoolInstantiate(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (poolEmpty) { /* TODO: make functionality for increasing max capacity*/ }

        GameObject chosenObject = firstAvailableObject;
        chosenObject.SetActive(true);
        chosenObject.transform.position = spawnPosition;
        chosenObject.transform.rotation = spawnRotation;
        return chosenObject;
    }
    #endregion


    #region Monobehavior Callbacks
    private void Awake()
    {
        //TODO: add a check that deletes this gameobject if the prefab template itself contains an object pool


        if (pools.TryAdd(prefabTemplate, this))
        {

        } else
        {
            Debug.LogError("An object pool tried to duplicate an existing object pool");
        }
    }

    private void Start()
    {
        if (isLoadingObjects == false) { loadingCoroutine = StartCoroutine("FillPool"); }
    }

    #endregion

    //Instantiates 1 object each frame until the pool is filled
    IEnumerator FillPool()
    {
        isLoadingObjects = true;

        while (objectPool.Count < maxObjects)
        {
            objectPool.Add(
                Instantiate(prefabTemplate, Vector3.zero, Quaternion.identity)
                );
            objectPool.Last().SetActive(false);

            yield return null;
        }

        isLoadingObjects = false;
    }
}
