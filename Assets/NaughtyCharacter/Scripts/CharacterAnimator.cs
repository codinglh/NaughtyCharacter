using UnityEngine;

namespace NaughtyCharacter
{
	public static class CharacterAnimatorParamId
	{
		public static readonly int HorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
		public static readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");

        public static readonly int MoveForward = Animator.StringToHash("MoveForward");
        public static readonly int MoveRight = Animator.StringToHash("MoveRight");

        public static readonly int MovementState = Animator.StringToHash("MovementState");
        public static readonly int IdlePose = Animator.StringToHash("IdlePose");
	}

	public class CharacterAnimator : MonoBehaviour
	{
		private Animator _animator;
		private Character _character;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_character = GetComponent<Character>();
		}

		public void UpdateState()
		{
			float normHorizontalSpeed = _character.HorizontalVelocity.magnitude / _character.MovementSettings.MaxHorizontalSpeed;
			_animator.SetFloat(CharacterAnimatorParamId.HorizontalSpeed, normHorizontalSpeed);

            Vector3 normalizedVelocity = _character.HorizontalVelocity.normalized;
            float moveForward = Vector3.Dot(_character.transform.forward, normalizedVelocity) * normHorizontalSpeed;
            float moveRight = Vector3.Dot(_character.transform.right, normalizedVelocity) * normHorizontalSpeed;

            _animator.SetFloat(CharacterAnimatorParamId.MoveForward, moveForward);
            _animator.SetFloat(CharacterAnimatorParamId.MoveRight, moveRight);

			float jumpSpeed = _character.MovementSettings.JumpSpeed;
			float normVerticalSpeed = _character.VerticalVelocity.y.Remap(-jumpSpeed, jumpSpeed, -1.0f, 1.0f);
			_animator.SetFloat(CharacterAnimatorParamId.VerticalSpeed, normVerticalSpeed);
            _animator.SetInteger(CharacterAnimatorParamId.MovementState, (int)_character.MovementState);
		}
	}
}
