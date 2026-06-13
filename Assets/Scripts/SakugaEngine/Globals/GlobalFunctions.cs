using UnityEngine;

namespace SakugaEngine.Global
{
    public static class GlobalFunctions
    {
        public static Vector2 ToScaledVector2(Vector2Int vector)
        {
            return new Vector2
            (
                vector.x / (float)GlobalVariables.SimulationScale,
                vector.y / (float)GlobalVariables.SimulationScale
            );
        }
        public static Vector3 ToScaledVector3(Vector2Int vector, float zScale = 0f)
        {
            return new Vector3
            (
                vector.x / (float)GlobalVariables.SimulationScale,
                vector.y / (float)GlobalVariables.SimulationScale,
                zScale
            );
        }
        public static Vector3 ToScaledVector3(Vector3Int vector)
        {
            return new Vector3
            (
                vector.x / (float)GlobalVariables.SimulationScale,
                vector.y / (float)GlobalVariables.SimulationScale,
                vector.z / (float)GlobalVariables.SimulationScale
            );
        }
        public static float ToScaledFloat(int value)
        {
            return value / (float)GlobalVariables.SimulationScale;
        }

        public static int IntLerp(int from, int to, int numberOfSteps, int currentStep)
        {
            return ((to - from) * currentStep) / numberOfSteps;
        }

        public static bool IsOnRange(int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static Vector2Int Distance(Vector2Int a, Vector2Int b)
        {
            int dx = b.x - a.x;
            int dy = b.y - a.y;

            if (b.x < a.x) dx = a.x - b.x;
            if (b.y < a.y) dx = a.y - b.y;

            return new Vector2Int(dx, dy);
        }

        public static int HorizontalDistance(Vector2Int a, Vector2Int b)
        {
            int dx = a.x - b.x;
            int dy = 0;

            return dx * dx + dy * dy;
        }

        public static int VerticalDistance(Vector2Int a, Vector2Int b)
        {
            int dx = 0;
            int dy = a.y - b.y;

            return dx * dx + dy * dy;
        }

        public static float SineWaveFunction(float amp, float len, int time, float phase)
        {
            return amp * Mathf.Sin(len * ((float)time / GlobalVariables.TicksPerSecond) + phase);
        }

        public static string GetPlayerPrefix(int id)
        {
            switch (id)
            {
                case 0:
                    return "k1";
                case 1:
                    return"k2";
                case 2:
                    return "j1";
                case 3:
                    return "j2";
                default:
                    return "general";
            }
        }
    }
}
