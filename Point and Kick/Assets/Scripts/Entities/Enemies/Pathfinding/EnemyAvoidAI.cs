using UnityEngine;

namespace Entities.Enemies.Pathfinding {
    public class EnemyAvoidAI : EnemyAI {
        private Transform closestEnemy;

        public void SetClosestEnemy(Transform enemy) {
            closestEnemy = enemy;
        }
        
        protected override Vector2 GetDestinationPosition() {
            if (!PlayerInVision() || closestEnemy == null)
                return Vector2.zero;

            Vector2 closestEnemyPosition = closestEnemy.position;
            Vector2 playerPosition = targetPosition.position;
            Vector2 distanceBetween = closestEnemyPosition - playerPosition;

            float angle = Mathf.Atan2(distanceBetween.y, distanceBetween.x);
            float xToEnemy = Mathf.Cos(angle) * actionArea;
            float yToEnemy = Mathf.Sin(angle) * actionArea;

            return closestEnemyPosition + new Vector2(xToEnemy, yToEnemy);

            /*Vector2 positionToGo = enemyPosition;
            float distanceRatio = Math.Abs(enemyPosition.x - playerPosition.x) / Math.Abs(enemyPosition.y - playerPosition.y);

            float newHorizontalDistance =
                math.sqrt((math.pow(distanceRatio, 2) + 1) / (math.pow(distanceRatio, 2) * math.pow(visionArea, 2)));
            float newVerticalDistance = newHorizontalDistance / distanceRatio;

            if (enemyPosition.x - playerPosition.x > 0)
                positionToGo.x += newHorizontalDistance;
            else
                positionToGo.x -= newHorizontalDistance;
            
            if (enemyPosition.y - playerPosition.y > 0)
                positionToGo.y += newVerticalDistance;
            else
                positionToGo.y -= newVerticalDistance;

            return positionToGo;*/
        }
    }
}