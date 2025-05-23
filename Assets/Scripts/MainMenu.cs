using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
	public class MainMenu : MonoBehaviour
	{
		void Start()
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		public void LoadLevel(int i)
		{
			SceneManager.LoadScene(i);
		}

		public void Quit()
		{
			Application.Quit();
		}
	}
}
