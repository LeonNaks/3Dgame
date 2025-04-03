using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
        public void OnButtonClick()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Button được bấm!");
    }
}