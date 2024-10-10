using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRotateObj : MonoBehaviour
{
    public Vector3 m_SpeedRotation;

    private void Start()
    {
        float randomValue = Random.Range(0f, 1f);
        transform.Rotate(m_SpeedRotation * randomValue);
    }

    private void Update()
    {
        transform.Rotate(m_SpeedRotation * Time.deltaTime);
    }
}
