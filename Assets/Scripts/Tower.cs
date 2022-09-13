using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tower : MonoBehaviour
{
    public Waypoint waypoint;
    
    [SerializeField] private Transform towerTop;
    [SerializeField] private Transform targetEnemy;
    [SerializeField] private float shootRange;
    [SerializeField] private ParticleSystem bulletParticles;

    private void Update()
    {
        if (targetEnemy)
        {
            towerTop.LookAt(targetEnemy.position + new Vector3(0f, towerTop.position.y, 0f));
            Fire();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }

        var closestEnemy = sceneEnemies[0].transform;
        foreach (var enemy in sceneEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy, enemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform firstEnemy, Transform secondEnemy)
    {
        var distToFirst = Vector3.Distance(firstEnemy.position, transform.position);
        var distToSecond = Vector3.Distance(secondEnemy.position, transform.position);

        return distToFirst < distToSecond ? firstEnemy : secondEnemy;
    }

    private void Fire()
    {
        var distanceToEnemy = Vector3.Distance(targetEnemy.position, transform.position);
        Shoot(distanceToEnemy <= shootRange);
    }

    private void Shoot(bool isActive)
    {
        var emission = bulletParticles.emission;
        emission.enabled = isActive;

        if (!isActive)
        {
            SetTargetEnemy();
        }
    }
}
