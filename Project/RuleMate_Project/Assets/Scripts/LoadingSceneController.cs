using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using Photon.Pun;

public class LoadingSceneController : MonoBehaviour
{
    private static LoadingSceneController loadingInstance;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Slider progressBar;
    [SerializeField] TextMeshProUGUI txtLoading;
    private string loadSceneName;

    public static LoadingSceneController LoadingInstance
    {
        get
        {
            if (loadingInstance == null)
            {
                var obj = FindObjectOfType<LoadingSceneController>();

                if (obj != null) 
                    loadingInstance = obj;
                else 
                    loadingInstance = Create();
            }
            return loadingInstance;
        }
    }

    private static LoadingSceneController Create()
    {
        return Instantiate(Resources.Load<LoadingSceneController>("LoadingUI"));
    }

    private void Awake()
    {
        if (LoadingInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        progressBar.value = 0f;
        txtLoading.text = 0 + "%";
        yield return StartCoroutine(Fade(true));
        
        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
        op.allowSceneActivation = false;    //씬 로딩이 끝나도 자동으로 씬 전환이 되지 않도록

        float timer = 0f;
        while (!op.isDone)  //씬 로딩이 끝나지 않았다면
        {
            yield return null;
            if (op.progress < 0.9f)
            {
                progressBar.value = op.progress * 100;
                txtLoading.text = (int)progressBar.value + "%";
            }
            else
            {
                timer += Time.unscaledDeltaTime;

                progressBar.value = Mathf.Lerp(0.9f, 1f, timer) * 100;
                txtLoading.text = (int)progressBar.value + "%";

                if (progressBar.value >= 100f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    //콜백함수
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == loadSceneName) //불러온 씬 == 부르려 했던 씬
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;  //등록했던 콜백 제거
        }
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 3f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator LoadLevelAsync()
    {
        PhotonNetwork.LoadLevel("MultiMainGameTest 1");

        while (PhotonNetwork.LevelLoadingProgress < 1f)
        {
            double progress = Mathf.Clamp01(PhotonNetwork.LevelLoadingProgress);
            Debug.Log(progress);
            yield return null;
        }
    }
}
