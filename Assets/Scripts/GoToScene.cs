using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;

public class GoToScene : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;
    [SerializeField]
    private Animator fadeAnimator;
    [SerializeField]
    private string fadeAnimationName = "FadeOut";
    [SerializeField]
    private UnityEvent onSceneLoad;
    [SerializeField]
    private UnityEvent onStartScene;

    private void Start()
    {
        onStartScene?.Invoke();
    }
    public void LoadScene()
    {
        onSceneLoad?.Invoke();
        SceneManager.LoadScene(_sceneName);
    }
    public void LoadSceneWithFade()
    {
        StartCoroutine(FadeAndLoadScene());
    }
    private IEnumerator FadeAndLoadScene()
    {
        fadeAnimator.Play(fadeAnimationName, 0, 0f);
        yield return new WaitForSeconds(fadeAnimator.GetCurrentAnimatorStateInfo(0).length);
        onSceneLoad?.Invoke();
        SceneManager.LoadScene(_sceneName);
    }
}
