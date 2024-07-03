using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject orc;

    [SerializeField]
    private float orcInterval = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(orcInterval, orc));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        //Hard coded map range
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-6f,6f), Random.Range(-17f, 2f), 0f), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
