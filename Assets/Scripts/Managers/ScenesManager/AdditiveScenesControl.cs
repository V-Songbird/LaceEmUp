using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JamOff.Scripts.Managers
{

    public class AdditiveScenesControl : MonoBehaviour
    {

        private const string MainSceneName = "main";
        private const string DevelopmentSceneName = "develop";
        public string CurrentLevel { get; set; }
        private readonly string[] _terminations = { "_Logic", "_Layout", "_Decoration" };
        private bool _onProcess;



        // Start is called before the first frame update
        private void Start()
        {
            if (!string.IsNullOrEmpty(CurrentLevel)) return;

            var inDevelopmentScene = false;
            var currentSceneName = "";

            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var loadedScene = SceneManager.GetSceneAt(i);
                if (loadedScene.name.Contains(_terminations[0])) currentSceneName = loadedScene.name.Substring(0, loadedScene.name.IndexOf(_terminations[0]));
                if (loadedScene.name == DevelopmentSceneName) inDevelopmentScene = true;
            }

            if (inDevelopmentScene)
            {
                CurrentLevel = currentSceneName;
            }
        }

        public void LoadLevel(string levelName)
        {
            if (!_onProcess) StartCoroutine(PerformPauseAndLoad(levelName));
        }

        private IEnumerator PerformPauseAndLoad(string levelName)
        {
            _onProcess = true;

            //TO DO : Make Fade In
            Time.timeScale = 0;

            var fadeTime = 0.5f;
            yield return new WaitForSecondsRealtime(fadeTime);


            yield return StartCoroutine(UnloadSceneGroup());
            yield return StartCoroutine(LoadSceneGroup(levelName));

            Time.timeScale = 1;
            //TO DO : Make Fade Out
            _onProcess = false;
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


            foreach (var term in _terminations)
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
            {
                if (loadedScene.name == DevelopmentSceneName) continue;
                if (loadedScene.name == MainSceneName) continue;
                var asyncUnloadScene = SceneManager.UnloadSceneAsync(loadedScene);
                yield return asyncUnloadScene;
            }

            yield return null;
        }


    }

}