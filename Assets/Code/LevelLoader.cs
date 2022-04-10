using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    public class LevelLoader : MonoBehaviour
    {
        public enum LoadingState
        {
            None,
            Started,
            InProgress,
            Options
        }
        public const string LoaderName = "Loader";
        public const string OptionsName = "OptionsMenu";

        public static LevelLoader Current
        {
            get;
            private set;
        }
        private LoadingState state = LoadingState.None;

        // viittaus alkuperäseen sceneen
        private Scene originalScene;
        // seuraavan scenen nimi
        private string nextSceneName;
        // viittaus loading-sceneen
        private Scene loadingScene;
        private Scene optionsScene;

        // Nk. Singleton, eli tästä oliosta voi olla vain yksi kopio olemassa kerralla
        private void Awake()
        {
            if(Current == null)
            {
                Current = this;
            }
            else
            {
                // LevelLoader on jo olemassa, tuhotaan uusi instanssi
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
        private void OnEnable()
        {
            // aletaan kuunteleen eventtiä
            SceneManager.sceneLoaded += OnLevelLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelLoaded;
        }
        public void LoadLevel(string sceneName)
        {
            nextSceneName = sceneName;
            originalScene = SceneManager.GetActiveScene();
            state = LoadingState.Started;
            // ladataan loading screen additiivisesti (nykyisen scenen rinnalle)
            SceneManager.LoadSceneAsync(LoaderName, LoadSceneMode.Additive);
        }

        public void LoadOptions()
        {
            Debug.Log("toimi saatana");
            state = LoadingState.Options;
            SceneManager.LoadSceneAsync(OptionsName, LoadSceneMode.Additive);
        }

        public void CloseOptions()
        {
            SceneManager.UnloadSceneAsync(optionsScene);
        }
        private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
        {
            switch(state)
            {
                case LoadingState.Started:
                    loadingScene = scene;
                    // aloitetaan fade animaatio
                    GameObject[] rootObjects = loadingScene.GetRootGameObjects(); // palauttaa scenen kaikki root-GameObjectit
                    foreach (GameObject item in rootObjects)
                    {
                        Fader fader = item.GetComponentInChildren<Fader>();
                        if(fader != null)
                        {
                            float fadeTime = fader.FadeIn();
                            StartCoroutine(ContinueLoad(fadeTime));
                            break; // poistutaan loopista
                        }
                    }
                break;
                case LoadingState.InProgress:
                    foreach (GameObject item in loadingScene.GetRootGameObjects())
                    {
                        Fader fader = item.GetComponentInChildren<Fader>();
                        if(fader != null)
                        {
                            float fadeTime = fader.FadeOut();
                            StartCoroutine(FinalizedLoad(fadeTime));

                            state = LoadingState.None;
                            break; // poistutaa loopista
                        }
                    }
                break;
            case LoadingState.Options:
                optionsScene = scene;
                break;
            }
        }

        private IEnumerator FinalizedLoad(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            SceneManager.UnloadSceneAsync(loadingScene);
        }
        private IEnumerator ContinueLoad(float waitTime)
        {
            yield return new WaitForSeconds(waitTime); // odottaa waitTime:n verran, suoritus jatkuu waitTime.n kuluttua

            SceneManager.UnloadSceneAsync(originalScene);
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
            state = LoadingState.InProgress;
        }
    }
}