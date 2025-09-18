extends StaticBody2D
@onready var raycast = $RayCastPush

@export var raycastSize: float = 20

func _ready():
	raycast.target_position.x = raycastSize
	

func _physics_process(delta):
	if raycast.is_colliding():
		var collider = raycast.get_collider()
		if collider and collider.is_in_group("player"):
			print("Player is pushing the object")
