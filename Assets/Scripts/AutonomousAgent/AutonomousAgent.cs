using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    [SerializeField] private Perceptio flockPerception;
    public ObstacleAvoidance obstacleAvoidance;
    public AutonomousAgentData data;


    public float wanderAngle { get; set; } = 0;



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
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * data.seekWeight);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * data.fleeWeight);

        }

        gameObjects = flockPerception.GetGameObjects();
        if (gameObjects.Length >= 1)
        {
            movement.ApplyForce(Steering.Cohesion(this, gameObjects) * data.cohesionWeight);
            movement.ApplyForce(Steering.Separation(this, gameObjects, data.separationRadius) * data.separationWeight);
            movement.ApplyForce(Steering.Alignment(this, gameObjects) * data.alignmentWeight);
        }

		if (obstacleAvoidance.IsObstacleInFront())
		{
			Vector3 direction = obstacleAvoidance.GetOpenDirection();
			movement.ApplyForce(Steering.CalculateSteering(this, direction) * data.obstacleWeight);
		}

		if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(Steering.Wander(this));
        }

		Vector3 position = transform.position;
		position = Utilities.Wrap(position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
		position.y = 0;
		transform.position = position;
	}
}
