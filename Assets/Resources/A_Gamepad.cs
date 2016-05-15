using UnityEngine;
using UnityEngine.InputNew;

// GENERATED FILE - DO NOT EDIT MANUALLY
public class GameControls : ActionMapInput {
	public GameControls (ActionMap actionMap) : base (actionMap) { }
	
	public AxisInputControl @move_X { get { return (AxisInputControl)this[0]; } }
	public AxisInputControl @move_Y { get { return (AxisInputControl)this[1]; } }
	public AxisInputControl @look_X { get { return (AxisInputControl)this[2]; } }
	public AxisInputControl @look_Y { get { return (AxisInputControl)this[3]; } }
	public ButtonInputControl @button_1 { get { return (ButtonInputControl)this[4]; } }
	public ButtonInputControl @button_2 { get { return (ButtonInputControl)this[5]; } }
	public ButtonInputControl @button_3 { get { return (ButtonInputControl)this[6]; } }
	public ButtonInputControl @button_4 { get { return (ButtonInputControl)this[7]; } }
	public ButtonInputControl @left_Bumper { get { return (ButtonInputControl)this[8]; } }
	public ButtonInputControl @right_Bumper { get { return (ButtonInputControl)this[9]; } }
	public ButtonInputControl @left_Trigger { get { return (ButtonInputControl)this[10]; } }
	public ButtonInputControl @right_Trigger { get { return (ButtonInputControl)this[11]; } }
	public Vector2InputControl @move { get { return (Vector2InputControl)this[12]; } }
	public Vector2InputControl @look { get { return (Vector2InputControl)this[13]; } }
	public ButtonInputControl @start { get { return (ButtonInputControl)this[14]; } }
	public ButtonInputControl @goBack { get { return (ButtonInputControl)this[15]; } }
}
