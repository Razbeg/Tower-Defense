using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float moveStep;
    [SerializeField] private ParticleSystem castleDamageParticle;
    
    private Pathfinder _pathfinder;
    private Castle _castle;

    private Vector3 _targetPos;

    private void Start()
    {
        _castle = FindObjectOfType<Castle>();
        _pathfinder = FindObjectOfType<Pathfinder>();
        var path = _pathfinder.GetPath();
        
        StartCoroutine(EnemyMove(path));
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * speed);
    }

    private IEnumerator EnemyMove(List<Waypoint> path)
    {
        foreach (var waypoint in path)
        {
            transform.LookAt(waypoint.transform);
            _targetPos = waypoint.transform.position;
            yield return new WaitForSeconds(moveStep);
        }
        
        _castle.Damage();
        GetComponent<EnemyDamage>().DestroyEnemy(castleDamageParticle, false);
    }
}
