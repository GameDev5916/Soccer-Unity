using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Hoop : MonoBehaviour
    {
        [SerializeField] private float jumpingForce;
        [Range(0f, 1f)]
        [SerializeField] private float jumpingAngle;
        [SerializeField] private float maxTorque;

        [SerializeField] private Rigidbody rigidbody;


        public void JumpRight()
        {
            rigidbody.velocity = new Vector3(jumpingAngle, 1, 0) * jumpingForce;
            ApplyRandomTorque();
        }

        public void JumpLeft()
        {
            rigidbody.velocity = new Vector3(-jumpingAngle, 1, 0) * jumpingForce;
            ApplyRandomTorque();
        }

        public void ApplyRandomTorque()
        {
            rigidbody.AddTorque(new Vector3(0, 0, Random.Range(-maxTorque, maxTorque)));
        }
    }

}
