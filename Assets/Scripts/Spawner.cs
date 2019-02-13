using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawnedObjects;

    public GameObject[] tapObjects;
    public GameObject spawnParticle;
    public bool stop = true;

    int randomObject;
    float randWaitTime;

    public void StartingGame()
    {
        stop = false;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
        while (!stop)
        {
            if (Random.value > 0.4)
                randomObject = 0;
            else
                randomObject = 1;

            Debug.Log(randomObject);

            Instantiating(RandomPositon(8, 4), tapObjects[randomObject], spawnParticle);
            randWaitTime = Random.Range(0.2f, 1f);
            yield return new WaitForSeconds(randWaitTime);
        }
    }
    private Vector3 RandomPositon(float x,float y)
    {
        Vector3 randomPostion = new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0);
        return randomPostion;
    }
    void Instantiating(Vector3 pos, GameObject randObj, GameObject particle)
    {
        GameObject go = Instantiate(randObj, pos, transform.rotation);
        spawnedObjects.Add(go);
        Instantiate(particle, pos, particle.transform.rotation);
    }
    
    public void ClearObjList()
    {
        for(int i = 0; i< spawnedObjects.Count; i++)
        {
            if (spawnedObjects[i] != null)
                Destroy(spawnedObjects[i]);
        }
    }
}
