using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameConfig gameConfig;
    public int currentWave;
    public Enemy meleePrefab;
    public Enemy rangePrefab;
    public Enemy exploderPrefab;
    void Awake()
    {
        currentWave = gameConfig.wave;
    }
    void Update()
    {
        
    }
}
