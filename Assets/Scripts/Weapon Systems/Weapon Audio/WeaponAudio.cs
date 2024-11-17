using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
	[RequireComponent(typeof(AudioSource))]
	public class WeaponAudio : MonoBehaviour, IWeaponAudio
	{
		public AudioClip SFX_WeaponFire;
		public AudioClip SFX_WeaponAim;
		public AudioClip SFX_WeaponUnAim;
		public AudioClip SFX_WeaponReload;
		public AudioClip SFX_WeaponSpecialAction;

		// privates
		private AudioSource audioSource;


		void Start()
		{
			SetUpAudio();
		}


		void IWeaponAudio.Aim()
		{
			PlayAimAudio();
		}

		void IWeaponAudio.UnAim()
		{
			PlayUnAimAudio();
		}


		void IWeaponAudio.Fire()
		{
			PlayFireAudio();
		}

		void IWeaponAudio.Reload()
		{
			PlayReloadAim();
		}

		void IWeaponAudio.SpecialAction()
		{
			PlaySpecialActionAudio();
		}

		protected virtual void SetUpAudio()
		{
			audioSource = GetComponent<AudioSource>();
		}

		protected virtual void PlayFireAudio()
		{
			if (SFX_WeaponFire == null) return;

			audioSource.PlayOneShot(SFX_WeaponFire);
		}

		protected virtual void PlayAimAudio()
		{
			if (SFX_WeaponAim == null) return;

			audioSource.PlayOneShot(SFX_WeaponAim);
		}

		protected virtual void PlayUnAimAudio()
		{
			if (SFX_WeaponUnAim == null) return;


			audioSource.PlayOneShot(SFX_WeaponUnAim);

		}

		protected virtual void PlayReloadAim()
		{
			if (SFX_WeaponReload == null) return;


			audioSource.PlayOneShot(SFX_WeaponReload);

		}

		protected virtual void PlaySpecialActionAudio()
		{
			if (SFX_WeaponSpecialAction == null) return;

			audioSource.PlayOneShot(SFX_WeaponSpecialAction);

		}
	}
}
