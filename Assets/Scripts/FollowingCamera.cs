using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Transform followedTransform;

    [SerializeField] private float devider;

    private Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
    }

    private void Update()
    {
        Vector3 offset = new Vector3(0, followedTransform.position.y / devider, 0);
        transform.position = initialPos + offset;
    }
}
