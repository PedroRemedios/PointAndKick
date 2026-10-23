using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Combat.CombatAttacks;
using Entities.HealthStatus.Damageables;
using GameFlow.EventChannels.GeneralEC;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player {
    public class PlayerController : Controller {
        [Header("Broadcasting On")]
        public BoolEventChannelSO orientationChannel;
        
        private PlayerInputActions playerInputActions;

        // Movement Input
        private Vector2 _inputVector;
        
        // Jump Input
        [NonSerialized] public bool jumpInput;
        
        // Combat Input
        [NonSerialized] public bool lightAttackInput;
        [NonSerialized] public bool heavyAttackInput;
        [NonSerialized] public bool attackModifierInput;
        [NonSerialized] public bool guardDodgeInput;
        
        // Combat Variables
        [NonSerialized] public bool attackInProgress;
        [NonSerialized] public int attackCombo;

        // Control Variables
        [NonSerialized] public bool isFacingRight = true;
        
        // Walking/Running Variables
        private enum MovementDirection {Up, Down, Left, Right, None};
        private MovementDirection previousMoveTap = MovementDirection.None;
        private float runInputTime = Mathf.NegativeInfinity; 
        private bool preRunning;
        private bool isMoving;
        
        [NonSerialized] public bool isRunning;

        private void Start() {
            playerInputActions = InputManager.Instance.GetPlayerInputActions();
            DefineExplorationMovementActions();
            DefineCombatMovementActions();
        }

        private void Update() {
            AdjustGroundMovement();
            ScalePlayer();
        }
        
        private void AdjustGroundMovement() {
            adjustedGroundMovement = new Vector2(_inputVector.x, _inputVector.y + _inputVector.x * groundSlope).normalized;
        }
        
        
        /* * * * * * * * * *
         * INPUT CALLBACKS *
         * * * * * * * * * */
        
        private void DefineExplorationMovementActions() {
            playerInputActions.Exploration.Move.performed += OnMovePressed;
            playerInputActions.Exploration.Move.canceled += OnMoveReleased;
            playerInputActions.Exploration.Jump.performed += OnJumpPressed;
            playerInputActions.Exploration.Jump.canceled += OnJumpReleased;
        }

        private void DefineCombatMovementActions() {
            playerInputActions.Combat.Move.performed += OnMovePressed;
            playerInputActions.Combat.Move.canceled += OnMoveReleased;
            playerInputActions.Combat.Jump.performed += OnJumpPressed;
            playerInputActions.Combat.Jump.canceled += OnJumpReleased;
            playerInputActions.Combat.LightAttack.performed += OnLightAttackPressed;
            playerInputActions.Combat.HeavyAttack.performed += OnHeavyAttackPressed;
            playerInputActions.Combat.DiaboloAttack.started += OnAttackModifierPressed;
            playerInputActions.Combat.DiaboloAttack.canceled += OnAttackModifierReleased;
            playerInputActions.Combat.GuardDodge.started += OnGuardDodgePressed;
            playerInputActions.Combat.GuardDodge.canceled += OnGuardDodgeReleased;
        }

        private void OnMovePressed(InputAction.CallbackContext callbackContext) {
            _inputVector = callbackContext.ReadValue<Vector2>();
            OnRunStart(_inputVector);
        }
        
        private void OnMoveReleased(InputAction.CallbackContext callbackContext) {
            _inputVector = callbackContext.ReadValue<Vector2>();
            OnRunStop();
        }

        private void OnJumpPressed(InputAction.CallbackContext obj) {
            jumpInput = true;
        }

        private void OnJumpReleased(InputAction.CallbackContext obj) {
            jumpInput = false;
        }
        
        private void OnLightAttackPressed(InputAction.CallbackContext callbackContext) {
            if (!attackInProgress)
                lightAttackInput = true;
        }
        
        private void OnHeavyAttackPressed(InputAction.CallbackContext callbackContext) {
            if (!attackInProgress)
                heavyAttackInput = true;
        }
        
        private void OnAttackModifierPressed(InputAction.CallbackContext callbackContext) {
            attackModifierInput = true;
        }

        private void OnAttackModifierReleased(InputAction.CallbackContext callbackContext) {
            attackModifierInput = false;
        }

        private void OnGuardDodgePressed(InputAction.CallbackContext obj) {
            guardDodgeInput = true;
        }

        private void OnGuardDodgeReleased(InputAction.CallbackContext obj) {
            guardDodgeInput = false;
        }
        
        
        /* * * * * * * * * *
         * MOVEMENT INPUT *
         * * * * * * * * * */
        
        private void OnRunStart(Vector2 moveInput) {
            MovementDirection movementDirection = CheckCharacterDirection(moveInput);
            if(isMoving) { }
            else if((!preRunning && !isRunning) || Time.time - runInputTime > 0.2) {
                runInputTime = Time.time;
                preRunning = true;
                isRunning = false;
                movementSpeed = data.walkSpeed;
            }
            else if(Time.time - runInputTime <= 0.2) {
                if(preRunning && previousMoveTap == movementDirection) {
                    preRunning = false;
                    isRunning = true;
                    movementSpeed = data.runSpeed;
                }
                else if(preRunning && previousMoveTap != movementDirection)
                    runInputTime = Time.time;
            }

            previousMoveTap = movementDirection;
            isMoving = true;
        }

        private void OnRunStop() {
            isMoving = false;

            if(preRunning && Time.time - runInputTime > 0.2)
                preRunning = false;
            else
                runInputTime = Time.time;
        }

        MovementDirection CheckCharacterDirection(Vector2 movementDirection) {
            if(Math.Abs(Mathf.Abs(movementDirection.x) - Mathf.Abs(movementDirection.y)) < 0.01f)
                return MovementDirection.None;
            
            if(movementDirection.x > movementDirection.y) {
                if(movementDirection.x > 0)
                    return MovementDirection.Right;
                if(movementDirection.y < 0)
                    return MovementDirection.Down;
            }
            else if(movementDirection.x < movementDirection.y) {
                if(movementDirection.x < 0)
                    return MovementDirection.Left;
                if(movementDirection.y > 0)
                    return MovementDirection.Up;
            }
            return MovementDirection.None;
        }


        /* * * * * * * * *
         * DEPTH SCALING *
         * * * * * * * * */

        // TODO: Change -3 to a variable (Where the variable is the y level where the scaling is 1)
        // TODO: Change default scale value from 1 to a variable as well (TS2 needs a bigger sprite)
        private void ScalePlayer() {
            Vector3 position = characterTransform.position;
            float scaleReferenceY = -3 + (position.x - scaleInclineBound) * groundSlope;
            float newScaleValue = 1 - (position.y - scaleReferenceY) * data.scalePerUnit;

            characterTransform.localScale = new Vector2(scaleDirection * newScaleValue, newScaleValue);
        }
        
        // TODO: Being used only for the Player and Interactible turns. Needs to be ajusted to the state machine
        public float CheckDirection(float horizontalInput) {
            if((horizontalInput < 0 && isFacingRight) || (horizontalInput > 0 && !isFacingRight)) {
                isFacingRight = !isFacingRight;
                scaleDirection *= -1;
                orientationChannel.RaiseEvent(isFacingRight);
            }

            return scaleDirection;
        }
        
        
        /* * * * * * * * * *
         * ATTACK CALLBACK *
         * * * * * * * * * */
        
        public void DealDamage(CombatAttackSO combatAttack) {
            bool hitSomething = false;

            GameObject[] alignedEnemies = GetAlignedEnemies();
            GameObject[] hitEnemies = GetHitEnemies(combatAttack);
            
            var enemiesToAttack = alignedEnemies.Intersect(hitEnemies).ToList();
            foreach (GameObject enemyGameObject in enemiesToAttack) {
                EnemyDamageable enemyDamageable = enemyGameObject.GetComponent<EnemyDamageable>();
                enemyDamageable.TakeDamage(combatAttack, spriteTransform);
                Instantiate(combatAttack.hitParticle,
                     (Vector2) spriteTransform.position + combatAttack.hitParticleOffset * spriteTransform.lossyScale,
                     spriteTransform.rotation);
                hitSomething = true;
            }

            if (!hitSomething) return;
            spriteAnimator.enabled = false;
            StartCoroutine(ResumeAnimation(enemiesToAttack));
        }
        
        private GameObject[] GetAlignedEnemies() {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(spriteTransform.position, attackAlignmentSize, 0f, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Enemy"));
            return raycastHits.Select(raycastHit => raycastHit.transform.gameObject).ToArray();
        }

        private GameObject[] GetHitEnemies(CombatAttackSO combatAttack) {
            Vector2 attackPosition = (Vector2) spriteTransform.position + combatAttack.attackPositionOffset * spriteTransform.lossyScale;
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(attackPosition, combatAttack.attackColliderArea, 0f, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Enemy GFX"));
            return raycastHits.Select(raycastHit => raycastHit.transform.gameObject).ToArray();
        }
        
        private IEnumerator ResumeAnimation(List<GameObject> attackedEnemies) {
            yield return new WaitForSeconds(2/24f);
            spriteAnimator.enabled = true;
            
            foreach (GameObject enemyGameObject in attackedEnemies) {
                EnemyDamageable enemyDamageable = enemyGameObject.GetComponent<EnemyDamageable>();
                enemyDamageable.resumingKnocked = true;
            }
        }
    }
}