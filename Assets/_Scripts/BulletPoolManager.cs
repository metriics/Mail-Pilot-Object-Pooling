using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!

[System.Serializable]
public class BulletPoolManager : MonoBehaviour
{
    public GameObject bullet;
    public int MaxBullets = 20;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

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
