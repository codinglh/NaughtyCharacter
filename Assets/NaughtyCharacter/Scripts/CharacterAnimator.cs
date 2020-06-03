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
		private float _currentUpperbodyWeight;
		private float _targetUpperbodyWeight;
		private float _currentDampVelocity;

		private void Awake()
		{
			_animator = GetComponentInChildren<Animator>();
			_character = GetComponent<Character>();
		}

		public void SetUpperBodyWeight(float weight)
		{
			_targetUpperbodyWeight = weight;
		}

		public void UpdateState()
		{
			_currentUpperbodyWeight = Mathf.SmoothDamp(_currentUpperbodyWeight, _targetUpperbodyWeight, ref _currentDampVelocity, 0.2f);
			_animator.SetLayerWeight(1, _currentUpperbodyWeight);

			Debug.LogWarning("set weight: " + _currentUpperbodyWeight + "," + _currentDampVelocity);

            Vector3 velocity = _character.HorizontalVelocity;
            float moveForward = Vector3.Dot(_character.transform.forward, velocity);
            float moveRight = Vector3.Dot(_character.transform.right, velocity);

            _animator.SetFloat(CharacterAnimatorParamId.MoveForward, moveForward, 0.15f, Time.deltaTime);
            _animator.SetFloat(CharacterAnimatorParamId.MoveRight, moveRight, 0.15f, Time.deltaTime);

            _animator.SetInteger(CharacterAnimatorParamId.MovementState, (int)_character.MovementState);

            _animator.SetInteger(CharacterAnimatorParamId.UpperState, (int)_character.UpperState);
		}
	}
}
