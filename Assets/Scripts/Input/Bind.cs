using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerButtons { None, A, B, X, Y, LB, RB, Back, Start, L3, R3 }
public enum BindType { Key, Mouse }
public enum Axis { None, DPadY, DPadX, RightStickY, RightStickX, LeftStickY, LeftStickX, Triggers, RightTrigger, LeftTrigger, MouseX, MouseY }//All these that are on controller need to be in the unity input 
public class Bind : ScriptableObject
{
    //Todo: make delegate function in each bind
    //Todo: make listening happen here instead
    [SerializeField]
    protected string bindName = "";

    public delegate void OnBindChange();
    public OnBindChange onBindChange;

    #region Controller Dictionary and array
    public static Dictionary<ControllerButtons, KeyCode> ControllerButtonToWindowsXboxControllerKeyCode = new Dictionary<ControllerButtons, KeyCode>
    {
        //Buttons
        {ControllerButtons.A,  KeyCode.JoystickButton0},
        {ControllerButtons.B,  KeyCode.JoystickButton1},
        {ControllerButtons.X,  KeyCode.JoystickButton2},
        {ControllerButtons.Y,  KeyCode.JoystickButton3},
        {ControllerButtons.LB,  KeyCode.JoystickButton4},
        {ControllerButtons.RB,  KeyCode.JoystickButton5},
        {ControllerButtons.Back,  KeyCode.JoystickButton6},
        {ControllerButtons.Start,  KeyCode.JoystickButton7},
        {ControllerButtons.L3,  KeyCode.JoystickButton8},
        {ControllerButtons.R3,  KeyCode.JoystickButton9},
        {ControllerButtons.None,  KeyCode.None}
    };
    public static Dictionary<KeyCode, ControllerButtons> WindowsXboxControllerKeyCodeToControllerButton = new Dictionary<KeyCode, ControllerButtons>
   {
        //Buttons
        {KeyCode.JoystickButton0, ControllerButtons.A},
        {KeyCode.JoystickButton1, ControllerButtons.B},
        {KeyCode.JoystickButton2, ControllerButtons.X},
        {KeyCode.JoystickButton3, ControllerButtons.Y},
        {KeyCode.JoystickButton4, ControllerButtons.LB},
        {KeyCode.JoystickButton5, ControllerButtons.RB},
        {KeyCode.JoystickButton6, ControllerButtons.Back},
        {KeyCode.JoystickButton7, ControllerButtons.Start},
        {KeyCode.JoystickButton8, ControllerButtons.L3},
        {KeyCode.JoystickButton9, ControllerButtons.R3},
        {KeyCode.None,  ControllerButtons.None}
    };
    public static KeyCode[] ControllerKeyCodes = new KeyCode[] {
        KeyCode.JoystickButton0,
        KeyCode.JoystickButton1,
        KeyCode.JoystickButton2,
        KeyCode.JoystickButton3,
        KeyCode.JoystickButton4,
        KeyCode.JoystickButton5,
        KeyCode.JoystickButton6,
        KeyCode.JoystickButton7,
        KeyCode.JoystickButton8,
        KeyCode.JoystickButton9,
    };

    #endregion


    public string BindName
    {
        get {
            if(bindName != "")
            {
                return bindName;
            } else
            {
                return name;
            }
        }
    }

    public virtual void LoadKey()
    {
    }
}
