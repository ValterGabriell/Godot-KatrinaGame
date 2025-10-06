using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha
{
    public partial class Myha_2
    {   
        private bool isPressingKeyToPush = false;

        // Função para empurrar o objeto.
        public void PushObject(float delta)
        {
            isPressingKeyToPush = Input.IsKeyPressed(Key.Tab);
            // Verifica se o RayCast está colidindo.
            if (PushRaycast.IsColliding() && isPressingKeyToPush)
            {
                var collider = PushRaycast.GetCollider();

            }
        }
    }
}
