using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public Enemy Prefab;
    public Launcher[] Launchers;

    private int _stage = 0;

    private float _timeout = 5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("x"))
            SpawnStage(10);

        if ((_stage > 0 && Enemy.Active == 0) || _timeout <= 0f)
        {
            _stage++;
            _timeout = SpawnStage(_stage);
        }
        else
        {
            _timeout -= Time.deltaTime;
        }
    }

    private float SpawnStage(int stage)
    {
        GetComponent<AudioSource>().Play();

        switch (stage)
        {
            case 1:

                spawnEnemy();
                spawnEnemy();

                return 30f;
            case 2:

                spawnEnemy();
                spawnEnemy();
                spawnEnemy();
                spawnEnemy();

                return 60f;
            case 3:

                spawnEnemy();
                spawnEnemy();
                spawnEnemy();
                spawnEnemy();
                spawnEnemy();
                spawnEnemy();

                return 60f;
            default:

                for (int i = 0; i < stage + 2; i++)
                {
                    spawnEnemy();
                }

                return 30f;
        }
    }

    private void spawnEnemy()
    {
        var enemy = Instantiate(Prefab);

        var size = Random.Range(5, 10);

        enemy.transform.localScale = new Vector3(size, size, size);

        var launcher = Random.Range(0, Launchers.Length);

        Launchers[launcher].Launch(enemy);
    }
}
