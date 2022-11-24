using UnityEngine;

public static class Utility
{

    public static bool IsWithinRange(Vector2 actor, Vector2 target, float distance)
    {
        if (Vector2.Distance(actor, target) < distance)
        {
            return true;
        }

        return false;
    }

    public static bool IsOutOfRange(Vector2 actor, Vector2 target, float distance)
    {
        if (Vector2.Distance(actor, target) > distance)
        {
            return true;
        }

        return false;
    }

    public static Vector2 GetDirection(Vector2 actor, Vector2 target, bool normalized = true)
    {
        if (normalized)
        {
            return (target - actor).normalized;
        }
        else
        {
            return (target - actor);
        }
    }

}
