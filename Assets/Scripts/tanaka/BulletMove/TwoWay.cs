using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TwoWay : EnemyShootBullet
{
    [SerializeField] int m_bulletAngle;
    private void Start()
    {
        StartCoroutine(TwoWayBullet());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TwoWayBullet()
    {
        while(true)
        {
            yield return new WaitForSeconds(m_interval);
            for (int i = 0; i < m_countAtOnce; i++)
            {
                PhotonNetwork.Instantiate(m_bulletResourceName, this.transform.position, m_barrel ? m_barrel.rotation = Quaternion.Euler(0, 0, 90 - m_bulletAngle) : Quaternion.identity);
                PhotonNetwork.Instantiate(m_bulletResourceName, this.transform.position, m_barrel ? m_barrel.rotation = Quaternion.Euler(0, 0, 90 + m_bulletAngle) : Quaternion.identity);
                yield return new WaitForSeconds(m_intervalAtOnce);
            }
            
        }
    }
}
