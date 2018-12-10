using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InputState { Pressed, Held, Released }

/// <summary>
/// this holds all the binds you want and sends them through delegate, also can listen to to keys and set the bind for it
/// </summary>
public class InputController : MonoBehaviour {

    public KeyBind[] binds;
    public AxisBind[] axes;
    [Range(0,2)]
    public float axisDeadZone = 0;
    private bool isListening = false;
    private KeyBind listenKey;

    private bool isKey;
    private int keyindex;
    private bool isButton;
    #region Singleton and Delegate
    public delegate void OnButtonInput(string axis, InputState state);
    public OnButtonInput onButtonInput;
    public delegate void OnAxisInput(string axis, float state);
    public OnAxisInput onAxisInput;
    public static InputController instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of InputController found");
            return;
        }
        instance = this;
        SetHotkeys();
    }
    #endregion
    
    private void SetHotkeys() {
        PlayerPrefs.DeleteAll();
        for(int i = 0; i < binds.Length; i++)
        {
            binds[i].LoadKey();
        }
        for(int i = 0; i < axes.Length; i++)
        {
            axes[i].LoadKey();
        }
    }

    void Update () {
        if(!isListening)
        {
            CheckButtonInput();
            CheckAxisInput();
        }
    }



    private void CheckButtonInput() {
        for(int i = 0; i < binds.Length; i++)
        {
                
            bool foundInput = CheckKey(binds[i].ControllerBind, binds[i].BindName);
            if(!foundInput)
            {
                KeyCode[] allBinds = binds[i].Binds;
                for(int j = 0; j < allBinds.Length; j++)
                {
                    foundInput = CheckKey(allBinds[j], binds[i].BindName);
                    if(foundInput)
                    {
                        break;
                    }
                }
            }
        }
    }
    private bool CheckKey(KeyCode key, string name)
    {
        if(Input.GetKey(key))
        {
            if(Input.GetKeyDown(key))
            {
                onButtonInput.Invoke(name, InputState.Pressed);
                return true;
            } else
            {
                onButtonInput.Invoke(name, InputState.Held);
                return true;
            }
        } else if(Input.GetKeyUp(key))
        {
            onButtonInput.Invoke(name, InputState.Released);
            return true;
        }
        return false;
    }
    //Todo: add virtualbuttons from axis

    private void CheckAxisInput()
    {
        for(int i = 0; i < axes.Length; i++)
        {
            float value = CheckAxis(axes[i].ActiveController);
            if(Mathf.Abs(value) >= axisDeadZone)
            {
                
                onAxisInput.Invoke(axes[i].BindName, value);
                continue;
            }
            if(axes[i].IsAxis)
            {
                value = CheckAxis(axes[i].ActiveAxis);
            } else
            {
                value =  CheckVirtualAxis(axes[i].ActivePositive, axes[i].ActiveNegative);
            }
            if(Mathf.Abs(value) >= axisDeadZone)
            {
                onAxisInput.Invoke(axes[i].BindName, value);
                continue;
            }
        }
    }
    private float CheckAxis(string axes) {
        return Input.GetAxisRaw(axes);
    }
    private float CheckVirtualAxis(KeyCode Positive, KeyCode Negative) {
        float value = 0;
        if(Input.GetKey(Positive))
        {
            value += 1;
        }
        if(Input.GetKey(Negative))
        {
            value -= 1;
        }
        return value;
    }



    


    //Todo: add ability to set axis
    public void ListenKey(KeyBind key, int index)
    {
        if(key.Binds.Length > index && index >= 0 )
        {
            isListening = true;
            isKey = true;
            keyindex = index;
            listenKey = key;
        } else
        {
            Debug.LogWarning("Index at " + index + " for key " + key.BindName + " does not exist to listen to");
        }
        
    }
    public void ListenController(KeyBind key)
    {
        isListening = true;
        isButton = true;
        listenKey = key;
    }
    


    //Reasigns bind keys
    void OnGUI()
    {
        if(isListening)
        {
            Event thisEvent = Event.current;
            if(isKey)
            {

                if(thisEvent.isKey)
                {
                    listenKey.BindKey(thisEvent.keyCode, keyindex);
                    isListening = false;
                    isKey = false;
                } else if(Input.GetKey(KeyCode.Mouse4))
                {
                    listenKey.BindKey(KeyCode.Mouse4, keyindex);
                    isListening = false;
                    isKey = false;
                } else if(Input.GetKey(KeyCode.Mouse3))
                {
                    listenKey.BindKey(KeyCode.Mouse3, keyindex);
                    isListening = false;
                    isKey = false;
                }
            } else if(isButton)
            {
                foreach(KeyCode item in KeyBind.ControllerKeyCodes)
                {
                    if(Input.GetKey(item))
                    {
                        listenKey.BindController(item);
                        isListening = false;
                        isButton = false;
                        return;
                    }
                }
            }
        }
    }

}
