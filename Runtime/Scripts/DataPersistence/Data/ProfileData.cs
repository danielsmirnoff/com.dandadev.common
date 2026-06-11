using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all information about all progression the player has unlocked.
/// This includes the collection, stats, current game, triggered events, etc.
/// </summary>
[System.Serializable]
public class ProfileData
{
    public string profileName;
    public bool isNewSave;

    public List<string> triggeredEvents;

    public ProfileData(string profileName)
    {
        this.profileName = profileName;
        isNewSave = true;
    }
}
