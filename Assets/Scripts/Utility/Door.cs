using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private bool to_outside;
        [SerializeField]
        private Transform _destination;
        [SerializeField]
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void ShowPopUp()
        {
            _animator.SetBool("show", true);
        }

        public void Interact()
        {
            if (to_outside)
            {
                StartCoroutine(TeleportPlayerToWorld());
            }
            else
            {
                CountersManager counters_manager = GameObject.FindGameObjectWithTag(Tags.LOGIC_TAG).GetComponent<CountersManager>();
                counters_manager.StopOxygenCounter();
                StartCoroutine(TeleportPlayerToBunker());
            }
        }

        private IEnumerator TeleportPlayerToWorld()
        {
            GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            PlayerController player_controller = player.GetComponent<PlayerController>();
            PlayerCamera player_camera = Camera.main.GetComponent<PlayerCamera>();
            player_controller.PlayInteractAnimation();
            GameUIManager game_ui_manager = GameObject.FindGameObjectWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
            yield return StartCoroutine(game_ui_manager.ShowBlackScreen());


            player_controller.SetOutsideAnimatorController();
            player.transform.position = _destination.position;
            player_camera.SnapCameraOnPlayer();

            yield return StartCoroutine(game_ui_manager.HideBlackScreen());
            player_controller.EnableMovement();

            CountersManager counters_manager = GameObject.FindGameObjectWithTag(Tags.LOGIC_TAG).GetComponent<CountersManager>();
            counters_manager.StartOxygenCounter();

            yield return null;
        }

        private IEnumerator TeleportPlayerToBunker()
        {
            GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            PlayerController player_controller = player.GetComponent<PlayerController>();
            PlayerCamera player_camera = Camera.main.GetComponent<PlayerCamera>();
            player_controller.PlayInteractAnimation();

            GameUIManager game_ui_manager = GameObject.FindGameObjectWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
            yield return StartCoroutine(game_ui_manager.ShowBlackScreen());

            player_controller.SetBunkerAnimatorController();
            player.transform.position = _destination.position;
            player_camera.SnapCameraOnPlayer();

            yield return StartCoroutine(game_ui_manager.HideBlackScreen());
            player_controller.EnableMovement();

            yield return null;
        }

        public void HidePopUp()
        {
            _animator.SetBool("show", false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
                ShowPopUp();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
                HidePopUp();
        }
    }
}