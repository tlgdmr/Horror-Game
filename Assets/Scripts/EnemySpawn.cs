using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] GameObject[] Enemy;

    [SerializeField] LayerMask Ground;

    public int numberOfEnemies = 0;


    [SerializeField] Transform enemyCollector;

    Vector3 spawnPosition;

    [SerializeField] int randomXRange;
    [SerializeField] int randomYRange;
    [SerializeField] int MaxNumberOfEnemy;

    
    private void Update()
    {
        SetSpawnPosition();
    }

    private void SetSpawnPosition()
    {
        while(numberOfEnemies < MaxNumberOfEnemy)
        {
            int RandomXValue = Random.Range(-randomXRange, randomXRange);
            int RandomYValue = Random.Range(-randomYRange, randomYRange);

            spawnPosition = new Vector3(Player.position.x + RandomXValue, Player.position.y, Player.position.z + RandomYValue);

            if (Physics.Raycast(spawnPosition, -transform.up, 30f, Ground))
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, 2);
        Instantiate(Enemy[randomEnemy], spawnPosition, Quaternion.identity, enemyCollector);
        numberOfEnemies++;
    }
}
