using UnityEngine;

[CreateAssetMenu(fileName = "BikeData", menuName = "ScriptableObjects/BikeData", order = 1)]
public class BikeData : ScriptableObject
{
    public string bikeVersion; // Version or name of the bike
    public Sprite BikeImage;   // Image or icon of the bike
    public int Health;         // Health value for the bike
    public float Boost;        // Boost value for the bike
    public int UpgradePrice;   // Price for upgrading the bike
}
