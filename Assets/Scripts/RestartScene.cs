using UnityEngine.SceneManagement;
using VContainer.Unity;

public class RestartScene: IStartable
{
    private readonly MainUI _mainUI;

    public RestartScene(MainUI mainUI)
    {
        _mainUI = mainUI;
    }

    public void Start()
    {
        _mainUI.RestartButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
