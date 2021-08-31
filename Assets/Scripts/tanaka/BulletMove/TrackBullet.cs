using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class TrackBullet : EnemyShootBullet
{
    GameObject[] m_players;
    // Start is called before the first frame update
    void Start()
    {
        m_players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(m_players[0]);
        StartCoroutine(Tracking());
    }

    // Update is called once per frame
    private IEnumerator Tracking()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_interval);
            for (int i = 0; i < m_countAtOnce; i++)
            {
                GameObject m_nearPlayer = m_players.OrderBy(p => Vector2.Distance(p.transform.position, this.transform.position)).ToArray().First();
                var vec = (m_nearPlayer.transform.position - m_barrel.transform.position).normalized;
                var angle = (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) - 90.0f;
                PhotonNetwork.Instantiate(m_bulletResourceName, this.transform.position, m_barrel ? m_barrel.rotation = Quaternion.Euler(0, 0, angle) : Quaternion.identity);
                yield return new WaitForSeconds(m_intervalAtOnce);
            }

        }
    }
}
