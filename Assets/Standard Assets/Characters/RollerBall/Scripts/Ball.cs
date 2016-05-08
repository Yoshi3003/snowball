using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float m_MovePower = 5; // The force added to the ball to move it.
        [SerializeField] private bool m_UseTorque = true; // Whether or not to use torque to move the ball.
        [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
        [SerializeField] private float m_JumpPower = 2; // The force added to the ball when it jumps.

        private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
        private Rigidbody m_Rigidbody;

		private float life = 100f;
		private bool hasJumped = false;
		private bool isInAir = false;
		private const float FALL_DAMAGE = 17f;

		public GUIText lifeText;

        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            // Set the maximum angular velocity.
            GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
			lifeText.text = "Life: " + (int)life;
        }


        public void Move(Vector3 moveDirection, bool jump)
        {
            // If using torque to rotate the ball...
            if (m_UseTorque)
            {
                // ... add torque around the axis defined by the move direction.
                m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x)*m_MovePower);
            }
            else
            {
                // Otherwise add force in the move direction.
                m_Rigidbody.AddForce(moveDirection*m_MovePower);
            }

            // If on the ground and jump is pressed...
			if (isOnGround() && jump)
            {
                // ... add force in upwards.
                m_Rigidbody.AddForce(Vector3.up*m_JumpPower, ForceMode.Impulse);
				hasJumped = true;
            }

			if (!isOnGround () && hasJumped) {
				isInAir = true;
			}
        }

		private void Update() {
			if (isInAir && isOnGround()) {
				Debug.Log ("Ball has landed, losing " + FALL_DAMAGE);
				damage (FALL_DAMAGE);
				isInAir = false;
				hasJumped = false;
			}

			if (!isInAir) {
				heal (0.08f);
			}

			lifeText.text = "Life: " + (int)life;
		}

		private bool isOnGround() {
			return Physics.Raycast (transform.position, -Vector3.up, k_GroundRayLength);
		}

		private void heal(float amount) {
			life = (life + amount >= 100f) ? 100f : life + amount;
		}

		private void damage(float amount) {
			life = (life - amount <= 0f) ? 0f : life - amount;
		}
    }
}
