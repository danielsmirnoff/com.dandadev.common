using System.Collections.Generic;
using UnityEngine;

public class SpatialHashGrid<Key, Value>
{
    protected Dictionary<Key, Value> _grid;
    
    public SpatialHashGrid()
    {
        _grid = new Dictionary<Key, Value>();
    }

    public bool HasTile(Key key)
    {
        return _grid.ContainsKey(key);
    }
    
    public Value GetTile(Key key)
    {
        return _grid[key];
    }

    public virtual bool CanAddTile(Key key, Value value)
    {
        return true;
    }

    public virtual bool CanRemoveTile(Key key)
    {
        return true;
    }

    public bool AddTile(Key key, Value value)
    {
        if(HasTile(key)) return false;
        if(!CanAddTile(key, value)) return false;
        
        _grid.Add(key, value);
        return true;
    }

    public bool RemoveTile(Key key)
    {
        if(!HasTile(key)) return false;
        if(!CanRemoveTile(key)) return false;

        _grid.Remove(key);
        return true;
    }
}