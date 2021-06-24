using UnityEngine;



[CreateAssetMenu(fileName = "Situaition_MainStage_Scripts", menuName = "ScriptableObjects/Situaition_MainStage_Scripts", order = 2)]
public sealed class Situaition_MainStage_Scripts : ScriptableObject
{
    public string[] titles;
    public string[] mainTexts;
    public int[] charactorIndexesPerText;
    public ImotType[] imots;
    public ImotDirection[] imotDirection;
}