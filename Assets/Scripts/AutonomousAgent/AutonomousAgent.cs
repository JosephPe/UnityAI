using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    // Update is called once per frame
    void Update()
    {
        var gameObjects = perception.GetGameObjects();
        foreach (var gameObject in gameObjects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position);
        }
        if (gameObjects.Length >= 1) 
        {
            Debug.Log(gameObjects[0]);
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * 0);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * 1);

        }
        transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }
}
