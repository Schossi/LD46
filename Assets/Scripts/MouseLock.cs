using UnityEngine;
using System.Collections;

public class MouseLock : MonoBehaviour
{
	public KeyCode escapeKey = KeyCode.Escape;

	public static bool IsLocked = false;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			IsLocked = true;
		}

		if (Input.GetKeyDown(escapeKey))
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			IsLocked = false;
		}
	}
}
