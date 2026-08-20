shader_type canvas_item;

uniform float speed : hint_range(1.0, 5.0, .1);
uniform float amplitude : hint_range(0.0, 50.0, .1);

void vertex() {
	float move_amount = sin(TIME*speed) * amplitude;
	VERTEX += vec2(move_amount, 0) * vec2(1. - UV.y, 0);
}