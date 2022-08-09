using Cinemachine;
using DG.Tweening;
using Gameplay;
using Pooling;
using SceneTransitions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneControllers
{
    public class GameController : SceneController
    {
        #region Fields
        public static GameController Instance { get; private set; }

        [Header("Hoops")]
        [SerializeField] private List<Hoop> hoops;

        [Header("Controls")]
        [SerializeField] private ButtonControls buttonControls;

        [Header("Spheres")]
        [SerializeField] private GameObject SpherePrefab;
        public MainSphere Sphere;

        [Header("Particle Systems")]
        [SerializeField] private List<ParticleSystem> particles;

        [Header("Camera")]
        [SerializeField] private CinemachineVirtualCamera camera;
        [SerializeField] private CinemaMachineShake cameraShake;
        [SerializeField] private float shakeIntensity;
        [SerializeField] private float shakeDuration;

        [Header("Score")]
        [SerializeField] private float maxScore;
        [SerializeField] private Image scoreboard;
        private List<int> scores;

        [Header("Timer")]
        [SerializeField] private int seconds;

        [Header("Time Stop")]
        [SerializeField] private float timeStopDelay;
        [SerializeField] private float timeStopMultiplier;

        #endregion

        #region Events

        public event Action<int, int> ScoreChanged;
        public event Action<int> TimeChanged;
        public event Action<int> GameEnded;

        #endregion

        private void Awake()
        {
            name = "Game";

            SetUpInstance();
        }

        private void SetUpInstance()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));

            Sphere = SimplePool.Spawn(SpherePrefab, new Vector3(0, 10, 0),
                Quaternion.identity).GetComponentInChildren<MainSphere>();

            Sphere.PlayerScored += OnPlayerScored;
            GameEnded += OnGameEnded;

            camera.m_Follow = Sphere.transform;

            PopulateScores();

            buttonControls.BindControls(hoops[0]);

            StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            while (seconds > 0)
            {
                yield return new WaitForSeconds(1);
                seconds--;

                TimeChanged?.Invoke(seconds);
            }

            int index = 0;
            int maxScore = 0;

            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i] > maxScore)
                {
                    maxScore = scores[i];
                    index = i;
                }
            }

            GameEnded?.Invoke(index);
        }

        private void PopulateScores()
        {
            scores = new List<int>(hoops.Count);

            for (int i = 0; i < hoops.Count; i++)
            {
                scores.Add(0);
            }
        }

        private void OnPlayerScored(Hoop hoop)
        {
            int index = hoops.IndexOf(hoop);
            scores[index]++;

            ScoreChanged?.Invoke(index, scores[index]);

            particles.ForEach(ps => ps.Play());
            cameraShake.ShakeCamera(shakeIntensity, shakeDuration);

            if (scores[index] >= maxScore)
            {
                GameEnded?.Invoke(index);
            }
        }

        #region Game Ending

        private void OnGameEnded(int index)
        {
            Sphere.StopColliding();

            StopAllCoroutines();

            StartCoroutine(ExitGameCoroutine(index));
        }

        private IEnumerator ExitGameCoroutine(int index)
        {
            yield return FreezeTimeCoroutine();

            SceneTransitioner.Instance.OpenScene("GameResultsMenu", args: new Dictionary<string, object>()
            {
                ["won"] = (index == 0)
            });
        }

        private IEnumerator FreezeTimeCoroutine()
        {
            yield return new WaitForSeconds(timeStopDelay);

            while (Time.timeScale > 0.1f)
            {
                Time.timeScale -= Time.deltaTime * timeStopMultiplier;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                yield return new WaitForEndOfFrame();
            }

            Time.timeScale = 0.1f;
        }

        #endregion

        #region OnLoad/Unload

        public override IEnumerator OnSceneLoaded()
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;


            scoreboard.rectTransform.DOMove(new Vector3(0, 0, 250), 1f);
            hoops[0].gameObject.transform.DOMoveX(-2, 1f);
            hoops[1].gameObject.transform.DOMoveX(2, 1f);

            yield return new WaitForEndOfFrame();
        }

        public override void ReceiveArgs(Dictionary<string, object> args)
        {
            string maxScoreKey = "maxScore";

            if (args != null && args.ContainsKey(maxScoreKey))
            {
                maxScore = (int)args[maxScoreKey];
            }
        }

        #endregion
    }
}

