using UnityEngine;

public class VehicleBehaviour : MonoBehaviour
{
    public Transform[] pathPoints;
    public int pointIndex = 0;
    public float moveSpeed = 1f;
    public float maxDisplacement = 0.05f;
    public float idlingOscillationSpeed = 1f;

    protected virtual void Start()
    {
        transform.position = pathPoints[pointIndex].position;
    }

    protected virtual void Update()
    {
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        float speed = moveSpeed * Time.deltaTime;
        if (pointIndex <= pathPoints.Length - 1)
        {
            float displacement = maxDisplacement * Mathf.Abs(Mathf.Sin(Time.time * idlingOscillationSpeed));
            transform.position = Vector3.MoveTowards(transform.position, pathPoints[pointIndex].position, speed);
            if (transform.position == pathPoints[pointIndex].position)
            {
                pointIndex++;
                if (pointIndex <= pathPoints.Length - 1)
                {
                    transform.LookAt(pathPoints[pointIndex].transform);
                }
            }
        }
        else
        {
            pointIndex = 0;
        }
    }
}