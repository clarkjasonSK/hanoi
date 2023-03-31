using UnityEngine;

public static class ScriptableObjectsHelper
{
    public static bool VerifySOt<T>(string filePath) where T : ScriptableObject
    {
        return Resources.Load<T>(filePath) != null ? true : false;
    }

    public static T GetSO<T>(string filePath) where T : ScriptableObject
    {
        return Resources.Load<T>(filePath);
    }

}