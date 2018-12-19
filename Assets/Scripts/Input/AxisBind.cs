using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New AxisBind", menuName = "Input/AxisBind")]
public class AxisBind : Bind
{


    //Hiden Default prefs
    [SerializeField]
    private bool isAxis;
    //Key and mouse axis virtual or otherwise
    private Axis defaultAxis;
    private KeyCode defaultPositive;
    private KeyCode defaultNegative;
    //controller axis
    private Axis defaultController;
    //Keys as player has customized
    private string activeAxis;
    private KeyCode activePositive;
    private KeyCode activeNegative;
    private string activeController;

    public bool IsAxis
    {
        get {
            return isAxis;
        }
    }
    public string ActiveAxis
    {
        get {
            return activeAxis;
        }
    }
    public KeyCode ActivePositive
    {
        get {
            return activePositive;
        }
    }
    public KeyCode ActiveNegative
    {
        get {
            return activeNegative;
        }
    }
    public string ActiveController
    {
        get {
            return activeController;
        }
    }

    /// <summary>
    /// Call to load playerprefs binds into exposed KeyCodes
    /// </summary>
    public override void LoadKey()
    {
        base.LoadKey();
        if(defaultAxis != Axis.None)
        {
            activeAxis =  PlayerPrefs.GetString(BindName + "Axis", defaultAxis.ToString());
        }
        if(defaultPositive != KeyCode.None)
        {
            activePositive = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(BindName + "PositiveAxis", defaultPositive.ToString()));
        }
        if(defaultNegative != KeyCode.None)
        {
            activeNegative = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(BindName + "NegativeAxis", defaultNegative.ToString()));
        }
        if(defaultController != Axis.None)
        {
            activeController = PlayerPrefs.GetString(BindName + "ControllerAxis", defaultController.ToString());
        }
    }

    /// <summary>
    /// Bind a key at index
    /// </summary>
    /// <param name="newKey"></param>
    /// <param name="index"></param>
    public void BindAxis(Axis newKey)
    {
        activeAxis = newKey.ToString();
        PlayerPrefs.SetString(BindName + "Axis", newKey.ToString());
        onBindChange.Invoke();
    }
    //Todo: save editor values
    [CustomEditor(typeof(AxisBind))]
    public class LevelScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            AxisBind myTarget = (AxisBind)target;
            myTarget.isAxis = EditorGUILayout.Toggle("Is default an axis?", myTarget.isAxis);
            if(myTarget.isAxis)
            {
                myTarget.defaultAxis = (Axis)EditorGUILayout.EnumPopup("Mouse Axis", myTarget.defaultAxis);
            } else
            {
                myTarget.defaultPositive = (KeyCode)EditorGUILayout.EnumPopup("Positive Key", myTarget.defaultPositive);
                myTarget.defaultNegative = (KeyCode)EditorGUILayout.EnumPopup("Negative Key", myTarget.defaultNegative);
            }
            myTarget.defaultController = (Axis)EditorGUILayout.EnumPopup("Controller Axis", myTarget.defaultController);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
