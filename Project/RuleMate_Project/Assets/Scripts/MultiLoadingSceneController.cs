using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiLoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField] Slider progressBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        PhotonNetwork.LoadLevel("Loading");
    }

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        progressBar.value = 0f;
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        StartCoroutine(Load());
        
        float timer = 0f;
        while(PhotonNetwork.LevelLoadingProgress <= 1f)
        {
            yield return null;

            Debug.Log(PhotonNetwork.LevelLoadingProgress);

            if (PhotonNetwork.LevelLoadingProgress < 0.9f)
            {
                progressBar.value = PhotonNetwork.LevelLoadingProgress * 100;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.value = Mathf.Lerp(0.9f, 1f, timer) * 100;

                if(progressBar.value >= 100f)
                {
                    yield break;
                }
            }
        }
    }

    IEnumerator Load()
    {
        PhotonNetwork.LoadLevel(nextScene);
        yield return null;
    }
}
