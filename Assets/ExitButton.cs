using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        exitButton=GetComponent<Button>();
        exitButton.onClick.AddListener(QuitGame);

    }

    // Update is called once per frame
    private void QuitGame()
    {
        Application.Quit();
    }
}
