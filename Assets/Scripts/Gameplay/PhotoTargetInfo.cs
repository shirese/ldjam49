using UnityEngine;

[CreateAssetMenu(fileName ="PhotoTarget Info",menuName ="Game/PhotoTarget Info")]
public class PhotoTargetInfo : ScriptableObject
{
    public bool important;
    public string publicName;
    public float points;
}
