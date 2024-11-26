using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public static int _nextSceneIndex;
    public static int _loadingSceneIndex;

    [SerializeField] Slider _progressBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CRT_LoadScene());
    }

    public static void LoadScene(int sceneIndex)
    {
        _nextSceneIndex = sceneIndex;
        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator CRT_LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextSceneIndex);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                _progressBar.value = Mathf.Lerp(_progressBar.value, op.progress, timer);
                if (_progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                _progressBar.value = Mathf.Lerp(_progressBar.value, 1f, timer);
                if (_progressBar.value == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }
}
