using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perceptio : MonoBehaviour
{
    //public Perceptio flockPerception;

    public string tagName = "";
    [Range(1, 40)]public float distance = 1.0f;
    [Range(0, 180)]public float maxAngle = 45;

    public abstract GameObject[] GetGameObjects();

	public int CompareDistance(GameObject a, GameObject b)
    {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }
}
