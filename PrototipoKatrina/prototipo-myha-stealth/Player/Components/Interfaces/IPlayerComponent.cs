using KatrinaGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Player.Components.Interfaces
{
    public interface IPlayerComponent
    {
        void Initialize(BasePlayer player);
        void Process(double delta);
        void PhysicsProcess(double delta);
        void HandleInput(double delta);
        void Cleanup();
    }
}
