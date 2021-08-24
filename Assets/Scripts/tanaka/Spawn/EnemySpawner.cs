using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class EnemySpawner : MonoBehaviour
{
    SpawnData data;

    bool seted;
    bool start;
    int count;

    float timer;
    public void SetData(SpawnData data)
    {
        this.data = data;
        seted = true;
    }

    private void Update()
    {
        if (!seted)
        {
            return;
        }
        if (!start)
        {
            if (data.Type == ESpawnDataType.Wait)
            {
                Destroy(this.gameObject);
            }
            start = true;
            return;
        }

        timer += Time.deltaTime;
        if (timer >= data.SpawnInterval)
        {
            timer -= data.SpawnInterval;
            count++;
            PhotonNetwork.Instantiate(data.Obj, this.transform.position, Quaternion.identity);
            if (count >= data.SpawnAmount)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
