using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BindOption : MonoBehaviour {
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI joyText;
    public KeyBind key;

    public void SetUp() {
        key.onBindChange += SetText;
        SetText();
    }

    public void SetText() {
        mainText.text = key.BindName;
        buttonText.text = key.Binds[0].ToString();
        joyText.text = KeyBind.WindowsXboxControllerKeyCodeToControllerButton[key.ControllerBind].ToString();
    }

    public void BindOne() {
        InputController.instance.ListenKey(key, 0);
        buttonText.text = "";
    }
    public void BindTwo()
    {
        InputController.instance.ListenKey(key, 1);
    }
    public void BindController()
    {
        InputController.instance.ListenController(key);
        joyText.text = "";
    }
}
