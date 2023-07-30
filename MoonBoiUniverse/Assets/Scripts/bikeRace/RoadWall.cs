using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadWall : MonoBehaviour
{
    public BikeController bikeController;
    public float yOffset = 5f; // This is the offset. Adjust as needed.

    void Update()
    {
        if (bikeController != null)
        {
            Vector3 bikePosition = bikeController.transform.position;
            Vector3 cameraPosition = new Vector3(transform.position.x, bikePosition.y + yOffset, transform.position.z);
            transform.position = cameraPosition;
        }
        else
        {
            Debug.Log(gameObject.name + "BikeController is not assigned.");
        }
    }
}
