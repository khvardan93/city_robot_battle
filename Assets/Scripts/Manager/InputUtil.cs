using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public static class InputUtil
{
	private static InputAction _action;
	
	static InputUtil()
	{
		WeaponInputs.Add(WeaponType.Shot, false);
		WeaponInputs.Add(WeaponType.Fire, false);
		WeaponInputs.Add(WeaponType.Laser, false);
		WeaponInputs.Add(WeaponType.Rocket, false);
	}
	
	public static readonly Dictionary<WeaponType, bool> WeaponInputs = new();

	public static Vector2 LookChangeVector;
	public static Vector2 MoveChangeVector;
}
