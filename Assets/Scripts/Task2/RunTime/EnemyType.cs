using System;

[Flags]
public enum EnemyType
{
    Solider = 1 << 0,
    Tank = 1 << 1,
    Art = 1 << 2
}