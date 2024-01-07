using System.ComponentModel.DataAnnotations;

namespace Lab3.Models;

using System.Xml.Serialization;

[XmlRoot(ElementName = "Step", IsNullable = false)]
public class Step
{
	[Key]
	public int Id { get; set; }
	public int StartX { get; set; }
	public int StartY { get; set; }
	public int EndX { get; set; }
	public int EndY { get; set; }
	public bool PenDown { get; set; }
	public PenColor Color { get; set; }

	public Step(int startX, int startY, int endX, int endY, bool penDown, PenColor color)
	{
		StartX = startX;
		StartY = startY;
		EndX = endX;
		EndY = endY;
		PenDown = penDown;
		Color = color;
	}

	public Step()
	{}

	public override string ToString()
	{
		string penState = PenDown ? "down" : "up";
		return $"({StartX}; {StartY}) -> ({EndX}; {EndY}), pen {penState}, color: {Color}";
	}
}
