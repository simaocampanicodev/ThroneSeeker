using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleScreenChanger : MonoBehaviour
{
    [SerializeField] private bool loadNextScene = true;
    [SerializeField] private int targetSceneIndex = 1;
    private bool anyInputTrigger = true;
    private InputAction inputAction;

    void OnEnable()
    {
        if (anyInputTrigger)
        {
            inputAction = new InputAction(type: InputActionType.PassThrough, binding: "*/<Button>");
            inputAction.performed += OnAnyInput;
            inputAction.Enable();
        }
    }

    void OnDisable()
    {
        if (inputAction != null)
        {
            inputAction.performed -= OnAnyInput;
            inputAction.Disable();
            inputAction.Dispose();
        }
    }

    private void OnAnyInput(InputAction.CallbackContext context)
    {
        ChangeScene();
    }

    private void OnSpecificInput(InputAction.CallbackContext context)
    {
        ChangeScene();
    }

    void ChangeScene()
    {
        if (loadNextScene)
        {
            LoadNextScene();
        }
        else
        {
            LoadSpecificScene(targetSceneIndex);
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        LoadSpecificScene(nextSceneIndex);
    }

    void LoadSpecificScene(int sceneIndex)
    {
        // Validate scene index
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void ChangeScenePublic()
    {
        ChangeScene();
    }

    public void LoadSceneByIndex(int index)
    {
        LoadSpecificScene(index);
    }
}