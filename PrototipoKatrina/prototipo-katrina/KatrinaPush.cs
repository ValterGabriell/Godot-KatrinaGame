using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoKatrina
{
    public partial class Katrina
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

                // Tenta converter o 'collider' para o tipo do objeto empurrável.
                if (collider is PushableObject pushable)
                {
                    var amount = Velocity.X * 1.1;
                    pushable.Push((float)amount);
                }
            }
        }
    }
}
