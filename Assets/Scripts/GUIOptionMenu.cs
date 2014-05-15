using UnityEngine;
using System.Collections;

public class GUIOptionMenu : MonoBehaviour {

    public GameScript main;
    public UIToggle mouse, controller, camera;
    public UISlider sensitivity;
    public UIButton back;
	// Use this for initialization
	void Start () {
        Hide();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSensitivityChanged()
    {
        Debug.Log(sensitivity.value);
    }

    public void OnMouseChanged()
    {
        Constants.useController = !mouse.value;
    }

    public void OnControllerChanged()
    {
        Constants.useController = controller.value;
    }

    public void OnInvertCameraChanged()
    {
        Constants.invertCamera = camera.value;
    }

    public void Show()
    {
        controller.value = Constants.useController;
        mouse.value = !controller.value;
        camera.value = Constants.invertCamera;
        //sensitivity.value = (Constants.sensitivity) / 3.0f;
        NGUITools.SetActive(gameObject, true);
    }

    public void Hide()
    {
        NGUITools.SetActive(this.gameObject, false);
    }

    public void Back()
    {
        Hide();
        main.ShowMenu();
    }
}
