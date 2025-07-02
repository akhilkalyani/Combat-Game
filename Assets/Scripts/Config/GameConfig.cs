using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class GameConfig : ScriptableObject
{
    public int wave = 2;
}
