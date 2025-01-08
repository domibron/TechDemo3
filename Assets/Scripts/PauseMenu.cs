using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
	public class PauseMenu : MonoBehaviour
	{
		public static PauseMenu Instance;


		public GameObject PauseMenuObject;

		public bool IsPaused = false;



		void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}

		// Start is called before the first frame update
		void Start()
		{
			Resume();
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				IsPaused = !IsPaused;
			}

			if (IsPaused)
			{
				Time.timeScale = 0;
				PauseMenuObject.SetActive(true);

				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else
			{
				Time.timeScale = 1;
				PauseMenuObject.SetActive(false);

				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}

		public void Resume()
		{
			IsPaused = false;
		}

		public void MainMenu()
		{


			SceneManager.LoadScene(0);
		}

		public void Quit()
		{
			Application.Quit();
		}
	}
}
