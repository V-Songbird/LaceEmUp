using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JamOff.Scripts.Managers
{
    public class AdditiveScenesControl : MonoBehaviour
    {
        private const string MainSceneName = "main";
        private const string DevelopmentSceneName = "develop";
        private readonly string[] terminations = { "_Logic", "_Layout", "_Decoration", "_Lighting" };
        private bool onProcess;
        public string CurrentLevel { get; set; }

        private void Start()
        {
            if (!string.IsNullOrEmpty(CurrentLevel)) return;

            var inDevelopmentScene = false;
            var currentSceneName = "";

            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var loadedScene = SceneManager.GetSceneAt(i);
                if (loadedScene.name.Contains(terminations[0]))
                    currentSceneName =
                        loadedScene.name[..loadedScene.name.IndexOf(terminations[0], StringComparison.Ordinal)];
                if (loadedScene.name == DevelopmentSceneName) inDevelopmentScene = true;
            }

            if (inDevelopmentScene) CurrentLevel = currentSceneName;
        }

        public void LoadLevel(string levelName)
        {
            if (!onProcess) StartCoroutine(PerformPauseAndLoad(levelName));
        }

        private IEnumerator PerformPauseAndLoad(string levelName)
        {
            onProcess = true;

            //TO DO : Make Fade In
            Time.timeScale = 0;

            const float fadeTime = 0.5f;
            yield return new WaitForSecondsRealtime(fadeTime);


            yield return StartCoroutine(UnloadSceneGroup());
            yield return StartCoroutine(LoadSceneGroup(levelName));

            Time.timeScale = 1;
            //TO DO : Make Fade Out
            onProcess = false;
        }


        private IEnumerator LoadSceneGroup(string levelName)
        {
            var currentNumberOfLoadedScenes = SceneManager.sceneCount;
            var loadedScenes = new Scene[currentNumberOfLoadedScenes];

            for (var i = 0; i < currentNumberOfLoadedScenes; i++) loadedScenes[i] = SceneManager.GetSceneAt(i);


            foreach (var loadedScene in loadedScenes)
            {
                if (loadedScene.name != DevelopmentSceneName || loadedScene.name != MainSceneName) continue;

                break;
            }


            foreach (var term in terminations)
            {
                var asyncLoadScene = SceneManager.LoadSceneAsync("Scenes/" + levelName + "/" + levelName + term,
                    LoadSceneMode.Additive);
                yield return asyncLoadScene;
            }

            yield return null;
        }

        private IEnumerator UnloadSceneGroup()
        {
            var currentNumberOfLoadedScenes = SceneManager.sceneCount;
            var loadedScenes = new Scene[currentNumberOfLoadedScenes];

            for (var i = 0; i < currentNumberOfLoadedScenes; i++) loadedScenes[i] = SceneManager.GetSceneAt(i);

            foreach (var loadedScene in loadedScenes)
                switch (loadedScene.name)
                {
                    case DevelopmentSceneName:
                    case MainSceneName:
                        continue;
                    default:
                    {
                        var asyncUnloadScene = SceneManager.UnloadSceneAsync(loadedScene);
                        yield return asyncUnloadScene;
                        break;
                    }
                }

            yield return null;
        }
    }
}