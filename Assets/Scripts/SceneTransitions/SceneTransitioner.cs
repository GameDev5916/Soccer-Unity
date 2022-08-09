using SceneControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneTransitions
{
    public class SceneTransitioner : MonoBehaviour
    {
        #region Singleton

        public static SceneTransitioner Instance { get; private set; }

        private void SetupSingleton()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
        }

        #endregion

        private void Awake()
        {
            SetupSingleton();
        }

        public void OpenScene(string toSceneName, string fromSceneName = null, Dictionary<string, object> args = null)
        {
            if (SceneManager.sceneCount > 1)
            {
                return;
            }

            Scene activeScene = SceneManager.GetActiveScene();

            if (fromSceneName == null)
            {
                fromSceneName = activeScene.name;
            }

            var handler = SceneManager.LoadSceneAsync(toSceneName, LoadSceneMode.Additive);
            handler.allowSceneActivation = true;

            SceneController activeController = FindSceneController(activeScene);
            StartCoroutine(activeController.OnSceneUnloaded());

            handler.completed += delegate (AsyncOperation _)
            {
                FindCamera(activeScene).enabled = false;

                Scene newScene = SceneManager.GetSceneByName(toSceneName);
                SceneManager.SetActiveScene(newScene);

                StartCoroutine(UnloadSceneWithDelay(activeScene));

                SceneController newController = FindSceneController(newScene);
                newController.ReceiveArgs(args);
                StartCoroutine(newController.OnSceneLoaded());
            };
        }

        private Camera FindCamera(Scene scene)
        {
            Camera camera = null;
            var rootGOs = scene.GetRootGameObjects();

            foreach (GameObject go in rootGOs)
            {
                camera = go.GetComponent<Camera>();
                if (camera != null)
                {
                    break;
                }
            }

            return camera;
        }

        private SceneController FindSceneController(Scene scene)
        {
            SceneController sceneController = null;
            var rootGOs = scene.GetRootGameObjects();

            foreach (GameObject go in rootGOs)
            {
                sceneController = go.GetComponent<SceneController>();
                if (sceneController != null)
                {
                    break;
                }
            }

            return sceneController;
        }

        private IEnumerator UnloadSceneWithDelay(Scene scene)
        {
            yield return new WaitForSecondsRealtime(0.7f);

            SceneManager.UnloadSceneAsync(scene);
        }
    }
}

