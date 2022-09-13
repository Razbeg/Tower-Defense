using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private int towerLimit = 4;

    private Queue<Tower> _towerQueue = new Queue<Tower>();
    
    private int _towersCount = 0;

    public void AddTower(Waypoint waypoint)
    {
        _towersCount = _towerQueue.Count;
        
        if (_towersCount < towerLimit)
        {
            InstantiateNewTower(waypoint);
        }
        else
        {
            MoveTowerToNewPosition(waypoint);
        }
    }

    private void InstantiateNewTower(Waypoint waypoint)
    {
        var newTower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity, transform);
        waypoint.isPlaceable = false;
        newTower.waypoint = waypoint;
        _towerQueue.Enqueue(newTower);
    }

    private void MoveTowerToNewPosition(Waypoint waypoint)
    {
        var oldTower = _towerQueue.Dequeue();
        
        oldTower.waypoint.isPlaceable = true;
        oldTower.waypoint = waypoint;
        oldTower.waypoint.isPlaceable = false;
        oldTower.transform.position = waypoint.transform.position;
        
        _towerQueue.Enqueue(oldTower);
    }
}
