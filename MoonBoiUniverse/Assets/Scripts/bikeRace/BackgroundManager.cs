using UnityEngine;

[System.Serializable]
public struct Background
{
    public Transform gameObject;
    public Transform endPoint;
}

public class BackgroundManager : MonoBehaviour
{
    public Background[] backgrounds; // Array of your background structs
    public BikeController bikeController;

    private int bottomIndex = 0;
    private int mediumIndex = 1;
    private int topIndex = 2;

    void Update()
    {
        // Calculate the midpoint of the current medium background
        float midPoint = (backgrounds[mediumIndex].gameObject.position.y + backgrounds[mediumIndex].endPoint.position.y) / 2;

        // If the player has crossed the midpoint of the medium background, shift the backgrounds up
        if (bikeController.transform.position.y > midPoint)
        {
            // Move the bottom background to the top
            backgrounds[bottomIndex].gameObject.position = new Vector3(backgrounds[bottomIndex].gameObject.position.x, backgrounds[topIndex].endPoint.position.y, backgrounds[bottomIndex].gameObject.position.z);

            // Update the indices
            int oldBottomIndex = bottomIndex;
            bottomIndex = mediumIndex;
            mediumIndex = topIndex;
            topIndex = oldBottomIndex;
        }
    }
}
