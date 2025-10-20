using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.Components.Interfaces
{

    public interface IEnemyBaseComponents
    {
        public void Initialize();
        public void Process(double delta);
        public void PhysicsProcess(double delta);
        
    }
}
