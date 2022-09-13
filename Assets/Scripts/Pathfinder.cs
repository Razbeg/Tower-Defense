using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Waypoint startPoint;
    [SerializeField] private Waypoint endPoint;
    [SerializeField] private Mesh cursedPath;
    
    private Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();
    private Queue<Waypoint> _queue = new Queue<Waypoint>();
    private List<Waypoint> _path = new List<Waypoint>();

    private bool _isRunning = true;
    private Waypoint _searchPoint;

    private Vector2Int[] _directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        if (_path.Count == 0)
        {
            CalculatePath();
        }

        return _path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        PathFindAlgorithm();
        CreatePath();
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        foreach (var waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            
            if (_grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Overlapping " + waypoint);
            }
            else
            {
                _grid.Add(gridPos, waypoint);
            }
        }
    }

    private void PathFindAlgorithm()
    {
        _queue.Enqueue(startPoint);

        while (_queue.Count > 0 && _isRunning)
        {
            _searchPoint = _queue.Dequeue();
            _searchPoint.isExplored = true;
            CheckForEndPoint();
            ExploreNearPoints();
        }
    }

    private void CheckForEndPoint()
    {
        if (_searchPoint == endPoint)
        {
            _isRunning = false;
        }
    }
    
    private void ExploreNearPoints()
    {
        if (!_isRunning)
        {
            return;
        }
        
        foreach (var direction in _directions)
        {
            var nearPointCoordinate = _searchPoint.GetGridPos() + direction;
            
            if (_grid.ContainsKey(nearPointCoordinate))
            {
                AddPointToQueue(_grid[nearPointCoordinate]);
            }
        }
    }

    private void AddPointToQueue(Waypoint waypoint)
    {
        if (waypoint.isExplored || _queue.Contains(waypoint)) return;
        
        var nearPoint = waypoint;
        _queue.Enqueue(nearPoint);
        nearPoint.exploredFrom = _searchPoint;
    }

    private void CreatePath()
    {
        AddToPath(endPoint);
        var prevPoint = endPoint.exploredFrom;
        
        while (prevPoint != startPoint)
        {
            AddToPath(prevPoint);
            prevPoint = prevPoint.exploredFrom;
        }
        
        AddToPath(startPoint);
        _path.Reverse();
    }

    private void AddToPath(Waypoint waypoint)
    {
        _path.Add(waypoint);
        waypoint.isPlaceable = false;
        waypoint.GetComponentInChildren<MeshFilter>().mesh = cursedPath;
    }
}
