shader_type canvas_item;

#define CROSSHAIR_TOGGLED

group_uniforms Circle_Settings;
uniform float center_circle_size: hint_range(0, 1.) = .5;
uniform float circle_size: hint_range(0, 1.) = .5;
uniform float circle_distance: hint_range(0, 1.) = .5;
uniform float outer_circle_gap_size: hint_range(0, 1.) = .5;

#if defined(CROSSHAIR_TOGGLED)
	group_uniforms Crosshair_Settings;
	uniform bool animated;
	uniform float crosshair_width: hint_range(0, 1.) = .5;
	uniform float crosshair_height: hint_range(0, 1.) = .5;
	uniform float distance_from_center: hint_range(0, 1.) = .5;
#endif

float create_circles(vec2 uv)
{
	float outer_circle = step(distance(uv, vec2(.5)), circle_distance);
	float inner_circle = step(distance(uv, vec2(.5)), circle_distance - circle_size);
	float center_circle = step(distance(uv, vec2(.5)), center_circle_size * .1);

	float v_line = step(distance(uv.x, .5), outer_circle_gap_size * .1);
	float h_line = step(distance(uv.y, .5), outer_circle_gap_size * .1);

	return center_circle + max(outer_circle - inner_circle - min(h_line + v_line, 1.), 0.);
}

#if defined(CROSSHAIR_TOGGLED)
	float create_lines(vec2 uv)
	{
		vec2 center_distance;

		if (animated) { 
			float animated_distance = abs(sin(TIME)) * .005;
			center_distance = vec2(.5) + distance_from_center + animated_distance;
		} else {
			center_distance = vec2(.5) + distance_from_center;
		}


		float h_line_width = step(distance(uv.x, center_distance.x), crosshair_width * .2) + 
							 step(distance(1. - uv.x, center_distance.x), crosshair_width * .2);
		float h_line_height = step(distance(uv.y, .5), crosshair_height * .02);

		float v_line_width = step(distance(uv.y, center_distance.y), crosshair_width * .2) + 
							 step(distance(1. - uv.y, center_distance.y), crosshair_width * .2);
		float v_line_height = step(distance(uv.x, .5), crosshair_height * .02);

		return h_line_width * h_line_height + v_line_width * v_line_height;
	}
#endif

void fragment() {
	COLOR.rgb = vec3(1.);
	
	#if defined(CROSSHAIR_TOGGLED)
		COLOR.a = create_lines(UV) + create_circles(UV);
	#else
		COLOR.a = create_circles(UV);
	#endif
}