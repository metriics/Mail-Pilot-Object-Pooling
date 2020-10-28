using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletPoolManager : MonoBehaviour
{
    private static BulletPoolManager _instance;
    public static BulletPoolManager Instance { get { return _instance; } }


    public GameObject bullet;
    public int MaxBullets = 20;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();


    // singleton pattern implemented using @PearsonArtPhoto's method
    // link: https://gamedev.stackexchange.com/a/116010

    // explanation: we check if there is already an instance that is not 'this'
    // if this is the case, that means 'this' is not the original instance and should be destroyed.
    // else, we return this instance since it is the original.


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _BuildBulletPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBullet()
    {
        if (PoolIsEmpty())
        {
            // if we are running out of bullets, add another
            var addition = Instantiate(bullet);
            addition.transform.SetParent(this.transform); // parent to BulletPoolManager
            bulletPool.Enqueue(addition);
        }

        var temp = bulletPool.Dequeue();
        temp.SetActive(true);

        return temp;
    }

    public void ResetBullet(GameObject bullet)
    {
        // disable and slap er back in the queue
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    private void _BuildBulletPool()
    {
        for (int i = 0; i < MaxBullets; i++)
        {
            var temp = Instantiate(bullet);
            temp.transform.SetParent(this.transform); // parent to BulletPoolManager
            bulletPool.Enqueue(temp);
        }
    }

    public int GetPoolSize()
    {
        return bulletPool.Count;
    }

    public bool PoolIsEmpty()
    {
        if (GetPoolSize() == 0)
        {
            return true;
        }

        return false;
    }
}
