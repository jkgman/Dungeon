using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Make one of these for each button function you want to have
/// </summary>
[CreateAssetMenu(fileName = "New KeyBind", menuName = "Input/KeyBind")]
public class KeyBind : Bind
{
    //Default Keys
    [SerializeField, Tooltip("Default button binds, supports multi-bind")]
    private KeyCode[] defaultBinds;
    [SerializeField, Tooltip("Controller Button Bind")]
    private ControllerButtons defaultControllerBind;

    //Keys as player has customized
    private KeyCode[] activeBinds;
    private KeyCode activeControllerBind;

    public KeyCode[] Binds
    {
        get {
            return activeBinds;
        }
    }
    public KeyCode ControllerBind
    {
        get {
            return activeControllerBind;
        }
    }

    /// <summary>
    /// Call to load playerprefs binds into exposed KeyCodes
    /// </summary>
    public override void LoadKey()
    {
        base.LoadKey();
        activeBinds = new KeyCode[defaultBinds.Length];
        for(int i = 0; i < defaultBinds.Length; i++)
        {
            activeBinds[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(BindName + i, defaultBinds[i].ToString()));
        }
        if(defaultControllerBind != ControllerButtons.None)
        {
            activeControllerBind = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(BindName + "Joy", ControllerButtonToWindowsXboxControllerKeyCode[defaultControllerBind].ToString()));
        }
    }

    /// <summary>
    /// Bind a key at index
    /// </summary>
    /// <param name="newKey"></param>
    /// <param name="index"></param>
    public void BindKey(KeyCode newKey, int index)
    {
        if(index >=0 && index < activeBinds.Length)
        {
            activeBinds[index] = newKey;
            PlayerPrefs.SetString(BindName + index, newKey.ToString());
            onBindChange.Invoke();
        }
    }
    /// <summary>
    /// bind a controller key at index
    /// </summary>
    /// <param name="newButton"></param>
    public void BindController(KeyCode newButton)
    {
        activeControllerBind = newButton;
        PlayerPrefs.SetString(BindName + "Joy", newButton.ToString());
        onBindChange.Invoke();
    }
}