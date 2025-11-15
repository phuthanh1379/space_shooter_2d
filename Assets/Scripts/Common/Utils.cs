using UnityEngine;

namespace Common
{
    public static class Utils
    {
        public static float GetFormatVolumeValue(float value) => Mathf.Round(value * 100);
    }
}