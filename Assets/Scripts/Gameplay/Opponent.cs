using SceneControllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Opponent : MonoBehaviour
    {
        [SerializeField] private float TimeBetweenLogicTicks;

        private Hoop hoop;

        private GameController gameController;

        private void Awake()
        {
            hoop = GetComponent<Hoop>();
        }

        private void Start()
        {
            gameController = GameController.Instance;

            StartCoroutine(AICoroutine());
        }

        private IEnumerator AICoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(TimeBetweenLogicTicks);

                if (gameController.Sphere != null)
                {
                    if (gameController.Sphere.transform.position.y > transform.position.y)
                    {
                        if (gameController.Sphere.transform.position.x < transform.position.x)
                        {
                            hoop.JumpLeft();
                        }
                        else
                        {
                            hoop.JumpRight();
                        }
                    }
                }
            }
        }
    }
}

