using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject orc;

    [SerializeField]
    private GameObject monster;

    [SerializeField]
    private float orcInterval;

    [SerializeField]
    private float monsterInterval;

    private float x_margin = 1;
    private float y_margin = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(orcInterval, orc));
        StartCoroutine(spawnEnemy(monsterInterval, monster));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        //Hard coded map range

        Vector3 spwan = new Vector3(Random.Range(-6f, 6f), Random.Range(-17f, 2f), 0f);

        var xdiff = Mathf.Abs(spwan.x - GameManager.Instance.player.transform.position.x);
        var ydiff = Mathf.Abs(spwan.y - GameManager.Instance.player.transform.position.y);

        while (xdiff <= x_margin || ydiff <= y_margin)
        {
            spwan = new Vector3(Random.Range(-6f, 6f), Random.Range(-17f, 2f), 0f);
            xdiff = Mathf.Abs(spwan.x - GameManager.Instance.player.transform.position.x);
            ydiff = Mathf.Abs(spwan.y - GameManager.Instance.player.transform.position.y);
        }


        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-6f,6f), Random.Range(-17f, 2f), 0f), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
