using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    float interval;
    [SerializeField]
    int amount;
    [SerializeField]
    string prefabName;

    float timer;
    int count;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer -= interval;
            count++;
            PhotonNetwork.Instantiate(prefabName, this.transform.position, Quaternion.identity);
            if (count >= amount)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
