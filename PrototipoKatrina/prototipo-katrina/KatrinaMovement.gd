extends CharacterBody2D

@export var speed: float = 200.0
@export var run_speed: float = 350.0
@export var jump_velocity: float = -400.0
@export var gravity: float = 900.0

func _physics_process(delta: float) -> void:
	var input_vector = Vector2.ZERO

	# Leitura do input (setas ou WASD)
	input_vector.x = Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left")
	input_vector = input_vector.normalized()

	# Correr rápido
	var current_speed = speed
	if Input.is_action_pressed("ui_focus_next"):
		current_speed = run_speed

	velocity.x = input_vector.x * current_speed

	# Gravidade
	if not is_on_floor():
		velocity.y += gravity * delta
	else:
		# Pulo
		if Input.is_action_just_pressed("ui_accept"):
			velocity.y = jump_velocity
		else:
			velocity.y = 0

	move_and_slide()
