using UnityEngine;



public enum ImotType
{
    None,
    IAmNanayang,
    Cheer,
    Talking,
    Timo,
    Warning
};
public enum ImotDirection
{
    None, Left, Right
};

[CreateAssetMenu(fileName = "Situaition_Home_Intro_Scripts", menuName = "ScriptableObjects/Situaition_Home_Intro", order = 1)]
public sealed class Situaition_Home_Intro_Scripts : ScriptableObject
{
    public Vector3 targetCameraPosition;
    public float baseCameraScale;
    public Vector2 targetCameraScale;
    public float cameraMoveSpeed;
    public float cameraScaleSpeed;

    public string[] titles;
    public string[] mainTexts;
    public int[] charactorIndexesPerText;
    public ImotType[] imots;
    public ImotDirection[] imotDirection;
}