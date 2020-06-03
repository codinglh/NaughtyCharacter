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
        public static readonly int UpperState = Animator.StringToHash("UpperState");
        public static readonly int IdlePose = Animator.StringToHash("IdlePose");
	}

	public class CharacterAnimator : MonoBehaviour
	{
		private Animator _animator;
		private Character _character;

		private void Awake()
		{
			_animator = GetComponentInChildren<Animator>();
			_character = GetComponent<Character>();
		}

		public void UpdateState()
		{
			float normHorizontalSpeed = _character.HorizontalVelocity.magnitude / _character.MovementSettings.MaxRunSpeed;
			_animator.SetFloat(CharacterAnimatorParamId.HorizontalSpeed, normHorizontalSpeed);

            Vector3 velocity = _character.HorizontalVelocity;
            float moveForward = Vector3.Dot(_character.transform.forward, velocity);
            float moveRight = Vector3.Dot(_character.transform.right, velocity);

            _animator.SetFloat(CharacterAnimatorParamId.MoveForward, moveForward, 0.15f, Time.deltaTime);
            _animator.SetFloat(CharacterAnimatorParamId.MoveRight, moveRight, 0.15f, Time.deltaTime);

			float jumpSpeed = _character.MovementSettings.JumpSpeed;
			float normVerticalSpeed = _character.VerticalVelocity.y.Remap(-jumpSpeed, jumpSpeed, -1.0f, 1.0f);
			_animator.SetFloat(CharacterAnimatorParamId.VerticalSpeed, normVerticalSpeed);
            _animator.SetInteger(CharacterAnimatorParamId.MovementState, (int)_character.MovementState);

            _animator.SetInteger(CharacterAnimatorParamId.UpperState, (int)_character.UpperState);
		}
	}
}
