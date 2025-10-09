using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.States
{
    public enum EnemyState
    {
        Idle,
        Roaming,
        Attack,
        Chasing,
        Dead,
        LookingForPlayerInDistractedArea,
        Patrolling,
        Waiting,
        Investigating,
        Alerted
    }
}
