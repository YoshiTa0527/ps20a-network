using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwanPrefab : MonoBehaviour
{
    [SerializeField]
    float interval;
    public float Interval => interval;


    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
