using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;

    Queue<Step> currentSteps;

    void Start()
    {
        currentSteps = GameManager.Instance.GetSteps();
        if(currentSteps != null)
            Spawn();
    }

    void Spawn()
    {
        GameObject clone = Instantiate(PlayerPrefab, this.transform.position, Quaternion.identity);
        clone.tag = "Enemy";
        PlayerStateManager playerStateManager = clone.GetComponent<PlayerStateManager>();
        playerStateManager.SetState(new PlayerStateEnemy());
        playerStateManager.StepsRecord = this.currentSteps;
    }
}
